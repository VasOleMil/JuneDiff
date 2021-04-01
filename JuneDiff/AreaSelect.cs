using System;
using System.Drawing;
using System.Windows.Forms;

namespace JuneDiff
{  
    public partial class AreaSelect : Form
    {
        private FormMain    ParntForm;
        private Bitmap      ScrBitmap;
        public AreaSelect(FormMain pForm)
        {
            InitializeComponent();

            ParntForm = pForm;
            Opacity = .5D;       
            panelDrag.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            Left = (Screen.GetWorkingArea(this).Width - Width) / 2;
            Top = (Screen.GetWorkingArea(this).Height - Height) / 2;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        protected override void OnPaint(PaintEventArgs e) // you can safely omit this method if you want
        {
            e.Graphics.FillRectangle(Brushes.Green, CTop);
            e.Graphics.FillRectangle(Brushes.Green, CLeft);
            e.Graphics.FillRectangle(Brushes.Green, CRight);
            e.Graphics.FillRectangle(Brushes.Green, CBottom);
        }

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        private const int _ = 10;

        Rectangle CTop { get { return new Rectangle(0, 0, ClientSize.Width, _); } }
        Rectangle CLeft { get { return new Rectangle(0, 0, _, ClientSize.Height); } }
        Rectangle CBottom { get { return new Rectangle(0, ClientSize.Height - _, ClientSize.Width, _); } }
        Rectangle CRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, ClientSize.Height); } }


        Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, _); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - _, _, _); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _); } }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (CTop.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (CLeft.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (CRight.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (CBottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
        private void panelDrag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        private void SelectArea_SizeChanged(object sender, EventArgs e)
        {
            bt_Select.Left = (panelDrag.Width - bt_Select.Width) / 2;
            bt_Select.Top = (panelDrag.Height - bt_Select.Height) / 2;
        }
        private void btnCaptureThis_Click(object sender, EventArgs e)
        {
            Hide();
           
            try
            {
                Rectangle ScrRectangle = new Rectangle(Location.X, Location.Y, Width, Height); ;
                ScrBitmap = new Bitmap(ScrRectangle.Width, ScrRectangle.Height, Graphics.FromHwnd(IntPtr.Zero));
                Graphics ScrGraphics = Graphics.FromImage(ScrBitmap); ParntForm.ScrBitmap = ScrBitmap;
                ScrGraphics.CopyFromScreen(ScrRectangle.Left, ScrRectangle.Top, 0, 0, ScrRectangle.Size);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ParntForm.Show();
        }
    }
}
