using System;
using System.Collections.Generic;
using System.Linq;

namespace ЧисленныеМетоды
{

    internal class DoubleSimplexMethod: LinearAlgoritm
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
            BuldMatrixRight(A: this.A , B: this.B);
            this._z = Z;
            BuldMatrixDown(value: Value , Z: this._z);

        }

        private void KanonFormA()
        {
            List<int> rowBaz = new List<int>();
            double[] vs = new double[A.GetLength(dimension: 1)];
            for (int columnIndex = 0; columnIndex < A.GetLength(dimension: 1); columnIndex++)
            {
                List<int> row = new List<int>();
                int countBaz = 0;
                for (int rowIndex = 0; rowIndex < A.GetLength(dimension: 0); rowIndex++)
                {
                    if (A[rowIndex , columnIndex] != 0)
                    {
                        row.Add(item: rowIndex + 1);
                        countBaz++;
                    }
                }

                if ((countBaz == 1) && (row.Count == 1))
                {
                    vs[columnIndex] = columnIndex + 1;
                    rowBaz.Add(item: row.First());
                }
            }

            this.bazis = vs.Where(predicate: val => Math.Abs(value: val) > 0).ToArray();
            if (this.bazis.Length > A.GetLength(dimension: 0))
            {
                throw new Exception(message: $"БАЗИСОВ В СИМПЛЕКС МЕТОДЕ {this.bazis.Length}: А{A.GetLength(dimension: 0)}");
            }
            else if (this.bazis.Length < A.GetLength(dimension: 0))
            {
                int raznos = A.GetLength(dimension: 0) - this.bazis.Length;
                double[,] newA = new double[A.GetLength(dimension: 0) , A.GetLength(dimension: 1) + raznos];
                Extension<double>.CopyArray(oldMatrix: A , newMatrix: ref newA);
                int column = A.GetLength(dimension: 1);
                for (int rowIndex = 0; rowIndex < newA.GetLength(dimension: 0); rowIndex++)
                {
                    if ((rowIndex + 1) != rowBaz.FirstOrDefault(predicate: item => item == (rowIndex + 1)))
                        for (int columnIndex = column; columnIndex < newA.GetLength(dimension: 1); columnIndex++)
                        {
                            if (Extension<int>.FindElem(list: rowBaz , find: rowIndex + 1))
                            {
                                newA[rowIndex , columnIndex] = 1;
                                column = (column + 1) < newA.GetLength(dimension: 1) ? column + 1 : column;
                                break;
                            }
                        }
                }

                A = newA;
            }
        }

        /// <summary>
        /// Получает объект класс симплекс метода
        /// </summary>
        /// <param name="A">коэффициенты ограничений</param>
        /// <param name="B">Недопустимый план</param>
        /// <param name="Z">коэфициенты целевой функции</param>
        /// <returns></returns>
        internal static DoubleSimplexMethod GetInstance (double[,] A , double[] B , double[] Z)
        {
            _instance ??= new DoubleSimplexMethod(A: A , B: B , Z: Z);
            return _instance;
        }
    }

    internal static class Extension<T> where T : struct
    {
        internal static void CopyArray (T[,] oldMatrix , ref T[,] newMatrix)
        {
            for (int rowIndex = 0; rowIndex < oldMatrix.GetLength(dimension: 0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < oldMatrix.GetLength(dimension: 1); columnIndex++)
                    newMatrix[rowIndex , columnIndex] = oldMatrix[rowIndex , columnIndex];
            }
        }

        internal static bool FindElem (List<T> list , T find)
        {
            T result = list.FirstOrDefault(predicate: item => item as int? == find as int?);
            int? res = 0;
            return res == result as int?;
        }
    }

}
