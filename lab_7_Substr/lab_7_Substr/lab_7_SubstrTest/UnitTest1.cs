using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab_7_SubstrTest
{
    [TestClass]
    public class UnitTest1
    {
        static Random rand = new Random();
        static int N = 500;

        [TestMethod]
        public void TestStandardRandom()
        {
            for (int i = 0; i < N; i++)
            {
                string s = rand.Next(1000, 9999999).ToString();
                string sub = rand.Next(100, 999).ToString();

                int iCorrect = s.IndexOf(sub);
                int res = lab_7_Substr.StrMatching.Standard(s, sub);
                Assert.AreEqual(iCorrect, res, "str: " + s + " sub: " + sub);
            }
        }

        [TestMethod]
        public void TestKMPRandom()
        {
            for (int i = 0; i < N; i++)
            {
                string s = rand.Next(1000, 9999999).ToString();
                string sub = rand.Next(100, 999).ToString();

                int iCorrect = s.IndexOf(sub);
                int res = lab_7_Substr.StrMatching.KMP(s, sub);
                Assert.AreEqual(iCorrect, res, "str: " + s + " sub: " + sub);
            }
        }

        [TestMethod]
        public void TestBMRandom()
        {
            for (int i = 0; i < N; i++)
            {
                string s = rand.Next(1000, 9999999).ToString();
                string sub = rand.Next(100, 999).ToString();

                int iCorrect = s.IndexOf(sub);
                int res = lab_7_Substr.StrMatching.BM(s, sub);
                Assert.AreEqual(iCorrect, res, "str: " + s + " sub: " + sub);
            }
        }

        [TestMethod]
        public void TestStandardSameLength()
        {
            for (int i = 0; i < N; i++)
            {
                string s = rand.Next(100, 999).ToString();
                string sub = rand.Next(100, 999).ToString();

                int iCorrect = s.IndexOf(sub);
                int res = lab_7_Substr.StrMatching.Standard(s, sub);
                Assert.AreEqual(iCorrect, res, "str: " + s + " sub: " + sub);
            }
        }

        [TestMethod]
        public void TestKMPSameLength()
        {
            for (int i = 0; i < N; i++)
            {
                string s = rand.Next(100, 999).ToString();
                string sub = rand.Next(100, 999).ToString();

                int iCorrect = s.IndexOf(sub);
                int res = lab_7_Substr.StrMatching.KMP(s, sub);
                Assert.AreEqual(iCorrect, res, "str: " + s + " sub: " + sub);
            }
        }

        [TestMethod]
        public void TestBMSameLength()
        {
            for (int i = 0; i < N; i++)
            {
                string s = rand.Next(100, 999).ToString();
                string sub = rand.Next(100, 999).ToString();

                int iCorrect = s.IndexOf(sub);
                int res = lab_7_Substr.StrMatching.BM(s, sub);
                Assert.AreEqual(iCorrect, res, "str: " + s + " sub: " + sub);
            }
        }
    }
}
