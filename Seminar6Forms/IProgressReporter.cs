using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar6Forms
{
    public interface IProgressReporter
    {
        void Prepared(Order order);
        void Served(Order order);
    }
}
