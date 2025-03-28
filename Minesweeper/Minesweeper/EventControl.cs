using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace Minesweeper
{
    public partial class EventControl : UserControl
    {
        const int sizeOfCtrl = 15;
        public frmMinesweeper mainform = null;
        int Ctrl_X;//Tọa độ X của các control ô vuông
        int Ctrl_Y;//Tọa độ Y của các control ô vuông
        public bool onMouseDownCtrl = false;
     
        public EventControl(int x, int y)
        {
            InitializeComponent();
            this.Top = x * 15;
            this.Left = y * 15;
            this.Size = new Size(sizeOfCtrl, sizeOfCtrl);
            Ctrl_X = x;
            Ctrl_Y = y;
        }

        //Hàm Loang
        //loang khi ta click vào control có flag=0
        //ta sẽ loang cho dến khi gặp control co flag>0
        void Loang(int i, int j, State[,] array)
        {
            if ((i >= 0 && i < mainform.bMines_Width) && (j >= 0 && j < mainform.bMines_Height))
            {
                //Neu nhu control do dang dong va co flag > -1
                //thi ta se mo control do
                if (array[i, j].FLG > -1 && array[i, j].STA == 0)
                {
                    array[i, j].STA = 1;//Mo control
                    mainform.arrayOfEventControls[i, j].Refresh();

                    //Neu nhu flag=0 thi ta se goi de quy ham loang voi 8 o ben canh
                    if (array[i, j].FLG == 0)
                    {
                        Loang(i - 1, j - 1, array);
                        Loang(i - 1, j, array);
                        Loang(i - 1, j + 1, array);
                        Loang(i, j - 1, array);
                        Loang(i, j + 1, array);
                        Loang(i + 1, j - 1, array);
                        Loang(i + 1, j, array);
                        Loang(i + 1, j + 1, array);
                    }
                }
            }
        }

        //Hàm vẽ các control
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if (onMouseDownCtrl == true)
                g.DrawImage(Properties.Resources._0, 0, 0, sizeOfCtrl, sizeOfCtrl);
            else
            {
                //Xet cac trang thai
                switch (mainform.arrayOfMines[Ctrl_X, Ctrl_Y].STA)
                {
                    case -1://Trạng thái đặt cờ
                        {
                            //Nếu thua thì ở những ô đặt cờ sai sẽ bị gạch chéo
                            if (mainform.winFlag == -1 && mainform.arrayOfMines[Ctrl_X, Ctrl_Y].FLG != -1)
                            {
                          
                                g.DrawImage(Properties.Resources.WrongFlag, 0, 0, sizeOfCtrl, sizeOfCtrl); 
                            }
                            else //Chơi tiếp và hình cờ được vẽ ở controls vừa chọn
                                g.DrawImage(Properties.Resources.Flag, 0, 0, sizeOfCtrl, sizeOfCtrl);

                            break;

                        }
                    case 0://Trạng thái đóng
                        {
                            //Nếu như nhấp chuột trúng mìn thì sẽ thua và hiện tất cả những trái mìn mà chưa được phá
                            if (mainform.winFlag == -1 && mainform.arrayOfMines[Ctrl_X, Ctrl_Y].FLG == -1)
                            {
                                
                                g.DrawImage(Properties.Resources.mine, 0, 0, sizeOfCtrl, sizeOfCtrl);
                            }
                            else
                                g.DrawImage(Properties.Resources.Node, 0, 0, sizeOfCtrl, sizeOfCtrl);
                            break;
                        }
                      case 1://Trạng thái cờ mở (đã mở rồi)
                        {
                            switch (mainform.arrayOfMines[Ctrl_X, Ctrl_Y].FLG)
                            {
                                case -1://Chọn đúng ô mìn
                                    {
                                        try
                                        {
                                            SoundPlayer player = new SoundPlayer(Application.StartupPath + @"/audio/false.wav");
                                            player.Play();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("loi am thanh " + ex.Message);
                                        }
                                        g.DrawImage(Properties.Resources.Eploded, 0, 0, sizeOfCtrl, sizeOfCtrl);
                                        break;
                                    }
                                case 0://Chọn đúng ô cạnh đó không có gì
                                    {
                                        g.DrawImage(Properties.Resources._0, 0, 0, sizeOfCtrl, sizeOfCtrl);
                                        break;
                                    }
                                case 1://Chọn đúng ô cạnh đó có 1 quả mìn
                                    {
                                        g.DrawImage(Properties.Resources._1, 0, 0, sizeOfCtrl, sizeOfCtrl);
                                        break;
                                    }
                                case 2://Chọn đúng ô cạnh đó có 2 quả mìn
                                    {
                                        g.DrawImage(Properties.Resources._2, 0, 0, sizeOfCtrl, sizeOfCtrl);
                                        break;
                                    }
                                case 3://Chọn đúng ô cạnh đó có 3 quả mìn
                                    {
                                        g.DrawImage(Properties.Resources._3, 0, 0, sizeOfCtrl, sizeOfCtrl);
                                        break;
                                    }
                                case 4://Chọn đúng ô cạnh đó có 4 quả mìn
                                    {
                                        g.DrawImage(Properties.Resources._4, 0, 0, sizeOfCtrl, sizeOfCtrl);
                                        break;
                                    }
                                case 5://Chọn đúng ô cạnh đó có 5 quả mìn
                                    {
                                        g.DrawImage(Properties.Resources._5, 0, 0, sizeOfCtrl, sizeOfCtrl);
                                        break;
                                    }
                                case 6://Chọn đúng ô cạnh đó có 6 quả mìn
                                    {
                                        g.DrawImage(Properties.Resources._6, 0, 0, sizeOfCtrl, sizeOfCtrl);
                                        break;
                                    }
                                case 7://Chọn đúng ô cạnh đó có 7 quả mìn
                                    {
                                        g.DrawImage(Properties.Resources._7, 0, 0, sizeOfCtrl, sizeOfCtrl);
                                        break;
                                    }
                                case 8://Chọn đúng ô cạnh đó có 8 quả mìn
                                    {
                                        g.DrawImage(Properties.Resources._8, 0, 0, sizeOfCtrl, sizeOfCtrl);
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }

        }

        public void OnRightClick()
        {
            //Nếu bỏ ô đã đánh dấu cờ
            //thì tăng số mìn lên và chuyển về trạng thái đóng
            if (mainform.arrayOfMines[Ctrl_X, Ctrl_Y].STA == -1)
            {
                mainform.arrayOfMines[Ctrl_X, Ctrl_Y].STA = 0;//Trạng thái mở
                mainform.mines++;
                if (mainform.mines >= 100)
                {
                    mainform.lblMines.Text = mainform.mines.ToString();//Cập nhật lại số mìn
                }
                else
                    if (mainform.mines >= 10)
                    {
                        mainform.lblMines.Text = "0" + mainform.mines.ToString();
                    }
                    else
                    {
                        mainform.lblMines.Text = "00" + mainform.mines.ToString();
                    }
            }
            else//Nếu ta đánh dấu cờ vào một ô nào đó thì ta sẽ giảm số mìn đi
            {
                if (mainform.arrayOfMines[Ctrl_X, Ctrl_Y].STA == 0)
                {
                    mainform.arrayOfMines[Ctrl_X, Ctrl_Y].STA = -1;//Trạng thái chọn cờ
                    mainform.mines--;
                    if (mainform.mines >= 100)
                    {
                        mainform.lblMines.Text = mainform.mines.ToString();//Cập nhật lại số mìn
                    }
                    else
                        if (mainform.mines >= 10)
                        {
                            mainform.lblMines.Text = "0" + mainform.mines.ToString();
                        }
                        else
                        {
                            mainform.lblMines.Text = "00" + mainform.mines.ToString();
                        }

                }
            }
            this.Refresh();
        }

        public void OnClickCtrl()
        {
            //Nếu control đang đóng và ta đang ở trạng thái chơi
            if(mainform.arrayOfMines[Ctrl_X,Ctrl_Y].STA==0&&mainform.winFlag==0)
            {
                if(mainform.arrayOfMines[Ctrl_X,Ctrl_Y].FLG==-1)//Có mìn
                {
                    mainform.arrayOfMines[Ctrl_X,Ctrl_Y].STA=1;//Mở control
                    mainform.arrayOfEventControls[Ctrl_X,Ctrl_Y].Refresh();
                    mainform.winFlag=-1;//Trạng thái thua

                    mainform.timer(true);

                    mainform.Smile.state=-1;
                    mainform.Smile.Refresh();

                    //Vẽ ra toàn bộ hình ảnh khi thua
                    //và gọi hàm onMouseClick cho những control có mìn
                    for (int i = 0; i < mainform.bMines_Width; i++)
                        for (int j = 0; j < mainform.bMines_Height; j++)
                            //Nếu control đang ở trạng thái đóng và có mìn
                            //hoặc ở trạng thái đặt cờ nhưng không có mìn
                            if ((mainform.arrayOfMines[i, j].STA == 0 && mainform.arrayOfMines[i, j].FLG == -1)
                                || (mainform.arrayOfMines[i, j].STA == -1 && mainform.arrayOfMines[i, j].FLG !=-1))
                                mainform.arrayOfEventControls[i, j].Refresh();   

                }
                else
                { 
                    //Nếu như ô không có gì thì ta gọi hàm loang   
                    if (mainform.arrayOfMines[Ctrl_X, Ctrl_Y].FLG == 0)
                    {   
                        Loang(Ctrl_X, Ctrl_Y, mainform.arrayOfMines);
                    }
                    else//Đưa về trạng thái mở và Refresh lại
                    {
                        mainform.arrayOfMines[Ctrl_X, Ctrl_Y].STA = 1;
                        mainform.arrayOfEventControls[Ctrl_X, Ctrl_Y].Refresh();
                    }
                } 

                //Nếu Kiểm tra đúng
                if (mainform.WinnerTesting())
                {
                    mainform.winFlag = 1;//Trạng thái thắng
                    mainform.timer(true);
                    mainform.Smile.state = 2;
                    mainform.Smile.Refresh();
                    string str = "";
                    string path = "";
                    try
                    {
                        SoundPlayer player = new SoundPlayer(Application.StartupPath + @"/audio/false.wav");
                        player.Play();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("loi am thanh " + ex.Message);
                    }

                    //Xem kết quả chơi có tốt hơn những lần chơi
                    //trước không nếu tốt hơn thì lưu KQ lại
                    switch (this.mainform.menuSta)
                    {
                        case 1:
                            {
                                path = @"C:\Users\HP\Downloads\domin\teambugbinh\Minesweeper\Minesweeper\Pictures\BeginBstTime.txt";
                                str = Properties.Resources.BeginBstTime.Substring(0, Properties.Resources.BeginBstTime.IndexOf(";"));
                                break;
                            }
                        case 2:
                            {
                                path = @"C:\Users\HP\Downloads\domin\teambugbinh\Minesweeper\Minesweeper\Pictures\InterBstTime.txt";
                                str = Properties.Resources.InterBstTime.Substring(0, Properties.Resources.InterBstTime.IndexOf(";"));
                                break;
                            }
                        case 3:
                            {
                                path = @"C:\Users\HP\Downloads\domin\teambugbinh\Minesweeper\Minesweeper\Pictures\ExpBstTime.txt";
                                str = Properties.Resources.ExpBstTime.Substring(0, Properties.Resources.ExpBstTime.IndexOf(";"));
                                break;
                            }
                    }

                    if (Convert.ToInt32(this.mainform.lblTime.Text) < Convert.ToInt32(str))
                    {
                        NameArchive ar = new NameArchive();
                        ar.state = this.mainform.menuSta;
                        ar.ShowDialog();
                        FileStream myStream = new FileStream(path, FileMode.Open, FileAccess.Write, System.IO.FileShare.None);
                        StreamWriter myWriter = new StreamWriter(myStream);
                        string t = this.mainform.lblTime.Text + ";" + ar.name;
                        myWriter.WriteLine(t);
                        myWriter.Close();
                        myStream.Close();
                    }
                }
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            //Nếu như đang chơi
            if (mainform.winFlag == 0)
            {
                base.OnMouseClick(e);

                //click chuột phải
                if (e.Button == MouseButtons.Right)
                {
                    mainform.timer(false);
                    OnRightClick();
                }
            }

        }

        //Khi sự kiện nhấn xảy ra thì control trở về trạng thái mở
        public void onDownCtrl()
        {
            onMouseDownCtrl = true;
            this.Refresh();
            mainform.Smile.state = 1;
            mainform.Smile.Refresh();
        }

        //Khi sự kiện buông xảy ra thì control trở về trạng thái mở
        public void onUpCtrl()
        {
            onMouseDownCtrl = false;
            this.Refresh();
            mainform.Smile.state = 0;
            mainform.Smile.Refresh();
        }

        //Sự kiện khi ấn nút giữa của mouse
        public void onMiddleDown(int i, int j)
        {
            if ((i >= 0 && i < mainform.bMines_Width) && (j >= 0 & j < mainform.bMines_Height))
                if (mainform.arrayOfMines[i, j].STA == 0)
                    mainform.arrayOfEventControls[i, j].onDownCtrl();
        }

        //Sự kiện khi thả nút giữa của mouse
        public void onMiddleUp(int i, int j, bool flg)
        {
            if ((i >= 0 && i < mainform.bMines_Width) && (j >= 0 & j < mainform.bMines_Height))
            {
                if (flg == true && mainform.arrayOfMines[i, j].STA == 0)
                    mainform.arrayOfEventControls[i, j].OnClickCtrl();
                else
                    if (mainform.arrayOfMines[i, j].STA == 0)
                        mainform.arrayOfEventControls[i, j].onUpCtrl();
            }
        }

        //Hàm đếm số quả mìn xung quanh ô[i,j]
        int FlagCounting(int i, int j, State[,] array)
        {
            int f = 0;
            for (int x = i - 1; x <= i + 1; x++)
                for (int y = j - 1; y <= j + 1; y++)
                {
                    if ((x >= 0 && x < mainform.bMines_Width) && (y >= 0 && y < mainform.bMines_Height))
                        if (mainform.arrayOfMines[x, y].STA == -1)//Nếu có mìn thì tăng lên 1
                            f++;
                }
            return f;
        }

        //Ấn chuột
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (mainform.winFlag == 0)//Đang chơi
            {
                //Nếu chuột trái được ấn
                if (e.Button == MouseButtons.Left)
                {
                    //Trạng thái control đóng thì ta thực hiện sự kiện ấn chuột
                    if (mainform.arrayOfMines[Ctrl_X, Ctrl_Y].STA == 0)
                        onDownCtrl();
                }
                else
                    //Trạng thái chuột giữa được ấn
                    if (e.Button == MouseButtons.Middle)
                    {
                        int i = Ctrl_X;
                        int j = Ctrl_Y;

                        //Nếu như control đó đang đóng thì bị lún xuống
                        if (mainform.arrayOfMines[i, j].STA == 0)
                            onDownCtrl();
                        //Ta thực hiện lún với các control bên cạnh
                        onMiddleDown(i - 1, j - 1);
                        onMiddleDown(i - 1, j);
                        onMiddleDown(i - 1, j + 1);
                        onMiddleDown(i, j - 1);
                        onMiddleDown(i, j + 1);
                        onMiddleDown(i + 1, j - 1);
                        onMiddleDown(i + 1, j);
                        onMiddleDown(i + 1, j + 1);
                    }
            }

        }

        //Buông chuột
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (mainform.winFlag == 0)//Trạng thái chơi
            {
                if (e.Button == MouseButtons.Left)
                {
                    mainform.timer(false);
                    onUpCtrl();
                    OnClickCtrl();
                }
                else
                    //Sự kiện mouse giữa
                    if (e.Button == MouseButtons.Middle)
                    {
                        mainform.timer(false);
                        int i = Ctrl_X;
                        int j = Ctrl_Y;
                        onMiddleUp(i - 1, j - 1, false);
                        onMiddleUp(i - 1, j, false);
                        onMiddleUp(i - 1, j + 1, false);
                        onMiddleUp(i, j - 1, false);
                        onMiddleUp(i, j + 1, false);
                        onMiddleUp(i + 1, j - 1, false);
                        onMiddleUp(i + 1, j, false);
                        onMiddleUp(i + 1, j + 1, false);
                        switch (mainform.arrayOfMines[Ctrl_X, Ctrl_Y].STA)
                        {
                            case 1://Trạng thái control mở
                                {
                                    if (FlagCounting(Ctrl_X, Ctrl_Y, mainform.arrayOfMines) == mainform.arrayOfMines[i, j].FLG)
                                    {
                                        onMiddleUp(i - 1, j - 1, true);
                                        onMiddleUp(i - 1, j, true);
                                        onMiddleUp(i - 1, j + 1, true);
                                        onMiddleUp(i, j - 1, true);
                                        onMiddleUp(i, j + 1, true);
                                        onMiddleUp(i + 1, j - 1, true);
                                        onMiddleUp(i + 1, j, true);
                                        onMiddleUp(i + 1, j + 1, true);
                                    }
                                    break;
                                }
                            case 0://Trạng thái control đóng
                                {
                                    onUpCtrl();
                                    break;
                                }
                        }
                    }
            }
        }

        private void EventControl_Load(object sender, EventArgs e)
        {

        }
    }
}
