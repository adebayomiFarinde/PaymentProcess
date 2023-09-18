using PaymentSystem.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.NUnit
{
    public class ObjectExtensionsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsNullReturnsTrueForNullObject()
        {
            object input = null;

            var result = input.IsNull();
            var expectedResult = true;

            Assert.That(result, Is.EqualTo(expectedResult));

        }

        [Test]

        public void IsNullReturnsFalseForNotNullableObject()
        {
            object input = "This is test";

            var result = input.IsNull();
            var expectedResult = false;

            Assert.That(result, Is.EqualTo(expectedResult));

        }

        [Test]
        public void IsNullOrEmptyReturnsTrueForNullObject()
        {
            var input = new List<int>();

            var result = input.IsNullOrEmpty();
            var expectedResult = true;

            Assert.That(result, Is.EqualTo(expectedResult));

        }

        [Test]

        public void IsNullOrEmptyReturnsFalseForNotNullableObject()
        {
            var input = new List<int> { 1, 2, 4};

            var result = input.IsNull();
            var expectedResult = false;

            Assert.That(result, Is.EqualTo(expectedResult));

        }
    }
}
