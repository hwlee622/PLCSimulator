using System;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class UserControl_Word : UserControl
    {
        private readonly string _code;
        private ushort[] _prevData = new ushort[0];
        private WordDataType _dataType = WordDataType.ASCII;

        public UserControl_Word()
        {
            InitializeComponent();
        }

        public UserControl_Word(string dataCode)
        {
            InitializeComponent();

            _code = dataCode;
            if (DataManager.Instance.WordDataDict.TryGetValue(_code, out var wordData))
                _prevData = new ushort[wordData.DataLength];
        }

        private void UserControlData_Load(object sender, EventArgs e)
        {
            try
            {
                if (!DataManager.Instance.WordDataDict.TryGetValue(_code, out var wordData))
                    return;

                dataGridView_Word.RowCount = wordData.DataLength;
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

        private void dataGridView_Word_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (!DataManager.Instance.WordDataDict.TryGetValue(_code, out var wordData))
                    return;
                if (e.RowIndex < 0 || e.RowIndex >= wordData.DataLength)
                    return;

                var data = wordData.GetData(e.RowIndex, 2);
                string sAddress = wordData.GetAddress(e.RowIndex);
                sAddress = $"{_code}{sAddress}";

                if (e.ColumnIndex == 0)
                    e.Value = sAddress;
                else if (e.ColumnIndex == 1)
                    e.Value = Util.StringParseWordData(data, _dataType);
                else if (e.ColumnIndex == 2)
                    e.Value = ProfileRecipe.Instance.GetDescription(sAddress);
            }
            catch
            {
            }
        }

        private void dataGridView_Data_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (!DataManager.Instance.WordDataDict.TryGetValue(_code, out var wordData))
                    return;
                if (e.RowIndex < 0 || e.RowIndex >= wordData.DataLength)
                    return;

                string sAddress = wordData.GetAddress(e.RowIndex);
                sAddress = $"{_code}{sAddress}";

                if (e.ColumnIndex == 1)
                {
                    var data = Util.UShortParseWordData(e.Value?.ToString(), _dataType);
                    wordData.SetData(e.RowIndex, data);

                    if (e.RowIndex > 0)
                        dataGridView_Word.InvalidateRow(e.RowIndex - 1);
                    dataGridView_Word.InvalidateRow(e.RowIndex);
                    if (e.RowIndex < dataGridView_Word.RowCount - 1)
                        dataGridView_Word.InvalidateRow(e.RowIndex + 1);
                }
                else if (e.ColumnIndex == 2)
                {
                    ProfileRecipe.Instance.SetDescription(sAddress, e.Value?.ToString());
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
                if (!DataManager.Instance.WordDataDict.TryGetValue(_code, out var wordData))
                    return;

                ushort[] data = wordData.GetData(0, wordData.DataLength);
                for (int i = 0; i < data.Length; i++)
                    if (data[i] != _prevData[i])
                    {
                        dataGridView_Word.InvalidateRow(i);
                        if (i > 0)
                            dataGridView_Word.InvalidateRow(i - 1);
                        if (i < data.Length - 1)
                            dataGridView_Word.InvalidateRow(i + 1);
                    }
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
                    if (!DataManager.Instance.WordDataDict.TryGetValue(_code, out var wordData))
                        return;

                    if (wordData.ValidateAddress(textBox_search.Text, out int index))
                    {
                        dataGridView_Word.ClearSelection();
                        dataGridView_Word.CurrentCell = dataGridView_Word.Rows[index].Cells[0];
                        dataGridView_Word.Rows[index].Cells[0].Selected = true;
                        dataGridView_Word.FirstDisplayedScrollingRowIndex = index;
                    }
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
                if (radioButton_ASCII.Checked)
                    _dataType = WordDataType.ASCII;
                else if (radioButton_short.Checked)
                    _dataType = WordDataType.Short;
                else if (radioButton_int.Checked)
                    _dataType = WordDataType.Int;
                else if (radioButton_hex.Checked)
                    _dataType = WordDataType.Hex;

                dataGridView_Word.Invalidate();
            }
            catch
            {
            }
        }
    }
}