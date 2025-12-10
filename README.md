## 📌 CashFlow — Sistema de Gerenciamento de Despesas

Um projeto completo para **gestão de despesas**, desenvolvido com **Clean Architecture**, **princípios SOLID**, **CQRS-lite + MediatR**, **Docker**, **CI/CD**, **caching** e **foco total em escalabilidade**.

**Objetivo**: Desenvolver uma base sólida para aplicações modernas, aplicando boas práticas de arquitetura, padrões de projeto e técnicas que promovam um código limpo, desacoplado, testável e escalável.

---


# 🚀 Tecnologias Utilizadas

- **ASP.NET Core 8**
- **C# 12**
- **Entity Framework Core**
- **MediatR (CQRS)**
- **AutoMapper**
- **Flunt (Notification Pattern)**
- **PostgreSQL**
- **Docker**
- **Migrations EF Core**
- (em construção) **Testes automatizados com xUnit**
- (em construção) **Caching com Redis**
- (em construção) **Autenticação / Autorização**
- (em construção) **Integração com Grafana e Prometheus para métricas e observabilidade**

# Arquitetura

O projeto segue os princípios da Clean Architecture, dividido em camadas:

- 📁 CashFlow.Api             → Endpoints / Controllers / Swagger
- 📁 CashFlow.Application     → Use Cases, CQRS (Commands/Queries), Validators
- 📁 CashFlow.Domain          → Entidades, Interfaces, Models, Regras de domínio
- 📁 CashFlow.Infrastructure  → Persistence (EF Core), Repositórios, Migrations, Redis
- 📁 CashFlow.Tests           → Unit & Integration Tests
