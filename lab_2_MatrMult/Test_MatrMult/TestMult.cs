using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace lab_2_MatrMult.Tests
{
    [TestClass]
    public class TestMult
    {
        [TestMethod]
        public void Test_WrongSize_NullReturned()
        {
            int sizeA = 2, sizeB = 3;
            int[][] a = new int[sizeA][];
            for (int i = 0; i < sizeA; i++)
                a[i] = new int[sizeA];
            int[][] b = new int[sizeB][];
            for (int i = 0; i < sizeB; i++)
                b[i] = new int[sizeB];

            var res = Mult.MultStand(a, b);

            Assert.IsNull(res);
        }
        
        [TestMethod]
        public void Test_StandSize1()
        {
            int[][] a = new int[1][];
            a[0] = new int[1] { 2 };
            int[][] b = new int[1][];
            b[0] = new int[1] { 3 };

            var res = Mult.MultStand(a, b);

            Assert.IsNotNull(res);
            Assert.AreEqual(6, res[0][0]);
        }

        [TestMethod]
        public void Test_StandSize2()
        {
            int[][] a = new int[2][] { new int[2] { 1, 2 }, new int[2] { 3, 4 } };
            int[][] b = new int[2][] { new int[2] { 5, 6 }, new int[2] { 7, 8 } };

            var res = Mult.MultStand(a, b);
            int[][] correctRes = new int[2][] { new int[2] { 19, 22 }, new int[2] { 43, 50 } };
            
            Assert.IsNotNull(res);
            
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    Assert.AreEqual(correctRes[i][j], res[i][j], "i, j: " + i.ToString() + " " + j.ToString());
        }

        [TestMethod]
        public void Test_EvenStandardEqualsVin_Random()
        {
            int n = 10;
            int[][] a = Program.FillMatr(n, n);
            int[][] b = Program.FillMatr(n, n);

            var resStand = Mult.MultStand(a, b);
            var resVin = Mult.MultVin(a, b);

            Assert.IsNotNull(resStand);
            Assert.IsNotNull(resVin);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Assert.AreEqual(resStand[i][j], resVin[i][j], "i, j: " + i.ToString() + " " + j.ToString());
        }

        [TestMethod]
        public void Test_EvenStandardEqualsVinOpt_Random()
        {
            int n = 10;
            int[][] a = Program.FillMatr(n, n);
            int[][] b = Program.FillMatr(n, n);

            var resStand = Mult.MultStand(a, b);
            var resVinOpt = Mult.MultVinOpt(a, b);

            Assert.IsNotNull(resStand);
            Assert.IsNotNull(resVinOpt);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Assert.AreEqual(resStand[i][j], resVinOpt[i][j], "i, j: " + i.ToString() + ", " + j.ToString());
        }

        [TestMethod]
        public void Test_OddStandardEqualsVin_Random()
        {
            int n = 11;
            int[][] a = Program.FillMatr(n, n);
            int[][] b = Program.FillMatr(n, n);

            var resStand = Mult.MultStand(a, b);
            var resVin = Mult.MultVin(a, b);

            Assert.IsNotNull(resStand);
            Assert.IsNotNull(resVin);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Assert.AreEqual(resStand[i][j], resVin[i][j], "i, j: " + i.ToString() + " " + j.ToString());
        }

        [TestMethod]
        public void Test_OddStandardEqualsVinOpt_Random()
        {
            int n = 11;
            int[][] a = Program.FillMatr(n, n);
            int[][] b = Program.FillMatr(n, n);

            var resStand = Mult.MultStand(a, b);
            var resVinOpt = Mult.MultVinOpt(a, b);

            Assert.IsNotNull(resStand);
            Assert.IsNotNull(resVinOpt);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Assert.AreEqual(resStand[i][j], resVinOpt[i][j], "i, j: " + i.ToString() + ", " + j.ToString());
        }
    }
}
