namespace Entidades
{
    #region ENUMERADO
    public enum ESistema
    {
        Decimal,
        Binario
    }
    #endregion

    public class Numeracion
    {
        private ESistema sistema;
        private double valorNumerico;

        public Numeracion(double valor, ESistema sistema)
        {
            this.sistema = sistema;
            this.valorNumerico = valor;
        }

        public Numeracion(string valor, ESistema sistema)
        {
            InicializarValores(valor, sistema);
        }

        public ESistema Sistema
        {
            get { return sistema; }
        }

        public string Valor
        {
            get { return valorNumerico.ToString(); }
        }

        private double BinarioADecimal(string valor)
        {
            double retorno = 0;

            if (EsBinario(valor))
            {
                char[] array = valor.ToCharArray();
                Array.Reverse(array);

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == '1')
                    {
                        // Se usa la potencia de 2, según la posición, porque se dio vuelta el array
                        retorno += (double)Math.Pow(2, i);
                    }
                }
            }
            return retorno;
        }

        public string ConvertirA(ESistema sistema)
        {
            string numConvertido;

            if (sistema == ESistema.Decimal)
            {
                numConvertido = this.valorNumerico.ToString();
            }
            else
            {
                numConvertido = DecimalABinario((int)this.valorNumerico);
            }

            return numConvertido;
        }

        private string DecimalABinario(int valor)
        {
            string retorno;

            int numMax = 33554432;
            string numBinario = "";

            if (valor >= 0)
            {
                for (int i = 0; i < 26; i++)
                {
                    if (valor >= numMax)
                    {
                        numBinario += "1";
                        valor = valor - numMax;
                    }
                    else
                    {
                        numBinario += "0";
                    }
                    numMax = numMax / 2;
                }
                retorno = numBinario;
            }
            else
            {
                retorno = "Numero inválido";
            }
            return retorno;
        }

        private string DecimalABinario(string valor)
        {
            string retorno;
            if (int.TryParse(valor, out int result) && result > 0)
            {
                retorno = DecimalABinario(result);
            }
            else
            {
                retorno = "Numero inválido";
            }
            return retorno;
        }

        private bool EsBinario(string valor)
        {
            bool retorno = true;

            foreach (var item in valor)
            {
                if (item != '0' && item != '1')
                {
                    retorno = false;
                    break;
                }
            }
            return retorno;
        }

        private void InicializarValores(string valor, ESistema sistema)
        {
            if (EsBinario(valor))
            {
                valorNumerico = BinarioADecimal(valor);
            }
            else if (sistema == ESistema.Decimal)
            {
                double.TryParse(valor, out valorNumerico);
            }
            else
            {
                valorNumerico = double.MinValue;
            }
        }

        public static bool operator ==(ESistema sistema, Numeracion numeracion)
        {
            return sistema == numeracion.sistema;
        }

        public static bool operator !=(ESistema sistema, Numeracion numeracion)
        {
            return !(sistema == numeracion);
        }

        public static bool operator ==(Numeracion n1, Numeracion n2)
        {
            return n1.sistema == n2.sistema;
        }

        public static bool operator !=(Numeracion n1, Numeracion n2)
        {
            return !(n1 == n2);
        }

        public static Numeracion operator -(Numeracion n1, Numeracion n2)
        {
            double result = n1.valorNumerico - n2.valorNumerico;
            Numeracion aux = new Numeracion(result, ESistema.Decimal);
            return aux;
        }

        public static Numeracion operator *(Numeracion n1, Numeracion n2)
        {
            double result = n1.valorNumerico * n2.valorNumerico;
            Numeracion aux = new Numeracion(result, ESistema.Decimal);
            return aux;
        }

        public static Numeracion operator /(Numeracion n1, Numeracion n2)
        {
            double result = n1.valorNumerico / n2.valorNumerico;
            Numeracion aux = new Numeracion(result, ESistema.Decimal);
            return aux;
        }

        public static Numeracion operator +(Numeracion n1, Numeracion n2)
        {
            double result = n1.valorNumerico + n2.valorNumerico;
            Numeracion aux = new Numeracion(result, ESistema.Decimal);
            return aux;
        }
    }
}