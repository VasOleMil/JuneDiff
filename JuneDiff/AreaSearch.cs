using System;
using System.Drawing;
using System.Windows.Forms;

namespace JuneDiff
{
    public partial class AreaSearch : Form
    {
        public Bitmap   BkLft;
        public Bitmap   BkRht;
        public Timer    BkTmr;
        
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int GWL_EXSTYLE = -20;

        private bool Switch = true;

        public AreaSearch(FormMain pForm)
        {
            InitializeComponent(); 
            SetWindowLong(Handle, GWL_EXSTYLE, GetWindowLong(Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT);

            BkLft = null;
            BkRht = null;

            BkTmr = new Timer(); 
            BkTmr.Interval = 200;
            BkTmr.Tick += new EventHandler(OnTimer);
        }     

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
        private void OnTimer(object sender, EventArgs e)
        {
            if(Switch)
            {              
                BackgroundImage = BkLft;Switch = false;
            }
            else
            {
                BackgroundImage = BkRht; Switch = true;
            }
        }
    }
}
