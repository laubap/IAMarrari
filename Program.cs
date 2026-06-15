var detector = new AnomalyDetectionService();

detector.CarregarModelo("ModelsML/modelo_tag.zip");

var resultado = detector.PreverNovaLeitura(
    caminhoHistorico: "Data/sensores.csv",
    dataHora: DateTime.Parse("12/06/2026 14:29"),
    valor: 81.0f
);

Console.WriteLine("MODELO TAG INDIVIDUAL - CARREGADO DO ZIP");
Console.WriteLine($"É anomalia? {resultado.EhAnomalia}");
Console.WriteLine($"Score: {resultado.Score}");

var detectorProcesso = new MultitagAnomalyService();

detectorProcesso.CarregarModelo("ModelsML/modelo_processo.zip");

var novaLeituraProcesso = ProcessoFeatureGenerator.GerarFeaturesNovaLeitura(
    historicoCsv: "Data/processo.csv",
    dataHora: DateTime.Parse("12/06/2026 14:29"),
    temperatura: 80.0f,
    pressao: 4.0f,
    corrente: 20.0f
);

var resultadoProcesso = detectorProcesso.Prever(novaLeituraProcesso);

Console.WriteLine("MODELO PROCESSO / MULTITAG - CARREGADO DO ZIP");
Console.WriteLine($"É anomalia? {resultadoProcesso.EhAnomalia}");
Console.WriteLine($"Score: {resultadoProcesso.Score}");