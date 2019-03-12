using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class RegexTest
    {
        [TestMethod]
        public void Pattern_ClassMethod()
        {
            var dot = new Regex(@"([^.]+)\.([^.]+)");

            Assert.AreEqual(false, dot.IsMatch(""));
            Assert.AreEqual(false, dot.IsMatch("^"));
            Assert.AreEqual(false, dot.IsMatch("abc"));
            Assert.AreEqual(false, dot.IsMatch("."));
            Assert.AreEqual(false, dot.IsMatch("a."));
            Assert.AreEqual(false, dot.IsMatch(".1"));
            Assert.AreEqual(false, dot.IsMatch("..."));
            Assert.AreEqual(true, dot.IsMatch("a.1"));
            Assert.AreEqual(true, dot.IsMatch("abc.123"));

            var match = dot.Match("abc.123");
            Assert.AreEqual("abc", match.Groups[1].Value);
            Assert.AreEqual("123", match.Groups[2].Value);
        }
    }
}
