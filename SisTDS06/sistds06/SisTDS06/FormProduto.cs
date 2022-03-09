using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisTDS06
{
    public partial class FormProduto : Form
    {
        public FormProduto()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormProduto_Load(object sender, EventArgs e)
        {
            Produto pro = new Produto();
            List<Produto> produtos = pro.listaproduto();
            dgvProduto.DataSource = produtos;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                int quantidade = Convert.ToInt32(txtQuantidade.Text.Trim());
                decimal valor = Convert.ToDecimal(txtValor.Text.Trim());
                Produto produto = new Produto();
                produto.Inserir(txtNome.Text, quantidade, valor);
                MessageBox.Show("Produto cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Produto> pro = produto.listaproduto();
                dgvProduto.DataSource = pro;
                txtNome.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
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
                int id = Convert.ToInt32(txtID.Text.Trim());
                Produto produto = new Produto();
                produto.Localiza(id);
                txtNome.Text = produto.nome;
                txtQuantidade.Text = Convert.ToString(produto.quantidade);
                txtValor.Text = Convert.ToString(produto.valor);
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
                int id = Convert.ToInt32(txtID.Text.Trim());
                int quantidade = Convert.ToInt32(txtQuantidade.Text.Trim());
                decimal valor = Convert.ToDecimal(txtValor.Text.Trim());
                Produto produto = new Produto();
                produto.Atualizar(id, txtNome.Text, quantidade, valor);
                MessageBox.Show("Produto atualizado com sucesso!", "Edição", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Produto> pro = produto.listaproduto();
                dgvProduto.DataSource = pro;
                txtID.Text = "";
                txtNome.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
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
                int id = Convert.ToInt32(txtID.Text.Trim());
                Produto produto = new Produto();
                produto.Exclui(id);
                MessageBox.Show("Produto excluído com sucesso!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Produto> pro = produto.listaproduto();
                dgvProduto.DataSource = pro;
                txtID.Text = "";
                txtNome.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtNome.Text = "";
            txtQuantidade.Text = "";
            txtValor.Text = "";
        }
    }
}
