using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_TP05_6
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] punto = new double[] { -3, 5 };
            List<Estado> estados = new List<Estado>();
            Random random = new Random();
            double ajusteMinimo;
            Estado padre1;
            Estado padre2;
            Estado hijo1;
            Estado hijo2;
            int cantidadDePasos = 0;

            for (int i = 0; i < 25; i++)
            {
                estados.Add(new Estado(random.Next(-5, 5), random.Next(-5, 5), random.Next(-5, 5), random.Next(-5, 5),
                                       random.Next(-5, 5), random.Next(-5, 5), random.Next(-5, 5), random.Next(-5, 5),
                                       random.Next(-5, 5), random.Next(-5, 5), random.Next(-5, 5), random.Next(-5, 5)));
            }

            estados = estados.OrderBy(x => x.ObtenerAjuste(punto)).ToList();
            padre1 = estados[0];
            padre2 = estados[1];
            ajusteMinimo = padre1.ObtenerAjuste(punto);

            while (ajusteMinimo > 0)
            {
                cantidadDePasos++;
                hijo1 = new Estado(padre1, padre2);
                if (!(estados.Where(x => Enumerable.SequenceEqual(x.ObtenerPesos(), hijo1.ObtenerPesos())).Count() > 0))
                {
                    estados.Add(hijo1);
                }

                hijo2 = new Estado(padre2, padre1);
                if (!(estados.Where(x => Enumerable.SequenceEqual(x.ObtenerPesos(), hijo2.ObtenerPesos())).Count() > 0))
                {
                    estados.Add(hijo2);
                }

                estados = estados.OrderBy(x => x.ObtenerAjuste(punto)).ToList();
                padre1 = estados[0];
                padre2 = estados[1];
                ajusteMinimo = padre1.ObtenerAjuste(punto);
            }

            Console.WriteLine("Solución encontrada en " + cantidadDePasos + " pasos con los pesos: ");
            Escribir(padre1.ObtenerPesos());
            Console.Write("Cuadrante: ");
            Escribir(padre1.Ejecutar(punto[0], punto[1]));
            Console.ReadLine();
        }

        public static void Escribir(double[] array)
        {
            foreach (double peso in array)
            {
                Console.Write(peso + " ");
            }
        }
    }
}
