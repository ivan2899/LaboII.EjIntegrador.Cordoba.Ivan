using Entidades;
using System.Runtime.Intrinsics.X86;

namespace MiCalculadora
{
    public partial class FrmCalculadora : Form
    {
        private Numeracion primerOperando;
        private Numeracion segundoOperando;
        private Numeracion resultado;
        private ESistema sistema;
        private Operacion calculadora;

        public FrmCalculadora()
        {
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            InitializeComponent();
        }

        private void FrmCalculadora_Load(object sender, EventArgs e)
        {
            cmbOperacion.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.txtPrimerOperador.Text = "";
            this.txtSegundoOperador.Text = "";
            this.cmbOperacion.Text = "";
            this.rdbDecimal.Checked = true;
            sistema = ESistema.Decimal;
            this.lblResultado.Text = "Resultado: ";
        }

        private void btnOperar_Click(object sender, EventArgs e)
        {
            setResultado();
        }

        private void FrmCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult msjCierre;
            msjCierre = MessageBox.Show("Desea cerrar la calculadora?", "Cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (msjCierre == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void rdbDecimal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDecimal.Checked == true)
            {
                sistema = ESistema.Decimal;
                setResultado();
            }
        }

        private void rdbBinario_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbBinario.Checked == true)
            {
                sistema = ESistema.Binario;
                setResultado();
            }
        }

        private void txtPrimerOperador_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.txtPrimerOperador.Text, out double aux);
            primerOperando = new Numeracion(aux, ESistema.Decimal);
        }

        private void txtSegundoOperador_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.txtSegundoOperador.Text, out double aux);
            segundoOperando = new Numeracion(aux, ESistema.Decimal);

        }

        private void setResultado()
        {
            string msjResultado;

            if ((txtPrimerOperador.Text != "") && (txtSegundoOperador.Text != ""))
            {
                string operador = this.cmbOperacion.Text;
                char auxOperador = '\n';

                for (int i = 0; i < operador.Length; i++)
                {
                    auxOperador = (char)operador[i];
                }
                calculadora = new Operacion(primerOperando, segundoOperando);
                resultado = calculadora.Operar(auxOperador);

                if (this.sistema == ESistema.Decimal)
                {
                    msjResultado = resultado.Valor;
                }
                else
                {
                    string auxNumeroConvertido = resultado.ConvertirA(sistema);
                    if (double.TryParse(auxNumeroConvertido, out double res))
                    {
                        Numeracion io = new Numeracion(res, ESistema.Binario);
                        resultado = io;
                        msjResultado = resultado.Valor;
                    }
                    else
                    {
                        msjResultado = auxNumeroConvertido;
                    }
                }
            }
            else
            {
                msjResultado = "Coloque ambos numeros";
            }

            this.lblResultado.Text = $"Resultado: {msjResultado}";
        }
    }
}