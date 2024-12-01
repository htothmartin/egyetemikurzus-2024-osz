using F1H43C_EEJYN9.Core;
using F1H43C_EEJYN9.Entities;
using NUnit.Framework;
using System;
using System.Reflection;
using System.Collections.Generic;

//For the console colors check this document: https://learn.microsoft.com/en-us/dotnet/api/system.consolecolor?view=net-8.0

namespace Battleship_Tests
{
    [TestFixture]
    public class PreferenceTests
    {
        private PreferenceManager _preferenceManager;
        private GamePreferences _defaultPreferences;
        private List<ConsoleColor> _availableColors;

        [SetUp]
        public void Setup()
        {
            Assert.IsNotNull(typeof(PreferenceManager), "PreferenceManager típus nem található");
            Assert.IsNotNull(typeof(GamePreferences), "GamePreferences típus nem található");

            _preferenceManager = new PreferenceManager();

            Assert.IsNotNull(_preferenceManager, "PreferenceManager nem hozható létre");

            _defaultPreferences = new GamePreferences();
            _availableColors = _preferenceManager.GetAvailableColors();

            Assert.IsTrue(_availableColors.Count > 0, "Nem találhatók elérhető konzol színek");
        }

        [Test]
        public void ValidateAndApplyDefaults_WhitespaceCharacters_ShouldApplyDefaultCharacters()
        {
            // Arrange
            var preferences = new GamePreferences
            {
                ShipCharacter = ' ',
                HitShipCharacter = '\t',
                SunkShipCharacter = '\n',
                WaterCharacter = '\r',
                MissedShotCharacter = ' '
            };

            // Act
            _preferenceManager.ValidateAndApplyDefaults(preferences);

            // Assert
            Assert.That(preferences.ShipCharacter, Is.EqualTo(_defaultPreferences.ShipCharacter), "ShipCharacter nem alapértelmezett");
            Assert.That(preferences.HitShipCharacter, Is.EqualTo(_defaultPreferences.HitShipCharacter), "HitShipCharacter nem alapértelmezett");
            Assert.That(preferences.SunkShipCharacter, Is.EqualTo(_defaultPreferences.SunkShipCharacter), "SunkShipCharacter nem alapértelmezett");
            Assert.That(preferences.WaterCharacter, Is.EqualTo(_defaultPreferences.WaterCharacter), "WaterCharacter nem alapértelmezett");
            Assert.That(preferences.MissedShotCharacter, Is.EqualTo(_defaultPreferences.MissedShotCharacter), "MissedShotCharacter nem alapértelmezett");
        }

        [Test]
        public void ValidateAndApplyDefaults_InvalidColors_ShouldApplyDefaultColors()
        {
            // Arrange
            var preferences = new GamePreferences
            {
                ShipColor = (ConsoleColor)999,
                HitShipColor = (ConsoleColor)888,
                SunkShipColor = (ConsoleColor)777,
                WaterColor = (ConsoleColor)666,
                MissedShotColor = (ConsoleColor)555
            };

            // Act
            _preferenceManager.ValidateAndApplyDefaults(preferences);

            // Assert
            Assert.That(preferences.ShipColor, Is.EqualTo(_defaultPreferences.ShipColor), "ShipColor nem alapértelmezett");
            Assert.That(preferences.HitShipColor, Is.EqualTo(_defaultPreferences.HitShipColor), "HitShipColor nem alapértelmezett");
            Assert.That(preferences.SunkShipColor, Is.EqualTo(_defaultPreferences.SunkShipColor), "SunkShipColor nem alapértelmezett");
            Assert.That(preferences.WaterColor, Is.EqualTo(_defaultPreferences.WaterColor), "WaterColor nem alapértelmezett");
            Assert.That(preferences.MissedShotColor, Is.EqualTo(_defaultPreferences.MissedShotColor), "MissedShotColor nem alapértelmezett");
        }

        [Test]
        public void ValidateAndApplyDefaults_ValidCharactersAndColors_ShouldNotChangePreferences()
        {
            // Arrange
            var validColor = ConsoleColor.Blue;
            var validChar = 'X';
            var preferences = new GamePreferences
            {
                ShipCharacter = validChar,
                HitShipCharacter = validChar,
                SunkShipCharacter = validChar,
                WaterCharacter = validChar,
                MissedShotCharacter = validChar,

                ShipColor = validColor,
                HitShipColor = validColor,
                SunkShipColor = validColor,
                WaterColor = validColor,
                MissedShotColor = validColor
            };

            // Act
            _preferenceManager.ValidateAndApplyDefaults(preferences);

            // Assert
            Assert.That(preferences.ShipCharacter, Is.EqualTo(validChar), "ShipCharacter megváltozott");
            Assert.That(preferences.HitShipCharacter, Is.EqualTo(validChar), "HitShipCharacter megváltozott");
            Assert.That(preferences.SunkShipCharacter, Is.EqualTo(validChar), "SunkShipCharacter megváltozott");
            Assert.That(preferences.WaterCharacter, Is.EqualTo(validChar), "WaterCharacter megváltozott");
            Assert.That(preferences.MissedShotCharacter, Is.EqualTo(validChar), "MissedShotCharacter megváltozott");

            Assert.That(preferences.ShipColor, Is.EqualTo(validColor), "ShipColor megváltozott");
            Assert.That(preferences.HitShipColor, Is.EqualTo(validColor), "HitShipColor megváltozott");
            Assert.That(preferences.SunkShipColor, Is.EqualTo(validColor), "SunkShipColor megváltozott");
            Assert.That(preferences.WaterColor, Is.EqualTo(validColor), "WaterColor megváltozott");
            Assert.That(preferences.MissedShotColor, Is.EqualTo(validColor), "MissedShotColor megváltozott");
        }

        [Test]
        public void ValidateAndApplyDefaults_MixedValidAndInvalidInputs_ShouldApplyDefaultsSelectively()
        {
            // Arrange
            var preferences = new GamePreferences
            {
                ShipCharacter = 'S',
                HitShipCharacter = ' ',
                SunkShipCharacter = 'X',
                WaterCharacter = '\t',
                MissedShotCharacter = 'M',

                ShipColor = ConsoleColor.Green,
                HitShipColor = (ConsoleColor)999,
                SunkShipColor = ConsoleColor.Red,
                WaterColor = (ConsoleColor)888,
                MissedShotColor = ConsoleColor.Blue
            };

            // Act
            _preferenceManager.ValidateAndApplyDefaults(preferences);

            // Assert
            Assert.That(preferences.ShipCharacter, Is.EqualTo('S'), "ShipCharacter hibásan módosult");
            Assert.That(preferences.HitShipCharacter, Is.EqualTo(_defaultPreferences.HitShipCharacter), "HitShipCharacter nem alapértelmezett");
            Assert.That(preferences.SunkShipCharacter, Is.EqualTo('X'), "SunkShipCharacter hibásan módosult");
            Assert.That(preferences.WaterCharacter, Is.EqualTo(_defaultPreferences.WaterCharacter), "WaterCharacter nem alapértelmezett");
            Assert.That(preferences.MissedShotCharacter, Is.EqualTo('M'), "MissedShotCharacter hibásan módosult");

            Assert.That(preferences.ShipColor, Is.EqualTo(ConsoleColor.Green), "ShipColor hibásan módosult");
            Assert.That(preferences.HitShipColor, Is.EqualTo(_defaultPreferences.HitShipColor), "HitShipColor nem alapértelmezett");
            Assert.That(preferences.SunkShipColor, Is.EqualTo(ConsoleColor.Red), "SunkShipColor hibásan módosult");
            Assert.That(preferences.WaterColor, Is.EqualTo(_defaultPreferences.WaterColor), "WaterColor nem alapértelmezett");
            Assert.That(preferences.MissedShotColor, Is.EqualTo(ConsoleColor.Blue), "MissedShotColor hibásan módosult");
        }
    }
}