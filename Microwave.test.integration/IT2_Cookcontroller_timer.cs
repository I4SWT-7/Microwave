using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Interfaces;
using Timer = MicrowaveOvenClasses.Boundary.Timer;


namespace Microwave.test.integration
{
    [TestFixture]
    public class IT2_Cookcontroller_timer
    {
        public IDisplay _display;
        public ITimer _timer;
        public IPowerTube _powerTube;
        public IOutput _output;
        public CookController _uut;
        public IUserInterface _userInterface;


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
        public void StartCooking_OnTimeExpired_PowerTube_TurnOff()
        {
            _uut.StartCooking(100,1);
            Thread.Sleep(1500);
            _powerTube.Received(1).TurnOff();
        }

        [Test]
        public void StartCooking_Check_TimeRemaining()
        {
            _uut.StartCooking(50,3);
            Thread.Sleep(2500);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(1));
        }

        [Test]
        public void StartCooking_OnTimeExpired_Not_CookingIsDone_NotCalled()
        {
            _uut.StartCooking(100,1);
            Thread.Sleep(900);
            _userInterface.DidNotReceive().CookingIsDone();
        }

        [Test]
        public void StartCooking_OnTimeExpire_Not_CookingIsDone_NotCalled()
        {
            _uut.StartCooking(50, 1);
            Thread.Sleep(900);
            _userInterface.DidNotReceive().CookingIsDone();
        }

        [Test]
        public void OnTimeTick()
        {
            _uut.StartCooking(50, 60);
            Thread.Sleep(1500);
            _display.Received(1).ShowTime(0,59);
        }
        [Test]
        public void OnStop()
        {
            _uut.StartCooking(50, 60);
            _uut.Stop();
            Thread.Sleep(3000);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(60));
        }
      
    }   
}
