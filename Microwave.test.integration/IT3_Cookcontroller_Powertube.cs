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
        private CookController cookController;
        private Display display;
        private ITimer timer;
        private PowerTube powerTube;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            display = new Display(output);
            powerTube = new PowerTube(output);
            timer = Substitute.For<ITimer>();
            cookController = new CookController(timer, display, powerTube);
        }


        [Test]
        public void Cookcontroller_powerOn_iscalled()
        {
            int power = 51;
            int time = 10;
            cookController.StartCooking(power, time);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("7 %")));
        }
    }
}