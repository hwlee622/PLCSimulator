using System;
using System.Globalization;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class UserControl_Bit : UserControl
    {
        private readonly string _code;
        private bool[] _prevData = new bool[0];

        public UserControl_Bit()
        {
            InitializeComponent();
        }

        public UserControl_Bit(string contactCode)
        {
            InitializeComponent();

            _code = contactCode;
            if (DataManager.Instance.BitDataDict.TryGetValue(_code, out var bitData))
                _prevData = new bool[bitData.DataLength];
        }

        private void UserControl_Contact_Load(object sender, EventArgs e)
        {
            try
            {
                if (!DataManager.Instance.BitDataDict.TryGetValue(_code, out var bitData))
                    return;

                dataGridView_Bit.RowCount = bitData.DataLength;
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
                if (!DataManager.Instance.BitDataDict.TryGetValue(_code, out var bitData))
                    return;
                if (e.RowIndex < 0 || e.RowIndex >= bitData.DataLength)
                    return;

                var data = bitData.GetData(e.RowIndex, 1);
                string address = $"{_code}{bitData.GetAddress(e.RowIndex)}".ToUpper();

                if (e.ColumnIndex == 0)
                    e.Value = address;
                else if (e.ColumnIndex == 1)
                    e.Value = data[0] ? 1 : 0;
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
                if (!DataManager.Instance.BitDataDict.TryGetValue(_code, out var bitData))
                    return;
                if (e.RowIndex < 0 || e.RowIndex >= bitData.DataLength)
                    return;

                string address = $"{_code}{bitData.GetAddress(e.RowIndex)}".ToUpper();
                
                if (e.ColumnIndex == 1)
                {
                    int.TryParse(e.Value?.ToString(), out int newValue);
                    bool value = newValue > 0;
                    bitData.SetData(e.RowIndex, new bool[] { value });
                }
                else if (e.ColumnIndex == 2)
                {
                    ProfileRecipe.Instance.SetDescription(address, e.Value?.ToString());
                    ProfileRecipe.Instance.Save();
                }
                dataGridView_Bit.InvalidateRow(e.RowIndex);
            }
            catch
            {
            }
        }

        private void timer_gui_update_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DataManager.Instance.BitDataDict.TryGetValue(_code, out var bitData))
                    return;

                var data = bitData.GetData(0, bitData.DataLength);
                for (int i = 0; i < data.Length; i++)
                    if (data[i] != _prevData[i])
                        dataGridView_Bit.InvalidateRow(i);
                _prevData = data;
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

                    if (index < 0 || index >= dataGridView_Bit.RowCount)
                        return;

                    dataGridView_Bit.ClearSelection();
                    dataGridView_Bit.CurrentCell = dataGridView_Bit.Rows[index].Cells[0];
                    dataGridView_Bit.Rows[index].Cells[0].Selected = true;
                    dataGridView_Bit.FirstDisplayedScrollingRowIndex = index;
                }
            }
            catch
            {
            }
        }
    }
}