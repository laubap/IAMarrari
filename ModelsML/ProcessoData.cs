using Microsoft.ML.Data;

public class ProcessoData
{
    [LoadColumn(1)]
    public float Temperatura { get; set; }

    [LoadColumn(2)]
    public float TemperaturaMedia5 { get; set; }

    [LoadColumn(3)]
    public float TemperaturaVariacao { get; set; }

    [LoadColumn(4)]
    public float Pressao { get; set; }

    [LoadColumn(5)]
    public float PressaoMedia5 { get; set; }

    [LoadColumn(6)]
    public float PressaoVariacao { get; set; }

    [LoadColumn(7)]
    public float Corrente { get; set; }

    [LoadColumn(8)]
    public float CorrenteMedia5 { get; set; }

    [LoadColumn(9)]
    public float CorrenteVariacao { get; set; }
}