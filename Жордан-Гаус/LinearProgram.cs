namespace ЧисленныеМетоды
{

    public static class LinearProgram
    {
        public static ILinearAlgoritm GetJourdanGayssLinearAlgoritm()
        {
            ILinearAlgoritm jourdan = JordanGayss.GetInstance();
            return jourdan;
        }

        public static ILinearAlgoritm GetJourdanGayssLinearAlgoritm(double[,] a,double[] b)
        {
            ILinearAlgoritm jourdan = JordanGayss.GetInstance(a,b);
            return jourdan;
        }

        public static ILinearAlgoritm GetDoubleSimplex(double[,] A,double[] B,double[] Z)
        {
            ILinearAlgoritm simplexAlgoritm = DoubleSimplexMethod.GetInstance(A,B,Z);
            return simplexAlgoritm;
        }
    }

}
