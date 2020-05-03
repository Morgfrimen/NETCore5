using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЧисленныеМетоды
{
    internal class DoubleSimplexMethod : LinearAlgoritm
    {
        private double[] _z;
        private double[] bazis;
        private static DoubleSimplexMethod _instance;
        private DoubleSimplexMethod() { }

        private DoubleSimplexMethod (double[,] A , double[] B , double[] Z)
        {
            this.A = A;
            //ToDo: составляем сразу целевую функцию
            this.KanonFormA();
            this.B = B;
            base.BuldMatrix(A,B);
            this._z = Z;
        }

        private void KanonFormA()
        {
            List<int> rowBaz = new List<int>();
            double[] vs = new double[A.GetLength(1)];
            for (int columnIndex = 0; columnIndex < A.GetLength(1); columnIndex++)
            {
                int countBaz = 0;
                for (int rowIndex = 0; rowIndex < A.GetLength(0); rowIndex++)
                {
                    if (A[rowIndex , columnIndex] != 0)
                        countBaz++;
                }

                if (countBaz == 1) 
                    vs[columnIndex] = columnIndex + 1;
            }

            this.bazis = vs.Where(val => Math.Abs(val) > 0).ToArray();
            if(this.bazis.Length > A.GetLength(0))
                throw new Exception($"БАЗИСОВ В СИМПЛЕКС МЕТОДЕ {bazis.Length}: А{A.GetLength(0)}");
            else if(this.bazis.Length < A.GetLength(0))
            {
                int raznos = A.GetLength(0) - this.bazis.Length;
                double[,] newA = new double[A.GetLength(0),A.GetLength(1)+raznos];
                for (int rowIndex = 0; rowIndex < A.GetLength(0); rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < A.GetLength(1); columnIndex++)
                    {
                        newA[rowIndex, columnIndex] = A[rowIndex, columnIndex];
                    }
                }

            }
        }




        /// <summary>
        /// Получает объект класс симплекс метода
        /// </summary>
        /// <param name="A">коэффициенты ограничений</param>
        /// <param name="B">Недопустимый план</param>
        /// <param name="Z">коэфициенты целевой функции</param>
        /// <returns></returns>
        internal static DoubleSimplexMethod GetInstance(double[,]A , double[] B,double[] Z)
        {
            _instance ??= new DoubleSimplexMethod(A,B,Z);
            return _instance;
        }

    }
}
