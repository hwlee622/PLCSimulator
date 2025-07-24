using System.Windows.Forms;

namespace PLCSimulator
{
    public class UntabButton : Button
    {
        public UntabButton()
        {
            TabStop = false;
        }

        // Disable Tab selected square
        protected override bool ShowFocusCues { get { return false; } }
    }
}
