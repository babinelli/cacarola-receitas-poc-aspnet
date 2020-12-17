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
    public partial class ListarReceitas : System.Web.UI.Page
    {
        // Instanciar a conexão
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-I5G5799\SQLEXPRESS;Initial Catalog=Receitas_P7_v2;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                // Invocar método que pega as receitas da base de dados
                DataTable dataTable = Receita.PegarReceitasBD(sqlConnection);

                if (dataTable == null)
                {
                    LabelMensagemErro.Text = "Nenhuma receita foi registada ainda... Seja o primeiro a inserir uma receita!";
                    return;
                }

                // Preencher GridView com os dados da BD
                GridViewReceitas.DataSource = dataTable;
                GridViewReceitas.DataBind();

            }
        }

        public void FillGridViewReceitas()
        {
            // Caso a conexão esteja fechada, abre a conexão
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // Cria um comando com a sp
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("uspReceitaViewAll", sqlConnection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Popula a dataTable com as informações do dataAdapter
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            // Fecha a conexão
            sqlConnection.Close();

            // Preenche a Grid com os dados da dataTable
            GridViewReceitas.DataSource = dataTable;
            GridViewReceitas.DataBind();
        }

        protected void LinkButtonDetalhe_Click(object sender, EventArgs e)
        {
            // Guarda na variável o que vem do CommandArgument, após converter para int
            // O sender é quem envia os dados do formulário (cliente) para o servidor
            int receitaID = Convert.ToInt16((sender as LinkButton).CommandArgument);

            // Direcionar para a página de detalhes
            Response.Redirect("Detalhes.aspx?receitaId=" + receitaID);

            
            
            
        }
    }
}