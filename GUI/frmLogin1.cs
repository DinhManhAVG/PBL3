﻿using BLL;
using GUI.MyCustomControl;
using Guna.UI2.WinForms.Enums;
using Org.BouncyCastle.Math.Field;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmLogin1 : Form
    {
        public frmLogin1()
        {
            InitializeComponent();
            //2 button nằm cùng 1 vị trí nhưng ban đầu cho buttonDoNotShow lên trước nên mới dùng BringToFont 
            btnDoNotShow.BringToFront();
        }

        // Custom Minisize and Close
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnMinisize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Ẩn hiện pasword
        private void btnDoNotShow_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar)
            {
                btnShow.BringToFront();
                txtPassword.PasswordChar = false;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if(!txtPassword.PasswordChar)
            {
                btnDoNotShow.BringToFront();
                txtPassword.PasswordChar = true;
            }
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            //Edit:2/3/2023 by Tai
            //Khi bấm vào lại textbox username thì ẩn mật khẩu đi
            //và hiện tại btnShow nằm chồng lên btnDoNotShow nên phải mang nó lên trước 
            txtPassword.PasswordChar = true;
            btnDoNotShow.BringToFront();
        }
        // Nút quên mật khẩu
        private void btnForgetPass_Click(object sender, EventArgs e)
        {
            frmForgetPass f = new frmForgetPass();
            f.ShowDialog();
            this.Close();
        }

        private void btnForgetPass_MouseHover(object sender, EventArgs e)
        {
            btnForgetPass.Font = new Font(btnForgetPass.Font, FontStyle.Underline);
            btnForgetPass.ForeColor = Color.HotPink;
        }

        private void btnForgetPass_MouseLeave(object sender, EventArgs e)
        {
            btnForgetPass.Font = new Font(btnForgetPass.Font, FontStyle.Regular);
            btnForgetPass.ForeColor = Color.Black;
        }
        // Đăng nhập
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (txtUsername.Texts.ToString() == "" || txtPassword.Texts.ToString() == "")
            {
                MessageBox.Show("Please fill in at least one field\r\nFill in at least one field to search for your account");
            }
            else
            {
                CheckLoginBLL checkLoginBLL = new CheckLoginBLL();
                //int count = checkLoginBLL.Check(txtUsername.Texts.ToString(), txtPassword.Texts.ToString());
                int count = 1;
                switch (count)
                {
                    case -1:
                        MessageBox.Show("Check your username and password");
                        break;
                    case 0:
                        MessageBox.Show("Đăng nhập với tư cách giáo viên");
                        break;
                    case 1:
                        MessageBox.Show("Đăng nhập với tư cách sinh viên");
                        break;

                }
            }
        }

        #region các thuộc tính xử lí form di chuyển
        private bool move;
        private int moveX, moveY;
        #endregion
        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            //Chỉ xử lí việc di chuyển form khi nhấn chuột trái
            if (e.Button == MouseButtons.Left)
            {
                move = true;
                moveX = e.X;
                moveY = e.Y;
            }
        }

        private void panelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                //set lại vị trí của form trên màn hình
                this.SetDesktopLocation(MousePosition.X - moveX, MousePosition.Y - moveY);
            }   
        }

        private void panelTitle_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }
    }
}
