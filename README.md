
# FleetManager API

Breve descrição do projeto — o que é, para que serve.

## Tecnologias
- .NET 8
- MySQL
- Entity Framework Core
- JWT Authentication
- FluentValidation

## Pré-requisitos
- .NET 8 SDK
- MySQL rodando localmente

## Como rodar localmente

1. Clone o repositório
2. Configure o appsettings.Development.json com suas credenciais locais
3. Execute as migrations:
```bash
   dotnet ef database update --project src/FleetManager.Infrastructure
```
4. Rode a API:
```bash
   dotnet run --project src/FleetManager.Api
```

## Como rodar os testes
```bash
dotnet test
```

## Estrutura do projeto
- **Api** — Controllers e configuração
- **Application** — Use Cases e validações  
- **Domain** — Entidades e regras de negócio
- **Infrastructure** — Banco de dados e serviços externos
- **Communication** — DTOs de request e response