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
                dataGridView_Prev.RowCount = _managerInfo.PrevSyncAddress.Count + 1;
                dataGridView_Next.RowCount = _managerInfo.NextSyncAddress.Count + 1;
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
                panel_prev.Width = width;
                panel_next.Width = width;
            }
            catch
            {
            }
        }

        private void button_Reconnect_Click(object sender, EventArgs e)
        {
            try
            {
                var prevSyncName = textBox_prevSyncName.Text;
                var nextSyncName = textBox_nextSyncName.Text;
                _syncManager.ReConnect(prevSyncName, nextSyncName);
            }
            catch
            {
            }
        }

        private void dataGridView_Prev_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= _managerInfo.PrevSyncAddress.Count)
                    return;

                string address = _managerInfo.PrevSyncAddress[e.RowIndex].ToUpper();

                if (e.ColumnIndex == 0)
                    e.Value = address;
                else if (e.ColumnIndex == 1)
                    e.Value = GetDescription(address);
            }
            catch
            {
            }
        }

        private void dataGridView_Prev_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex > _managerInfo.PrevSyncAddress.Count)
                    return;

                if (e.ColumnIndex == 0)
                {
                    string sAddress = e.Value?.ToString().ToUpper();
                    if (string.IsNullOrEmpty(sAddress))
                    {
                        _managerInfo.PrevSyncAddress.RemoveAt(e.RowIndex);
                        dataGridView_Prev.RowCount = _managerInfo.PrevSyncAddress.Count + 1;
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

                    if (e.RowIndex < _managerInfo.PrevSyncAddress.Count)
                        _managerInfo.PrevSyncAddress[e.RowIndex] = sAddress;
                    else
                        _managerInfo.PrevSyncAddress.Add(sAddress);
                }
                else if (e.ColumnIndex == 1)
                {
                    if (e.RowIndex >= _managerInfo.PrevSyncAddress.Count)
                        return;

                    string sAddress = _managerInfo.PrevSyncAddress[e.RowIndex];
                    SetDescription(sAddress, e.Value?.ToString());
                }

                if (e.RowIndex == dataGridView_Prev.RowCount - 1)
                    dataGridView_Prev.RowCount++;
                dataGridView_Prev.InvalidateRow(e.RowIndex);
            }
            catch
            {
            }
        }

        private void dataGridView_Next_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= _managerInfo.NextSyncAddress.Count)
                    return;

                string address = _managerInfo.NextSyncAddress[e.RowIndex].ToUpper();

                if (e.ColumnIndex == 0)
                    e.Value = address;
                else if (e.ColumnIndex == 1)
                    e.Value = GetDescription(address);
            }
            catch
            {
            }
        }

        private void dataGridView_Next_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex > _managerInfo.NextSyncAddress.Count)
                    return;

                if (e.ColumnIndex == 0)
                {
                    string sAddress = e.Value?.ToString().ToUpper();
                    if (string.IsNullOrEmpty(sAddress))
                    {
                        _managerInfo.NextSyncAddress.RemoveAt(e.RowIndex);
                        dataGridView_Next.RowCount = _managerInfo.NextSyncAddress.Count + 1;
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

                    if (e.RowIndex < _managerInfo.NextSyncAddress.Count)
                        _managerInfo.NextSyncAddress[e.RowIndex] = sAddress;
                    else
                        _managerInfo.NextSyncAddress.Add(sAddress);
                }
                else if (e.ColumnIndex == 1)
                {
                    if (e.RowIndex >= _managerInfo.NextSyncAddress.Count)
                        return;

                    string sAddress = _managerInfo.NextSyncAddress[e.RowIndex];
                    SetDescription(sAddress, e.Value?.ToString());
                }

                if (e.RowIndex == dataGridView_Next.RowCount - 1)
                    dataGridView_Next.RowCount++;
                dataGridView_Next.InvalidateRow(e.RowIndex);
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
                label_prev_connected.BackColor = _syncManager.IsPrevConnected() ? Color.Lime : Color.Tomato;
                label_next_connected.BackColor = _syncManager.IsNextConnected() ? Color.Lime : Color.Tomato;
            }
            catch
            {
            }
        }
    }
}