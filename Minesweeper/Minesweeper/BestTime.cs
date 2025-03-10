using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Minesweeper
{
    public partial class BestTime : Form
    {
        public BestTime()
        {
            InitializeComponent();
        }

        void ResetTime()
        {

            //Lấy các chuổi con gán vào các label
            string bgStr = Properties.Resources.BeginBstTime;
            lblBeginBst.Text = bgStr.Substring(0, bgStr.IndexOf(";"));
            lblBeginName.Text = bgStr.Substring(bgStr.IndexOf(";") + 1, bgStr.Length - bgStr.IndexOf(";") - 1);

            string interStr = Properties.Resources.InterBstTime;
            lblInterBst.Text = interStr.Substring(0, interStr.IndexOf(";"));
            lblInterName.Text = interStr.Substring(interStr.IndexOf(";") + 1, interStr.Length - interStr.IndexOf(";") - 1);

            string expStr = Properties.Resources.ExpBstTime;
            lblExpBst.Text = expStr.Substring(0, expStr.IndexOf(";"));
            lblExpName.Text = expStr.Substring(expStr.IndexOf(";") + 1, expStr.Length - expStr.IndexOf(";") - 1);
        }
        private void BestTime_Load(object sender, EventArgs e)
        {
            ResetTime();
        }
        
        //Hàm reset
        void ResetFile(string path)
        {
            FileStream myStream = new FileStream(path, FileMode.Open, FileAccess.Write, System.IO.FileShare.None);
            StreamWriter myWriter = new StreamWriter(myStream);
            string t = "999;Anonymous";
            myWriter.WriteLine(t);
            myWriter.Close();
            myStream.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            ResetFile(@"C:\Users\HP\Downloads\domin\teambugbinh\Minesweeper\Minesweeper\Pictures\BeginBstTime.txt");
            ResetFile(@"C:\Users\HP\Downloads\domin\teambugbinh\Minesweeper\Minesweeper\Pictures\InterBstTime.txt");
            ResetFile(@"C:\Users\HP\Downloads\domin\teambugbinh\Minesweeper\Minesweeper\Pictures\ExpBstTime.txt");
            ResetTime();
        }
    }
}