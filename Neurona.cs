using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_TP05_6
{
    public class Neurona
    {
        public double[] Entradas;
        public double[] Pesos;

        public Neurona(double[] pEntradas, double[] pPesos)
        {
            Entradas = pEntradas;
            Pesos = pPesos;
        }

        public double ObtenerSalida() 
        {
            double salida = 0;
            for (int i = 0; i < Entradas.Length; i++)
            {
                salida += Pesos[i] * Entradas[i];
            }
            return Math.Round(Math.Tanh(salida),2);
        }
    }
}
