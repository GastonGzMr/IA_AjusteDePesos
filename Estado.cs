using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace IA_TP05_6
{
    public class Estado
    {
        public double PesoEntrada11;
        public double PesoEntrada12;
        
        public double PesoEntrada21;
        public double PesoEntrada22;

        public double PesoOculta11;
        public double PesoOculta12;

        public double PesoOculta21;
        public double PesoOculta22;

        public double PesoSalida11;
        public double PesoSalida12;

        public double PesoSalida21;
        public double PesoSalida22;

        public Estado(double pPesoEntrada11, double pPesoEntrada12, double pPesoEntrada21, double pPesoEntrada22,
                      double pPesoOculta11, double pPesoOculta12, double pPesoOculta21, double pPesoOculta22,
                      double pPesoSalida11, double pPesoSalida12, double pPesoSalida21, double pPesoSalida22)
        {
            PesoEntrada11 = pPesoEntrada11;
            PesoEntrada12 = pPesoEntrada12;
            
            PesoEntrada21 = pPesoEntrada21;
            PesoEntrada22 = pPesoEntrada22;
            
            PesoOculta11 = pPesoOculta11;
            PesoOculta12 = pPesoOculta12;

            PesoOculta21 = pPesoOculta21;
            PesoOculta22 = pPesoOculta22;

            PesoSalida11 = pPesoSalida11;
            PesoSalida12 = pPesoSalida12;

            PesoSalida21 = pPesoSalida21;
            PesoSalida22 = pPesoSalida22;
        }

        public Estado(Estado padre1, Estado padre2)
        {
            PesoEntrada11 = (padre1.PesoEntrada11 + padre2.PesoEntrada11) / 2;
            PesoEntrada12 = (padre1.PesoEntrada12 + padre2.PesoEntrada12) / 2;

            PesoEntrada21 = (padre1.PesoEntrada21 + padre2.PesoEntrada21) / 2;
            PesoEntrada22 = (padre1.PesoEntrada22 + padre2.PesoEntrada22) / 2;

            PesoOculta11 = (padre1.PesoOculta11 + padre2.PesoOculta11) / 2;
            PesoOculta12 = (padre1.PesoOculta12 + padre2.PesoOculta12) / 2;

            PesoOculta21 = (padre1.PesoOculta21 + padre2.PesoOculta21) / 2;
            PesoOculta22 = (padre1.PesoOculta22 + padre2.PesoOculta22) / 2;

            PesoSalida11 = (padre1.PesoSalida11 + padre2.PesoSalida11) / 2;
            PesoSalida12 = (padre1.PesoSalida12 + padre2.PesoSalida12) / 2;

            PesoSalida21 = (padre2.PesoSalida21 + padre2.PesoSalida21) / 2;
            PesoSalida22 = (padre2.PesoSalida22 + padre2.PesoSalida22) / 2;

            Mutar();
        }

        private void Mutar()
        {
            Random random = new Random();
            string atributoAModificar = "Peso";
            switch(random.Next(1, 3))
            {
                case 1:
                    atributoAModificar += "Entrada";
                    break;
                case 2:
                    atributoAModificar += "Oculta";
                    break;
                case 3:
                    atributoAModificar += "Salida";
                    break;
            }
            atributoAModificar += random.Next(1, 2);
            atributoAModificar += random.Next(1, 2);
            double valor = (double)typeof(Estado).GetField(atributoAModificar).GetValue(this);
            valor += (random.Next(-100, 100) / 100) * valor;
            typeof(Estado).GetField(atributoAModificar).SetValue(this, valor);
        }

        public double[] Ejecutar(double x, double y)
        {
            double[] entradas = new double[] { x, y };
            
            Neurona entrada1 = new Neurona(entradas, new double[] { PesoEntrada11, PesoEntrada12 });
            Neurona entrada2 = new Neurona(entradas, new double[] { PesoEntrada21, PesoEntrada22 });
            entradas = new double[] { entrada1.ObtenerSalida(), entrada2.ObtenerSalida() };

            Neurona oculta1 = new Neurona(entradas, new double[] { PesoOculta11, PesoOculta12 });
            Neurona oculta2 = new Neurona(entradas, new double[] { PesoOculta21, PesoOculta22 });
            entradas = new double[] { oculta1.ObtenerSalida(), oculta2.ObtenerSalida() };

            Neurona salida1 = new Neurona(entradas, new double[] { PesoSalida11, PesoSalida12 });
            Neurona salida2 = new Neurona(entradas, new double[] { PesoSalida21, PesoSalida22 });
            return new double[] { salida1.ObtenerSalida(), salida2.ObtenerSalida() };
        }

        public double ObtenerAjuste(double[] punto)
        {
            double ajuste = 0;
            double[] resultado;
            resultado = Ejecutar(punto[0], punto[1]);

            if (punto[0] > 0)
            {
                ajuste += Math.Abs(resultado[0] - 1);
            }
            else
            {
                ajuste += Math.Abs(resultado[0] - 0);
            }

            if (punto[1] > 0)
            {
                ajuste += Math.Abs(resultado[1] - 1);
            }
            else
            {
                ajuste += Math.Abs(resultado[1] - 0);
            }
            return ajuste;
        }

        public double[] ObtenerPesos()
        {
            return new double[]{ PesoEntrada11, PesoEntrada12, PesoEntrada21, PesoEntrada22, PesoOculta11,
                                 PesoOculta12, PesoOculta21, PesoOculta22, PesoSalida11, PesoSalida12,
                                 PesoSalida21, PesoSalida22};
        }
    } 
}
