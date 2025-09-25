# PrevisaoDoTempo - API de Previsão do Tempo

## Sobre o Projeto
API para consulta de previsão do tempo com sistema de cache automático e limpeza programada.

### Utiliza
.NET 8.0  
Entity Framework Core 9  
SQL Server LocalDB

### Configurações

A configuração da chave da WeatherApi e da connection string são feitas no arquivo appsettings.json na pasta API

```
{
  "WeatherApi": {
    "BaseUrl": "https://api.weatherapi.com/v1",
    "Chave": "CHAVE-AQUI"
  },
  "ConnectionStrings": {
    "PrevisaoTempoConnection": "Server=(localdb)\\MSSQLLocalDB;Database=PrevisaoTempoDb;Trusted_Connection=True;"
  }
}
```

### Banco
O banco de dados foi criado usando migrations do Entity

### Executar

Para executar a API pode-se rodar direto pelo Visual Studio como projeto de inicialização ou executar o comando abaixo  no terminal na pasta API 
```
dotnet run
```
Acesso pelo endereço
```
http://localhost:5125/swagger/index.html
```
Para executar o serviço de limpeza de cache  basta executar o comando abaixo via terminal na pasta ServicosBackground

```
dotnet run
```

### Endpoints

A API conta com 3 endpoints, como nos exemplos:

```
Clima atual
GET /v1/clima/atual/São Paulo

Previsão 5 dias
GET /v1/clima/previsao5dias/Rio de Janeiro

Histórico de consultas
GET /v1/clima/historico
```

### Principais Funcionalidades

Sistema de Cache Inteligente  
```
Primeira consulta: Busca da API externa e salva no banco
Consultas seguintes: Retorna dados do cache (mais rápido)
Histórico: Registra todas as consultas realizadas
```

Limpeza Automática
```
Serviço em background: Limpa cache a cada 1 hora
São limpos os dados de clima, previsões e histórico
```

## Decisões Técnicas

### 1. Separação em Camadas
```
Isola a resposabilidade utilizando injeção de dependência

As camadas do projeto são:
API;
Dados;
Servicos;

API recebe as requisições do usuário e devolve uma dto com as informações obtidas;
Dados isola o acesso ao banco de dados através de repositórios;
Servicos isola a lógica de negócios da aplicação;
```

### 2. Cache em Banco de Dados
```
Garante:
Persistência entre reinícios da aplicação
Consistência de dados
Fácil monitoramento e backup
```

### 3. Background Service para Limpeza
```
Garante que dados antigos não sejam mantidos no banco
É possível alterar o intervalo com a modificação de uma variável no projeto
```

### 4. Padrão Repository + DTOs
```
Os repositories simplificam e isolam o acesso aos dados do banco
Os DTOs permitem que as entidades do negócio não sejam expostas para os usuários
Uso de extensions para mapear a conversão de entidades para DTOs facilita o processo
```

### Fluxo de uma Consulta
```
Usuário → Faz requisição para API

API → Chama serviço correspondente

Serviço → Verifica se tem no cache

Se existir histórico → Retorna do banco 

Se não existir → Busca API externa → Salva no banco → Retorna
```

### Estrutura do Banco

```
Climas        (Dados atuais)
Previsoes     (Previsão 5 dias) 
Historicos    (Registro de consultas)
```
