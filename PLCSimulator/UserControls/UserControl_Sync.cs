using System;
using System.Drawing;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class UserControl_Sync : UserControl
    {
        private SyncManagerInfo _managerInfo;
        private SyncManager _syncManager;

        public UserControl_Sync(SyncManager syncManager)
        {
            InitializeComponent();
            _syncManager = syncManager;
        }

        private void UserControl_Sync_Load(object sender, EventArgs e)
        {
            try
            {
                _managerInfo = ProfileRecipe.Instance.ProfileInfo.SyncManagerInfo;
                dataGridView_Input.RowCount = _managerInfo.InputAddress.Count + 1;
                dataGridView_Output.RowCount = _managerInfo.OutputAddress.Count + 1;

                textBox_myPlc.Text = _managerInfo.MyPlc;
                textBox_syncPlc.Text = _managerInfo.SyncPlc;
            }
            catch
            {
            }
        }

        private void UserControl_Sync_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                timer_gui_update.Enabled = Visible;
            }
            catch
            {
            }
        }

        private void UserControl_Sync_Layout(object sender, LayoutEventArgs e)
        {
            try
            {
                var width = Width / 2;
                panel_input.Width = width;
                panel_output.Width = width;
            }
            catch
            {
            }
        }

        private void button_Reconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _managerInfo.MyPlc = textBox_myPlc.Text;
                _managerInfo.SyncPlc = textBox_syncPlc.Text;
                _syncManager.ReConnect();
            }
            catch
            {
            }
        }

        private void dataGridView_Input_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= _managerInfo.InputAddress.Count)
                    return;

                string address = _managerInfo.InputAddress[e.RowIndex].ToUpper();

                if (e.ColumnIndex == 0)
                    e.Value = address;
                else if (e.ColumnIndex == 1)
                    e.Value = GetDescription(address);
            }
            catch
            {
            }
        }

        private void dataGridView_Input_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex > _managerInfo.InputAddress.Count)
                    return;

                if (e.ColumnIndex == 0)
                {
                    string sAddress = e.Value?.ToString().ToUpper();
                    if (string.IsNullOrEmpty(sAddress))
                    {
                        _managerInfo.InputAddress.RemoveAt(e.RowIndex);
                        dataGridView_Input.RowCount = _managerInfo.InputAddress.Count + 1;
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

                    if (e.RowIndex < _managerInfo.InputAddress.Count)
                        _managerInfo.InputAddress[e.RowIndex] = sAddress;
                    else
                        _managerInfo.InputAddress.Add(sAddress);
                }
                else if (e.ColumnIndex == 1)
                {
                    if (e.RowIndex >= _managerInfo.InputAddress.Count)
                        return;

                    string sAddress = _managerInfo.InputAddress[e.RowIndex];
                    SetDescription(sAddress, e.Value?.ToString());
                }

                if (e.RowIndex == dataGridView_Input.RowCount - 1)
                    dataGridView_Input.RowCount++;
                dataGridView_Input.InvalidateRow(e.RowIndex);
            }
            catch
            {
            }
        }

        private void dataGridView_Output_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= _managerInfo.OutputAddress.Count)
                    return;

                string address = _managerInfo.OutputAddress[e.RowIndex].ToUpper();

                if (e.ColumnIndex == 0)
                    e.Value = address;
                else if (e.ColumnIndex == 1)
                    e.Value = GetDescription(address);
            }
            catch
            {
            }
        }

        private void dataGridView_Output_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex > _managerInfo.OutputAddress.Count)
                    return;

                if (e.ColumnIndex == 0)
                {
                    string sAddress = e.Value?.ToString().ToUpper();
                    if (string.IsNullOrEmpty(sAddress))
                    {
                        _managerInfo.OutputAddress.RemoveAt(e.RowIndex);
                        dataGridView_Output.RowCount = _managerInfo.OutputAddress.Count + 1;
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

                    if (e.RowIndex < _managerInfo.OutputAddress.Count)
                        _managerInfo.OutputAddress[e.RowIndex] = sAddress;
                    else
                        _managerInfo.OutputAddress.Add(sAddress);
                }
                else if (e.ColumnIndex == 1)
                {
                    if (e.RowIndex >= _managerInfo.OutputAddress.Count)
                        return;

                    string sAddress = _managerInfo.OutputAddress[e.RowIndex];
                    SetDescription(sAddress, e.Value?.ToString());
                }

                if (e.RowIndex == dataGridView_Output.RowCount - 1)
                    dataGridView_Output.RowCount++;
                dataGridView_Output.InvalidateRow(e.RowIndex);
            }
            catch
            {
            }
        }

        private string GetDescription(string sAddress)
        {
            string result = string.Empty;

            if (Util.ValidWordAddress(sAddress, out string code, out int index) || Util.ValidBitAddress(sAddress, out code, out index))
                result = ProfileRecipe.Instance.GetDescription(sAddress);

            return result;
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
                label_input_connected.BackColor = _syncManager.IsInputConnected() ? Color.Lime : Color.Tomato;
                label_output_connected.BackColor = _syncManager.IsOutputConnected() ? Color.Lime : Color.Tomato;
            }
            catch
            {
            }
        }
    }
}