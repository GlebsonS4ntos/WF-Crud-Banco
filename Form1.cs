using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace App11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizarDataGrid();
            btnAtualizar.Enabled = false;
            btnDeletar.Enabled = false;
            btnInsert.Enabled = false;
            txtId.Enabled = false;
        }

        private void AtualizarDataGrid()
        {
            string con = @"Data Source=DESKTOP-7Q70RLR\SQLEXPRESS;Initial Catalog=bdVendas;Integrated Security=True";
            
            SqlConnection sqlCon = new SqlConnection(con);
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("Select * From Produto", sqlCon);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter();

            DataSet ds = new DataSet();
            ad.SelectCommand = cmd;
            ad.Fill(ds);

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].TableName;

            sqlCon.Close();
        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtPreco.Text = "";
            txtQuantidade.Text = "";
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string con = @"Data Source=DESKTOP-7Q70RLR\SQLEXPRESS;Initial Catalog=bdVendas;Integrated Security=True";
            string insert = "Insert into Produto(NomeProduto, Preco, Quantidade) values" +
                "('" + txtNome.Text + "','" + decimal.Parse(txtPreco.Text) +"','"+int.Parse(txtQuantidade.Text)+"')";
            SqlConnection sqlCon = new SqlConnection(con);

            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand(insert, sqlCon);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    MessageBox.Show("Produto Cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                sqlCon.Close();
                LimparCampos();
                AtualizarDataGrid();
            }
        }

        private void txtPreco_TextChanged(object sender, EventArgs e)
        {
            if(txtPreco.Text != "" && txtNome.Text != "" && txtQuantidade.Text != "")
                btnInsert.Enabled = true;
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            if (txtPreco.Text != "" && txtNome.Text != "" && txtQuantidade.Text != "")
                btnInsert.Enabled = true;
        }

        private void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            if (txtPreco.Text != "" && txtNome.Text != "" && txtQuantidade.Text != "")
                btnInsert.Enabled = true;
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            string con = @"Data Source=DESKTOP-7Q70RLR\SQLEXPRESS;Initial Catalog=bdVendas;Integrated Security=True";
            SqlConnection sqlCom = new SqlConnection(con);
            string delete = "Delete from Produto where ProdutoId = '" + txtId.Text +"'"; 

            SqlCommand cmd = new SqlCommand(delete, sqlCom);
            cmd.CommandType = CommandType.Text;

            try
            {
                sqlCom.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Deletado com Sucesso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCom.Close();
                LimparCampos();
                AtualizarDataGrid();
                btnDeletar.Enabled = false;
                btnAtualizar.Enabled = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtPreco.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtQuantidade.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            btnAtualizar.Enabled = true;
            btnDeletar.Enabled = true;
            btnInsert.Enabled = false;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            string con = @"Data Source=DESKTOP-7Q70RLR\SQLEXPRESS;Initial Catalog=bdVendas;Integrated Security=True";
            SqlConnection sqlCom = new SqlConnection(con);

            string sql = "Update produto set NomeProduto = '" + txtNome.Text + "', Preco = '" + double.Parse(txtPreco.Text) +"', Quantidade= '" + txtQuantidade.Text + "' where ProdutoId = '" + txtId.Text + "'";

            SqlCommand cmd = new SqlCommand(sql, sqlCom);
            cmd.CommandType = CommandType.Text;

            try
            {
                sqlCom.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Alterado com Sucesso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCom.Close();
                LimparCampos();
                AtualizarDataGrid();
                btnDeletar.Enabled = false;
                btnAtualizar.Enabled = false;
            }
        }
    }
}
