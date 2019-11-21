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
            _light = Substitute.For<ILight>();
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
        public void test_start_cooking()
        {
            // Lav fakes for button, time og power. Muligvis mocks til (time og power)
            // Brug button til at trigge userinterfaced som kalder cookcontroller, og assert at cookcontroller kalder power og time med de rigtige tal.
            // Der skal laves boundary analysis, der tjekkes for disse tal (timer og power tal)
            _powerButton.Press();

        }

    }
}
