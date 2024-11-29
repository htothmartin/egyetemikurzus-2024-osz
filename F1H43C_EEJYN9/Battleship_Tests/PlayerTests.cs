using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using F1H43C_EEJYN9.Core;
using F1H43C_EEJYN9.Entities;

namespace Battleship_Tests
{
    [TestFixture]
    public class PlayerTests
    {
        private Player _player;

        [SetUp]
        public void Setup()
        {
            UserManager.Instance.CurrentUser = new User("TestUser");
            _player = new HumanPlayer("TestUser");
        }

        [Test]
        public void ConstructorTest_SetUsername_CorrectUsername()
        {

            //Assert
            Assert.That(_player.Name, Is.EqualTo("TestUser"));

        }

        [Test]
        public void Fire_HitTarget_ReturnsTrue()
        {
            //Arrange
            var target = new Coordinate(0, 0);

            //Act
            bool result = _player.Fire(target);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Fire_HitTarget_ReturnsFalse()
        {
            //Arrange
            var target = new Coordinate(0, 0);

            //Act
            _player.Fire(target);
            bool result = _player.Fire(target);

            //Assert
            Assert.That(result, Is.False);
        }

    }
}
