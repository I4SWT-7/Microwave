﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.test.integration
{
    [TestFixture]
    class IT6_Cookcontroller_Userinterface
    {
        private CookController _uut;
        private IUserInterface _userInterface;
        private ILight _light;
        private IDisplay _display;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IOutput _output;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _start_cancel_button;
        private IDoor _door;

        [SetUp]
        public void SetUp()
        {
            //Substitutes
            _output = Substitute.For<IOutput>();
            _door = Substitute.For<IDoor>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _start_cancel_button = Substitute.For<IButton>();

            //Real
            _light = new Light(_output);
            _display = new Display(_output);
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _uut = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _start_cancel_button, _door, _display,
                _light, _uut);
            _uut.UI = _userInterface;
        }


        [Test]
        public void CookingIsDone_Received_inUserInterface()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _start_cancel_button.Pressed += Raise.Event();

            Thread.Sleep(6000);

            _output.Received(1).OutputLine("Display cleared");
        }

    }
}
