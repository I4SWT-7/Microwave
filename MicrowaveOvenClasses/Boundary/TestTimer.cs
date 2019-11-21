using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Interfaces;

namespace MicrowaveOvenClasses.Boundary
{
    public class TestTimer : ITimer
    {
        public int TimeRemaining { get; private set; }
        public event EventHandler Expired;
        private System.Timers.Timer timer;
        public event EventHandler TimerTick;

        public TestTimer()
        {
            timer = new System.Timers.Timer();
        }
        public void RaiseEvent()
        {
            TimerTick?.Invoke(
                this,
                EventArgs.Empty);
        }

        public void Start(int time)
        {
            TimeRemaining = time;
            timer.Enabled = true;

        }

        public void Stop()
        {

        }

        public void Expire()
        {

        }
    }
}
