using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
    class IT7_Door_Userinterface
    {
        private IOutput _output;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IDisplay _display;
        private ICookController _cookController;
        private ILight _light;
        private IUserInterface _userInterface;
        private IDoor _uut;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        [SetUp]
        public void SetUp()
        {
            // Substitudes
            _output = Substitute.For<IOutput>();
            _timer = Substitute.For<ITimer>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            // Real classes
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _uut = new Door();
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton,
                _startCancelButton, _uut, _display, _light, _cookController);
        }

        [Test]
        public void Door_Opens_Light_Turns_On()
        {
            _uut.Open();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void DoorIsOpen_Closes_Light_Turns_Off()
        {
            _uut.Open();
            _uut.Close();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }


    }
}
