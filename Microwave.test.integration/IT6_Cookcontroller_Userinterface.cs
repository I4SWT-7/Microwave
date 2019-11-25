using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Microwave.test.integration
{
    [TestFixture]
    class IT6_Cookcontroller_Userinterface
    {
        private ICookController _uut;
        private IUserInterface _userInterface;
        private ILight _light;
        private IDisplay _display;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();

            _userInterface = new UserInterface(_output);
        }

    }
}
