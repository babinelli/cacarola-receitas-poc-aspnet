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
    public partial class Detalhes : System.Web.UI.Page
    {
        // Instanciar a conexão
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-I5G5799\SQLEXPRESS;Initial Catalog=Receitas_P7_v2;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            // Pegar o receitaId a partir da querystring
            int receitaId = int.Parse(Context.Request.QueryString["receitaId"]);

            // Buscar a receita pelo Id
            Receita receita = Receita.BuscarReceita(sqlConnection, receitaId);

            // Preencher labels com dados da receita
            LabelNomepReceita.Text = receita.NomeReceita;
            LabelpCategoria.Text = Categoria.BuscarCategoria(sqlConnection, receita.CategoriaID);
            LabelpModoPreparo.Text = receita.ModoPreparo;
            LabelpDificuldade.Text = receita.Dificuldade.ToString();
            LabelpTempoPreparo.Text = receita.TempoPreparo.ToString();
            LabelpDataRegisto.Text = receita.DataRegisto.ToShortDateString();

            FillGridViewIngredientes(sqlConnection, receitaId);
        }

        public void FillGridViewIngredientes(SqlConnection sqlConnection, int receitaId)
        {
            DataTable dataTable = Ingrediente.ListarIngredientes(sqlConnection, receitaId);

            if (dataTable.Rows.Count == 0)
            {
                LabelMensagemErro.Text = "Não foram registados ingredientes para esta receita.";
            }
            else
            {
                // Preenche a Grid com os dados da dataTable
                GridViewIngredientes.DataSource = dataTable;
                GridViewIngredientes.DataBind();
            }

        }
    }
}