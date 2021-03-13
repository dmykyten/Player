using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Player.Tests
{
    public class ListerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        public string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        [Test]
        public void Test1()
        {
            Lister lister = new Lister();
            Assert.AreEqual(@"C:\", lister.CurrentPath);
        }

        [Test]
        public void Test2()
        {
            string tmp = GetTemporaryDirectory();
            File.CreateText(Path.Combine(tmp, "222.txt"));
            Directory.CreateDirectory(Path.Combine(tmp, "111"));
            /*----------------------*/
            Lister lister = new Lister(tmp);
            var items = lister.GetChildrenNames();
            var strings = new List<string> { "111", "222.txt" };
            //Assert.IsTrue(strings, items);
            Assert.AreEqual(strings, items);
        }
    }
}