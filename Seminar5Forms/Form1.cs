using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seminar5Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            int downloadPercent = 0;
            while (downloadPercent < 100)
            {
                Thread.Sleep(100);
                backgroundWorker1.ReportProgress(downloadPercent);
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    backgroundWorker1.ReportProgress(0);
                    return;
                }
                downloadPercent++;
            }
            e.Result = downloadPercent;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = $"Task is {e.ProgressPercentage}% compeleted";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                progressBar1.Value = 0;
                label1.Text = "Task is cancelled";
            }
            else if (e.Error != null)
            {
                label1.Text = $"Task is failed with {e.Error.Message} exception";
            }
            else
            {
                label1.Text = $"Result of the Task is {e.Result.ToString()}";
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
    }
}
