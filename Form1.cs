using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;

namespace BaiTapVeNha
{
    public partial class Form1 : Form
    {
        Panel pnTable, pnInput, pnProgbar;
        Label lblTitleInput;
        TextBox txtSoLuongO;
        Button btn;
        ProgressBar prBarTime;
        public static System.Timers.Timer aTimer;
        int t = 0;
        Button[,] MangButton;
        int soCap;

        int demN_O = 0;
        int demN_X = 0;
        int demD_O = 0;
        int demD_X = 0;
        public Form1()
        {

            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            pnTable = new Panel();
            pnTable.Size = new Size(1050,550);
            pnTable.Left = 10;
            pnTable.Top = 10;
            pnTable.BackColor = Color.Gray;
            this.Controls.Add(pnTable);


            //================================================//
            //Tạo panel chưa progbar
            pnProgbar = new Panel();
            pnProgbar.Size = new Size(1050, 100);
            pnProgbar.Left = 10;
            pnProgbar.Top = 560;
            pnProgbar.BackColor = Color.Gold;
            this.Controls.Add(pnProgbar);

            prBarTime = new ProgressBar();
            pnProgbar.Controls.Add(prBarTime);
            prBarTime.Minimum = 0; //Đặt giá trị nhỏ nhất cho ProgressBar
            prBarTime.Maximum = 10; //Đặt giá trị lớn nhất cho ProgressBar
            

                
            

            //===========================================//
            //Tạo panel chứa nội dung input
            pnInput = new Panel();
            pnInput.Size = new Size(250, 650);
            pnInput.Left = 1110;
            pnInput.Top = 10;
            pnInput.BackColor = Color.Silver;
            
            //
            lblTitleInput = new Label();
            lblTitleInput.Text = "Nhập số lượng cột";
            lblTitleInput.Left = 10;
            lblTitleInput.Top = 40;
            lblTitleInput.Size = new System.Drawing.Size(150, 30);
            pnInput.Controls.Add(lblTitleInput);
            //Tạo các Textbox để nhập số lượng ô chơi
            txtSoLuongO = new TextBox();
            txtSoLuongO.Left = 10;
            txtSoLuongO.Top = 80;
            txtSoLuongO.Size = new Size(150, 40);
            pnInput.Controls.Add(txtSoLuongO);
            //Tạo button tạo bàn cờ
            btn = new Button();
            btn.Name = "btnOK";
            btn.Text = "Bắt Đầu";
            btn.Size = new Size(100, 30);
            btn.Left = 10;
            btn.Top = 110;
            pnInput.Controls.Add(btn);
            btn.Click += new System.EventHandler(bt_Click);
            this.Controls.Add(pnInput);
        }
        private void bt_Click(object sender, EventArgs e)
        {
            
            try
            {
                soCap = int.Parse(txtSoLuongO.Text);
            }
            catch
            {
                soCap = 0;
            }
            if (soCap == 0 || soCap>20)
            {
                MessageBox.Show("Bạn chỉ được nhập số và phải bé hơn 20");
                txtSoLuongO.Text = "";
            }
            else
            {
                TaoBanCo(soCap);
            }
        }
        

        


        private Button TaoMotButton(int l, int t,int tt)
        {
            btn = new Button();
            btn.Name = "btn"+tt;
            btn.Size = new Size(30, 30);
            btn.Left = l;
            btn.Top = t;
            btn.Click += new System.EventHandler(bt_Click_QuanCo);
            this.Controls.Add(pnInput);
            pnTable.Controls.Add(btn);
            return btn;
        }
        private void bt_Click_QuanCo(object sender, EventArgs e)
        {
            
            Button b = (Button)(sender);
            if (b.Text.ToString().Trim().Equals(""))
            {

                if (t == 1)
                {
                    //b.Image = Image.FromFile("C:\\PHANMEM\\WORKSPACE\\PTPMVUDTM\\2001170072_NguyenQuangHuy_Buoi01\\BaiTapVeNha\\BaiTapVeNha\\img\\o.png");
                    b.Text = "O";
                    b.ForeColor = Color.Red;
                    t = 0;
                    XuLyGame(soCap);
                    XuLyCheoChinh(soCap);
                    XuLyCheoPhu(soCap);
                }
                else
                {
                    //b.Image = Image.FromFile("C:\\PHANMEM\\WORKSPACE\\PTPMVUDTM\\2001170072_NguyenQuangHuy_Buoi01\\BaiTapVeNha\\BaiTapVeNha\\img\\x.png");
                    b.Text = "X";
                    b.ForeColor = Color.Blue;
                    t = 1;
                    XuLyGame(soCap);
                    XuLyCheoChinh(soCap);
                    XuLyCheoPhu(soCap);
                }
            }

            
        }

        


        //code xử lý game
        private void XuLyGame(int c)
        {
            
            //th có 4 quân ngang
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    
                    if (MangButton[i, j].Text.Equals("O"))
                    {
                        demN_O++;
                        if (demN_O == 4)
                        {
                            if (MangButton[i, j + 1].Text.ToString().Trim().Equals("X") || MangButton[i, j].Text.ToString().Trim().Equals("X"))
                            {
                                break;
                            }
                            else
                            {
                                MessageBox.Show("O Thang X Thua");
                                Application.Restart();
                            }
                        }
                    }
                    else
                        demN_O = 0;

                    
                    if (MangButton[i, j].Text.Equals("X"))
                    {
                        demN_X++;
                        if (demN_X == 4)
                        {
                            MessageBox.Show("X Thang O Thua");
                            Application.Restart();
                        }
                    }
                    else
                        demN_X = 0;
                }

            }

            //Xử lý dọc
            
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (MangButton[j, i].Text.Equals("O"))
                    {
                        demD_O++;
                        if (demD_O == 4)
                        {
                            MessageBox.Show("O Thang X Thua");
                            Application.Restart();
                        }
                    }
                    else
                        demD_O = 0;
                    
                    if (MangButton[j, i].Text.Equals("X"))
                    {
                        demD_X++;
                        if (demD_X == 4)
                        {
                            MessageBox.Show("X Thang O Thua");
                            Application.Restart();
                        }
                    }
                    else
                        demD_X = 0;
                }

            }

            //Xử Lý chéo 1.0
            int dem_C_X = 1;
            int cX = c;
            while (cX > 0)
            {
                for (int i = 0, j = cX - 1; i < cX && j > 0; i++, j--)
                {
                    if (MangButton[i, j].Text.ToString().Trim().Equals((MangButton[i + 1, j - 1]).Text.ToString().Trim()) && (MangButton[i + 1, j - 1]).Text.ToString().Trim().Equals("X"))
                    {
                        dem_C_X++;
                        if (dem_C_X == 4)
                        {
                            MessageBox.Show("X Thang O Thua");
                            Application.Restart();
                        }
                    }
                    else
                        dem_C_X = 1;


                }
                cX--;
            }

            int dem_C_O = 1;
            int cO = c;
            while (cO > 0)
            {
                for (int i = 0, j = cO - 1; i < cO && j > 0; i++, j--)
                {
                    if (MangButton[i, j].Text.ToString().Trim().Equals((MangButton[i + 1, j - 1]).Text.ToString().Trim()) && (MangButton[i + 1, j - 1]).Text.ToString().Trim().Equals("O"))
                    {
                        dem_C_O++;
                        if (dem_C_O == 4)
                        {
                            MessageBox.Show("O Thang X Thua");
                            Application.Restart();
                        }
                    }
                    else
                        dem_C_O = 1;
                }
                cO--;
            }


            //===================
            //Xử Lý chéo 2.0
            
            
            
        }
        private void XuLyCheoPhu(int c)
        {
            int dem = 1;
            int j = c - 1;
            for (int i = 0; i < c-1; i++)
            {
                if (MangButton[i, j].Text.ToString().Trim().Equals(MangButton[i + 1, j - 1].Text.ToString().Trim()) && MangButton[i, j].Text.ToString().Trim().Equals("X"))
                {
                    dem++;
                    j--;
                    if (dem == 4)
                    {
                        MessageBox.Show("X Thang O Thua");
                        Application.Restart();
                    }
                }
                else
                    dem = 1;
            }

            //Xử lý cho O
            int demO = 1;
            int jO = c - 1;
            for (int i = 0; i < c - 1; i++)
            {
                if (MangButton[i, jO].Text.ToString().Trim().Equals(MangButton[i + 1, jO - 1].Text.ToString().Trim()) && MangButton[i, jO].Text.ToString().Trim().Equals("O"))
                {
                    demO++;
                    jO--;
                    if (demO == 4)
                    {
                        MessageBox.Show("O Thang X Thua");
                        Application.Restart();
                    }
                }
                else
                    demO = 1;
            }
        }

        //xử lý chéo chính
        private void XuLyCheoChinh(int c)
        {
            //Xử lý quân X
            int demX = 1;
            int jX = 0;
            for (int i = 0; i < c - 1; i++)
            {
                if (MangButton[i, jX].Text.ToString().Trim().Equals(MangButton[i + 1, jX + 1].Text.ToString().Trim()) && MangButton[i, jX].Text.ToString().Trim().Equals("X"))
                {
                    demX++;
                    jX++;
                    if (demX == 4)
                    {
                        MessageBox.Show("X Thang O Thua");
                        Application.Restart();
                    }
                }
                else
                    demX = 1;
            }

            //Xử lý quân X

            int demO = 1;
            int jO = 0;
            for (int i = 0; i < c - 1; i++)
            {
                if (MangButton[i, jO].Text.ToString().Trim().Equals(MangButton[i + 1, jO + 1].Text.ToString().Trim()) && MangButton[i, jO].Text.ToString().Trim().Equals("O"))
                {
                    demO++;
                    jO++;
                    if (demO == 4)
                    {
                        MessageBox.Show("O Thang X Thua");
                        Application.Restart();
                    }
                }
                else
                    demO = 1;
            }
        }

        private void TaoBanCo(int x)
        {
            MangButton = new Button[x, x];
            int left = 20;
            int top  = 20;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    MangButton[i,j] = TaoMotButton(left, top, i*j);
                    left += 30;
                }
                left = 20;
                top += 30;
            }
        }
    }
    

}
