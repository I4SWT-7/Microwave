using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Castle.Core.Smtp;
using Microsoft.Win32;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace Microwave.test.integration
{
    [TestFixture]
    public class IT1_Cookcontroller_display
    {
        private IDisplay _display;
        private ITimer _timer;
        private IPowerTube _powertube;
        private IOutput _output;
        private CookController _uut;

        [SetUp]
        public void SetUp()
        {
            _display = new Display(_output);
            _timer = Substitute.For<ITimer>();
            _powertube = Substitute.For<IPowerTube>();
            _output = Substitute.For<IOutput>();
            _uut = new CookController(_timer,_display,_powertube);

        }

        [Test]
        public void StartCooking_ShowTime()
        {
            _uut.StartCooking(50,30);
            _output.Received(1).OutputLine($"Display shows: 50 W");
        }


        //[Test]
        //public void StartCooking_ShowPower()
        //{

        //}


    }
    
}
