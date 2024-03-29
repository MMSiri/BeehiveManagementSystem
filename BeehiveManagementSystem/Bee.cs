﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    abstract class Bee : IWorker
    {
        public Bee(string job)
        {
            Job = job;
        }

        public string Job { get; private set; }

        public abstract float CostPerShift { get; }


        public void WorkTheNextShift()
       {
            if (HoneyVault.ConsumeHoney(CostPerShift))
            {
                DoJob();
            }

        }

        protected abstract void DoJob();

    }
}
