using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PLCSimulator
{
    public class Macro
    {
        public int MacroStep { get; private set; } = 0;
        private DataManager _dataManager;
        private MacroInfo _macroInfo;
        private CancellationTokenSource _cts = new CancellationTokenSource(0);
        private Dictionary<MacroType, Func<MacroContext, Task<bool>>> _macroDict = new Dictionary<MacroType, Func<MacroContext, Task<bool>>>();

        public Macro(DataManager dataManager, MacroInfo macroInfo = null)
        {
            _dataManager = dataManager;
            _macroInfo = macroInfo ?? new MacroInfo();

            _macroDict.Add(MacroType.Delay, RunDeleay);
            _macroDict.Add(MacroType.SetValue, RunSetValue);
            _macroDict.Add(MacroType.WaitValue, RunWaitValue);
            _macroDict.Add(MacroType.Increase, RunIncrease);
            _macroDict.Add(MacroType.Decrease, RunDecrease);
            _macroDict.Add(MacroType.CopyValue, RunCopyValue);
        }

        public void Run()
        {
            Stop();
            _cts = new CancellationTokenSource();
            Task.Run(() => RunMacro(_cts.Token));
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public bool IsRun()
        {
            return !_cts.IsCancellationRequested;
        }

        public List<MacroContext> GetMacroContextList()
        {
            return _macroInfo.MacroContextList;
        }

        public void AddMacroContext(int index)
        {
            if (IsRun() == true)
                return;

            _macroInfo.MacroContextList.Add(new MacroContext());
            if (index >= _macroInfo.MacroContextList.Count - 2)
                return;

            for (int i = _macroInfo.MacroContextList.Count - 1; i > index; i--)
            {
                var temp = _macroInfo.MacroContextList[i];
                _macroInfo.MacroContextList[i] = _macroInfo.MacroContextList[i - 1];
                _macroInfo.MacroContextList[i - 1] = temp;
            }
        }

        public void RemoveMacroContext(int index)
        {
            if (IsRun() == true)
                return;
            if (_macroInfo.MacroContextList.Count <= index || index < 0)
                return;

            _macroInfo.MacroContextList.RemoveAt(index);
        }

        private async Task RunMacro(CancellationToken token)
        {
            try
            {
                MacroStep = 0;
                var maxMacroStep = _macroInfo.MacroContextList.Count;
                while (token.IsCancellationRequested == false)
                {
                    await Task.Delay(20);

                    if (MacroStep >= maxMacroStep)
                        break;

                    var context = _macroInfo.MacroContextList[MacroStep];
                    if (_macroDict.TryGetValue(context.MacroType, out var macro) == false)
                        continue;

                    if (await macro(context) == true)
                        MacroStep = (MacroStep + 1) % maxMacroStep;
                }
            }
            catch (Exception ex)
            {
                LogWriter.Instance.LogError(ex);
            }
        }

        private async Task<bool> RunDeleay(MacroContext context)
        {
            int.TryParse(context.Value, out var delay);
            await Task.Delay(delay);
            return true;
        }

        private async Task<bool> RunSetValue(MacroContext context)
        {
            await Task.Delay(0);
            if (Util.ValidWordAddress(context.Address, out var code, out var index))
            {
                short.TryParse(context.Value, out var value);
                _dataManager.WordDataDict[code].SetData(index, new ushort[] { (ushort)value });
                return true;
            }
            else if (Util.ValidBitAddress(context.Address, out code, out index))
            {
                int.TryParse(context.Value, out var value);
                _dataManager.BitDataDict[code].SetData(index, new bool[] { value > 0 });
                return true;
            }
            return false;
        }

        private async Task<bool> RunWaitValue(MacroContext context)
        {
            await Task.Delay(0);
            if (Util.ValidWordAddress(context.Address, out var code, out var index))
            {
                short.TryParse(context.Value, out var value);
                var data = (short)_dataManager.WordDataDict[code].GetData(index, 1)[0];
                if (data == value)
                    return true;
            }
            else if (Util.ValidBitAddress(context.Address, out code, out index))
            {
                int.TryParse(context.Value, out var value);
                var data = _dataManager.BitDataDict[code].GetData(index, 1)[0];
                if ((data ? 1 : 0) == value)
                    return true;
            }
            return false;
        }

        private async Task<bool> RunIncrease(MacroContext context)
        {
            await Task.Delay(0);
            if (Util.ValidWordAddress(context.Address, out var code, out var index))
            {
                int.TryParse(context.Value, out var value);
                var data = _dataManager.WordDataDict[code].GetData(index, 1);
                data[0] = (ushort)(data[0] + value);
                _dataManager.WordDataDict[code].SetData(index, data);
                return true;
            }
            else if (Util.ValidBitAddress(context.Address, out code, out index))
            {
                var data = _dataManager.BitDataDict[code].GetData(index, 1);
                data[0] = !data[0];
                _dataManager.BitDataDict[code].SetData(index, data);
                return true;
            }
            return false;
        }

        private async Task<bool> RunDecrease(MacroContext context)
        {
            await Task.Delay(0);
            if (Util.ValidWordAddress(context.Address, out var code, out var index))
            {
                int.TryParse(context.Value, out var value);
                var data = _dataManager.WordDataDict[code].GetData(index, 1);
                data[0] = (ushort)(data[0] - value);
                _dataManager.WordDataDict[code].SetData(index, data);
                return true;
            }
            else if (Util.ValidBitAddress(context.Address, out code, out index))
            {
                var data = _dataManager.BitDataDict[code].GetData(index, 1);
                data[0] = !data[0];
                _dataManager.BitDataDict[code].SetData(index, data);
                return true;
            }
            return false;
        }

        private async Task<bool> RunCopyValue(MacroContext context)
        {
            await Task.Delay(0);
            if (Util.ValidWordAddress(context.Address, out var destCode, out var destIndex) && Util.ValidWordAddress(context.Value, out var sourceCode, out var sourceIndex))
            {
                var sourceData = _dataManager.WordDataDict[sourceCode].GetData(sourceIndex, 1);
                _dataManager.WordDataDict[destCode].SetData(destIndex, sourceData);
                return true;
            }
            else if (Util.ValidBitAddress(context.Address, out destCode, out destIndex) && Util.ValidBitAddress(context.Value, out sourceCode, out sourceIndex))
            {
                var sourceData = _dataManager.BitDataDict[sourceCode].GetData(sourceIndex, 1);
                _dataManager.BitDataDict[destCode].SetData(destIndex, sourceData);
                return true;
            }
            return false;
        }
    }
}