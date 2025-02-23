using Domain.Entity;
using Domain.Relatorios.AcumuladoMensal;
using Domain.Relatorios.Entity;
using Infra.Data.Mongo.Config;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infra.Data.Mongo.Repositorys
{
    public class RepositoryAcumuladoMensal : IAcumuladoMensalReportRepository
    {
        protected readonly IMongoCollection<Rendimento> _rendimentoCollection;
        protected readonly IMongoCollection<Despesa> _despesaCollection;
        protected readonly IMongoCollection<Investimento> _investimentoCollection;

        public RepositoryAcumuladoMensal(IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase(MongoDBSettings.DataBaseName);
            _rendimentoCollection = mongoDatabase.GetCollection<Rendimento>(nameof(Rendimento));
            _despesaCollection = mongoDatabase.GetCollection<Despesa>(nameof(Despesa));
            _investimentoCollection = mongoDatabase.GetCollection<Investimento>(nameof(Investimento));
        }

        public async Task<AcumuladoMensalReport> Obter(int mes, int ano)
        {
            var totalrendimento = ObterValorMes(mes, ano, _rendimentoCollection);
            var totalDespesa = ObterValorMes(mes, ano, _despesaCollection);
            var totalInvestimento = ObterValorMes(mes, ano, _investimentoCollection);

            await Task.WhenAll(totalrendimento, totalDespesa, totalInvestimento);

            return new AcumuladoMensalReport(ano, mes, await totalrendimento, await totalInvestimento, await totalDespesa);
        }

        private async Task<decimal> ObterValorMes<T>(int mes, int ano, IMongoCollection<T> mongoCollection) where T : Transacao
        {
            return await mongoCollection
            .AsQueryable()
            .Where(x => x.Ano == ano && x.Mes == mes)
            .SumAsync(x => x.Valor);
        }
    }
}
