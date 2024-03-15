using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seminar6Forms
{
    public partial class Form1 : Form, IProgressReporter
    {
        private int numOfOrders = 1;

        public Form1()
        {
            InitializeComponent();
            List<Order> orders = new List<Order>();
        }

        public void Prepared(Order order)
        {
            string orderNumber = "Order #" + order.Number;

            Invoke(
                new MethodInvoker(
                    delegate ()
                    {
                        foreach (ListViewItem item in listView1.Items)
                        {
                            if (item.Text == orderNumber)
                            {
                                listView1.Items.Remove(item);
                                listView2.Items.Add(orderNumber);
                            }
                        }
                    }
                )
            );
        }

        public void Served(Order order)
        {
            string orderNumber = "Order #" + order.Number;

            Invoke(
                new MethodInvoker(
                    delegate ()
                    {
                        foreach (ListViewItem item in listView2.Items)
                        {
                            if (item.Text == orderNumber)
                            {
                                listView2.Items.Remove(item);
                            }
                        }
                    }
                )
            );
        }

        private void orderBtn_Click(object sender, EventArgs e)
        {
            Order order = new Order(numOfOrders, this);
            numOfOrders++;
            listView1.Items.Add("Order #" + numOfOrders);
        }
    }
}
