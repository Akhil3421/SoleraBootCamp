using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorWebAPI.Services;
using CalculatorWebAPI.Models;
using System;

namespace CalculatorWebAPI.Services.Tests
{
    [TestClass]
    public class CalculatorServiceTests
    {
        private CalculatorService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new CalculatorService();
        }

        [TestMethod]
        public void AddTestPass()
        {
            // Arrange
            int op1 = 5;
            int op2 = 3;

            // Act
            Calculator result = _service.Add(op1, op2);

            // Assert
            Assert.AreEqual(8, result.result);
            Assert.AreEqual("+", result.operation);
            Assert.AreEqual(op1, result.operand1);
            Assert.AreEqual(op2, result.operand2);
        }

        [TestMethod]
        public void SubtractTestPass()
        {
            var result = _service.Subtract(10, 4);
            Assert.AreEqual(6, result.result);
            Assert.AreEqual("-", result.operation);
        }

        [TestMethod]
        public void MultiplyTestPass()
        {
            var result = _service.Multiply(7, 6);
            Assert.AreEqual(42, result.result);
            Assert.AreEqual("*", result.operation);
        }

        [TestMethod]
        public void DivideTestPass()
        {
            var result = _service.Divide(20, 5);
            Assert.AreEqual(4, result.result);
            Assert.AreEqual("/", result.operation);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivideByZero_ThrowsException()
        {
            _service.Divide(10, 0);
        }

        [TestMethod]
        public void Calculate_AddOperation_ReturnsCorrectResult()
        {
            var calc = new Calculator { operand1 = 1, operand2 = 2, operation = "+" };
            var result = _service.Calculate(calc);
            Assert.AreEqual(3, result.result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_InvalidOperation_ThrowsException()
        {
            var calc = new Calculator { operand1 = 1, operand2 = 1, operation = "%" };
            _service.Calculate(calc);
        }
    }
}
