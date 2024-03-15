using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncProblems
{
    public class ReaderWriter
    {
        // Problem
        // * A database is to be shared among several concurrent threads
        // * Some threads want to only read, others may read & write to the database

        // Task
        // Is to avoid race condition, data consistency

        // Solution 
        // When a Thread writing: writer should have exclusive access, lock writing and reading for others
        // When multiple threads are reading: do nothing
    }
}
