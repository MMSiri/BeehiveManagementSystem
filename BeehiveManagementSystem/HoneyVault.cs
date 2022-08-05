using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    static class HoneyVault
    {
        public const float NECTAR_CONVERSION_RATIO = .19f;
        public const float LOW_LEVEL_WARNING = 10f;
        private static float honey = 25f;
        private static float nectar = 100f;

        public static string StatusReport
        {
            get
            {
                string status = $"Honey left: {honey:0.0} \nNectar left: {nectar:0.0}";


                string lowLevelWarnings = "";
                if (honey < LOW_LEVEL_WARNING) lowLevelWarnings += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
                if (nectar < LOW_LEVEL_WARNING) lowLevelWarnings += "\nLOW NECTAR - ADD A NECTAR COLLECTOR";

                return status + lowLevelWarnings;
            }
        }

        public static void ConvertNectarToHoney(float amount)
        {
            float nectarToConvert = amount;
            if (nectarToConvert > nectar) nectarToConvert = nectar;
            else nectar -= nectarToConvert;
            honey += nectarToConvert * NECTAR_CONVERSION_RATIO;
        }

        public static bool ConsumeHoney(float amount)
        {
            float honeyToConsume = amount;
            if (honey >= honeyToConsume)
            {
                honey -= honeyToConsume;
                return true;
            }
            return false;
        }

        public static void CollectNectar(float amount)
        {
            if (amount > 0f) nectar += amount;
        }
    }
}
