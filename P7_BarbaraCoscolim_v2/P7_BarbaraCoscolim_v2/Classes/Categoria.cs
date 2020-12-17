using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace P7_BarbaraCoscolim_v2
{
    public class Categoria : ICategoria
    {
        #region Properties
        public int CategoriaID { get; set; }
        public string NomeCategoria { get; set; }
        #endregion

        #region Methods
        public static string BuscarCategoria(SqlConnection sqlConnection, int categoriaId)
        {
            // Se a conexão estiver fechada, abre
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // Cria o comando usando a sp
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("uspCategoriaSearchById", sqlConnection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Adiciona valor ao parameter
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@categoriaId", categoriaId);

            // Preenche a dataTable a partir do dataAdapter
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            // Fecha a conexão
            sqlConnection.Close();

            string categoria = dataTable.Rows[0]["NomeCategoria"].ToString();

            return categoria;
        }
        #endregion
    }
}