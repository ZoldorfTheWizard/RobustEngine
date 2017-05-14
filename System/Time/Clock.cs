using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobustEngine.System.Time
{
    public class Clock
    {

        public DateTime StartTime;
        public DateTime StopTime;

        public Clock()
        {
                     
        }

        public void Reset()
        {
            StartTime = new DateTime();
            StopTime  = new DateTime();  
        }

        public void Start()
        {
            StartTime = DateTime.Now;
        }

        public void Stop()
        {
            StopTime = DateTime.Now;
        }

        public TimeSpan GetTime()
        {
            return StartTime.Subtract(StopTime); ;
        }

        public TimeSpan GetElapsed()
        {
            return -StartTime.Subtract(DateTime.Now);
        
        }

    }
}
