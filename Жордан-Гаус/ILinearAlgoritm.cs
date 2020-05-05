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
            if (this.A is null || this.B is null)
                throw new Exception(message: $"A={null} или B{null}");
        }

        protected void SaveAB()
        {
            double[] B = new double[this.Value.GetLength(dimension: 0)];
            for (int rowIndex = 0; rowIndex < B.Length; rowIndex++)
                B[rowIndex] = this.Value[rowIndex , this.Value.GetLength(dimension: 1) - 1];
            double[,] A = new double[this.Value.GetLength(dimension: 0) , this.Value.GetLength(dimension: 1) - 1];
            for (int rowIndex = 0; rowIndex < A.GetLength(dimension: 0); rowIndex++)
            {
                for (int columIndex = 0; columIndex < A.GetLength(dimension: 1); columIndex++)
                    A[rowIndex , columIndex] = this.Value[rowIndex , columIndex];
            }

            this.A = A;
            this.B = B;
        }

        /// <summary>
        /// Добавляет элементы справа матрицы
        /// </summary>
        /// <param name="A">матрица</param>
        /// <param name="B">элементы</param>
        protected void BuldMatrixRight (double[,] A , double[] B)
        {

            double[,] value = new double[A.GetLength(dimension: 0) , A.GetLength(dimension: 1) + 1];
            for (int rowIndex = 0; rowIndex < value.GetLength(dimension: 0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < value.GetLength(dimension: 1); columnIndex++)
                {
                    if ((value.GetLength(dimension: 1) - 1) == columnIndex)
                        value[rowIndex , columnIndex] = B[rowIndex];
                    else
                        value[rowIndex , columnIndex] = A[rowIndex , columnIndex];
                }
            }

            this.Value = value;
            this.SaveAB();
        }

        /// <summary>
        /// Добавляет элементы снизу матрицы
        /// </summary>
        /// <param name="value">матрица</param>
        /// <param name="Z">элементы</param>
        protected void BuldMatrixDown (double[,] value , double[] Z)
        {
            double[,] newValue = new double[value.GetLength(dimension: 0) + 1 , value.GetLength(dimension: 1)];
            double[] newZ = new double[value.GetLength(dimension: 1)];

            Extension<double>.CopyArray(oldMatrix: this.Value , newMatrix: ref newValue);
            Array.Copy(sourceArray: Z , destinationArray: newZ , length: Z.Length);
            for (int columnIndex = 0; columnIndex < newValue.GetLength(dimension: 1); columnIndex++)
                newValue[newValue.GetLength(dimension: 0) - 1 , columnIndex] = newZ[columnIndex];
            this.Value = newValue;
        }
    }

}
