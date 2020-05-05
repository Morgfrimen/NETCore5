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
            base.BuldMatrixRight(this.A,this.B);
            this._z = Z;
            base.BuldMatrixDown(this.Value,this._z);

        }

        private void KanonFormA()
        {
            List<int> rowBaz = new List<int>();
            double[] vs = new double[A.GetLength(1)];
            for (int columnIndex = 0; columnIndex < A.GetLength(1); columnIndex++)
            {
                List<int> row = new List<int>();
                int countBaz = 0;
                for (int rowIndex = 0; rowIndex < A.GetLength(0); rowIndex++)
                {
                    if (A[rowIndex , columnIndex] != 0)
                    {
                        row.Add(rowIndex + 1);
                        countBaz++;
                    }
                }

                if (countBaz == 1 && row.Count == 1)
                {
                    vs[columnIndex] = columnIndex + 1;
                    rowBaz.Add(row.First());
                } 
            }

            this.bazis = vs.Where(val => Math.Abs(val) > 0).ToArray();
            if(this.bazis.Length > A.GetLength(0))
                throw new Exception($"БАЗИСОВ В СИМПЛЕКС МЕТОДЕ {bazis.Length}: А{A.GetLength(0)}");
            else if(this.bazis.Length < A.GetLength(0))
            {
                int raznos = A.GetLength(0) - this.bazis.Length;
                double[,] newA = new double[A.GetLength(0),A.GetLength(1)+raznos];
                Extension<double>.CopyArray( A,ref newA);
                int column = A.GetLength(1);
                for (int rowIndex = 0; rowIndex < newA.GetLength(0); rowIndex++)
                {
                    if (rowIndex + 1 != rowBaz.FirstOrDefault(item=>item == rowIndex + 1))
                    {
                        for (int columnIndex = column; columnIndex < newA.GetLength(1); columnIndex++)
                        {
                            if (Extension<int>.FindElem(rowBaz, rowIndex + 1))
                            {
                                newA[rowIndex, columnIndex] = 1;
                                column = (column + 1) < newA.GetLength(1) ? column + 1 : column;
                                break;
                            }
                        } 
                    }
                }

                this.A = newA;
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

    internal static class Extension<T> where T : struct 
    {
        internal static void CopyArray<T>(T[,] oldMatrix,ref T[,] newMatrix)
        {
            for (int rowIndex = 0; rowIndex < oldMatrix.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < oldMatrix.GetLength(1); columnIndex++)
                {
                    newMatrix[rowIndex, columnIndex] = oldMatrix[rowIndex, columnIndex];
                }
            }
        }

        internal static bool FindElem<T> (List<T> list, T find)
        {
            //if (list.Count > 0)
            //{
            //    var result = list.FirstOrDefault(item => item as int? == find as int?);
            //    int? res = default;
            //    return res == result as int?; 
            //}

            //return true;

            var result = list.FirstOrDefault(item => item as int? == find as int?);
            int? res = 0;
            return res == result as int?;
        }
    }
}
