# SIMI_SistemaIntegradoDeMonitoramentoIndustrial

# 🏭 API de Monitoramento Industrial (SIMI)

## 📌 Descrição

Esta API faz parte do sistema **SIMI (Sistema Integrado de Monitoramento Industrial)**, responsável por receber, validar, armazenar e disponibilizar dados de sensores industriais.

Os sensores simulados enviam informações de:

* 🌡️ Temperatura (°C)
* ⚡ Corrente elétrica (Ampere - A)

Os dados são validados conforme limites configurados e persistidos em banco de dados SQLite.

---

## ⚙️ Tecnologias Utilizadas

* .NET 6 / 7
* ASP.NET Core Web API
* Entity Framework Core
* SQLite
* Swagger (OpenAPI)

---

## 🧠 Regras de Negócio

* Temperatura não pode ultrapassar o limite definido em configuração
* Corrente elétrica não pode ultrapassar o limite definido
* Dados válidos são armazenados no banco
* Dados inválidos retornam erro HTTP 400

---

## 🚀 Como Executar o Projeto

### 1. Clonar o repositório

```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio
```

---

### 2. Restaurar dependências

```bash
dotnet restore
```

---

### 3. Criar banco de dados (SQLite)

```bash
dotnet ef migrations add Inicial
dotnet ef database update
```

---

### 4. Executar a API

```bash
dotnet run
```

---

### 5. Acessar Swagger

```
https://localhost:7007/swagger
```

---

## 🔌 Endpoints da API

### 📥 POST /api/v1/sensores

Recebe dados de sensores industriais.

#### 📌 Exemplo de requisição:

```json
{
  "temperatura": 50,
  "corrente": 30,
  "timestamp": "2026-04-26T10:00:00"
}
```

#### ✅ Respostas:

* `200 OK` → Dados aceitos e armazenados
* `400 Bad Request` → Valores acima do limite

---

### 📤 GET /api/v1/sensores

Retorna todos os dados registrados.

#### 📌 Exemplo de resposta:

```json
[
  {
    "id": 1,
    "temperatura": "50 ºC",
    "corrente": "30 A",
    "timestamp": "2026-04-26T10:00:00"
  }
]
```

---

## 🗄️ Banco de Dados

O sistema utiliza **SQLite**, com a tabela:

### 📊 Sensores

| Campo       | Tipo     |
| ----------- | -------- |
| Id          | int      |
| Temperatura | double   |
| Corrente    | double   |
| Timestamp   | DateTime |

---

## 🔧 Configuração (appsettings.json)

```json
"ApiConfig": {
  "MaxTemperatura": 80,
  "MaxCorrente": 70
}
```

---

## 🔄 Alterações Implementadas

✔ Integração com banco de dados SQLite
✔ Persistência de dados via Entity Framework Core
✔ Criação do AppDbContext
✔ Adição de novo sinal industrial: **Corrente elétrica (Ampere)**
✔ Validação de limites de temperatura e corrente
✔ Implementação de documentação XML (summary, remarks, etc)
✔ Formatação dos dados no retorno da API
✔ Estrutura organizada em camadas (Controller + Data + Config)
         

Desenvolvido para fins acadêmicos (SENAI)
