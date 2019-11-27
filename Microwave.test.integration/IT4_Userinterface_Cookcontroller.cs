using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.test.integration
{
    [TestFixture]
    class IT4_Userinterface_Cookcontroller
    {
        private IDisplay _display;
        private ICookController _cookController;
        private ITimer _timer;
        private IPowerTube _powertube;
        private IOutput _output;
        private ILight _light;
        private IDoor _door;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _start_cancel_button;
        private UserInterface _uut_IuserInterface;

        [SetUp]
        public void SetUp()
        {
            //Subs
            _output = Substitute.For<IOutput>();
            _timer = Substitute.For<ITimer>();
            _door = Substitute.For<IDoor>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _start_cancel_button = Substitute.For<IButton>();

            //Real
            _light = new Light(_output);
            _powertube = new PowerTube(_output);
            _display = new Display(_output);
            _cookController = new CookController(_timer, _display, _powertube);

            //uut
            _uut_IuserInterface = new UserInterface(_powerButton, _timeButton, _start_cancel_button, _door, _display,
                _light, _cookController);

        }

        [Test]
        public void test_power_gets_set_by_PowerButton()
        {
            _powerButton.Pressed += Raise.Event();
            _output.Received(1).OutputLine("Display shows: 50 W");
        }

        [Test]
        public void test_time_gets_set_by_TimeButton()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _output.Received(1).OutputLine("Display shows: 01:00");
        }

        [Test]
        public void test_start_cooking_by_startbutton()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _start_cancel_button.Pressed += Raise.Event();

            _output.Received(1).OutputLine("PowerTube works with 7 %");
        }

        [Test]
        public void test_door_closes_cancels_cooking()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _start_cancel_button.Pressed += Raise.Event();
            _door.Opened += Raise.Event();
            _output.Received().OutputLine("PowerTube turned off");
        }

        [Test]
        public void test_that_open_door_interrupts_start_cooking()
        {
            _door.Opened += Raise.Event();
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _start_cancel_button.Pressed += Raise.Event();
            _output.Received(1).OutputLine("Light is turned on");
            _output.DidNotReceive().OutputLine("PowerTube works with 7 %");
        }
    }
}
