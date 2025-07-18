using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class UserControl_Favorites : UserControl
    {
        private List<string> _addressList;
        private List<string> _prevData = new List<string>();
        private WordDataType _dataType = WordDataType.ASCII;

        public UserControl_Favorites()
        {
            InitializeComponent();
        }

        private void UserControl_Favorites_Load(object sender, EventArgs e)
        {
            try
            {
                _addressList = ProfileRecipe.Instance.ProfileInfo.FavoriteAddress;
                dataGridView_Data.RowCount = _addressList.Count + 1;
                radioButton_short.Checked = true;
            }
            catch
            {
            }
        }

        private void UserControl_Favorites_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                timer_gui_update.Enabled = Visible;
            }
            catch
            {
            }
        }

        private void dataGridView_Data_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= _addressList.Count)
                    return;

                string sAddress = _addressList[e.RowIndex].ToUpper();

                if (e.ColumnIndex == 0)
                    e.Value = sAddress;
                else if (e.ColumnIndex == 1)
                    e.Value = GetData(sAddress);
                else if (e.ColumnIndex == 2)
                    e.Value = GetDescription(sAddress);
            }
            catch
            {
            }
        }

        private string GetData(string sAddress)
        {
            string result = string.Empty;

            if (Util.ValidWordAddress(sAddress, out string code, out int index))
            {
                var data = DataManager.Instance.WordDataDict[code].GetData(index, 2);
                result = Util.StringParseWordData(data, _dataType);
            }
            else if (Util.ValidBitAddress(sAddress, out code, out index))
            {
                var data = DataManager.Instance.BitDataDict[code].GetData(index, 1)[0];
                return data ? "1" : "0";
            }

            return result;
        }

        private string GetDescription(string sAddress)
        {
            string result = string.Empty;

            if (Util.ValidWordAddress(sAddress, out string code, out int index) || Util.ValidBitAddress(sAddress, out code, out index))
                result = ProfileRecipe.Instance.GetDescription(sAddress);

            return result;
        }

        private void dataGridView_Data_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex > _addressList.Count)
                    return;

                if (e.ColumnIndex == 0)
                {
                    string sAddress = e.Value?.ToString().ToUpper();
                    if (string.IsNullOrEmpty(sAddress))
                    {
                        _addressList.RemoveAt(e.RowIndex);
                        dataGridView_Data.RowCount = _addressList.Count + 1;
                        return;
                    }

                    if (Util.ValidBitAddress(sAddress, out string code, out int index))
                    {
                        sAddress = DataManager.Instance.BitDataDict[code].GetAddress(index);
                        sAddress = $"{code}{sAddress}";
                    }
                    else if (Util.ValidWordAddress(sAddress, out code, out index))
                    {
                        sAddress = DataManager.Instance.WordDataDict[code].GetAddress(index);
                        sAddress = $"{code}{sAddress}";
                    }
                    else
                        return;

                    if (e.RowIndex < _addressList.Count)
                        _addressList[e.RowIndex] = sAddress;
                    else
                        _addressList.Add(sAddress);
                }
                else if (e.ColumnIndex == 1)
                {
                    if (e.RowIndex >= _addressList.Count)
                        return;

                    string sAddress = _addressList[e.RowIndex];
                    SetData(sAddress, e.Value?.ToString());
                }
                else if (e.ColumnIndex == 2)
                {
                    if (e.RowIndex >= _addressList.Count)
                        return;

                    string sAddress = _addressList[e.RowIndex];
                    SetDescription(sAddress, e.Value?.ToString());
                }

                if (e.RowIndex == dataGridView_Data.RowCount - 1)
                    dataGridView_Data.RowCount++;

                if (e.RowIndex > 0)
                    dataGridView_Data.InvalidateRow(e.RowIndex - 1);
                dataGridView_Data.InvalidateRow(e.RowIndex);
                if (e.RowIndex < dataGridView_Data.RowCount - 1)
                    dataGridView_Data.InvalidateRow(e.RowIndex + 1);
            }
            catch
            {
            }
        }

        private void SetData(string sAddress, string text)
        {
            if (Util.ValidWordAddress(sAddress, out string code, out int index))
            {
                var data = Util.UShortParseWordData(text, _dataType);
                DataManager.Instance.WordDataDict[code].SetData(index, data);
            }
            else if (Util.ValidBitAddress(sAddress, out code, out index))
            {
                int.TryParse(text, out int value);
                var data = value > 0;
                DataManager.Instance.BitDataDict[code].SetData(index, new bool[] { data });
            }
        }

        private void SetDescription(string sAddress, string description)
        {
            if (Util.ValidWordAddress(sAddress, out _, out _) || Util.ValidBitAddress(sAddress, out _, out _))
                ProfileRecipe.Instance.SetDescription(sAddress, description);
        }

        private void timer_gui_update_Tick(object sender, EventArgs e)
        {
            try
            {
                List<string> data = new List<string>();

                foreach (var sAddress in _addressList)
                {
                    if (Util.ValidWordAddress(sAddress, out string code, out int index))
                        data.Add(DataManager.Instance.WordDataDict[code].GetData(index, 1)[0].ToString());
                    else if (Util.ValidBitAddress(sAddress, out code, out index))
                        data.Add(DataManager.Instance.BitDataDict[code].GetData(index, 1)[0].ToString());
                }

                if (data.Count != _prevData.Count)
                {
                    dataGridView_Data.Invalidate();
                }
                else
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i] != _prevData[i])
                            dataGridView_Data.InvalidateRow(i);
                    }
                }

                _prevData = data;
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

                dataGridView_Data.Invalidate();
            }
            catch
            {
            }
        }
    }
}