using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Interfaces;

namespace MicrowaveOvenClasses.Boundary
{
    public class FakeTimer : ITimer
    {
        public int TimeRemaining { get; private set; }
        public event EventHandler Expired;
        private System.Timers.Timer timer;
        public event EventHandler TimerTick;

        public FakeTimer()
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
