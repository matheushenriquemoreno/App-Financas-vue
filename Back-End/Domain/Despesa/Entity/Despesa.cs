using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Despesa : Transacao
    {
        public Despesa(int ano, int mes, string descricao, decimal valor, Categoria categoria) 
            : base(ano, mes, descricao, valor, categoria)
        {
        }

        protected override bool CategoriaEhValida(Categoria categoria)
        {
            if (categoria.Tipo == Enum.TipoCategoria.Despesa)
                return true;

            return false;
        }
    }
}
