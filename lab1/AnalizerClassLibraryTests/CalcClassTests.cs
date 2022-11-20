using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcClassBr;
using System;
using ErrorLibrary;

namespace AnalaizerClassLibrary.Tests
{
    [TestClass]
    public class CalcClassTests
    {
        public TestContext TestContext { get; set; }
     
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "testData.xml", "TestWithValidNumbers", DataAccessMethod.Sequential)]
        public void Mod_WhenNumbersAreValid_ReturnsExpectedResult()
        {
            //Arrange
            long incomingFirstNumber = Convert.ToInt64(TestContext.DataRow["incomingFirstNumber"]);
            long incomingSecondNumber = Convert.ToInt64(TestContext.DataRow["incomingSecondNumber"]);
            long expected = Convert.ToInt64(TestContext.DataRow["expectedResult"]);

            //Actual
            long actual = CalcClass.Mod(incomingFirstNumber, incomingSecondNumber);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "testData.xml", "TestWithInvalidNumbers", DataAccessMethod.Sequential)]
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ErrorsExpression.ERROR_06)]
        public void Mod_WhenNumbersAreInvalid_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            long incomingFirstNumber = Convert.ToInt64(TestContext.DataRow["incomingFirstNumber"]);
            long incomingSecondNumber = Convert.ToInt64(TestContext.DataRow["incomingSecondNumber"]);

            //Actual
            long actual = CalcClass.Mod(incomingFirstNumber, incomingSecondNumber);

            //Assert - Expects exception ArgumentOutOfRangeException
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "testData.xml", "TestWithZeroDivider", DataAccessMethod.Sequential)]
        [TestMethod]
        [ExpectedException(typeof(System.DivideByZeroException), ErrorsExpression.ERROR_09)]
        public void Mod_WhenSecondNumberIsZero_ThrowsDivideByZeroException()
        {
            //Arrange
            long incomingFirstNumber = Convert.ToInt64(TestContext.DataRow["incomingFirstNumber"]);
            long incomingSecondNumber = Convert.ToInt64(TestContext.DataRow["incomingSecondNumber"]);

            //Actual
            long actual = CalcClass.Mod(incomingFirstNumber, incomingSecondNumber);

            //Assert - Expects exception ArgumentOutOfRangeException
        }
    }
}