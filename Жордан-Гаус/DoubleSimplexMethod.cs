using System;
using System.Collections.Generic;
using System.Linq;

namespace ЧисленныеМетоды
{

    internal class DoubleSimplexMethod: LinearAlgoritm
    {
        private double[] _z;
        private double[] bazis;
        private List<int> _rowBazList;
        private List<int> _columnBazList;
        private static DoubleSimplexMethod _instance;
        private readonly int levZ;

        private DoubleSimplexMethod() { }

        private DoubleSimplexMethod (double[,] A , double[] B , double[] Z)
        {
            this.levZ = Z.Length;
            this.A = A;
            this.KanonFormA();
            this.B = B;
            BuldMatrixRight(A: this.A, B: this.B,this._columnBazList);
            this._z = Z;

            
            
            BuldMatrixDown(value: Value , Z: this._z);

        }

        private void KanonFormA()
        {
            List<int> rowBaz = new List<int>();
            List<int> columnBazList = new List<int>();
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
                    columnBazList.Add(columnIndex);
                    rowBaz.Add(item: row.First());
                }
            }

            this.bazis = vs.Where(predicate: val => Math.Abs(value: val) > 0).ToArray();
            this._rowBazList = rowBaz;
            this._columnBazList = columnBazList;
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
                                this._columnBazList.Add(column);
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


        public override void Run()
        {
            base.Run();
            
            //находим необходимую строку для старта
            double[] lastColumnValue = new double[this.Value.GetLength(0)-1];
            for (int rowIndex = 0; rowIndex < lastColumnValue.Length; rowIndex++)
            {
                lastColumnValue[rowIndex] = this.Value[rowIndex , this.Value.GetLength(1) - 1];
            }

            int startRowIndex = lastColumnValue.ToList().FindIndex(item=>
                                                                       Math.Abs(item) == lastColumnValue.Select(item=>Math.Abs(item)).Max());

            double[] lastRowValue = new double[this.levZ];
            for (int columnIndex = 0; columnIndex < this.levZ; columnIndex++)
            {
                //lastRowValue[columnIndex] = 
                //    Value[startRowIndex , columnIndex] < 0 
                //        ? -1 * Value[Value.GetLength(0) - 1 , columnIndex]/ Value[startRowIndex, columnIndex] 
                //        : Double.MaxValue;
                lastRowValue[columnIndex] = Value[startRowIndex , columnIndex] != 0
                                                ? -1 * Value[Value.GetLength(0) - 1 , columnIndex] / Value[startRowIndex , columnIndex]
                                                : Double.MaxValue;
            }

            int startColumn = lastRowValue.ToList()
                                          .FindIndex(item => 
                                                         item == lastRowValue.Min());

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
