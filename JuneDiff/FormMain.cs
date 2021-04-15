using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace JuneDiff
{
    
    public partial class FormMain : Form
    {

        public  Bitmap      ScrBitmap;
        private int         ScrShiftL;
        private int         ScnLength;//scan region length
        private int         ScnCounts;//Point to scan
        private int[]       ScnPixelS;
        private Color[]     ScnPixelc;
        private AreaSelect  ScrSelect;
        private AreaSearch  ScrSearch;



        public FormMain()
        {
            InitializeComponent();

            ScnLength = 50;       tb_Len.Text = ScnLength.ToString();
            ScnCounts = 10;       tb_Cnt.Text = ScnCounts.ToString();

            Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2, 0);
            StartPosition = FormStartPosition.Manual;

            ScrSelect = null;
            ScrSearch = null;
        }
        private void bt_Call_Click(object sender, EventArgs e)
        {

            try { ScnLength = Convert.ToInt32(tb_Len.Text); } catch (Exception) { tb_Len.Text = ScnLength.ToString(); tb_Len.Focus(); return; };
            try { ScnCounts = Convert.ToInt32(tb_Cnt.Text); } catch (Exception) { tb_Cnt.Text = ScnCounts.ToString(); tb_Cnt.Focus(); return; };


            if (ScrSelect == null) ScrSelect = new AreaSelect(this);

            Hide(); ScrSelect.ShowDialog();

            int i, j, x, y, w, X, Y, W, H, c; Random Rnd = new Random();
            double S, M;

            W = ScrSelect.Width;
            H = ScrSelect.Height;
            w = 2 * ScnLength + 1;

            ScnPixelc = new Color[w];
            ScnPixelS = new int [W]; for (x = 0; x < W; x++) ScnPixelS[x] = 0;


            for (i = 0; i < ScnCounts; i++)
            {
                //set random point
                X =  W / 4;
                Y =  Rnd.Next(0, H);

                //Get pattern data
                for (j = 0; j < w; j++)
                {
                    ScnPixelc[j] = ScrBitmap.GetPixel(X - ScnLength + j, Y);
                }

                for (x = W / 2, M = w * w, c = X; x < W - ScnLength; x++) 
                {
                    for (j = 0, S = 0; j < w; j++)
                    {
                        S += ColorDiff(ScnPixelc[j], ScrBitmap.GetPixel(x - ScnLength + j, Y));
                    }

                    if (S < M)
                    {
                        M = S;
                        c = x;
                    }
                }
                
                ScnPixelS[c - X]++;
            }
            //out data
            tb_Dif.Text = "{" + Environment.NewLine; for (i = 0; i < W - 1; i++)
            {
                tb_Dif.Text += ScnPixelS[i].ToString().Replace(",", ".") + "," + Environment.NewLine;
            }
            tb_Dif.Text += ScnPixelS[W - 1].ToString().Replace(",", ".") + Environment.NewLine + "}";
            //find histogram maximum and image shift
            for (x = 0, ScrShiftL = 0, i = 1; i < W; i++)
            {
                if (x < ScnPixelS[i])
                {
                    x = ScnPixelS[i]; ScrShiftL = i;
                }
            }
        }
        private void bt_Find_Click(object sender, EventArgs e)
        {
            if (ScrSelect == null) return;

            if (ScrSearch == null)
            {
                ScrSearch = new AreaSearch(this);
            }
            if (ScrSearch.Visible) 
            {
                ScrSearch.BkTmr.Stop();
                ScrSearch.Hide();
            }

            ScrSearch.Top = ScrSelect.Top;
            ScrSearch.Left = ScrSelect.Left;
            ScrSearch.Height = ScrSelect.Height;
            ScrSearch.Width = ScrSelect.Width / 2;

            Rectangle ScrRectangle = new Rectangle(ScrSelect.Left, ScrSelect.Top, ScrSelect.Width, ScrSelect.Height);
            ScrBitmap = new Bitmap(ScrRectangle.Width, ScrRectangle.Height, Graphics.FromHwnd(IntPtr.Zero));
            Graphics ScrGraphics = Graphics.FromImage(ScrBitmap);
            ScrGraphics.CopyFromScreen(ScrRectangle.Left, ScrRectangle.Top, 0, 0, ScrRectangle.Size);

            ScrSearch.BkLft = ScrBitmap.Clone(new Rectangle(0, 0, ScrSearch.Width, ScrSearch.Height), ScrBitmap.PixelFormat);
            ScrSearch.BkRht = ScrBitmap.Clone(new Rectangle(0, 0, ScrSearch.Width, ScrSearch.Height), ScrBitmap.PixelFormat);

            int i, j, sHeight = ScrSearch.Height, sWidth = ScrShiftL > ScrSearch.Width ? ScrRectangle.Width - ScrShiftL : ScrSearch.Width;
            
            for (i = 0; i < sWidth; i++) 
            {
                for (j = 0; j < sHeight; j++)
                {
                    ScrSearch.BkRht.SetPixel(i, j, ScrBitmap.GetPixel(ScrShiftL + i, j));
                }
            }

            ScrSearch.BkTmr.Start();
            ScrSearch.Show();            
        }
        private void bt_Hide_Click(object sender, EventArgs e)
        {
            if (ScrSelect == null) return;

            if (ScrSearch == null)
            {
                return;
            }
            if (ScrSearch.Visible)
            {
                ScrSearch.BkTmr.Stop();
                ScrSearch.Hide();
            }
        }
        private double ColorDiff(Color t, Color p)
        {
            double r = 0.21 * (t.R - p.R) + .71 * (t.G - p.G) + .071 * (t.B - p.B);
            return r*r;
        }
    }
}
