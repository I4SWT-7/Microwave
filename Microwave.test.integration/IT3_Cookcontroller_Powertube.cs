using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit;
using NUnit.Framework;

namespace Microwave.test.integration
{
    [TestFixture]
    class IT3_Cookcontroller_Powertube
    {
        private CookController _cookController;
        private Display _display;
        private ITimer _timer;
        private PowerTube _powerTube;
        private IOutput _output;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = Substitute.For<ITimer>();
            _cookController = new CookController(_timer, _display, _powerTube);
        }


        [Test]
        public void Cookcontroller_turnOnPowerTube_iscalled()
        {
            int power = 50;
            int time = 10;
            _cookController.StartCooking(power, time);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("7 %")));
        }

        [Test]
        public void Cookcontroller_turnOffPowerTube_iscalled()
        {
            int power = 50;
            int time = 10;
            _cookController.StartCooking(power, time);
            _cookController.Stop();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("PowerTube turned off")));
        }
    }
}