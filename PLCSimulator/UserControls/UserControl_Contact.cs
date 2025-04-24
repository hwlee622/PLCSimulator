using System;
using System.Globalization;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class UserControl_Contact : UserControl
    {
        private string m_contactCode;
        private ushort[] m_prevData = new ushort[DataManager.MaxContactAddress];

        public UserControl_Contact()
        {
            InitializeComponent();
        }

        public UserControl_Contact(string contactCode)
        {
            InitializeComponent();

            m_contactCode = contactCode;
        }

        private void UserControl_Contact_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView_Contact.RowCount = DataManager.MaxContactAddress * 16;
            }
            catch
            {
            }
        }

        private void UserControl_Contact_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                timer_gui_update.Enabled = Visible;
            }
            catch
            {
            }
        }

        private void dataGridView_Contact_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(m_contactCode, out var contactArea))
                    return;
                if (e.RowIndex < 0 || e.RowIndex >= DataManager.MaxContactAddress * 16)
                    return;

                var data = contactArea.GetData(e.RowIndex / 16, 1);
                var showData = new bool[data.Length * 16];
                for (int i = 0; i < showData.Length; i++)
                {
                    int index = i / 16;
                    int hex = i % 16;
                    showData[i] = ((data[0] >> hex) & 1) == 1;
                }
                string address = $"{m_contactCode}{e.RowIndex / 16:D3}{e.RowIndex % 16:X}".ToUpper();

                if (e.ColumnIndex == 0)
                    e.Value = address;
                else if (e.ColumnIndex == 1)
                    e.Value = showData[e.RowIndex % 16] ? 1 : 0;
                else if (e.ColumnIndex == 2)
                    e.Value = ProfileRecipe.Instance.GetDescription(address);
            }
            catch
            {
            }
        }

        private void dataGridView_Contact_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(m_contactCode, out var contactArea))
                    return;
                if (e.RowIndex < 0 || e.RowIndex >= DataManager.MaxContactAddress * 16)
                    return;
                if (e.ColumnIndex == 1)
                {
                    int.TryParse(e.Value?.ToString(), out int newValue);
                    int address = e.RowIndex / 16;
                    int hex = e.RowIndex % 16;
                    int mask = 1 << hex;
                    var singleData = contactArea.GetData(address, 1);
                    singleData[0] = newValue > 0 ? (ushort)(singleData[0] | mask) : (ushort)(singleData[0] & ~mask);
                    contactArea.SetData(address, singleData);
                }
                else if (e.ColumnIndex == 2)
                {
                    string address = $"{m_contactCode}{e.RowIndex / 16:D3}{e.RowIndex % 16:X}".ToUpper();

                    ProfileRecipe.Instance.SetDescription(address, e.Value?.ToString());
                    ProfileRecipe.Instance.Save();
                }
                dataGridView_Contact.InvalidateRow(e.RowIndex);
            }
            catch
            {
            }
        }

        private void timer_gui_update_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DataManager.Instance.PlcArea.TryGetValue(m_contactCode, out var contactArea))
                    return;

                ushort[] data = contactArea.GetData(0, DataManager.MaxContactAddress);
                for (int i = 0; i < data.Length; i++)
                    for (int j = 0; j < 16; j++)
                        if ((data[i] >> j) != m_prevData[i] >> j)
                            dataGridView_Contact.InvalidateRow(i * 16 + j);
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
                    int address = 0;
                    int hex = 0;

                    if (string.IsNullOrEmpty(textBox_search.Text))
                        return;
                    if (textBox_search.Text.Length == 1)
                    {
                        if (!int.TryParse(textBox_search.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out hex))
                            return;
                    }
                    else
                    {
                        if (!int.TryParse(textBox_search.Text.Substring(0, textBox_search.Text.Length - 1), out address))
                            return;
                        if (!int.TryParse(textBox_search.Text.Substring(textBox_search.Text.Length - 1, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out hex))
                            return;
                    }

                    int index = address * 16 + hex;

                    if (index < 0 || index >= dataGridView_Contact.RowCount)
                        return;

                    dataGridView_Contact.ClearSelection();
                    dataGridView_Contact.CurrentCell = dataGridView_Contact.Rows[index].Cells[0];
                    dataGridView_Contact.Rows[index].Cells[0].Selected = true;
                    dataGridView_Contact.FirstDisplayedScrollingRowIndex = index;
                }
            }
            catch
            {
            }
        }
    }
}