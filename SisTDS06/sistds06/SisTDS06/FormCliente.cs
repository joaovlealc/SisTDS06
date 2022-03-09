using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace SisTDS06
{
    public partial class FormCliente : Form
    {
        public FormCliente()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
            Cliente cli = new Cliente();
            List<Cliente> clientes = cli.listacliente();
            dgvCliente.DataSource = clientes;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            try
            {
                Cliente cliente = new Cliente();
                cliente.Inserir(txtCPF.Text,  txtNome.Text, txtCEP.Text, txtEndereco.Text, txtBairro.Text, txtCidade.Text, txtCelular.Text, txtEmail.Text, dtpDT_Nascimento.Value);
                MessageBox.Show("Cliente cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> cli = cliente.listacliente();
                dgvCliente.DataSource = cli;
                txtCPF.Text = "";
                txtNome.Text = "";
                txtCelular.Text = "";
                this.dtpDT_Nascimento.Value = DateTime.Now.Date;
                txtCEP.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtBairro.Text = "";
                txtEmail.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                string cpf = txtCPF.Text;
                Cliente cliente = new Cliente();
                cliente.Localiza(cpf);
                txtNome.Text = cliente.nome;
                txtCelular.Text = cliente.celular;
                dtpDT_Nascimento.Value = Convert.ToDateTime(cliente.dt_nascimento);
                txtCEP.Text = cliente.cep;
                txtEndereco.Text = cliente.endereco;
                txtCidade.Text = cliente.cidade;
                txtBairro.Text = cliente.bairro;
                txtEmail.Text = cliente.email;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Atualizar(txtCPF.Text, txtNome.Text, txtCelular.Text, dtpDT_Nascimento.Value, txtCEP.Text, txtEndereco.Text, txtCidade.Text, txtBairro.Text, txtEmail.Text);
                MessageBox.Show("Cliente atualizado com sucesso!", "Edição", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> cli = cliente.listacliente();
                dgvCliente.DataSource = cli;
                txtCPF.Text = "";
                txtNome.Text = "";
                txtCelular.Text = "";
                this.dtpDT_Nascimento.Value = DateTime.Now.Date;
                txtCEP.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtBairro.Text = "";
                txtEmail.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int cpf = Convert.ToInt32(txtCPF.Text.Trim());
                Cliente cliente = new Cliente();
                cliente.Exclui(cpf);
                MessageBox.Show("Usuário excluído com sucesso!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> cli = cliente.listacliente();
                dgvCliente.DataSource = cli;
                txtCPF.Text = "";
                txtNome.Text = "";
                txtCelular.Text = "";
                this.dtpDT_Nascimento.Value = DateTime.Now.Date;
                txtCEP.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtBairro.Text = "";
                txtEmail.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnCEP_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + txtCEP.Text + "/json");
            request.AllowAutoRedirect = false;
            HttpWebResponse ChecaServidor = (HttpWebResponse)request.GetResponse();
            if (ChecaServidor.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Servidor Indisponível!");
                return; //Sai da rotina e para e codificação
            }
            using (Stream webStream = ChecaServidor.GetResponseStream())
            {
                if (webStream != null)
                {
                    using (StreamReader responseReader = new StreamReader(webStream))
                    {
                        string response = responseReader.ReadToEnd();
                        response = Regex.Replace(response, "[{},]", string.Empty);
                        response = response.Replace("\"", "");

                        String[] substrings = response.Split('\n');

                        int cont = 0;
                        foreach (var substring in substrings)
                        {
                            if (cont == 1)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                if (valor[0] == "  erro")
                                {
                                    MessageBox.Show("CEP não encontrado!");
                                    txtCEP.Focus();
                                    return;
                                }
                            }

                            //Endereço
                            if (cont == 2)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                txtEndereco.Text = valor[1];
                            }

                            //Bairro
                            if (cont == 4)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                txtBairro.Text = valor[1];
                            }

                            //Cidade
                            if (cont == 5)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                txtCidade.Text = valor[1];
                            }
                            cont++;
                        }
                    }
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCPF.Text = "";
            txtNome.Text = "";
            txtCelular.Text = "";
            this.dtpDT_Nascimento.Value = DateTime.Now.Date;
            txtCEP.Text = "";
            txtEndereco.Text = "";
            txtCidade.Text = "";
            txtBairro.Text = "";
            txtEmail.Text = "";
        }
    }
}
