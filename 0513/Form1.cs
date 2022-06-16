using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static _0513.TaskbarManager;

namespace _0513
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

       // private TaskbarManager trackBar = new TaskbarManager();
        private void button1_Click(object sender, EventArgs e)
        {
            TaskbarManager.SetProgressState(TaskbarProgressBarState.NoProgress);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TaskbarManager.SetProgressState(TaskbarProgressBarState.Normal);
            //TaskbarManager.SetProgressValue(trackBar.Value, trackBar.Maximum);
            TaskbarManager.SetProgressValue(50, 100);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TaskbarManager.SetProgressState(TaskbarProgressBarState.Error);
            TaskbarManager.SetProgressValue(30, 100);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TaskbarManager.SetProgressState(TaskbarProgressBarState.Paused);
            TaskbarManager.SetProgressValue(60, 100);
        }
        Thread th1 = null;
        private void button5_Click(object sender, EventArgs e)
        {
            th1 = new Thread(TaskCreat);
            if (!th1.IsAlive)
            {
                TaskbarManager.SetProgressState(TaskbarProgressBarState.Paused);
                th1.IsBackground = true;
                th1.Start();
            }
        }
        void TaskCreat()
        {
            for (int i = 0; i < 101; i++)
            {
                this.Invoke((Action)(() =>
                {
                    TaskbarManager.SetProgressValue(i, 100);
                    progressBar1.Value = i;

                }));
                Thread.Sleep(100);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
