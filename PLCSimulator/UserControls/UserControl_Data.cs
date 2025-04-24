using System;
using System.Text;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class UserControl_Data : UserControl
    {
        private string m_dataCode;
        private ushort[] m_prevData = new ushort[DataManager.MaxDataAreaAddress];

        public UserControl_Data()
        {
            InitializeComponent();
        }

        public UserControl_Data(string dataCode)
        {
            InitializeComponent();

            m_dataCode = dataCode;
        }

        private void UserControlData_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView_Data.RowCount = DataManager.MaxDataAreaAddress;
                radioButton_short.Checked = true;
            }
            catch
            {
            }
        }

        private void UserControlData_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                timer_gui_update.Enabled = Visible;
            }
            catch
            {
            }
        }

        private void dataGridView_DT_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(m_dataCode, out var dataArea))
                    return;
                if (e.RowIndex < 0 || e.RowIndex >= DataManager.MaxDataAreaAddress)
                    return;

                var data = dataArea.GetData(e.RowIndex, 2);
                string address = $"DT{e.RowIndex:D5}".ToUpper();

                if (e.ColumnIndex == 0)
                    e.Value = address;
                else if (e.ColumnIndex == 1)
                {
                    if (radioButton_ASCII.Checked)
                    {
                        byte[] bitData = BitConverter.GetBytes(data[0]);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(bitData);
                        e.Value = Encoding.ASCII.GetString(bitData);
                    }
                    else if (radioButton_short.Checked)
                    {
                        e.Value = (short)data[0];
                    }
                    else if (radioButton_int.Checked)
                    {
                        if (e.RowIndex < DataManager.MaxDataAreaAddress - 1)
                            e.Value = (data[1] << 16) | data[0];
                        else
                            e.Value = (int)data[0];
                    }
                    else if (radioButton_hex.Checked)
                    {
                        e.Value = $"{data[0]:X2}";
                    }
                }
                else if (e.ColumnIndex == 2)
                    e.Value = ProfileRecipe.Instance.GetDescription(address);
            }
            catch
            {
            }
        }

        private void dataGridView_Data_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(m_dataCode, out var dataArea))
                    return;
                if (e.RowIndex < 0 || e.RowIndex >= DataManager.MaxDataAreaAddress)
                    return;
                if (e.ColumnIndex == 1)
                {
                    string text = e.Value?.ToString();
                    ushort[] data = dataArea.GetData(e.RowIndex, 2);
                    if (radioButton_ASCII.Checked)
                    {
                        if (text.Length % 2 != 0)
                            text += "\0\0";
                        text = text.Substring(0, 2);
                        data[0] = (ushort)(text[0] << 8 | text[1]);
                    }
                    else if (radioButton_short.Checked)
                    {
                        short.TryParse(text, out short value);
                        data[0] = (ushort)value;
                    }
                    else if (radioButton_int.Checked)
                    {
                        int.TryParse(text, out int value);
                        data[0] = (ushort)(value & 0xFFFF);
                        data[1] = (ushort)((value >> 16) & 0x0FFFF);
                    }
                    else if (radioButton_hex.Checked)
                    {
                        if (text.Length % 4 != 0)
                            text += "\0\0\0\0";
                        text = text.Substring(0, 4);
                        data[0] = Convert.ToUInt16(text, 16);
                    }

                    dataArea.SetData(e.RowIndex, data);
                    if (e.RowIndex > 0)
                        dataGridView_Data.InvalidateRow(e.RowIndex - 1);
                    dataGridView_Data.InvalidateRow(e.RowIndex);
                    if (e.RowIndex < dataGridView_Data.RowCount - 1)
                        dataGridView_Data.InvalidateRow(e.RowIndex + 1);
                }
                else if (e.ColumnIndex == 2)
                {
                    string address = $"DT{e.RowIndex:D5}".ToUpper();
                    ProfileRecipe.Instance.SetDescription(address, e.Value?.ToString());
                    ProfileRecipe.Instance.Save();
                }
            }
            catch
            {
            }
        }

        private void timer_gui_update_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DataManager.Instance.PlcArea.TryGetValue(m_dataCode, out var dataArea))
                    return;

                ushort[] data = dataArea.GetData(0, DataManager.MaxDataAreaAddress);
                for (int i = 0; i < data.Length; i++)
                    if (data[i] != m_prevData[i])
                    {
                        dataGridView_Data.InvalidateRow(i);
                        if (i > 0)
                            dataGridView_Data.InvalidateRow(i - 1);
                        if (i < data.Length - 1)
                            dataGridView_Data.InvalidateRow(i + 1);
                    }
                m_prevData = data;
            }
            catch
            {
            }
        }

        private void textBox_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!int.TryParse(textBox_search.Text, out int index))
                        return;

                    if (index < 0 || index >= dataGridView_Data.RowCount)
                        return;

                    dataGridView_Data.ClearSelection();
                    dataGridView_Data.CurrentCell = dataGridView_Data.Rows[index].Cells[0];
                    dataGridView_Data.Rows[index].Cells[0].Selected = true;
                    dataGridView_Data.FirstDisplayedScrollingRowIndex = index;
                }
            }
            catch
            {
            }
        }

        private void radioButton_format_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView_Data.Invalidate();
            }
            catch
            {
            }
        }
    }
}