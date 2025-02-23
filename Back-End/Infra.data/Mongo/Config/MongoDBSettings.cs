namespace Infra.Data.Mongo.Config;

internal class MongoDBSettings
{
    public static string ConnectionString => "mongodb://teste:123456@localhost:27017";
    public static string DataBaseName => "FinancasPessoais";
}
