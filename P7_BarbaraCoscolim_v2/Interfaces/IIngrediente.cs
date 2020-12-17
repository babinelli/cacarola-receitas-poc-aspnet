using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7_BarbaraCoscolim_v2
{
    interface IIngrediente
    {
        #region Properties
        int IngredienteID { get; }
        int ReceitaID { get; }
        string NomeIngrediente { get; }
        string Quantidade { get; }
        #endregion

        #region Methods
        void InserirIngrediente(SqlConnection sqlConnection);
        #endregion
    }
}
