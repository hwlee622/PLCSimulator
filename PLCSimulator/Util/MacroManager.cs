using System;
using System.Threading;
using System.Threading.Tasks;

namespace PLCSimulator
{
    public enum MacroType
    {
        Delay,
        SetValue,
        WaitValue,
        Increase,
        Decrease,
    }

    public class MacroManager
    {
        public MacroManager(MacroInfo[] macroInfoArray)
        {
            MacroInfoArray = macroInfoArray;
            MacroStepArray = new int[MacroInfoArray.Length];
            m_ctsArray = new CancellationTokenSource[MacroInfoArray.Length];
            for (int i = 0; i < MacroInfoArray.Length; i++)
                m_ctsArray[i] = new CancellationTokenSource(0);
        }

        private MacroInfo[] MacroInfoArray;
        private int[] MacroStepArray;
        private CancellationTokenSource[] m_ctsArray;

        public void RunMacro(int index)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return;

            if (m_ctsArray[index].IsCancellationRequested)
            {
                m_ctsArray[index] = new CancellationTokenSource();
                MacroStepArray[index] = 0;
                Task.Run(() => RunMacro(m_ctsArray[index].Token, index));
            }
        }

        public void StopMacro(int index)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return;

            m_ctsArray[index].Cancel();
        }

        public bool IsRunMacro(int index)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return false;

            return !m_ctsArray[index].IsCancellationRequested;
        }

        public int GetAllMacroLength()
        {
            return MacroInfoArray.Length;
        }

        public int GetMacroCount(int index)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return 0;

            return MacroInfoArray[index].MacroContextList.Count;
        }

        public int GetMacroStep(int index)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return 0;

            return MacroStepArray[index];
        }

        public MacroContext GetMacroContext(int index, int step)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return null;
            if (step < 0 || step >= MacroInfoArray[index].MacroContextList.Count)
                return null;

            return MacroInfoArray[index].MacroContextList[step];
        }

        public void AddMacroContext(int index, int addIndex)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return;
            if (!m_ctsArray[index].IsCancellationRequested)
                return;

            var macroInfo = MacroInfoArray[index];
            macroInfo.MacroContextList.Add(new MacroContext());
            if (addIndex >= macroInfo.MacroContextList.Count - 1)
                return;

            for (int i = macroInfo.MacroContextList.Count - 1; i > addIndex + 1; i--)
            {
                var context = macroInfo.MacroContextList[i];
                macroInfo.MacroContextList[i] = macroInfo.MacroContextList[i - 1];
                macroInfo.MacroContextList[i - 1] = context;
            }
        }

        public void RemoveMacroContext(int index, int removeIndex)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return;
            if (!m_ctsArray[index].IsCancellationRequested)
                return;

            MacroInfoArray[index].MacroContextList.RemoveAt(removeIndex);
        }

        private async Task RunMacro(CancellationToken token, int index)
        {
            try
            {
                var macroContextList = MacroInfoArray[index].MacroContextList;
                while (!token.IsCancellationRequested)
                {
                    int step = MacroStepArray[index];
                    if (macroContextList.Count <= step)
                        break;

                    var context = macroContextList[step];
                    if (await RunMacroType(context))
                        MacroStepArray[index] = (step + 1) % macroContextList.Count;
                    else
                        await Task.Delay(20);
                }
            }
            catch (Exception ex)
            {
                LogWriter.Instance.LogError(ex);
            }
        }

        private async Task<bool> RunMacroType(MacroContext context)
        {
            switch (context.MacroType)
            {
                case MacroType.Delay:
                    return await RunDelay(context);

                case MacroType.SetValue:
                    return RunSetValue(context);

                case MacroType.WaitValue:
                    return RunWaitValue(context);

                case MacroType.Increase:
                    return RunIncrease(context);

                case MacroType.Decrease:
                    return RunDecrease(context);

                default:
                    return false;
            }
        }

        private async Task<bool> RunDelay(MacroContext context)
        {
            int.TryParse(context.Value, out var value);
            await Task.Delay(value);
            return true;
        }

        private bool RunSetValue(MacroContext context)
        {
            if (Util.ValidWordAddress(context.Address, out string code, out int index))
            {
                short.TryParse(context.Value, out var value);
                DataManager.Instance.WordDataDict[code].SetData(index, new ushort[] { (ushort)value });
                return true;
            }
            else if (Util.ValidBitAddress(context.Address, out code, out index))
            {
                int.TryParse(context.Value, out var value);
                DataManager.Instance.BitDataDict[code].SetData(index, new bool[] { value > 0 });
                return true;
            }
            return false;
        }

        private bool RunWaitValue(MacroContext context)
        {
            if (Util.ValidWordAddress(context.Address, out string code, out int index))
            {
                short.TryParse(context.Value, out var value);
                var data = (short)DataManager.Instance.WordDataDict[code].GetData(index, 1)[0];
                if (data == value)
                    return true;
            }
            else if (Util.ValidBitAddress(context.Address, out code, out index))
            {
                int.TryParse(context.Value, out var value);
                var data = DataManager.Instance.BitDataDict[code].GetData(index, 1)[0];
                if ((data ? 1 : 0) == value)
                    return true;
            }
            return false;
        }

        private bool RunIncrease(MacroContext context)
        {
            if (Util.ValidWordAddress(context.Address, out string code, out int index))
            {
                int.TryParse(context.Value, out var value);
                var data = DataManager.Instance.WordDataDict[code].GetData(index, 1);
                data[0] = (ushort)(data[0] + value);
                DataManager.Instance.WordDataDict[code].SetData(index, data);
                return true;
            }
            else if (Util.ValidBitAddress(context.Address, out code, out index))
            {
                var data = DataManager.Instance.BitDataDict[code].GetData(index, 1);
                data[0] = !data[0];
                DataManager.Instance.BitDataDict[code].SetData(index, data);
                return true;
            }
            return false;
        }

        private bool RunDecrease(MacroContext context)
        {
            if (Util.ValidWordAddress(context.Address, out string code, out int index))
            {
                int.TryParse(context.Value, out var value);
                var data = DataManager.Instance.WordDataDict[code].GetData(index, 1);
                data[0] = (ushort)(data[0] - value);
                DataManager.Instance.WordDataDict[code].SetData(index, data);
                return true;
            }
            else if (Util.ValidBitAddress(context.Address, out code, out index))
            {
                var data = DataManager.Instance.BitDataDict[code].GetData(index, 1);
                data[0] = !data[0];
                DataManager.Instance.BitDataDict[code].SetData(index, data);
                return true;
            }
            return false;
        }
    }
}