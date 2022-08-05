using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    class Queen : Bee
    {
        public const float EGGS_PER_SHIFT = 0.45f;
        public const float HONEY_PER_UNASSIGNED_WORKER = 0.5f;


        private float eggs = 0;
        private float unassignedWorkers = 3;

        public override float CostPerShift { get { return 2.15f; } }
        public string StatusReport { get; private set; }


        private Bee[] workers = new Bee[0];

        public Queen() : base("Queen")
        {
            AssignBee("Egg Care");
            AssignBee("Nectar Collector");
            AssignBee("Honey Manufacturer");
        }

        /// <summary>
        /// Expand the workers array by one slot and add a Bee reference.
        /// </summary>
        /// <param name="worker">Worker to add to the workers array.</param>
        private void AddWorker(Bee worker)
        {
            if (unassignedWorkers >= 1)
            {
                unassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = worker;
            }
        }

        public void AssignBee(string jobTitle)
        {
            switch (jobTitle)
            {
                case "Egg Care":
                    AddWorker(new EggCare(this));
                    break;
                case "Nectar Collector":
                    AddWorker(new NectarCollector());
                    break;
                case "Honey Manufacturer":
                    AddWorker(new HoneyManufacturer());
                    break;
            }
            UpdateStatusReport();
        }

        protected override void DoJob()
        {
            eggs += EGGS_PER_SHIFT;
            foreach (Bee worker in workers)
            {
                worker.WorkTheNextShift();
            }
            HoneyVault.ConsumeHoney(HONEY_PER_UNASSIGNED_WORKER * unassignedWorkers);
            UpdateStatusReport();
        }

        private void UpdateStatusReport()
        {
            StatusReport = $"Vault Report:\n{HoneyVault.StatusReport}\n\nEgg Count: {eggs:0.0}\nUnassigned Workers: {unassignedWorkers:0.0}\n" +
                $"{WorkerStatus("Nectar Collector")}\n{WorkerStatus("Honey Manufacturer")}\n{WorkerStatus("Egg Care")}\nTOTAL WORKERS: {workers.Length}";
        }

        private string WorkerStatus(string jobTitle)
        {
            int count = 0;
            foreach (Bee worker in workers)
            {
                if (worker.Job == jobTitle) count++;
            }
            string s = "s";
            if (count == 1) s = "";
            return $"{count} {jobTitle} bee{s}";
        }

        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            }
        }
    }
}
