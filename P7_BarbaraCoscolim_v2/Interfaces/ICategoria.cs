using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7_BarbaraCoscolim_v2
{
    interface ICategoria
    {
        #region Propriedades
        int CategoriaID { get; }
        string NomeCategoria { get; }
        #endregion
    }
}
