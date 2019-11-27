using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.test.integration
{
    [TestFixture]
    class IT9_Door_Output
    {
        private ICookController _cookController;
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
            _output = Substitute.For<Output>();

            _door = new Door();
            _start_cancel_button = new Button();
            _timeButton = new Button();
            _powerButton = new Button();
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _display = new Display(_output);
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _start_cancel_button, _door, _display, _light, _cookController);
        }
        [Test]
        public void test_open_door_turns_on_light()
        {
            _door.Open();
           // _output.Received(1).OutputLine("Light is turned on");
           _output.Received(1).OutputLine("Light turned on");
        }
        [Test]
        public void test_closed_door_turns_off_light()
        {
            _door.Close();
            _output.Received(1).OutputLine("Light is turned off");
        }

    }

}
