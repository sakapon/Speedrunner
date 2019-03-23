using System;
using System.Linq;
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

        [TestMethod]
        public void Pattern_Assignment_Chars()
        {
            var chars = "-+*/%&|^<>=!";
            var pattern = new Regex($@"^[{chars}]$");

            for (var i = 32; i <= 126; i++)
            {
                var c = (char)i;
                Assert.AreEqual(chars.Contains(c), pattern.IsMatch(c.ToString()));
            }
            Assert.AreEqual(false, pattern.IsMatch(""));
            Assert.AreEqual(false, pattern.IsMatch("あ"));
        }

        [TestMethod]
        public void Pattern_Assignment()
        {
            var expression = new Regex(@"^\s*(.+?)\s*[-+*/%&|^<>=!]*=.+$");

            string GetVarName(string text)
            {
                var match = expression.Match(text);
                return match.Success ? match.Groups[1].Value : null;
            }

            Assert.AreEqual("abc", GetVarName("abc=123"));
            Assert.AreEqual("abc", GetVarName("abc = 123"));

            var chars = " -+*/%&|^<>=!";
            for (var i = 32; i <= 126; i++)
            {
                var c = (char)i;
                Assert.AreEqual(chars.Contains(c) ? "abc" : $"abc{c}", GetVarName($"abc{c}=123"));
                Assert.AreEqual(chars.Contains(c) ? "abc" : $"abc {c}", GetVarName($"abc {c}= 123"));
            }

            Assert.AreEqual("abc", GetVarName("abc<<=123"));
            Assert.AreEqual("abc", GetVarName("abc <<= 123"));
            Assert.AreEqual("abc", GetVarName("abc>>=123"));
            Assert.AreEqual("abc", GetVarName("abc >>= 123"));
        }
    }
}
