using F1H43C_EEJYN9.Core;
using F1H43C_EEJYN9.Entities;
using F1H43C_EEJYN9.Exceptions;

namespace Battleship_Tests
{
    [TestFixture]
    public class BoardTests
    {
        private Board _board;

        [SetUp]
        public void Setup()
        {
            UserManager.Instance.CurrentUser = new User("TestUser");
            _board = new Board();
        }

        [Test]
        public void MoveSelection_UpArrow_StayInTheSamePositionDueToTopBorder()
        {

            //Arrange
            _board.SelectedCell = new Coordinate(0, 0);

            //Act
            _board.MoveSelection(ConsoleKey.UpArrow);

            //Assert
            Assert.That(_board.SelectedCell.X, Is.EqualTo(0), "Az X koordináta nem megfelelő balra lépés esetén.");
            Assert.That(_board.SelectedCell.Y, Is.EqualTo(0), "Az Y koordináta nem megfelelő balra lépés esetén.");
        }

        [Test]
        public void MoveSelection_DownArrow_ValidMove()
        {

            //Arrange
            _board.SelectedCell = new Coordinate(0, 0);

            //Act
            _board.MoveSelection(ConsoleKey.DownArrow);

            //Assert
            Assert.That(_board.SelectedCell.X, Is.EqualTo(1), "Az X koordináta nem megfelelő jobbra lépés esetén.");
            Assert.That(_board.SelectedCell.Y, Is.EqualTo(0), "Az Y koordináta nem megfelelő jobbra lépés esetén.");
        }

        [Test]
        public void MoveSelection_LeftArrow_StayInTheSamePositionDueToLeftBorder()
        {

            //Arrange
            _board.SelectedCell = new Coordinate(0, 0);

            //Act
            _board.MoveSelection(ConsoleKey.UpArrow);

            //Assert
            Assert.That(_board.SelectedCell.X, Is.EqualTo(0), "Az X koordináta nem megfelelő balra lépés esetén.");
            Assert.That(_board.SelectedCell.Y, Is.EqualTo(0), "Az Y koordináta nem megfelelő balra lépés esetén.");
        }

        [Test]
        public void MoveSelection_RigthArrow_ValidMove()
        {

            //Arrange
            _board.SelectedCell = new Coordinate(0, 0);

            //Act
            _board.MoveSelection(ConsoleKey.RightArrow);

            //Assert
            Assert.That(_board.SelectedCell.X, Is.EqualTo(0), "Az X koordináta nem megfelelő jobbra lépés esetén.");
            Assert.That(_board.SelectedCell.Y, Is.EqualTo(1), "Az Y koordináta nem megfelelő jobbra lépés esetén.");
        }


        [Test]
        public void MoveSelection_DownArrow_StayInTheSamePositionDueToBottomBorder()
        {

            //Arrange
            _board.SelectedCell = new Coordinate(7, 0);

            //Act
            _board.MoveSelection(ConsoleKey.DownArrow);

            //Assert
            Assert.That(_board.SelectedCell.X, Is.EqualTo(7), "Az X koordináta nem megfelelő lefele lépés esetén.");
            Assert.That(_board.SelectedCell.Y, Is.EqualTo(0), "Az Y koordináta nem megfelelő lefele lépés esetén.");
        }

        [Test]
        public void MoveSelection_UpArrow_ValidMove()
        {

            //Arrange
            _board.SelectedCell = new Coordinate(7, 0);

            //Act
            _board.MoveSelection(ConsoleKey.UpArrow);

            //Assert
            Assert.That(_board.SelectedCell.X, Is.EqualTo(6), "Az X koordináta nem megfelelő felfele lépés esetén.");
            Assert.That(_board.SelectedCell.Y, Is.EqualTo(0), "Az Y koordináta nem megfelelő felfele lépés esetén.");
        }

        [Test]
        public void MoveSelection_LeftArrow_ValidMove()
        {

            //Arrange
            _board.SelectedCell = new Coordinate(7, 7);

            //Act
            _board.MoveSelection(ConsoleKey.LeftArrow);

            //Assert
            Assert.That(_board.SelectedCell.X, Is.EqualTo(7), "Az X koordináta nem megfelelő balra lépés esetén.");
            Assert.That(_board.SelectedCell.Y, Is.EqualTo(6), "Az Y koordináta nem megfelelő balra lépés esetén.");
        }

        [Test]
        public void MoveSelection_RigthArrow_StayInTheSamePositionDueToRigthBorder()
        {

            //Arrange
            _board.SelectedCell = new Coordinate(7, 7);

            //Act
            _board.MoveSelection(ConsoleKey.RightArrow);

            //Assert
            Assert.That(_board.SelectedCell.X, Is.EqualTo(7), "Az X koordináta nem megfelelő jobbra lépés esetén.");
            Assert.That(_board.SelectedCell.Y, Is.EqualTo(7), "Az Y koordináta nem megfelelő jobbra lépés esetén.");
        }

        [Test]
        public void CheckShipPositionIsValid_ValidPosition_ReturnsTrue()
        {
            //Arrange
            var ship = new Ship("teszt", 3);
            _board.SelectedCell = new Coordinate(0, 0);

            //Act
            bool result = _board.CheckShipPositionIsValid(ship);

            //Assert
            Assert.That(result, Is.True, "A hajó pozíciója érvényes, de a metódus hamisat adott vissza."); 
        }

        [Test]
        public void CheckShipPositionIsValid_TooBigShip_ReturnsFalse()
        {
            //Arrange
            var ship = new Ship("teszt", 9);
            _board.SelectedCell = new Coordinate(0, 0);

            //Act
            bool result = _board.CheckShipPositionIsValid(ship);

            //Assert
            Assert.That(result, Is.False, "A hajó pozíciója érvénytelen, de a metódus igazat adott vissza.");
        }

        [Test]
        public void CheckShipPositionIsValid_RotatedTooBigShip_ReturnsFalse()
        {
            //Arrange
            var ship = new Ship("teszt", 9);
            _board.SelectedCell = new Coordinate(0, 0);
            _board.Rotate90Degrees();
            _board.Rotate90Degrees();
            _board.Rotate90Degrees();

            //Act
            bool result = _board.CheckShipPositionIsValid(ship);

            //Assert
            Assert.That(result, Is.False, "A hajó pozíciója érvénytelen, de a metódus igazat adott vissza.");
        }

        [Test]
        public void CheckShipPositionIsValid_RotatedInvalidPosition_ReturnsFalse()
        {
            //Arrange
            var ship = new Ship("teszt", 3);
            _board.SelectedCell = new Coordinate(0, 0);
            _board.Rotate90Degrees();

            //Act
            bool result = _board.CheckShipPositionIsValid(ship);

            //Assert
            Assert.That(result, Is.False, "A hajó pozíciója érvénytelen, de a metódus igazat adott vissza.");
        }

        [Test]
        public void CheckShipPositionIsValid_MultipleRotatedValidPosition_ReturnsTrue()
        {
            //Arrange
            var ship = new Ship("teszt", 3);
            _board.SelectedCell = new Coordinate(0, 0);
            _board.Rotate90Degrees();
            _board.Rotate90Degrees();
            _board.Rotate90Degrees();

            //Act
            bool result = _board.CheckShipPositionIsValid(ship);

            //Assert
            Assert.That(result, Is.True, "A hajó pozíciója érvényes, de a metódus hamisat adott vissza.");
        }

        [Test]
        public void Hit_FirstTimeTargetIsHit_DoesNotThrowException()
        {
            //Arrange
            var target = new Coordinate(3, 3);


            //Act and Assert
            Assert.DoesNotThrow(() => _board.Hit(target), "A cellát először találtuk el, de a metódus hibat dobott.");
            Assert.That(_board.Grid[target.X, target.Y].IsHit, Is.True, "A cellát eltaláltként kellett volna megjelölni.");
        }

        [Test]
        public void Hit_SecondTimeTargetIsHit_ReturnsFalse()
        {
            //Arrange
            var target = new Coordinate(3, 3);

            //Act
            _board.Hit(target);

            //Assert
            Assert.Throws<InvalidActionException>(() => _board.Hit(target), "A cellát masodjara találtuk el, de a metódus nem dobott kivetelt.");
            Assert.That(_board.Grid[target.X, target.Y].IsHit, Is.True, "A cellát eltaláltként kellett volna megjelölni.");
        }
    }
}