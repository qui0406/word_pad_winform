using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Diagnostics.Eventing.Reader;

namespace MyWordPad
{
    public partial class WordPad : Form
    {

        public static String findString = "";
        Font font;
        float fontSize = 11;
        bool check = false;
        StringFormat stringFormat = new StringFormat();
        Point pCurrent;
        int x, y; // current pos 

        bool checkInDam = false;
        bool checkInNghieng = false;
        bool checkGachChan = false;
        bool checkGachNgang = false;
        bool checkSubscript = false;
        bool checkSuperscript = false;
        bool checkPen = false;

        //Color 
        Color defaultColor = Color.Black;
        int penWidth = 1;
        Point pO;
        Bitmap bmp;

        private void LoadIconMenuForm()
        {
            btnPaste.ImageIndex=0;
            btnCut.ImageIndex=0;
            btnCopy.ImageIndex=1;

            btnGrowFont.ImageIndex=2;
            btnShrinkFont.ImageIndex=3;

            btnInDam.ImageIndex=4;
            btnInNghieng.ImageIndex=5;
            btnGachChan.ImageIndex=6;
            btnGachNgang.ImageIndex=7;
            btnSubScript.ImageIndex=8;
            btnSuperScript.ImageIndex=9;

            btnPen.ImageIndex=10;

            btnDecreaseIndent.ImageIndex=11;
            btnIncreaseIndemnt.ImageIndex=12;
            btnStartAList.ImageIndex=13;
            btnLineSpacing.ImageIndex=14;
            btnTextAlineLeft.ImageIndex=15;
            btnTextCenter.ImageIndex=16;
            btnTextAlineRight.ImageIndex=17;
            btnJustify.ImageIndex=18;
            btnParagraph.ImageIndex=19;

            //insert
            btnPicture.ImageIndex=1;
            btnDateTime.ImageIndex=2;
            btnInsertObejct.ImageIndex=3;

            //editing
            btnFind.ImageIndex=20;
            btnReplace.ImageIndex=21;
            btnSelectAll.ImageIndex=22;

            //btn Zoom
            btnZoomIn.ImageIndex=4;
            btnZoomOut.ImageIndex=5;
            btnNormal.ImageIndex=6;
            //Load font


            foreach (FontFamily font in FontFamily.Families)
            {
                cbFont.Items.Add(font.Name);
            }

            rtbMain.AcceptsTab=true;

        }
        public WordPad()
        {
            InitializeComponent();
        }

        private void WordPad_Load(object sender, EventArgs e)
        {
            Invalidate();
            LoadIconMenuForm();
            DefaultFormat();
            bmp= new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            homeToolStripMenuItem.BackColor=Color.SkyBlue;
            viewToolStripMenuItem.BackColor=Color.White;
            this.Controls.Add(panelHome);
            this.Controls.Remove(panelView);
            panelHome.Dock=DockStyle.Top;
            this.Controls.Add(menuStrip1);
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            homeToolStripMenuItem.BackColor=Color.White;
            viewToolStripMenuItem.BackColor=Color.SkyBlue;
            this.Controls.Add(panelView);
            this.Controls.Remove(panelHome);
            panelView.Dock=DockStyle.Top;
            this.Controls.Add(menuStrip1);
        }

        // File
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res=
                MessageBox.Show("Do you want to save changes to Document?", "MyWordPad", MessageBoxButtons.YesNoCancel);
            if (res==DialogResult.Yes)
            {
                SaveFile();
            }
            if (res==DialogResult.No)
            {
                rtbMain.Clear();
            }
        }

        private void SaveFile()
        {
            SaveFileDialog save = new SaveFileDialog();
            try
            {
                DialogResult res = new DialogResult();
                save.Filter="save | *rtf";
                res=save.ShowDialog();
                if (res==DialogResult.OK)
                {
                    rtbMain.SaveFile(save.FileName, RichTextBoxStreamType.RichText);
                }
            }
            catch
            {
                MessageBox.Show("Không lưu được file", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void DefaultFormat()
        {
            cbFontSize.SelectedIndex=3;
            this.Controls.Remove(panelView);
            cbFont.SelectedIndex=0;
            cbWrap.SelectedIndex=0;
            cbDoDai.SelectedIndex=0;
            font = new Font("Cabliri", 11);
            rtbMain.SelectionFont=font;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            try
            {
                open.Filter="open |*.rtf";
                DialogResult res = new DialogResult();
                res=open.ShowDialog();
                if (res==DialogResult.OK)
                {
                    rtbMain.LoadFile(open.FileName, RichTextBoxStreamType.RichText);
                }
            }
            catch
            {
                MessageBox.Show("Không mở được file", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog print = new PrintDialog();
            DialogResult res= new DialogResult();
            res=print.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Home
        private void btnCopy_Click(object sender, EventArgs e)
        {
            rtbMain.Copy();
        }
        private void btnPaste_Click(object sender, EventArgs e)
        {
            rtbMain.Paste();
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            rtbMain.Cut();
        }

        private void cbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rtbMain.SelectionFont = new Font(cbFont.SelectedItem.ToString(), fontSize);
            }
            catch
            {
                MessageBox.Show("Err");
            }
        }

        private void cbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fontSize=float.Parse(cbFontSize.SelectedItem.ToString());
                rtbMain.SelectionFont = new Font(Font.Name,fontSize);
            }
            catch
            {
                MessageBox.Show("Err");
            }
        }

        private void btnGrowFont_Click(object sender, EventArgs e)
        {
            try
            {
                fontSize++;
                cbFontSize.Text=fontSize.ToString();

                rtbMain.SelectionFont = new Font(Font.Name, fontSize);
            }
            catch
            {
                MessageBox.Show("Err");
            }
        }

        private void btnShrinkFont_Click(object sender, EventArgs e)
        {
            try
            {
                fontSize--;
                cbFontSize.Text=fontSize.ToString();

                rtbMain.SelectionFont = new Font(Font.Name, fontSize);
            }
            catch
            {
                MessageBox.Show("Err");
            }
        }

        private void btnInDam_Click(object sender, EventArgs e)
        {
            
            rtbMain.Font= new Font(this.rtbMain.SelectionFont, this.rtbMain.Font.Style ^ FontStyle.Bold);
            rtbMain.SelectionColor=btnColorText.ForeColor;

            checkInDam=!checkInDam;
            if (checkInDam)
            {
                btnInDam.BackColor=Color.Gray;
            }
            else
            {
                btnInDam.BackColor=Color.White;
            }

        }
        private void btnInNghieng_Click(object sender, EventArgs e)
        {
            rtbMain.Font= new Font(this.rtbMain.SelectionFont, this.rtbMain.Font.Style ^ FontStyle.Italic);
            rtbMain.SelectionColor=btnColorText.ForeColor;
            //mau khi active 
            checkInNghieng=!checkInNghieng;
            if (checkInNghieng)
            {
                btnInNghieng.BackColor=Color.Gray;
            }
            else
            {
                btnInNghieng.BackColor=Color.White;
            }
        }

        private void btnGachChan_Click(object sender, EventArgs e)
        {
            rtbMain.Font= new Font(this.rtbMain.SelectionFont, this.rtbMain.Font.Style ^ FontStyle.Underline);
            rtbMain.SelectionColor=btnColorText.ForeColor;
            //mau khi active
            checkGachChan=!checkGachChan;
            if (checkGachChan)
            {
                btnGachChan.BackColor=Color.Gray;
            }
            else
            {
                btnGachChan.BackColor=Color.White;
            }
        }

        private void btnGachNgang_Click(object sender, EventArgs e)
        {
            rtbMain.Font= new Font(this.rtbMain.SelectionFont, this.rtbMain.Font.Style ^ FontStyle.Strikeout);
            rtbMain.SelectionColor=btnColorText.ForeColor;

            checkGachNgang=!checkGachNgang;
            if (checkGachNgang)
            {
                btnGachNgang.BackColor=Color.Gray;
            }
            else
            {
                btnGachNgang.BackColor=Color.White;
            }
        }

        private void btnTextAlineLeft_Click(object sender, EventArgs e)
        {
            rtbMain.SelectionAlignment=HorizontalAlignment.Left;
        }

        private void btnTextCenter_Click(object sender, EventArgs e)
        {
            rtbMain.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void btnTextAlineRight_Click(object sender, EventArgs e)
        {
            rtbMain.SelectionAlignment=HorizontalAlignment.Right;
        }

        private void btnJustify_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSubScript_Click(object sender, EventArgs e)
        {
            float tmpFontSize = fontSize/2;
            check=!check;
            if (check)
            {
                rtbMain.SelectionCharOffset=-5;
                rtbMain.SelectionFont= new Font(Font.Name, tmpFontSize);
            }
            else
            {
                rtbMain.SelectionCharOffset=0;
                rtbMain.SelectionFont= new Font(Font.Name, fontSize);
            }

            checkSubscript=!checkSubscript;
            if (checkSubscript)
            {
                btnSubScript.BackColor=Color.Gray;
            }
            else
            {
                btnSubScript.BackColor=Color.White;
            }
        }

        private void btnSuperScript_Click(object sender, EventArgs e)
        {
            float tmpFontSize = fontSize/2;
            check=!check;
            if (check)
            {
                rtbMain.SelectionCharOffset=5;
                rtbMain.SelectionFont= new Font(Font.Name, tmpFontSize);
            }
            else
            {
                rtbMain.SelectionCharOffset=0;
                rtbMain.SelectionFont= new Font(Font.Name, fontSize);
            }

            checkSuperscript=!checkSuperscript;
            if (checkSuperscript)
            {
                btnSuperScript.BackColor=Color.Gray;
            }
            else
            {
                btnSuperScript.BackColor=Color.White ;
            }
        }

        private void btnColorText_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            color.AllowFullOpen=true;
            color.FullOpen=true;
            color.AnyColor=true;
            if (color.ShowDialog()==DialogResult.OK)
            {
                rtbMain.SelectionColor=color.Color;
                btnColorText.ForeColor=color.Color;
            }
        }

        private void btnPen_Click(object sender, EventArgs e)
        {
                
        }
        private void btnDecreaseIndent_Click(object sender, EventArgs e)
        {
            int lineMouse = rtbMain.GetLineFromCharIndex(rtbMain.SelectionStart); //lay thu tu hang con tro chuot dang o           
            int charIndex = rtbMain.GetFirstCharIndexFromLine(lineMouse); //vi tri ki tu thu may o dau dong
            rtbMain.SelectionStart= charIndex+1;
            if (rtbMain.SelectedText=="\t")
            {
                rtbMain.SelectedText="";
            }
        }

        private void rtbMain_KeyPress(object sender, KeyPressEventArgs e) //xu ly phim tab
        {
            if (e.KeyChar==9)
            {
                rtbMain.SelectedText="\t";
                e.Handled=true;
            }
        }

        private void btnIncreaseIndemnt_Click(object sender, EventArgs e)
        {
            int lineMouse = rtbMain.GetLineFromCharIndex(rtbMain.SelectionStart); //lay thu tu hang con tro chuot dang o           
            int charIndex = rtbMain.GetFirstCharIndexFromLine(lineMouse); //vi tri ki tu thu may o dau dong
            rtbMain.SelectionStart= charIndex;
            rtbMain.SelectedText="\t";
        }

        private void btnLineSpacing_Click(object sender, EventArgs e)
        {
            
        }

        private void btnParagraph_Click(object sender, EventArgs e)
        {

        }

        public static String GetContentText = "";

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindForm f = new FindForm();
            f.ShowDialog();
            GetContentText=rtbMain.Text;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            rtbMain.SelectAll();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            Replace rp = new Replace();
            rp.ShowDialog();
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.ValidateNames = false;
                openFileDialog.CheckFileExists = false;
                openFileDialog.CheckPathExists = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string file = openFileDialog.FileName;
                        if (file.EndsWith(".jpg")|| file.EndsWith(".png") ||
                            file.EndsWith(".gif") || file.EndsWith(".bmp"))
                        {
                            PictureBox pictureBox = new PictureBox();
                            pictureBox.Location= new Point(x, y); 
                            pictureBox.SizeMode=PictureBoxSizeMode.StretchImage;
                            Image img = Image.FromFile(file);
                            pictureBox.Image = img;
                            Clipboard.SetImage(img);
                            rtbMain.Paste();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi file hình ảnh", "Thông báo");
                    }
                }
            }
        }

        private void btnDateTime_Click(object sender, EventArgs e)
        {
            DateAndTime dt = new DateAndTime();
            dt.ShowDialog();
            rtbMain.Text=DateAndTime.getDate;
        }

        private void btnInsertObejct_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.ShowDialog();
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            rtbMain.Left-=10;
            rtbMain.Width+=20;
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            rtbMain.Left+=10;
            rtbMain.Width-=20;
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            rtbMain.Left=100;
            rtbMain.Width=ClientRectangle.Right-200;
        }

        private void WordPad_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void WordPad_MouseDown(object sender, MouseEventArgs e)
        {
            pO=e.Location;
        }

        private void WordPad_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                Pen pen = new Pen(defaultColor, penWidth);
                pen.StartCap=LineCap.Round;
                pen.EndCap=LineCap.Round;
                Graphics g = Graphics.FromImage(bmp);
                g.DrawLine(pen, pO, e.Location);
                pO=e.Location;
                Invalidate();
            }
        }

        private void btnPen_Click_1(object sender, EventArgs e)
        {
            checkPen=!checkPen;
            if (checkPen)
            {
                this.Controls.Remove(rtbMain);
                this.BackColor=Color.White;
            }
            else
            {
                this.Controls.Add(rtbMain);
                this.BackColor=Color.Silver;
            }
        }

        private void cbWrap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbWrap.SelectedIndex==0)
            {
                rtbMain.Left=100;
                rtbMain.Width=ClientRectangle.Right-200;
            }
            if (cbWrap.SelectedIndex==1)
            {
                rtbMain.Left=0;
                rtbMain.Width=ClientRectangle.Width;
            }
        }       
    }
}
