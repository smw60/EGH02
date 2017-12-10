using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGH01DB.Primitives
{
    public class SunRadiation
    {
        public string period { get; private set; }
        public int from_rad { get; private set; }
        public int to_rad { get; private set; }
        public float average_rad { get; private set; }

        public SunRadiation()
        {
            this.period = "";
            this.from_rad = 0;
            this.to_rad = 0;
            this.average_rad = 0.0f;
        }

        public SunRadiation(string period, int from_rad, int to_rad, float average_rad)
        {
            this.period = period;
            this.from_rad = from_rad;
            this.to_rad = to_rad;
            this.average_rad = average_rad;
        }
    }
}
