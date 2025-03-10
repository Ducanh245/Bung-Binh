using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Minesweeper
{
    using System.Drawing;
    public partial class frmMinesweeper : Form
    {
        //Độ rộng và độ cao của control
        const int sizeOfCtrl = 15;

        private int time = 0;

        //Tổng số mìn
        int totalOfMines;

        public bool TimerStart = false;
        public Timer t;

        //Các mức chơi
        public int menuSta;
        public int bMines_Height;
        public int bMines_Width;
        public int mines;
        public State[,] arrayOfMines = null;
        public EventControl[,] arrayOfEventControls = null;
        public PicSmileControl Smile = null;
        public int winFlag = 0;//cờ 0: trạng thái chơi, -1: thua, 1: thắng

        public bool WinnerTesting()
        {
            //Duyệt qua toàn bộ mảng control
            for (int i = 0; i < bMines_Width ; i++)
                for (int j = 0; j < bMines_Height ; j++)
                {
                    //Nếu đang ở trạng thái chơi và cờ từ (0-8)
                    if (this.arrayOfMines[i, j].FLG > -1 && this.arrayOfMines[i, j].STA == 0)
                        return false;
                }
            for (int i = 0; i < bMines_Width; i++)
                for (int j = 0; j < bMines_Height; j++)
                    //Nếu đang ở trạng thái chơi và control co mìn
                    if (this.arrayOfMines[i, j].FLG == -1 && this.arrayOfMines[i, j].STA == 0)
                    {
                        arrayOfEventControls[i, j].
                            OnRightClick();
                    }
            return true;
        }

        void CreateMap(int w, int h, int m)//Tạo một mảng control
        {

            this.SuspendLayout();
            //Size của Form
            this.ClientSize = new System.Drawing.Size(h * sizeOfCtrl + 25, w * sizeOfCtrl + 120);

            //Group Hộp Chứa Số mìn và thời gian chơi
            groupBox2.Size = new System.Drawing.Size(h * sizeOfCtrl + 14, 60);
            groupBox2.Top = 30;
            groupBox2.Left = (this.Width - groupBox2.Width) / 2-3;

            //Picture Chứa Số mìn và thời gian chơi
            pctHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pctHead.Size = new System.Drawing.Size(h * sizeOfCtrl + 4, 40);
            pctHead.Top = 10;
            pctHead.Left = 5;
           
            //Group Hộp chứa các controls
            groupBox1.Size = new System.Drawing.Size(h * sizeOfCtrl + 14, w * sizeOfCtrl + 20);
            groupBox1.Top = groupBox2.Top + groupBox2.Height + 5;
            groupBox1.Left = (this.Width - groupBox1.Width) / 2-3;

            //Hộp chứa các controls
            pctOfMines.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pctOfMines.Top = 10;// pctHead.Top + pctHead.Height + 10;
            pctOfMines.Size = new System.Drawing.Size(h * sizeOfCtrl + 4, w * sizeOfCtrl + 4);
            pctOfMines.Left = 6;// (this.Width - pctOfMines.Width) / 2;


            //Label chứa số quả mìn hiện có
            lblMines.AutoSize = true;
            lblMines.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            lblMines.Font = new System.Drawing.Font(("MS Reference Sans Serif"), 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblMines.Size = new System.Drawing.Size(40, 20);
            lblMines.Top = pctHead.Height / 2-14;
            lblMines.Left = 2;
            if (m >= 100)
            {
                lblMines.Text = m.ToString();
            }
            else
                if (m >= 10)
                {
                    lblMines.Text = "0" + m.ToString();
                }
                else
                    lblMines.Text = "00" + m.ToString();
            lblMines.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            //Label chứa thời gian chơi
            lblTime.AutoSize = true;
            lblTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            lblTime.Font = new System.Drawing.Font(("MS Reference Sans Serif"), 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblTime.Size = new System.Drawing.Size(40, 20);
            lblTime.Top = pctHead.Height / 2 - 14;
            lblTime.Left = (pctHead.Width- lblTime.Width-5);
            lblTime.Text = 0.ToString();
            lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;


            bMines_Height = h;
            bMines_Width= w;
            mines = m;
            winFlag = 0;
            totalOfMines = m;

            //dừng thoi gian lại
            t.Stop();
            time = 0;
            TimerStart = false;


            lblTime.Text = "000";

            arrayOfMines = new State[bMines_Width,bMines_Height];
            arrayOfEventControls = new EventControl[bMines_Width, bMines_Height];

            //Chỉnh vị trí mặt cười
            Smile.Top = (this.pctHead.Height - Smile.Height) / 2-1;
            Smile.Left = (this.pctHead.Width - Smile.Width) / 2-2;



            //Khởi tạo từng phần tử trong mảng            
            for (int i = 0; i < bMines_Width; i++)
                for (int j = 0; j < bMines_Height; j++)
                {
                    arrayOfMines[i, j] = new State();
                }


            //Tạo mảng random với từng phần tử nhận gtrị true hoặc false
            // Biến đếm số mìn đang được tạo
            int count = 0;

            Random rd = new Random();//biến random cho i            
            
            //Count là biến dùng để đếm số mìn được tạo
            //Vòng lặp tạo các Flag cho các control
            while (count < mines)
            {
                int i = rd.Next(bMines_Width- 1);
                int j = rd.Next(bMines_Height- 1);

                while (arrayOfMines[i, j].FLG == -1)//tránh lỗi trùng lập mìn
                {
                    i = rd.Next(bMines_Width - 1);
                    j = rd.Next(bMines_Height - 1);
                }

                arrayOfMines[i, j].FLG = -1;

                //tăng giá trị của các ô xung quanh lên 1; value 
                setSate(i - 1, j - 1, arrayOfMines);
                setSate(i - 1, j, arrayOfMines);
                setSate(i - 1, j + 1, arrayOfMines);
                setSate(i, j - 1, arrayOfMines);
                setSate(i, j + 1, arrayOfMines);
                setSate(i + 1, j - 1, arrayOfMines);
                setSate(i + 1, j, arrayOfMines);
                setSate(i + 1, j + 1, arrayOfMines);
                count++;
            }

            this.pctHead.Controls.Clear();
            this.pctHead.Controls.Add(lblMines);
            this.pctHead.Controls.Add(lblTime);
            this.pctHead.Controls.Add(Smile);

            this.pctOfMines.Controls.Clear();

            //Tạo các controls
            for (int i = 0; i < bMines_Width; i++)
                for (int j = 0; j < bMines_Height; j++)
                {
                    arrayOfEventControls[i, j] = new EventControl(i, j);
                    arrayOfEventControls[i, j].mainform = this;
                    this.pctOfMines.Controls.Add(arrayOfEventControls[i, j]);
                }
        }

        public frmMinesweeper()
        {
            InitializeComponent();
            this.SuspendLayout();

            arrayOfMines = new State[bMines_Width,bMines_Height];
            arrayOfEventControls = new EventControl[bMines_Width, bMines_Height];
            Smile = new PicSmileControl();

            Smile.boxMines = this;
            t = new Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);

        }

        private void frmMinesweeper_Load(object sender, EventArgs e)
        {
            CreateMap(9, 9, 10);
            New();
            menuSta = 1;
            mnuBegginer.CheckState = CheckState.Checked;
        }

        //Hàm In chữ v khi ta chọn một trạng thái trong menu
        private void StateChange(int i)
        {
            //Tất cả đều Chưa được chọn
            mnuBegginer.CheckState = CheckState.Unchecked;
            mnuIntermediate.CheckState = CheckState.Unchecked;
            mnuExpert.CheckState = CheckState.Unchecked;
            mnuCustom.CheckState = CheckState.Unchecked;
            switch (i)
            {
                case 1:
                    {
                        mnuBegginer.CheckState = CheckState.Checked;
                        break;
                    }
                case 2:
                    {
                        mnuIntermediate.CheckState = CheckState.Checked;
                        break;
                    }
                case 3:
                    {
                        mnuExpert.CheckState = CheckState.Checked;
                        break;
                    }
                case 4:
                    {
                        mnuCustom.CheckState = CheckState.Checked;
                        break;
                    }
            }
        }

        void setSate(int i, int j, State[,] array)
        {
            if ((i >= 0 && i < bMines_Width) && (j >= 0 && j < bMines_Height))
                if (array[i, j].FLG > -1)
                    array[i, j].FLG++;
        }

        public void New()
        {
            CreateMap(this.bMines_Width, this.bMines_Height, this.totalOfMines);
        }

        public void timer(bool stop)
        {
            if (stop)
                t.Stop();
            else
            {
                if (!TimerStart)
                {
                    t.Start();
                    TimerStart = true;
                }
            }
        }

        void t_Tick(object sender, EventArgs e)
        {
            time++;
            if (time>=100) 
            {
                lblTime.Text = time.ToString();
            }
            else
                if (time >= 10)
                {
                    lblTime.Text = "0" + time.ToString();
                }
                else
                {
                    lblTime.Text = "00" + time.ToString();
                }

        }

        private void mnuCustom_Click(object sender, EventArgs e)
        {
            //Đánh dấu chọn
            StateChange(4);
            Custom custom = new Custom();
            custom.mainform = this;
            DialogResult r = custom.ShowDialog();

            if (r == DialogResult.OK)
            {
                CreateMap(custom.cusWidth, custom.cusHeight, custom.mines);
            }
            else custom.Close();

        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void mnuBegginer_Click(object sender, EventArgs e)
        {
            menuSta = 1;
            StateChange(1);
            CreateMap(9, 9, 10);
        }

        private void mnuIntermediate_Click(object sender, EventArgs e)
        {
            menuSta = 2;
            StateChange(2);
            CreateMap(16, 16, 40);
        }

        private void mnuExpert_Click(object sender, EventArgs e)
        {
            menuSta = 3;
            StateChange(3);
            CreateMap(24, 30, 99);
        }

        private void mnuBestTime_Click(object sender, EventArgs e)
        {
            BestTime bstDlg = new BestTime();
            bstDlg.Show();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }
    }
}