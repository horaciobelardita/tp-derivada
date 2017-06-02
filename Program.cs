using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {

        public string formato(string funcion)

        {

            string salir = "";

            int j = 0;

            for (int i = 0; i < funcion.Length - 1; i++)

            {

                if ((funcion[i] == '-' || funcion[i] == '+') && i > 0)

                {

                    if (funcion[i - 1] != '^')

                    {

                        salir += funcion.Substring(j, i - j) + " ";

                        j = i;

                    }

                }

            }

            salir += funcion.Substring(j, funcion.Length - j);

            return salir;

        }   //funciona


        public string derivar(string funcion)

        {

            string salida = "";

            string[] terminos;

            string dx;

            bool EsPositivo = true;

            double potencia = 0, coeficiente = 0;

            funcion = formato(funcion);

            terminos = funcion.Split();

            foreach (string termino in terminos)

            {

                separar(termino, ref coeficiente, ref potencia, ref EsPositivo);

                dx = derivarTermino(coeficiente, potencia, EsPositivo);

                salida += dx;

            }

            if (!(salida.Trim().Length > 0))

            {

                return "0";

            }

            if (salida[1] == '+')

            {

                return salida.Substring(3);

            }

            if (salida[1] == '-')

            {

                return Convert.ToString(salida[1]) + salida.Substring(3);

            }

            if (salida[0] == ' ')

            {

                return salida.Substring(1);

            }

            return salida;

        }

        private void separar(string termino, ref double coeficiente, ref double potencia, ref bool EsPositivo)

        {

            int fincoeficiente = termino.Length;

            int inicio = 0;

            EsPositivo = true;

            if (termino[0] == '-')

            {

                EsPositivo = false;

                inicio = 1;

            }

            else if (termino[0] == '+')

            {


                inicio = 1;

            }

            else

            {

                inicio = 0;

            }

            if (termino == Convert.ToString('x'))

            {

                coeficiente = 1;

            }

            else

            {

                for (int i = inicio; i < termino.Length; i++)

                {

                    if (!(char.IsNumber(termino[i])))

                    {

                        fincoeficiente = i;

                        break;

                    }

                }

                string c;

                if (fincoeficiente <= 0)

                {

                    c = termino.Substring(inicio, inicio + 1);



                }

                else
                {
                    c = termino.Substring(inicio, fincoeficiente - inicio);
                }

                if (c == Convert.ToString('x') || c.Length == 0)

                {

                    coeficiente = 1;

                }

                else

                {

                    coeficiente = Double.Parse(c);

                }

            }



            if (termino.IndexOf("^") >= 0 && termino.IndexOf("(") < 0)

            {

                potencia = Convert.ToInt32(termino.Substring(termino.IndexOf("^") + 1));

            }

            else if (termino.IndexOf("x") >= 0 && termino.IndexOf("^") < 0)

            {

                potencia = 1;

            }

            else if (termino.IndexOf(")") >= 0)

            {

                string fraccion;

                inicio = termino.IndexOf("(") + 1;

                fraccion = termino.Substring(inicio, (termino.Length - inicio) - 1);

                potencia = fraccionADecimal(fraccion);

            }

            else

            {

                potencia = 0;

            }

        }

        private Single fraccionADecimal(string fraccion)

        {

            int primer, segundo;

            primer = Convert.ToInt32(fraccion.Substring(0, fraccion.IndexOf('/')));

            segundo = Convert.ToInt32(fraccion.Substring(fraccion.IndexOf('/') + 1));

            return primer / segundo;

        }

        private string derivarTermino(double coeficiente, double potencia, bool EsPositivo)

        {

            string salida = "";

            double termino;

            if (!(EsPositivo))

            {

                if (potencia < 0)

                {

                    salida += " + ";

                }

                else

                {

                    salida += " - ";

                }

            }

            else if (potencia >= 0)

            {

                salida += " + ";

            }

            else

            {

                salida += " - ";

            }

            termino = Math.Abs(coeficiente * potencia);

            switch (potencia)

            {

                case 1:

                    return salida + termino;



                case 2:

                    return salida + termino + "x";

                case 0:

                    return "";

                default:

                    if (termino != 1)

                    {

                        return salida + termino + "x^" + (potencia - 1);

                    }

                    else

                    {

                        return salida + "x^" + (potencia - 1);

                    }

            }

        }




        static void Main(string[] args)
        {
            Program derivar = new Program();
            double coeficiente = 0, potencia = 0;
            bool EsPositivo = true;
            Console.WriteLine(derivar.formato("-2x+10x+5x^3"));
            derivar.separar("+2x", ref coeficiente, ref potencia, ref EsPositivo);
            Console.WriteLine(coeficiente + " "+ potencia);
            derivar.separar("10x", ref coeficiente, ref potencia, ref EsPositivo);
            Console.WriteLine(coeficiente + " " + potencia);
            derivar.separar("10", ref coeficiente, ref potencia, ref EsPositivo);
            Console.WriteLine(coeficiente + " " + potencia);
            derivar.separar("-5x^3", ref coeficiente, ref potencia, ref EsPositivo);
            Console.WriteLine(coeficiente + " " + potencia);
            derivar.separar("-5x", ref coeficiente, ref potencia, ref EsPositivo);
            Console.WriteLine(coeficiente + " " + potencia);
            derivar.separar("x", ref coeficiente, ref potencia, ref EsPositivo);
            Console.WriteLine(coeficiente + " " + potencia);
            derivar.separar("-x", ref coeficiente, ref potencia, ref EsPositivo);
            Console.WriteLine(coeficiente + " " + potencia);
            derivar.separar("-10", ref coeficiente, ref potencia, ref EsPositivo);
            Console.WriteLine(coeficiente + " " + potencia);

            Console.WriteLine("TEST DE DERIVADA");

            Console.WriteLine(derivar.derivar("x^4-3x^5+2x^2+5"));
            Console.WriteLine(derivar.derivar("1-x^4"));
            Console.WriteLine(derivar.derivar("x^3+x^5+x^2"));
            Console.WriteLine(derivar.derivar("x^3-x-7/2"));

            Console.ReadKey();
        }
    }
}
