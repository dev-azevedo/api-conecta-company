# ConectaCompany 
 
## Descrição 
Projeto criado com arquitetura DDD (Domain-Driven Design) 
 
## Estrutura do Projeto 
- **Domain**: Contém os modelos e interfaces do domínio 
- **Application**: Lógica de aplicação, serviços, DTOs e validadores 
- **Infra**: Implementações de infraestrutura (Banco de dados, repositórios, Api's externas) 
- **Api**: Camada de apresentação (Web API) 
 
## Como Executar 
1. Restaure os pacotes: ```dotnet restore``` 
2. Execute a API: ```dotnet run --project src/Api``` 
 
## Tecnologias Utilizadas 
- .NET 
- ASP.NET Core 
- Entity Framework Core

## Comandos Entity Framework Core
- Adicionar migração: ```dotnet ef migrations add InitDb --project .\src\ConectaCompany.Infra\ --startup-project .\src\ConectaCompany.Api\```
- Atualizar DB: ```dotnet ef database update --project .\src\ConectaCompany.Infra\ --startup-project .\src\ConectaCompany.Api\```


## Objetivos dessa API para estudos
- Gerenciamento de usuario com Identity
- MemoryCache
- Unit of Work
- Repository Pattern
- Result Pattern
- Validação com FluentValidation
- Mapping com Adapter

## Autor
- [✌🏼 @dev-azevedo](https://www.linkedin.com/in/dev-azevedo/)