using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Microwave.test.integration
{
    [TestFixture]
    class IT5_Userinterface_Light
    {
        private IUserInterface _uut;
        private ILight _light;
        private ICookController _cookController;
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
            _door = Substitute.For<IDoor>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _start_cancel_button = Substitute.For<IButton>();
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _output = Substitute.For<IOutput>();
            //Fo Realz
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _display = new Display(_output);
            _uut = new UserInterface(_powerButton, _timeButton, _start_cancel_button, _door, _display,
                _light, _cookController);
        }

        [Test]
        public void LightOn_with_StartCancelButton()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _start_cancel_button.Pressed += Raise.Event();
            
            _output.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void LightOn_when_OpenDoor()
        {
            _door.Opened += Raise.Event();

            _output.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void LightOff_when_CloseDoor()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();

            _output.Received(1).OutputLine("Light is turned off");
        }

        [Test]
        public void LightOff_with_StartCancelButton()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _start_cancel_button.Pressed += Raise.Event();
            Thread.Sleep(5000);
            _start_cancel_button.Pressed += Raise.Event();

            _output.Received(1).OutputLine("Light is turned off");
        }

        [Test]
        public void LightOff_when_DoneCooking()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _start_cancel_button.Pressed += Raise.Event();

            _uut.CookingIsDone();

            _output.Received(1).OutputLine("Light is turned off");
        }
    
    }
}
