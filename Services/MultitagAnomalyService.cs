using Microsoft.ML;
using Microsoft.ML.Data;

public class MultitagAnomalyService
{
    private readonly MLContext _mlContext = new();

    private PredictionEngine<ProcessoData, SensorPrediction>? _predictor;
    private ITransformer? _modelo;
    private DataViewSchema? _schema;

    public void Treinar(string caminhoCsv)
    {
        var dataView = _mlContext.Data.LoadFromTextFile<ProcessoData>(
            path: caminhoCsv,
            hasHeader: true,
            separatorChar: ';'
        );

        var pipeline = _mlContext.Transforms.Concatenate(
                "Features",
                nameof(ProcessoData.Temperatura),
                nameof(ProcessoData.TemperaturaMedia5),
                nameof(ProcessoData.TemperaturaVariacao),
                nameof(ProcessoData.Pressao),
                nameof(ProcessoData.PressaoMedia5),
                nameof(ProcessoData.PressaoVariacao),
                nameof(ProcessoData.Corrente),
                nameof(ProcessoData.CorrenteMedia5),
                nameof(ProcessoData.CorrenteVariacao)
            )
            .Append(_mlContext.AnomalyDetection.Trainers.RandomizedPca(
                featureColumnName: "Features",
                rank: 3
            ));

        var modelo = pipeline.Fit(dataView);

        _modelo = modelo;
        _schema = dataView.Schema;

        _predictor = _mlContext.Model.CreatePredictionEngine<ProcessoData, SensorPrediction>(modelo);

        Console.WriteLine("Modelo multitag treinado!");
    }

    public void SalvarModelo(string caminho)
    {
        if (_modelo == null || _schema == null)
            throw new InvalidOperationException("Modelo ainda não foi treinado.");

        _mlContext.Model.Save(_modelo, _schema, caminho);

        Console.WriteLine($"Modelo salvo em: {caminho}");
    }

    public void CarregarModelo(string caminhoModelo)
    {
        var modelo = _mlContext.Model.Load(caminhoModelo, out var schema);

        _predictor = _mlContext.Model.CreatePredictionEngine<ProcessoData, SensorPrediction>(modelo);

        Console.WriteLine($"Modelo carregado de: {caminhoModelo}");
    }

    public SensorPrediction Prever(ProcessoData entrada)
    {
        if (_predictor == null)
            throw new InvalidOperationException("Modelo multitag ainda não foi treinado.");

        return _predictor.Predict(entrada);
    }
}