using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Rendimento : Transacao
    {
        public Rendimento(int ano, int mes, string descricao, decimal valor, Categoria categoria) 
            : base(ano, mes, descricao, valor, categoria)
        {
        }

        protected override bool CategoriaEhValida(Categoria categoria)
        {
           if(categoria.Tipo == Enum.TipoCategoria.Rendimento)
                return true;

           return false;
        }
    }
}
