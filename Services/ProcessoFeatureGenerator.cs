using System.Globalization;

public static class ProcessoFeatureGenerator
{
    public static void Gerar(string entrada, string saida)
    {
        var linhas = File.ReadAllLines(entrada).Skip(1).ToList();

        var registros = linhas.Select(l =>
        {
            var p = l.Split(';');

            return new ProcessoBruto
            {
                DataHora = DateTime.ParseExact(p[0], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Temperatura = float.Parse(p[1], CultureInfo.InvariantCulture),
                Pressao = float.Parse(p[2], CultureInfo.InvariantCulture),
                Corrente = float.Parse(p[3], CultureInfo.InvariantCulture)
            };
        }).OrderBy(x => x.DataHora).ToList();

        var saidaLinhas = new List<string>
        {
            "DataHora;Temperatura;TemperaturaMedia5;TemperaturaVariacao;Pressao;PressaoMedia5;PressaoVariacao;Corrente;CorrenteMedia5;CorrenteVariacao"
        };

        for (int i = 0; i < registros.Count; i++)
        {
            var janela = registros.Skip(Math.Max(0, i - 4)).Take(i - Math.Max(0, i - 4) + 1).ToList();

            var tempVar = i == 0 ? 0 : registros[i].Temperatura - registros[i - 1].Temperatura;
            var pressaoVar = i == 0 ? 0 : registros[i].Pressao - registros[i - 1].Pressao;
            var correnteVar = i == 0 ? 0 : registros[i].Corrente - registros[i - 1].Corrente;

            saidaLinhas.Add(
                $"{registros[i].DataHora:dd/MM/yyyy HH:mm};" +
                $"{registros[i].Temperatura.ToString(CultureInfo.InvariantCulture)};" +
                $"{janela.Average(x => x.Temperatura).ToString("F4", CultureInfo.InvariantCulture)};" +
                $"{tempVar.ToString("F4", CultureInfo.InvariantCulture)};" +
                $"{registros[i].Pressao.ToString(CultureInfo.InvariantCulture)};" +
                $"{janela.Average(x => x.Pressao).ToString("F4", CultureInfo.InvariantCulture)};" +
                $"{pressaoVar.ToString("F4", CultureInfo.InvariantCulture)};" +
                $"{registros[i].Corrente.ToString(CultureInfo.InvariantCulture)};" +
                $"{janela.Average(x => x.Corrente).ToString("F4", CultureInfo.InvariantCulture)};" +
                $"{correnteVar.ToString("F4", CultureInfo.InvariantCulture)}"
            );
        }

        File.WriteAllLines(saida, saidaLinhas);
        Console.WriteLine("CSV de processo enriquecido gerado!");
    }

    public static ProcessoData GerarFeaturesNovaLeitura(string historicoCsv, DateTime dataHora, float temperatura, float pressao, float corrente)
    {
        var linhas = File.ReadAllLines(historicoCsv).Skip(1).ToList();

        var registros = linhas.Select(l =>
        {
            var p = l.Split(';');

            return new ProcessoBruto
            {
                DataHora = DateTime.ParseExact(p[0], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Temperatura = float.Parse(p[1], CultureInfo.InvariantCulture),
                Pressao = float.Parse(p[2], CultureInfo.InvariantCulture),
                Corrente = float.Parse(p[3], CultureInfo.InvariantCulture)
            };
        }).OrderBy(x => x.DataHora).ToList();

        registros.Add(new ProcessoBruto
        {
            DataHora = dataHora,
            Temperatura = temperatura,
            Pressao = pressao,
            Corrente = corrente
        });

        var i = registros.Count - 1;
        var janela = registros.Skip(Math.Max(0, i - 4)).Take(i - Math.Max(0, i - 4) + 1).ToList();

        return new ProcessoData
        {
            Temperatura = temperatura,
            TemperaturaMedia5 = janela.Average(x => x.Temperatura),
            TemperaturaVariacao = temperatura - registros[i - 1].Temperatura,

            Pressao = pressao,
            PressaoMedia5 = janela.Average(x => x.Pressao),
            PressaoVariacao = pressao - registros[i - 1].Pressao,

            Corrente = corrente,
            CorrenteMedia5 = janela.Average(x => x.Corrente),
            CorrenteVariacao = corrente - registros[i - 1].Corrente
        };
    }
}

public class ProcessoBruto
{
    public DateTime DataHora { get; set; }
    public float Temperatura { get; set; }
    public float Pressao { get; set; }
    public float Corrente { get; set; }
}