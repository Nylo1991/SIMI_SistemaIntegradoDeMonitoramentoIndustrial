using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class SensorData
    {
        public int Id { get; set; }
        public double Temperatura    { get; set; }
        public double Corrente { get; set; }
        public DateTime Timestamp { get; set; }
        
    }
}
