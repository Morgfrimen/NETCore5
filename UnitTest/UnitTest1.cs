using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using „исленныећетоды;

namespace UnitTest
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestJordanGayss()
        {
            double[,] A = new double[,] {{4 , -7 , 8} , {2 , -4 , 5} , {-3 , 11 , 1}};
            double[] B = new double[] {-23 , -13 , 16};
            ILinearAlgoritm jordan = LinearProgram.GetJourdanGayssLinearAlgoritm(A , B);
            jordan.Run();
            var test = jordan.Value;
            double[,] expecteDoubles = new double[,] {{1 , 0 , 0 , -2} , {0 , 1 , 0 , 1} , {0 , 0 , 1 , -1}};
            for (int rowIndex = 0; rowIndex < test.GetLength(0); rowIndex++)
            {
                for (int columIndex = 0; columIndex < test.GetLength(1); columIndex++)
                {
                    Assert.AreEqual(expecteDoubles[rowIndex , columIndex] , test[rowIndex , columIndex], delta: jordan.Delta);
                }
            }

            A = new double[,] {{3 , 4} , {8 , -10}};
            B = new double[] {4 , 5};

            expecteDoubles = new double[,]{{1,0,0.967},{0,1,0.274}};
            jordan = LinearProgram.GetJourdanGayssLinearAlgoritm();
            jordan.A = A;
            jordan.B = B;
            jordan.Run();
            test = jordan.Value;

            for (int rowIndex = 0; rowIndex < test.GetLength(0); rowIndex++)
            {
                for (int columIndex = 0; columIndex < test.GetLength(1); columIndex++)
                {
                    Assert.AreEqual(expecteDoubles[rowIndex, columIndex], test[rowIndex, columIndex],delta:jordan.Delta);
                }
            }
        }

        [TestMethod]
        public void TestSimplex()
        {
            double[,] A = new double[,]
            {
                {0,1,1,1,0,0 },
                {2,1,2,0,1,0 },
                {2,-1,2,0,0,1}
            };
            double[] B = new double[] {0, 0, 0,-4,-6,-2};
            double[] Z = new double[] {3,2,1,0,0,0};

            LinearProgram.GetDoubleSimplex(A, B, Z);
        }
    }

}
