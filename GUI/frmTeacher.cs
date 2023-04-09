﻿using BLL;
using DTO;
using GUI.MyUserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmTeacher : Form
    {
        private string ButtonClicked = "Home";
        private UC_TeacherInfo Info;
        public frmTeacher(string Account)
        {
            InitializeComponent();
            CollapseMenu();
            Info = new UC_TeacherInfo();
            LoadInfo(Account);
        }
        #region Methods
        private void CollapseMenu()
        {
            if (this.panelMenu.Width > 200) //Collapse menu
            {
                panelMenu.Width = 100;
                pictureBox1.Visible = false;
                btnMenu.Dock = DockStyle.Top;
                //panelProfile.Size = new Size(1350, 400);
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
                pnlMain.Width += 100;
                pnlMain.Location = new Point(pnlMain.Location.X - 100, pnlMain.Location.Y);
            }
            else
            { //Expand menu
                panelMenu.Width = 250;
                pictureBox1.Visible = true;
                btnMenu.Dock = DockStyle.None;
                //panelProfile.Size = new Size(1200, 400);
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "   " + menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
                pnlMain.Width -= 100;
                pnlMain.Location = new Point(pnlMain.Location.X + 100, pnlMain.Location.Y);
            }
        }
        void LoadInfo(string Account)
        {
            GiangVien_DTO gv = GiangVienBLL.Instance.GetGiangVienById(Account);
            Info.Name = gv.Ho + " " + gv.Ten;
            Info.IdTeacher = gv.MaNguoiDung;
            Info.Khoa = gv.Khoa;
            Info.NgaySinh = gv.NgaySinh.ToShortDateString();
            Info.GioiTinh = (gv.GioiTinh) ? "Nam" : "Nữ";
            Info.TrinhDo = gv.TrinhDo;
            Info.Luong = gv.Luong.ToString();
            Info.EmailCaNhan = gv.EmailCaNhan;
            Info.EmailTruongCap = gv.EmailTruongCap;
            Info.SDT = gv.Sdt;
            Info.DanToc = gv.DanToc;
            Info.QuocTich = gv.QuocTinh;
            Info.NoiSinh = gv.NoiSinh;
            Info.CCCD = gv.MaCCCD;
            Info.SetDiaChi(gv.DiaChi);
        }
        #endregion
        #region Events
        private void btnMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            //Clear hết control trong panel chứa các chức năng trên màn hình
            if(ButtonClicked != "Home")
            {
                pnlMain.Controls.Clear();
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            UtilityClass.OpenNewForm(this, new frmLogin());
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (ButtonClicked != "Thông tin cá nhân")
            {
                pnlMain.Controls.Clear();
                pnlMain.Controls.Add(Info);
            }
        }

        private void btnManageClass_Click(object sender, EventArgs e)
        {
            if (ButtonClicked != "Quản lí lớp")
            {
                pnlMain.Controls.Clear();
            }
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            if (ButtonClicked != "Xem lịch")
            {
                pnlMain.Controls.Clear();
            }
        }

        private void btnDarkMode_Click(object sender, EventArgs e)
        {
            if (ButtonClicked != "Nền tối")
            {
                pnlMain.Controls.Clear();
            }
        }
        #endregion
    }
}
