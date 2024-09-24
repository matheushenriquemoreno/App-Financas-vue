using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Despesa : Transacao
    {
        public Despesa(string id, int ano, int mes, string descricao, decimal valor, Categoria categoria) 
            : base(id, ano, mes, descricao, valor, categoria)
        {
        }
    }
}
