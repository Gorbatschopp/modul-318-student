using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit;
using NUnit.Framework;
using TrainProject;

namespace TrainProjectTest
{
    [TestClass]
    public class UnitTest1
    {
        readonly Format Format = new Format();
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestCase("2018-11-27T09:07:00+0100", ExpectedResult = "2018.11.27 09:07")]
        [TestCase("2018-02-28T11:07:00+0100", ExpectedResult = "2018.02.28 11:07")]
        [TestCase("2016-02-29T23:07:00+0100", ExpectedResult = "2016.02.29 23:07")]
        [TestCase("2000-02-28T01:07:00+0100", ExpectedResult = "2000.02.28 01:07")]
        public string TestFormatDateCorrectly(string time)
        {
            // arrange
            string retrievedTime = Format.FormatDateCorrectly(time);

            // act

            // assert
            return retrievedTime;
        }

        [TestCase("23,343434", ExpectedResult = "23.343434")]
        [TestCase("23.343434", ExpectedResult = "23.343434")]
        [TestCase("123,343434", ExpectedResult = "123.343434")]
        [TestCase("1,121212", ExpectedResult = "1.121212")]
        public string TestFormatCoordinatesCorrectly(string coord)
        {
            // arrange
            string retrievedCoords = Format.FormatCoordinatesCorrectly(coord);

            // act

            // assert
            return retrievedCoords;
        }
    }
}
