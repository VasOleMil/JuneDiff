using System;
using System.Drawing;
using System.Windows.Forms;

namespace JuneDiff
{
    
    public partial class FormMain : Form
    {

        public  Bitmap      ScrBitmap;
        private int         ScrShiftL;
        private int         ScnLength;//scan region length
        private int         ScnCounts;//Point to scan
        private Point[]     ScnPixelS;
        private Point[]     ScnPixelR;
        private Color[]     ScnPixelc;
        private AreaSelect  ScrSelect;
        private AreaSearch  ScrSearch;



        public FormMain()
        {
            InitializeComponent();

            ScnLength = 10;         tb_Len.Text = ScnLength.ToString();
            ScnCounts = 1000;       tb_Cnt.Text = ScnCounts.ToString();

            Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2, 0);
            StartPosition = FormStartPosition.Manual;

            ScrSelect = null;
            ScrSearch = null;
        }
        private void bt_Call_Click(object sender, EventArgs e)
        {
            try { ScnLength = Convert.ToInt32(tb_Len.Text); } catch (Exception) { tb_Len.Text = ScnLength.ToString(); tb_Len.Focus(); return; };
            try { ScnCounts = Convert.ToInt32(tb_Cnt.Text); } catch (Exception) { tb_Cnt.Text = ScnCounts.ToString(); tb_Cnt.Focus(); return; };


            ScnPixelc = new Color[2 * ScnLength + 1];
            ScnPixelS = new Point[ScnCounts];
            ScnPixelR = new Point[ScnCounts];

            if (ScrSelect == null) ScrSelect = new AreaSelect(this);
            
            Hide(); ScrSelect.ShowDialog();
            
            int i, j, x, w, X, Y, W, H, c; Random Rnd = new Random();

            W = ScrSelect.Width;
            H = ScrSelect.Height;
            w = 2 * ScnLength + 1;

            for (i = 0; i < ScnCounts; i++) 
            {
                //set random point
                X = ScnPixelS[i].X = Rnd.Next(ScnLength, W - ScnLength);
                Y = ScnPixelS[i].Y = Rnd.Next(0, H);

                for (j = 0; j < w; j++) 
                {
                    ScnPixelc[j] = ScrBitmap.GetPixel(X - ScnLength + j, Y);                  
                }
                //scan similar region
                for (x = ScnLength; x < W - ScnLength; x++)
                {
                    if (x < X - w || X + w < x) 
                    {
                        ScnPixelR[i].Y = Y;
                        ScnPixelR[i].X = 0;

                        for (j = 0, c = 0; j < w; j++)
                        {
                            if (Math.Abs(ScnPixelc[j].ToArgb() - ScrBitmap.GetPixel(x - ScnLength + j, Y).ToArgb()) < 500) 
                            {
                                c++;
                            }
                        }
                        if (c > ScnLength) 
                        {                       
                            ScnPixelR[i].X = x; break;
                        }
                    }     
                }
                            
            }
            //Plot distance histogram
            int[] ScnCountx = new int[W]; for (i = 0; i < W; i++) ScnCountx[i] = 0;
            X = (W * 1) / 4;
            Y = (W * 3) / 4;
            for (i = 0; i < ScnCounts; i++)
            {
                if ((X < (x = Math.Abs(ScnPixelS[i].X - ScnPixelR[i].X))) && (x < Y) && (ScnPixelR[i].X > 0)) 
                {
                        ScnCountx[x]++;
                }                               
            }           
            //out data
            tb_Dif.Text = "{" + Environment.NewLine; for (i = 0; i < W - 1; i++)
            {
                tb_Dif.Text += ScnCountx[i].ToString().Replace(",", ".") + "," + Environment.NewLine;
            }
            tb_Dif.Text += ScnCountx[W - 1].ToString().Replace(",", ".") + Environment.NewLine + "}";

            tb_Dif.Focus(); tb_Dif.SelectAll(); tb_Dif.Copy();
            //find histogram maximum and image shift
            for (x = 0, ScrShiftL = 0, i = 0; i < W; i++)
            {
                if (x < ScnCountx[i])
                {
                    x = ScnCountx[i]; ScrShiftL = i;
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
    }
}
