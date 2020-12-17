using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace P7_BarbaraCoscolim_v2
{
    interface IReceita
    {
        #region Properties
        int ReceitaID { get; }
        string NomeReceita { get; }
        int CategoriaID { get; }
        string ModoPreparo { get; }
        int TempoPreparo { get; }
        DateTime DataRegisto { get; }
        #endregion

        #region Methods
        void InserirReceita(SqlConnection sqlConnection);
        void AtualizarReceita(SqlConnection sqlConnection);
        #endregion
    }
}
