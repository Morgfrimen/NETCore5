using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЧисленныеМетоды
{
    internal class DoubleSimplexMethod : LinearAlgoritm
    {
        private double[,] _a;
        private double[] _b;
        private double[,] _value;
        private double _delta;


        private static DoubleSimplexMethod _instance;
        private DoubleSimplexMethod() { }

        internal static DoubleSimplexMethod GetInstance()
        {
            _instance ??= new DoubleSimplexMethod();
            return _instance;
        }

    }
}
