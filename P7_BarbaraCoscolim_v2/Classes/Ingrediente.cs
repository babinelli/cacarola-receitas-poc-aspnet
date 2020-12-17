using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace P7_BarbaraCoscolim_v2
{
    public class Ingrediente : IIngrediente
    {
        #region Properties
        public int IngredienteID { get; set; }
        public int ReceitaID { get; set; }
        public string NomeIngrediente { get; set; }
        public string Quantidade { get; set; }

        #endregion

        #region Constructors
        public Ingrediente(string nomeIngrediente, string quantidade)
        {
            NomeIngrediente = nomeIngrediente;
            Quantidade = quantidade;
        }
        public Ingrediente(int receitaId, string nomeIngrediente, string quantidade)
        {
            ReceitaID = receitaId;
            NomeIngrediente = nomeIngrediente;
            Quantidade = quantidade;
        }
        #endregion

        #region Methods
        public static bool ValidarCamposPreenchidos(string ingrediente, string quantidade)
        {
            return (ingrediente != string.Empty && quantidade != string.Empty);
        }

        public void InserirIngrediente(SqlConnection sqlConnection)
        {
            // Se a conexão estiver fechada, abre
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // Cria o comando de Add or Update, usando a sp
            SqlCommand sqlCommand = new SqlCommand("uspAddIngrediente", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            // Adiciona valores aos parameters
            sqlCommand.Parameters.AddWithValue("@receitaId", this.ReceitaID);
            sqlCommand.Parameters.AddWithValue("@nomeIngrediente", this.NomeIngrediente);
            sqlCommand.Parameters.AddWithValue("@quantidade", this.Quantidade);
            
            // Executa a query
            sqlCommand.ExecuteNonQuery();

            // Fecha a conexão
            sqlConnection.Close();

            
        }

        public static DataTable ListarIngredientes(SqlConnection sqlConnection, int receitaId)
        {
            // Se a conexão estiver fechada, abre
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // Cria o comando com a sp
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("uspIngredientesByReceitaId", sqlConnection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Adiciona valores aos parameters
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@receitaId", receitaId);

            // Popula a dataTable com as informações do dataAdapter
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            // Fecha a conexão
            sqlConnection.Close();

            return dataTable;
        }
        #endregion
    }
}