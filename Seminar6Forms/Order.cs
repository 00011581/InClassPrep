using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seminar6Forms
{
    public class Order
    {
        public int Number { get; set; }
        public bool isPrepared { get; set; } = false;
        public bool isServed { get; set; } = false;
        public IProgressReporter progressReporter { get; set; }

        public Order(int number, IProgressReporter progressReporter)
        {
            this.Number = number;
            this.progressReporter = progressReporter;
            Thread thread = new Thread(createOrder);
            thread.Start();
        }

        public void createOrder()
        {
            Thread.Sleep(5000);
            isPrepared = true;
            progressReporter.Prepared(this);
            //MessageBox.Show("Order is ready");
            
            Thread.Sleep(5000);
            isServed = true;
            progressReporter.Served(this);
            //MessageBox.Show("Order is served");
        }
    }
}
