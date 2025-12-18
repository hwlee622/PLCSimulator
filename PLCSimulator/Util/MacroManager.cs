namespace PLCSimulator
{
    public class MacroManager
    {
        private Macro[] _macros;

        public MacroManager(DataManager dataManager, MacroInfo[] macroInfoArray)
        {
            _macros = new Macro[macroInfoArray.Length];
            for (int i = 0; i < macroInfoArray.Length; i++)
                _macros[i] = new Macro(dataManager, macroInfoArray[i]);
        }

        public Macro GetMacro(int index)
        {
            if (index < _macros.Length)
                return _macros[index];
            else
                return null;
        }

        public int GetAllMacroLength()
        {
            return _macros.Length;
        }
    }
}