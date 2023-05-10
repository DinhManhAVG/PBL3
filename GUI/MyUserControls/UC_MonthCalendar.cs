﻿using GUI.MyCustomControl;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.MyUserControls
{
    public partial class UC_MonthCalendar : UserControl
    {
        //public event EventHandler ButtonPrevious;
        //public event EventHandler ButtonToday;
        //public event EventHandler ButtonNext;
        public event EventHandler ButtonDate;

        public UC_Calendar_New UC_Calendar { get; set; }
        public CustomButton[,] btn = new CustomButton[6, 7];
        private Size formOriginalSize;
        private Color colorBack;
        public Color ColorBack
        {
            get => colorBack;
            set
            {
                colorBack = value;
                this.BackColor = colorBack;
                tableLayoutPanel1.BackColor = colorBack;
                lbMonday.ForeColor = ((colorBack == Color.White) ? Color.Black : Color.White);
                lbTuesday.ForeColor = ((colorBack == Color.White) ? Color.Black : Color.White);
                lbWednesday.ForeColor = ((colorBack == Color.White) ? Color.Black : Color.White);
                lbThursday.ForeColor = ((colorBack == Color.White) ? Color.Black : Color.White);
                lbFriday.ForeColor = ((colorBack == Color.White) ? Color.Black : Color.White);
                lbSaturday.ForeColor = ((colorBack == Color.White) ? Color.Black : Color.White);
                lbSunday.ForeColor = ((colorBack == Color.White) ? Color.Black : Color.White);
                //btnPrevious.BackColor = ((colorBack == Color.White) ? Color.White : Color.FromArgb(58, 59, 60));
                //btnPrevious.ForeColor = ((colorBack == Color.White) ? Color.Black : Color.White);
                //btnNext.BackColor = ((colorBack == Color.White) ? Color.White : Color.FromArgb(58, 59, 60));
                //btnNext.ForeColor = ((colorBack == Color.White) ? Color.Black : Color.White);
                btnDate.BackColor = ((colorBack == Color.White) ? Color.White : Color.FromArgb(58, 59, 60));
                btnDate.ForeColor = ((colorBack == Color.White) ? Color.Black : Color.White);
                btnDate.ForeColor = (colorBack == Color.White) ? Color.FromArgb(13, 87, 119) : Color.FromArgb(227, 111, 38);
                LoadDays();
                this.Invalidate();
            }
        }
        int MONTH, YEAR;
        //Button[,] btn = new Button[6, 7];
        String[,] dTime = new String[6, 7];
        public UC_MonthCalendar()
        {
            InitializeComponent();
            init();
            formOriginalSize = this.Size;
        }
        //Methods
        #region Methods
        public void init()
        {
            colorBack = Color.White;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                {
                    btn[i, j] = new CustomButton();
                    btn[i, j].BorderSize = 1;
                    btn[i, j].BorderColor = Color.LightGray;
                    btn[i, j].BorderRadius = 40;
                    btn[i, j].Font = new Font("Lucida Handwriting", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn[i, j].Size = new Size(152, 82);
                    btn[i, j].BackColor = ((colorBack == Color.White) ? Color.White : Color.FromArgb(58, 59, 60));
                    btn[i, j].ForeColor = ((colorBack == Color.White) ? Color.Black : Color.White);
                    btn[i, j].FlatStyle = FlatStyle.Flat;
                    btn[i, j].FlatAppearance.BorderSize = 1;
                    btn[i, j].Click += buttonDate_Click;
                    tableLayoutPanel1.Controls.Add(btn[i, j]);
                }
        }

        //Kiem tra nam nhuan
        public bool isLeapYear(int N)
        {
            if (N % 4 == 0 && N % 100 != 0)
                return true;
            if (N % 400 == 0)
                return true;
            return false;
        }

        //Determine number of day in the month
        public int Nday(int month, int year)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    if (isLeapYear(year))
                        return 29;
                    return 28;
            }
            return 0;
        }

        public string leng2(String s)
        {
            if (s.Length == 1)
                return "0" + s;
            return s;
        }

        public int getDay(int month, int year)
        {
            int N = year - 1;
            int d = N * 365 + N / 4 - N / 100 + N / 400;
            for (int i = 1; i < month; i++)
                d += Nday(i, N + 1);
            return d;
        }

        public int getThu(int month, int year)
        {
            //Lấy thứ trong tuần
            return getDay(month, year) % 7 + 2;
        }

        public void reset()
        {
            float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
            float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                {
                    btn[i, j].BorderSize = 1;
                    btn[i, j].Font = new Font("Segoe UI", 8F, ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn[i, j].BorderColor = Color.LightGray;
                    btn[i, j].Size = new Size((int)(btn[i, j].Width * xRatio), (int)(btn[i, j].Height * yRatio));
                    btn[i, j].Location = new Point((int)(btn[i, j].Location.X * xRatio), (int)(btn[i, j].Location.Y * yRatio));
                    btn[i, j].BackColor = ((colorBack == Color.White) ? Color.WhiteSmoke : Color.FromArgb(58, 59, 60));
                    btn[i, j].ForeColor = ((colorBack == Color.White) ? Color.Black : Color.WhiteSmoke);
                    dTime[i, j] = "";
                }
            formOriginalSize = this.Size;
        }

        public int[,] update(int month, int year)
        {
            reset();
            int[,] a = new int[6, 7];
            int thu = getThu(month, year);
            int day = Nday(month, year);
            //Previous day
            int pday = 0;
            if (month > 1)
                pday = Nday(month - 1, year);
            else
                pday = Nday(12, year - 1);
            int start = thu - 1;
            if (start == 7)
                start = 0;
            int I = 0, J = start;
            for (int i = 1; i <= day; i++)
            {
                btn[I, J].Text = i.ToString();
                btn[I, J].ForeColor = ((colorBack == Color.White) ? Color.Black : Color.WhiteSmoke);
                btn[I, J].BackColor = ((colorBack == Color.White) ? Color.WhiteSmoke : Color.FromArgb(58, 59, 60));
                if (i == DateTime.Now.Day && month == DateTime.Now.Month && year == DateTime.Now.Year)
                {
                    btn[I, J].BorderSize = 3;
                    btn[I, J].BorderColor = Color.DeepSkyBlue;
                    btn[I, J].BackColor = ((colorBack == Color.White) ? Color.FromArgb(215, 249, 249) : Color.FromArgb(238, 191, 109));
                }
                dTime[I, J] = leng2(i + "") + "-" + leng2(month + "") + "-" + year;
                if (year != YEAR && MONTH == month + 1 && i == day)
                {
                    btn[I, J].BackColor = Color.Cyan;
                }
                J++;
                if (J == 7)
                {
                    J = 0;
                    I++;
                }
            }

            //Xác định phần ngày của tháng trước đó
            for (int i = start - 1; i >= 0; i--)
            {
                btn[0, i].Text = (pday-- + "");
                btn[0, i].BackColor = ((colorBack == Color.White) ? Color.LightGray : colorBack);
            }

            //Xác định phần ngày của tháng sau đó
            int st = 1;
            while (!(I == 6 && J == 0))
            {
                btn[I, J].Text = (st++ + "");
                btn[I, J].BackColor = ((colorBack == Color.White) ? Color.LightGray : colorBack);
                J++;
                if (J == 7)
                {
                    J = 0;
                    I++;
                }
            }
            return a;
        }

        public void LoadDays()
        {
            int[,] a = update(MONTH, YEAR);
            int check = 0;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (check == 0 && (j != 0 && a[i, j] < a[i, j - 1]))
                    {
                        check++;
                    }
                    if (check == 1)
                    {
                        if (j != 6)
                        {
                            if (a[i, j] > a[i, j + 1])
                                check++;
                        }
                        else
                        {
                            if (i == 5)
                                check++;
                            else if (a[i, j] > a[i + 1, 0])
                                check++;
                        }
                    }
                }
            }
        }

        private string GetStringMonth(int month)
        {
            string[] ds = {"","T1","T2", "T3", "T4", "T5", "T6",
                "T7", "T8","T9","T10","T11","T12"};
            return ds[month];
        }

        #endregion

        #region Events
        private void buttonDate_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            UC_Calendar.ButtonDateMonth = btn;
            ButtonDate.Invoke(this, e);
        }

        public void LoadTodayDate()
        {
            DateTime now = DateTime.Now;
            MONTH = now.Month;
            YEAR = now.Year;
            btnDate.Text = GetStringMonth(MONTH) + "  " + YEAR;
            LoadDays();
        }

        /*private void buttonToday_Click(object sender, EventArgs e)
        {
            ButtonToday.Invoke(this, e);
            LoadTodayDate();
        }*/

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadTodayDate();
        }

        public void LoadPreviousDate()
        {
            MONTH--;
            if (MONTH == 0)
            {
                YEAR--;
                MONTH = 12;
            }
            btnDate.Text = GetStringMonth(MONTH) + "  " + YEAR;
            LoadDays();
        }

        /*private void btnPrevious_Click(object sender, EventArgs e)
        {
            ButtonPrevious.Invoke(this, e);
            LoadPreviousDate();
        }*/
        public void LoadNextDate()
        {
            MONTH++;
            if (MONTH == 13)
            {
                YEAR++;
                MONTH = 1;
            }
            btnDate.Text = GetStringMonth(MONTH) + "  " + YEAR;
            LoadDays();
        }
        /*private void btnNext_Click(object sender, EventArgs e)
        {
            ButtonNext.Invoke(this, e);
            LoadNextDate();
        }*/

        private void UC_Calendar_Resize(object sender, EventArgs e)
        {
            //29/3/2023: Edit by Tai -> Label nằm chính giữa của mỗi ngày
            double xRatio = this.Width * 1.0 / formOriginalSize.Width;
            double yRatio = this.Height * 1.0 / formOriginalSize.Height;

            tableLayoutPanel1.Size = new Size((int)(tableLayoutPanel1.Width * xRatio), (int)(tableLayoutPanel1.Height * yRatio));
            tableLayoutPanel1.Location = new Point((int)(tableLayoutPanel1.Location.X * xRatio), (int)(tableLayoutPanel1.Location.Y * yRatio));
            btnDate.Size = new Size((int)(btnDate.Width * xRatio), (int)(btnDate.Height * yRatio));
            btnDate.Location = new Point((int)(btnDate.Location.X * xRatio), (int)(btnDate.Location.Y * yRatio));

            int offset = tableLayoutPanel1.Location.X;
            lbSunday.Size = new Size((int)(lbSunday.Width * xRatio), (int)(lbSunday.Height * yRatio));
            lbSunday.Location = new Point((int)(lbSunday.Location.X * xRatio), (int)(lbSunday.Location.Y * yRatio));

            lbMonday.Size = new Size((int)(lbMonday.Width * xRatio), (int)(lbMonday.Height * yRatio));
            int posX = offset + btn[0, 1].Location.X + (btn[0, 1].Width - lbMonday.Width) / 2;
            lbMonday.Location = new Point(posX, lbSunday.Location.Y);

            lbTuesday.Size = new Size((int)(lbTuesday.Width * xRatio), (int)(lbTuesday.Height * yRatio));
            posX = offset + btn[0, 2].Location.X + (btn[0, 1].Width - lbTuesday.Width) / 2;
            lbTuesday.Location = new Point(posX, lbSunday.Location.Y);

            lbWednesday.Size = new Size((int)(lbWednesday.Width * xRatio), (int)(lbWednesday.Height * yRatio));
            posX = offset + btn[0, 3].Location.X + (btn[0, 1].Width - lbWednesday.Width) / 2;
            lbWednesday.Location = new Point(posX, lbSunday.Location.Y);

            lbThursday.Size = new Size((int)(lbThursday.Width * xRatio), (int)(lbThursday.Height * yRatio));
            posX = offset + btn[0, 4].Location.X + (btn[0, 1].Width - lbThursday.Width) / 2;
            lbThursday.Location = new Point(posX, lbSunday.Location.Y);

            lbFriday.Size = new Size((int)(lbFriday.Width * xRatio), (int)(lbFriday.Height * yRatio));
            posX = offset + btn[0, 5].Location.X + (btn[0, 1].Width - lbFriday.Width) / 2;
            lbFriday.Location = new Point(posX, lbSunday.Location.Y);

            lbSaturday.Size = new Size((int)(lbSaturday.Width * xRatio), (int)(lbSaturday.Height * yRatio));
            posX = offset + btn[0, 6].Location.X + (btn[0, 1].Width - lbSaturday.Width) / 2;
            lbSaturday.Location = new Point(posX, lbSunday.Location.Y);

            //btnNext.Size = new Size((int)(btnNext.Width * xRatio), (int)(btnNext.Height * yRatio));
            //btnNext.Location = new Point((int)(btnNext.Location.X * xRatio), (int)(btnNext.Location.Y * yRatio));

            //btnPrevious.Size = new Size((int)(btnPrevious.Width * xRatio), (int)(btnPrevious.Height * yRatio));
            //btnPrevious.Location = new Point((int)(btnPrevious.Location.X * xRatio), (int)(btnPrevious.Location.Y * yRatio));

            btnDate.Size = new Size((int)(btnDate.Width * xRatio), (int)(btnDate.Height * yRatio));
            btnDate.Location = new Point((int)(btnDate.Location.X * xRatio), (int)(btnDate.Location.Y * yRatio));

            formOriginalSize = this.Size;
        }
        #endregion
    }
}
