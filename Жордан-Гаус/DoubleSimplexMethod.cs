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
            this.bazis = new double[A.GetLength(1)];
            //ToDo: составляем сразу целевую функцию
            this.B = B;
            base.BuldMatrix(A,B);
            this._z = Z;
        }

        private void KanonFormA()
        {
            for (int columnIndex = 0; columnIndex < A.GetLength(1); columnIndex++)
            {
                int countBaz = 0;
                for (int rowIndex = 0; rowIndex < A.GetLength(0); rowIndex++)
                {
                    if (A[rowIndex , columnIndex] != 0)
                        countBaz++;
                }

                if (countBaz == 1)
                    this.bazis[columnIndex] = columnIndex + 1;

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
            _instance ??= new DoubleSimplexMethod();
            return _instance;
        }

    }
}
