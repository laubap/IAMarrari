# EstudoML - Detecção de Anomalias Industriais com ML.NET

## Objetivo

Este projeto tem como objetivo estudar e implementar Machine Learning para detecção de anomalias em ambientes industriais.

A solução foi dividida em dois tipos de análise:

### Modelo de Tag Individual

Analisa o comportamento de uma única tag ao longo do tempo.

Exemplos:

* Temperatura
* Pressão
* Corrente
* Vazão
* Nível

O modelo aprende o comportamento normal da tag através do histórico e identifica possíveis desvios.

---

### Modelo de Processo (Multitag)

Analisa várias tags simultaneamente.

Exemplo:

```text
Temperatura
Pressão
Corrente
```

O objetivo é identificar comportamentos anormais no processo como um todo.

Por exemplo:

```text
Temperatura ↑
Pressão ↓
Corrente ↑
```

Mesmo que cada variável isoladamente pareça normal, o conjunto pode indicar um problema operacional.

---

# Estrutura do Projeto

```text
EstudoML
│
├── README.md
│
├── Models
│   ├── LeituraBruta.cs
│   ├── SensorData.cs
│   ├── SensorPrediction.cs
│   └── ProcessoData.cs
│
├── Services
│   ├── CsvFeatureGenerator.cs
│   ├── ProcessoFeatureGenerator.cs
│   ├── AnomalyDetectionService.cs
│   └── MultitagAnomalyService.cs
│
├── Data
│   ├── sensores.csv
│   ├── sensores_enriquecido.csv
│   ├── processo.csv
│   └── processo_enriquecido.csv
│
└── Program.cs
```

---

# Arquitetura da Solução

## Modelo de Tag Individual

```text
Sensor
↓
Histórico da Tag
↓
sensores.csv
↓
CsvFeatureGenerator
↓
sensores_enriquecido.csv
↓
ML.NET
↓
Detecção de Anomalia
```

---

## Modelo de Processo (Multitag)

```text
Múltiplas Tags
↓
processo.csv
↓
ProcessoFeatureGenerator
↓
processo_enriquecido.csv
↓
ML.NET
↓
Detecção de Anomalia do Processo
```

---

# Models

## LeituraBruta.cs

Representa uma leitura simples de uma tag.

Exemplo:

```csv
Date;Valor
12/06/2026 13:47;65.0
```

---

## SensorData.cs

Representa as features utilizadas pelo modelo de tag individual.

Campos:

* ValorAtual
* MediaMovel5
* Variacao
* MinJanela5
* MaxJanela5
* DesvioPadrao5

---

## ProcessoData.cs

Representa as features utilizadas pelo modelo multitag.

Atualmente:

* Temperatura

* TemperaturaMedia5

* TemperaturaVariacao

* Pressao

* PressaoMedia5

* PressaoVariacao

* Corrente

* CorrenteMedia5

* CorrenteVariacao

---

## SensorPrediction.cs

Representa o resultado retornado pelo modelo.

Campos:

* EhAnomalia
* Score

---

# Services

## CsvFeatureGenerator.cs

Responsável por:

* Ler sensores.csv
* Calcular features
* Gerar sensores_enriquecido.csv

Features calculadas:

* Média móvel
* Variação
* Mínimo da janela
* Máximo da janela
* Desvio padrão

---

## ProcessoFeatureGenerator.cs

Responsável por:

* Ler processo.csv
* Calcular features para múltiplas tags
* Gerar processo_enriquecido.csv

Features calculadas:

* Média móvel por variável
* Variação por variável

---

## AnomalyDetectionService.cs

Modelo de Machine Learning para análise individual de tags.

Responsável por:

* Treinar o modelo
* Realizar previsões
* Identificar anomalias

---

## MultitagAnomalyService.cs

Modelo de Machine Learning para análise de processo.

Responsável por:

* Treinar modelo multivariável
* Avaliar o comportamento conjunto das tags
* Detectar anomalias de processo

---

# Fluxo Atual

Ao executar o projeto:

1. Gera o CSV enriquecido da tag individual

2. Treina o modelo individual

3. Realiza uma previsão

4. Gera o CSV enriquecido do processo

5. Treina o modelo multitag

6. Realiza uma previsão multitag

---

# Tecnologias Utilizadas

* C#
* .NET 10
* ML.NET
* CSV
* Machine Learning
* Detecção de Anomalias

---

# Evoluções Futuras

* Integração com OPC UA
* Integração com MQTT
* Leitura de tags em tempo real
* Persistência de modelos (.zip)
* API REST
* Dashboard de monitoramento
* Agrupamento automático de tags por comportamento
* Detecção preditiva de falhas
* Treinamento incremental dos modelos

---

# Autor

Laura Baptistini

Projeto de estudo para aplicação de Inteligência Artificial em sistemas de automação industrial.



# INFORMAÇÕES
como as  tags podem ter comportamentos diferentes pensei em agrupar elas de acordo com o comportamento e fazer um modelo para grupo de tags.

