﻿using System;
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
        private FakeTimer _fakeTimer;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _fakeTimer = new FakeTimer();
            _display = new Display(_output);
            //_timer = Substitute.For<ITimer>();
            _powertube = Substitute.For<IPowerTube>();
            
            _uut = new CookController(_fakeTimer,_display,_powertube);


        }

        [Test]
        public void StartCooking_ShowTime()
        {
            _uut.StartCooking(50,30);
            _fakeTimer.RaiseEvent();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:30")));
        }



    }
    
}
