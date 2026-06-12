var detector = new AnomalyDetectionService();

CsvFeatureGenerator.Gerar(
    caminhoEntrada: "Data/sensores.csv",
    caminhoSaida: "Data/sensores_enriquecido.csv"
);

detector.Treinar("Data/sensores_enriquecido.csv");

var resultado = detector.PreverNovaLeitura(
    caminhoHistorico: "Data/sensores.csv",
    dataHora: DateTime.Parse("12/06/2026 14:29"),
    valor: 81.0f
);

Console.WriteLine("MODELO TAG INDIVIDUAL");
Console.WriteLine($"É anomalia? {resultado.EhAnomalia}");
Console.WriteLine($"Score: {resultado.Score}");

ProcessoFeatureGenerator.Gerar(
    entrada: "Data/processo.csv",
    saida: "Data/processo_enriquecido.csv"
);

var detectorProcesso = new MultitagAnomalyService();

detectorProcesso.Treinar("Data/processo_enriquecido.csv");

var novaLeituraProcesso = ProcessoFeatureGenerator.GerarFeaturesNovaLeitura(
    historicoCsv: "Data/processo.csv",
    dataHora: DateTime.Parse("12/06/2026 14:29"),
    temperatura: 80.0f,
    pressao: 4.0f,
    corrente: 20.0f
);

var resultadoProcesso = detectorProcesso.Prever(novaLeituraProcesso);

Console.WriteLine("MODELO PROCESSO / MULTITAG");
Console.WriteLine($"É anomalia? {resultadoProcesso.EhAnomalia}");
Console.WriteLine($"Score: {resultadoProcesso.Score}");