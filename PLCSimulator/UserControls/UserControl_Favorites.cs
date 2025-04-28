using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class UserControl_Favorites : UserControl
    {
        private List<string> m_addressList;
        private List<ushort> m_prevData = new List<ushort>();

        public UserControl_Favorites()
        {
            InitializeComponent();
        }

        private void UserControl_Favorites_Load(object sender, EventArgs e)
        {
            try
            {
                m_addressList = ProfileRecipe.Instance.ProfileInfo.FavoriteAddress;
                dataGridView_Data.RowCount = m_addressList.Count + 1;
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
                if (e.RowIndex < 0 || e.RowIndex >= m_addressList.Count)
                    return;

                string sAddress = m_addressList[e.RowIndex].ToUpper();

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

            if (Util.IsDTAddress(sAddress, out int address))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(DataManager.DataCode, out var dataArea))
                    return result;

                var data = dataArea.GetData(address, 2);
                if (radioButton_ASCII.Checked)
                {
                    byte[] bitData = BitConverter.GetBytes(data[0]);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(bitData);
                    result = Encoding.ASCII.GetString(bitData);
                }
                else if (radioButton_short.Checked)
                {
                    result = ((short)data[0]).ToString();
                }
                else if (radioButton_int.Checked)
                {
                    if (address < DataManager.MaxDataAreaAddress - 1)
                        result = ((data[1] << 16) | data[0]).ToString();
                    else
                        result = ((int)data[0]).ToString();
                }
                else if (radioButton_hex.Checked)
                {
                    result = $"{data[0]:X2}";
                }
            }
            else if (Util.IsContactAddress(sAddress, out string contactCode, out address, out int hex))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(sAddress.Substring(0, 1), out var contactArea))
                    return result;

                result = ((contactArea.GetData(address, 1)[0] >> hex) & 1).ToString();
            }

            return result;
        }

        private string GetDescription(string sAddress)
        {
            string result = string.Empty;

            if (Util.IsDTAddress(sAddress, out int address))
            {
                result = ProfileRecipe.Instance.GetDescription(sAddress);
            }
            else if (Util.IsContactAddress(sAddress, out string contactCode, out address, out int hex))
            {
                result = ProfileRecipe.Instance.GetDescription(sAddress);
            }

            return result;
        }

        private void dataGridView_Data_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex > m_addressList.Count)
                    return;

                if (e.ColumnIndex == 0)
                {
                    string sAddress = e.Value?.ToString();
                    if (string.IsNullOrEmpty(sAddress))
                    {
                        m_addressList.RemoveAt(e.RowIndex);
                        dataGridView_Data.RowCount--;
                        return;
                    }
                    sAddress = sAddress.ToUpper();

                    if (Util.IsDTAddress(sAddress, out int address))
                        sAddress = $"DT{address:D5}";
                    else if (Util.IsContactAddress(sAddress, out string contactCode, out address, out int hex))
                        sAddress = $"{contactCode}{address:D3}{hex:X}";
                    else
                        return;

                    if (e.RowIndex < m_addressList.Count)
                        m_addressList[e.RowIndex] = sAddress;
                    else
                        m_addressList.Add(sAddress);
                }
                else if (e.ColumnIndex == 1)
                {
                    if (e.RowIndex >= m_addressList.Count)
                        return;

                    string sAddress = m_addressList[e.RowIndex];
                    SetData(sAddress, e.Value?.ToString());
                }
                else if (e.ColumnIndex == 2)
                {
                    if (e.RowIndex >= m_addressList.Count)
                        return;

                    string sAddress = m_addressList[e.RowIndex];
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
            if (Util.IsDTAddress(sAddress, out int address))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(DataManager.DataCode, out var dataArea))
                    return;

                ushort[] data = dataArea.GetData(address, 2);
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
                dataArea.SetData(address, data);
            }
            else if (Util.IsContactAddress(sAddress, out string contactCode, out address, out int hex))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(contactCode, out var contactArea))
                    return;

                int.TryParse(text, out int newValue);
                int mask = 1 << hex;
                var singleData = contactArea.GetData(address, 1);
                singleData[0] = newValue > 0 ? (ushort)(singleData[0] | mask) : (ushort)(singleData[0] & ~mask);
                contactArea.SetData(address, singleData);
            }
        }

        private void SetDescription(string sAddress, string description)
        {
            if (Util.IsDTAddress(sAddress, out int address))
            {
                ProfileRecipe.Instance.SetDescription(sAddress, description);
            }
            else if (Util.IsContactAddress(sAddress, out string contactCode, out address, out int hex))
            {
                ProfileRecipe.Instance.SetDescription(sAddress, description);
            }
        }

        private void timer_gui_update_Tick(object sender, EventArgs e)
        {
            try
            {
                List<ushort> data = new List<ushort>();

                foreach (var sAddress in m_addressList)
                {
                    if (Util.IsDTAddress(sAddress, out int address) && DataManager.Instance.PlcArea.TryGetValue(DataManager.DataCode, out var dataArea))
                        data.Add(dataArea.GetData(address, 1)[0]);
                    else if (Util.IsContactAddress(sAddress, out string contactCode, out address, out int hex) && DataManager.Instance.PlcArea.TryGetValue(contactCode, out var contactArea))
                        data.Add(contactArea.GetData(address, 1)[0]);
                }

                if (data.Count != m_prevData.Count)
                {
                    dataGridView_Data.Invalidate();
                }
                else
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i] != m_prevData[i])
                            dataGridView_Data.InvalidateRow(i);
                    }
                }

                m_prevData = data;
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
