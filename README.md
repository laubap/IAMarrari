# EstudoML - Detecção de Anomalias em Tags Industriais

## Objetivo

Este projeto tem como objetivo estudar e implementar Machine Learning para detecção de anomalias em tags industriais.

A ideia é que o sistema aprenda o comportamento normal de uma tag (temperatura, pressão, corrente, vazão, etc.) através do histórico armazenado e consiga identificar comportamentos fora do padrão quando novas leituras forem recebidas.

---

# Estrutura do Projeto

text
EstudoML
│
├── Program.cs
│
├── Models
│   ├── LeituraBruta.cs
│   ├── SensorData.cs
│   └── SensorPrediction.cs
│
├── Services
│   ├── CsvFeatureGenerator.cs
│   └── AnomalyDetectionService.cs
│
└── Data
    ├── sensores.csv
    └── sensores_enriquecido.csv


---

# Program.cs

Arquivo principal da aplicação.

Responsável por coordenar a execução do sistema.

Fluxo executado:

1. Gerar o CSV enriquecido
2. Treinar o modelo de Machine Learning
3. Simular uma nova leitura
4. Executar a previsão
5. Exibir o resultado



---

# Pasta Models

Contém as classes utilizadas para representar os dados da aplicação.

## LeituraBruta.cs

Representa os dados originais recebidos da tag.

---

## SensorData.cs

Representa os dados tratados que serão enviados para o modelo de Machine Learning.

Esses dados são chamados de Features.

Essas informações ajudam o modelo a compreender melhor o comportamento da tag.

---

## SensorPrediction.cs

Representa o resultado retornado pelo modelo.

* EhAnomalia = indica se o comportamento é considerado anômalo
* Score = nível de confiança da previsão

---

# Pasta Services

Contém as regras de negócio do sistema.

---

## CsvFeatureGenerator.cs

Responsável por processar o histórico bruto da tag.

Funções principais:

### CarregarHistorico()

Lê o arquivo CSV original.

---

### Gerar()

Gera um novo CSV enriquecido contendo informações adicionais calculadas pelo sistema.

---

### GerarFeaturesParaNovaLeitura()

Quando uma nova leitura chega ao sistema, calcula automaticamente as Features necessárias para o modelo.

Essas informações não são fornecidas pelo sensor.

São calculadas pelo sistema utilizando o histórico da tag.

---

### CalcularDesvioPadrao()

Calcula o desvio padrão da janela utilizada.

Essa métrica ajuda a identificar o nível de variação do comportamento da tag.

---

## AnomalyDetectionService.cs

Responsável pela parte de Machine Learning.

---

### Treinar()

Lê o CSV enriquecido e treina o modelo utilizando ML.NET.

Objetivo:

Aprender o comportamento normal da tag.

---

### PreverNovaLeitura()

Recebe uma nova leitura.

Executa:

1. Busca histórico da tag
2. Calcula Features
3. Envia os dados para o modelo
4. Retorna o resultado da análise

---

# Pasta Data

Contém os arquivos de dados utilizados pelo projeto.

---

## sensores.csv

Arquivo bruto.

Representa os dados recebidos da tag.

---

## sensores_enriquecido.csv

Arquivo gerado automaticamente pelo sistema.

Contém informações adicionais calculadas a partir do histórico.

Esse arquivo é utilizado pelo ML.NET para treinamento.

---

# Fluxo Completo da Solução


Sensor Industrial
        ↓
Tag
        ↓
Sistema
        ↓
CSV Histórico
        ↓
CsvFeatureGenerator
        ↓
CSV Enriquecido
        ↓
ML.NET
        ↓
Detecção de Anomalias
        ↓
Alerta

# Cenário Futuro

Atualmente os dados são simulados utilizando arquivos CSV.

Em uma aplicação real, as leituras poderão ser obtidas através de:

* OPC UA
* MQTT
* Banco de Dados
* APIs
* Historiadores Industriais

Mantendo a mesma arquitetura do sistema.
