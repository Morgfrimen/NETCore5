using System;
using System.Diagnostics;

namespace ЧисленныеМетоды
{

    internal class JordanGayss : LinearAlgoritm
    {
        private double[,] _aFirstVid;
        private double[] _bFirstVid;

        private static JordanGayss _instance;

        private JordanGayss (double[,] a , double[] b, double delta = DeltaDefault)
        {
            A = a;
            B = b;
            this._aFirstVid = A;
            this._bFirstVid = B;
            Delta = delta;
            BuldMatrixRight(A: A , B: B);
        }

        private JordanGayss() { }

        internal static JordanGayss GetInstance (double[,] a , double[] b)
        {
            if (_instance == null)
                _instance = new JordanGayss(a: a , b: b);
            return _instance;
        }

        internal static JordanGayss GetInstance()
        {
            if (_instance == null)
                _instance = new JordanGayss();
            return _instance;
        }

        internal double[,] Step (double[,] value , int rowStart , int columnStart)
        {
            bool FirstStep = true;
            int rowBaz = rowStart;
            int columBaz = columnStart;
            for (int rowIndex = 0; rowIndex < value.GetLength(dimension: 0); rowIndex++)
            {
                if (FirstStep && (value[rowIndex , columBaz] != 0) && (rowBaz == rowIndex))
                {
                    double b = value[rowIndex , columBaz];
                    for (int columIndex = 0; columIndex < value.GetLength(dimension: 1); columIndex++)
                        value[rowIndex , columIndex] = value[rowIndex , columIndex] / b;
                    FirstStep = false;
                    rowBaz = rowIndex;
                    rowIndex = -1;
                }
                else if (!FirstStep && (rowIndex != rowBaz))
                {
                    double b = value[rowIndex , columBaz];
                    for (int columnIndex = 0; columnIndex < value.GetLength(dimension: 1); columnIndex++)
                        value[rowIndex , columnIndex] = value[rowIndex , columnIndex] - (b * value[rowBaz , columnIndex]);
                }
            }

            return value;
        }

        internal double[,] Step (double[,] A , double[] B , int rowStart , int columStart)
        {
            BuldMatrixRight(A: A , B: B);
            return this.Step(value: Value , rowStart: rowStart , columnStart: columStart);
        }

        public override void Run()
        {
            base.Run();
            this._aFirstVid = A;
            this._bFirstVid = B;
            BuldMatrixRight(A: A,B: B);
            for (int index = 0; index < Value.GetLength(dimension: 0); index++)
                Value = this.Step(value: Value , rowStart: index , columnStart: index);
            SaveAB();
            if (!this.IsValitResult())
                throw new Exception(message: $"{nameof(JordanGayss)}.{nameof(this.IsValitResult)} : return {this.IsValitResult()}");
            
        }

        private bool IsValitResult()
        {
            bool validresult = false;
            for (int rowIndex = 0; rowIndex < A.GetLength(dimension: 0); rowIndex++)
            {
                double result = 0;
                for (int colIndex = 0; colIndex < A.GetLength(dimension: 1); colIndex++)
                    result += this._aFirstVid[rowIndex , colIndex] * B[colIndex];
                if ((this._bFirstVid[rowIndex] >= (result - this.Delta)) && (this._bFirstVid[rowIndex] <= (result + this.Delta)))
                {
                    validresult = true;
                }
                else
                {
                    validresult = false;
                    break;
                }
                    
            }

            return validresult;
        }
    }

}
