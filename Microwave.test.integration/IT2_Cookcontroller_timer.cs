using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Interfaces;



namespace Microwave.test.integration
{
    [TestFixture]
    public class IT2_Cookcontroller_timer
    {
        private IDisplay _display;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IOutput _output;
        private CookController _uut;
        private IUserInterface _userInterface;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powerTube = Substitute.For<IPowerTube>();
            _timer = new Timer();
            _userInterface = Substitute.For<IUserInterface>();
            _uut = new CookController(_timer, _display, _powerTube);
        }
        [Test]
        public void StartCooking_TurnedOn_TimerStarted(int power, int time)
        {
            var isOn = false;
           _powerTube.When(x => x.TurnOn(power))
               .Do(x => isOn = true);
           _powerTube.TurnOn(power);
           Assert.IsTrue(isOn);
        }


    }   
}
