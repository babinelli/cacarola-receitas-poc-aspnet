using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace P7_BarbaraCoscolim_v2
{
    public class Receita : IReceita
    {
        #region Enumerators
        public enum EnumDificuldade
        {
            Fácil,
            Moderado,
            Difícil
        }
        #endregion

        #region Properties
        public int ReceitaID { get; set; }
        public string NomeReceita { get; set; }
        public int CategoriaID { get; set; }
        public string ModoPreparo { get; set; }
        public EnumDificuldade Dificuldade { get; set; }
        public int TempoPreparo { get; set; }
        public DateTime DataRegisto { get; set; }
        public List<Ingrediente> Ingredientes { get; set; }
        #endregion

        #region Constructor
        // Constructor para buscar receitas
        public Receita()
        {
            ReceitaID = 0;
            NomeReceita = string.Empty;
            CategoriaID = 0;
            ModoPreparo = string.Empty;
            Dificuldade = EnumDificuldade.Fácil;
            TempoPreparo = 0;
            DataRegisto = DateTime.Today;
        }

        // Construtor para inserir nova receita
        public Receita(string nomeReceita, int categoriaId, string modoPreparo, EnumDificuldade dificuldade, int tempoPreparo, DateTime dataRegisto, List<Ingrediente> ingredientes)
        {
            ReceitaID = 0;
            NomeReceita = nomeReceita;
            CategoriaID = categoriaId;
            ModoPreparo = modoPreparo;
            Dificuldade = dificuldade;
            TempoPreparo = tempoPreparo;
            DataRegisto = dataRegisto;
            Ingredientes = ingredientes;
        }

        // Construtor para atualizar receita
        public Receita(int receitaId, string nomeReceita, int categoriaId, string modoPreparo, EnumDificuldade dificuldade, int tempoPreparo, DateTime dataRegisto)
        {
            ReceitaID = receitaId;
            NomeReceita = nomeReceita;
            CategoriaID = categoriaId;
            ModoPreparo = modoPreparo;
            Dificuldade = dificuldade;
            TempoPreparo = tempoPreparo;
            DataRegisto = dataRegisto;
        }
        #endregion

        #region Methods
        public void InserirReceita(SqlConnection sqlConnection)
        {
            // Se a conexão estiver fechada, abre
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // Cria o comando de Add Or Update, usando a SP
            SqlCommand sqlCommand = new SqlCommand("uspAddOrUpdateReceita", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            // Adiciona valores aos parameters
            sqlCommand.Parameters.AddWithValue("@receitaId", this.ReceitaID); // 0, caso construtor não tenha o parameter
            sqlCommand.Parameters.AddWithValue("@categoriaId", this.CategoriaID);
            sqlCommand.Parameters.AddWithValue("@nomeReceita", this.NomeReceita);
            sqlCommand.Parameters.AddWithValue("@modoPreparo", this.ModoPreparo);
            sqlCommand.Parameters.AddWithValue("@dificuldade", Enum.GetName(typeof(EnumDificuldade), this.Dificuldade)); // Pega o "name" do Enum
            sqlCommand.Parameters.AddWithValue("@tempoPreparo", this.TempoPreparo);
            sqlCommand.Parameters.AddWithValue("@dataRegisto", this.DataRegisto);

            // Executa a Query
            this.ReceitaID = (int)sqlCommand.ExecuteScalar();

            // Fecha a conexão
            sqlConnection.Close();

            // Para cada ingrediente da lista
            foreach (Ingrediente ingrediente in this.Ingredientes)
            {
                ingrediente.ReceitaID = this.ReceitaID;

                ingrediente.InserirIngrediente(sqlConnection);
            }
        }

        public void AtualizarReceita(SqlConnection sqlConnection)
        {
            // Se a conexão estiver fechada, abre
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // Cria o comando de Add Or Update, usando a SP
            SqlCommand sqlCommand = new SqlCommand("uspAddOrUpdateReceita", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            // Adiciona valores aos parameters
            sqlCommand.Parameters.AddWithValue("@receitaId", this.ReceitaID); // 0, caso construtor não tenha o parameter
            sqlCommand.Parameters.AddWithValue("@categoriaId", this.CategoriaID);
            sqlCommand.Parameters.AddWithValue("@nomeReceita", this.NomeReceita);
            sqlCommand.Parameters.AddWithValue("@modoPreparo", this.ModoPreparo);
            sqlCommand.Parameters.AddWithValue("@dificuldade", Enum.GetName(typeof(EnumDificuldade), this.Dificuldade)); // Pega o "name" do Enum
            sqlCommand.Parameters.AddWithValue("@tempoPreparo", this.TempoPreparo);
            sqlCommand.Parameters.AddWithValue("@dataRegisto", this.DataRegisto);

            // Executa a Query
            sqlCommand.ExecuteNonQuery();

            // Fecha a conexão
            sqlConnection.Close();

        }

        public static void DeletarReceita(SqlConnection sqlConnection, int receitaId)
        {
            // Se a conexão estiver fechada, abre
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // Cria o comando de Delete, usando a SP
            SqlCommand sqlCommand = new SqlCommand("uspDeleteReceitaById", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            // Adiciona parameter ao comando
            sqlCommand.Parameters.AddWithValue("@receitaId", receitaId);

            // Executa a query
            sqlCommand.ExecuteNonQuery();

            // Fecha a conexão
            sqlConnection.Close();
        }

        public static DataTable PegarReceitasBD(SqlConnection sqlConnection)
        {
            try
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

                return dataTable;
            }
            catch (Exception)
            {
                return null;

            }

        }

        public static Receita BuscarReceita(SqlConnection sqlConnection, int receitaId)
        {
            // Se a conexão estiver fechada, abre
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // Cria o comando usando a SP
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("uspReceitaSearchById", sqlConnection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Adiciona valor ao parameter
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@receitaId", receitaId);

            // Preenche a dataTable a partir do dataAdapter
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            // Fecha a conexão
            sqlConnection.Close();

            // Instancia um objeto receita
            Receita receita = new Receita();

            // Atribui o valor dos campos da base de dados às propriedades do objeto
            receita.ReceitaID = (int)dataTable.Rows[0]["ReceitaID"];
            receita.CategoriaID = (int)dataTable.Rows[0]["CategoriaID"];
            receita.NomeReceita = dataTable.Rows[0]["NomeReceita"].ToString();
            receita.ModoPreparo = dataTable.Rows[0]["ModoPreparo"].ToString();

            string dif = dataTable.Rows[0]["Dificuldade"].ToString();
            Enum.TryParse(dif, out EnumDificuldade dificuldade);
            receita.Dificuldade = dificuldade;

            receita.TempoPreparo = (int)dataTable.Rows[0]["TempoPreparo"];
            receita.DataRegisto = (DateTime)dataTable.Rows[0]["DataRegisto"];

            return receita;
        }

        public static bool ValidarCamposPreenchidos(string nomeReceita, string modoPreparo, string tempoPreparo)
        {
            return (nomeReceita != string.Empty && modoPreparo != string.Empty && tempoPreparo != string.Empty);
        }

        public static int ValidarTempoPreparo(string tempoPreparo)
        {
            int tempoPreparoInt = 0;
            int.TryParse(tempoPreparo, out tempoPreparoInt);

            return tempoPreparoInt;
        }


        #endregion

    }
}