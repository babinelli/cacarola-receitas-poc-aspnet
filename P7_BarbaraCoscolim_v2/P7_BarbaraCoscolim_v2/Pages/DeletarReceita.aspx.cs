using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P7_BarbaraCoscolim_v2.Pages
{
    public partial class DeletarReceita : System.Web.UI.Page
    {
        // Instanciar a conexão
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-I5G5799\SQLEXPRESS;Initial Catalog=Receitas_P7_v2;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Carregar dropdown da escolha do ID (table)
                FazerDropDownReceitaID();
            }
        }

        protected void DropDownListReceitaIDDeletar_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Desabilita seleção de receita
            DropDownListReceitaIDDeletar.Enabled = false;

            // Habilita botões
            ButtonDelete.Enabled = true;
            ButtonTrocarSelecaoReceita.Enabled = true;

            // Pegar id selecionado
            string receitaField = DropDownListReceitaIDDeletar.SelectedValue;

            // Converter receitaId para int
            int.TryParse(receitaField, out int receitaId);

            // Buscar receita a partir do ID
            Receita receita = Receita.BuscarReceita(sqlConnection, receitaId);

            // Preencher campos da receita (labels)
            LabelpNomeReceita.Text = receita.NomeReceita;
            LabelpCategoria.Text = Categoria.BuscarCategoria(sqlConnection, receita.CategoriaID); // Preenche com o nome da categoria
            LabelpModoPreparo.Text = receita.ModoPreparo;
            LabelpDificuldade.Text = receita.Dificuldade.ToString();
            LabelpTempoPreparo.Text = receita.TempoPreparo.ToString();
            LabelpDataRegisto.Text = receita.DataRegisto.ToShortDateString();
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            // Pegar ReceitaId
            string receitaField = DropDownListReceitaIDDeletar.SelectedValue;

            // Converter id para int
            int.TryParse(receitaField, out int receitaId);

            // Invocar método para deletar a receita da BD
            Receita.DeletarReceita(sqlConnection, receitaId);

            // Limpa os campos do formulário
            ResetForm();

            // Exibir mensagem de sucesso
            LabelMensagemSucesso.Text = "Deletado com sucesso!";
        }

        protected void ButtonTrocarSelecaoReceita_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        public void FazerDropDownReceitaID()
        {
            // Caso a conexão esteja fechada, abre a conexão
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // Criar um comando a partir da sp
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("uspReceitaIDViewAll", sqlConnection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Preenche a dataTable com as informações do dataAdapter
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            // Fecha a conexão
            sqlConnection.Close();

            // Habilitar dropdown para escolha da receita (mais uma garantia)
            DropDownListReceitaIDDeletar.Enabled = true;

            // Pegar os dados da tabela e popular o dropdown
            DropDownListReceitaIDDeletar.DataSource = dataTable;
            DropDownListReceitaIDDeletar.DataTextField = "NomeReceita";
            DropDownListReceitaIDDeletar.DataValueField = "ReceitaID";
            DropDownListReceitaIDDeletar.DataBind();

            // Adiciona texto por default
            DropDownListReceitaIDDeletar.Items.Insert(0, "Selecione...");
        }

        public void ResetForm()
        {
            // Atualizar dropdown
            FazerDropDownReceitaID();

            // Limpar campos do formulário
            DropDownListReceitaIDDeletar.SelectedIndex = 0;
            LabelpNomeReceita.Text = string.Empty;
            LabelpCategoria.Text = string.Empty;
            LabelpModoPreparo.Text = string.Empty;
            LabelpDificuldade.Text = string.Empty;
            LabelpTempoPreparo.Text = string.Empty;
            LabelpDataRegisto.Text = string.Empty;
            LabelMensagemSucesso.Text = string.Empty;
            LabelMensagemErro.Text = string.Empty;

            // Habilita dropdown para escolha da receita
            DropDownListReceitaIDDeletar.Enabled = true;

            // Desabilitar botões
            ButtonDelete.Enabled = false;
            ButtonTrocarSelecaoReceita.Enabled = false;
        }
    }
}