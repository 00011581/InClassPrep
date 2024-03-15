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

namespace Seminar7Forms
{
    public partial class Form1 : Form
    {
        private Semaphore semaphore = new Semaphore(3, 3);
        private List<Order> orders = new List<Order>();

        public Form1()
        {
            InitializeComponent();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            createOrders();
            createPackingThreads();
        }

        private void createOrders()
        {
            for (int i = 1; i <= 15; i++)
            {
                Order order = new Order("#N" + i);
                orders.Add(order);
            }
        }

        private void createPackingThreads()
        {
            foreach (Order order in orders)
            {
                Thread packingThread = new Thread(() => packingProgressReporter(order.Number));
                packingThread.Name = "Thread " + order.Number;
                packingThread.Start();
            }
        }

        private void packingProgressReporter(string orderNumber)
        {
            semaphore.WaitOne();
            Console.WriteLine("Requesting " + Thread.CurrentThread.Name);
            Invoke(
                new MethodInvoker(
                    delegate()
                    {
                        Console.WriteLine("Running " + Thread.CurrentThread.Name);

                        textBox1.AppendText($"Started packing {orderNumber}\n" + Environment.NewLine);
                        Thread.Sleep(1000);
                        textBox1.AppendText($"Finished packing {orderNumber}\n" + Environment.NewLine);
                        Thread.Sleep(1000);
                    }
            ));
            semaphore.Release();
        }
    }
}
