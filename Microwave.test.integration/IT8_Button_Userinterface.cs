using System;
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

namespace Microwave.test.integration
{
    [TestFixture]
    class IT8_Button_Userinterface
    {
        private IButton _uut_powerButton;
        private IButton _uut_timeButton;
        private IButton _uut_startCancelButton;
        private IUserInterface _userInterface;
        private ILight _light;
        private ICookController _cookController;
        private IDisplay _display;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IOutput _output;
        private IDoor _door;

        [SetUp]
        public void SetUp()
        {
            //Fakes
            _timer = Substitute.For<ITimer>();
            _output = Substitute.For<IOutput>();
            _door = Substitute.For<IDoor>();

            //Reals
            _uut_powerButton = new Button();
            _uut_timeButton = new Button();
            _uut_startCancelButton = new Button();

            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_uut_powerButton, _uut_timeButton, _uut_startCancelButton, _door, _display, _light, _cookController);
        }

        //POWERBUTTON TESTS
        [Test]
        public void PowerButton_Pressed_Once_Display50()
        {
            _uut_powerButton.Press();
            _output.Received(1).OutputLine("Display shows: 50 W");
        }

        [Test]
        public void PowerButton_Pressed_14times_Display700()
        {
            for (int i = 0; i < 14; i++)
            {
                _uut_powerButton.Press();
            }

            _output.Received(1).OutputLine("Display shows: 700 W");
        }

        [Test]
        public void PowerButton_Pressed_15times_Display50()
        {
            for (int i = 0; i < 15; i++)
            {
                _uut_powerButton.Press();
            }

            _output.Received(2).OutputLine("Display shows: 50 W");
        }
        //TIMERBUTTON TESTS
        [Test]
        public void TimerButton_Pressed_Once_Display_1()
        {
            _uut_powerButton.Press();
            _uut_timeButton.Press();

            _output.Received(1).OutputLine("Display shows: 01:00");
        }

        [Test]
        public void TimerButton_Pressed_100_Display_100()
        {
            _uut_powerButton.Press();
            for (int i = 0; i < 100; i++)
            {
                _uut_timeButton.Press();
            }

            _output.Received(1).OutputLine("Display shows: 100:00");
        }
        //START/CANCEL-BUTTON TESTS
        [Test]
        public void StartCancel_Pressed_When_Off_PowerTubeStarts()
        {
            _uut_powerButton.Press();
            _uut_timeButton.Press();
            _uut_startCancelButton.Press();

            _output.Received().OutputLine("PowerTube works with 7 %");
        }

        [Test]
        public void StartCancel_Pressed_When_On_PowerTubeStops()
        {
            _uut_powerButton.Press();
            _uut_timeButton.Press();
            _uut_startCancelButton.Press();
            Thread.Sleep(1000);
            _uut_startCancelButton.Press();

            _output.Received().OutputLine("PowerTube turned off");
        }



    }
}
