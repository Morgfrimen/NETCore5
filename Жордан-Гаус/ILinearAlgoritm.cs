using System;

namespace ЧисленныеМетоды
{

    public interface ILinearAlgoritm
    {
        public double[,] A { get; set; }
        public double[] B { get; set; } 
        
        public double[,] Value { get; set; }

        public double Delta { get; set; }

        void Run();
    }

    internal abstract class LinearAlgoritm: ILinearAlgoritm
    {
        public const double DeltaDefault = 0.001;

        /// <inheritdoc />
        public double[,] A { get; set; }

        /// <inheritdoc />
        public double[] B { get; set; }

        /// <inheritdoc />
        public double[,] Value { get; set; }

        /// <inheritdoc />
        public double Delta { get; set; } = DeltaDefault;

        /// <inheritdoc />
        public virtual void Run()
        {
            if(this.A is null || this.B is null)
                throw new Exception($"A={null} или B{null}");
        }

        protected void SaveAB()
        {
            double[] B = new double[this.Value.GetLength(dimension: 0)];
            for (int rowIndex = 0; rowIndex < B.Length; rowIndex++)
                B[rowIndex] = this.Value[rowIndex, this.Value.GetLength(dimension: 1) - 1];
            double[,] A = new double[this.Value.GetLength(dimension: 0), this.Value.GetLength(dimension: 1) - 1];
            for (int rowIndex = 0; rowIndex < A.GetLength(dimension: 0); rowIndex++)
            {
                for (int columIndex = 0; columIndex < A.GetLength(dimension: 1); columIndex++)
                    A[rowIndex, columIndex] = this.Value[rowIndex, columIndex];
            }

            this.A = A;
            this.B = B;
        }

        protected void BuldMatrix(double[,] A, double[] B)
        {

            double[,] value = new double[A.GetLength(dimension: 0), A.GetLength(dimension: 1) + 1];
            for (int rowIndex = 0; rowIndex < value.GetLength(dimension: 0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < value.GetLength(dimension: 1); columnIndex++)
                {
                    if ((value.GetLength(dimension: 1) - 1) == columnIndex)
                        value[rowIndex, columnIndex] = B[rowIndex];
                    else
                        value[rowIndex, columnIndex] = A[rowIndex, columnIndex];
                }
            }
            Value = value;
            this.SaveAB();
        }
    }

}
