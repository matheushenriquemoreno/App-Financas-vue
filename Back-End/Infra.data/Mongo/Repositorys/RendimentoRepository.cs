using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repository;
using Infra.Data.Mongo.Config;
using Infra.Data.Mongo.RepositoryBase;
using MongoDB.Driver;

namespace Infra.Data.Mongo.Repositorys
{
    public class RendimentoRepository : RepositoryTransacaoBase<Rendimento>, IRendimentoRepository
    {
        public RendimentoRepository(IMongoClient mongoClient, ICategoriaRepository categoriaRepository) : base(mongoClient, categoriaRepository)
        {
        }

        public override string GetCollectionName()
        {
            return nameof(Rendimento);
        }

        public async Task<IEnumerable<Rendimento>> ObterPeloMes(int mes, int ano)
        {
            var rendimentos = await _entityCollection.Find(x => x.Ano == ano && x.Mes == mes).ToListAsync();

            foreach (var rendimento in rendimentos)
            {
                await IncluirDependencias(rendimento);
            }

            return rendimentos;
        }
    }
}
