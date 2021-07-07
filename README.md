## Teste Evolucional - Fábio Muniz

Este é um modelo de solução para a criação de uma API da Web ASP.NET Core seguindo os princípios da Arquitetura Limpa.

## Tecnologias
* .NET 5.0
* ASP .NET 5.0
* Entity Framework Core 5.0
* MediatR
* Mapster
* FluentValidation
* Elasticsearch, Serilog, Kibana

## Configuracao de Banco de Dados

O modelo é configurado para usar um banco de dados na memória por padrão. 
Isso garante que todos os usuários serão capazes de executar a solução sem a necessidade de configurar uma infraestrutura adicional (por exemplo, SQL Server).

Se você quiser usar o SQL Server, você precisará atualizar WebApi/appsettings.json conforme abaixo:

  "UseInMemoryDatabase": false,
Verifique se o DefaultConnection string de conexão dentro do appsettings.json aponta para uma instância válida do SQL Server.

Ao executar o aplicativo, o banco de dados será criado automaticamente (se necessário) e as migrações mais recentes serão aplicadas.

## Overview

### Domain

Aqui tem todas as entidades, enums, exceções, interfaces, tipos e lógicas específicas da camada de domínio.

### Application

Esta camada contém toda a lógica do aplicativo. Depende da camada de domínio, mas não depende de nenhuma outra camada ou projeto. Esta camada define interfaces que são implementadas por camadas externas. Por exemplo, se o aplicativo precisar acessar um serviço de notificação, 
uma nova interface seria adicionada ao aplicativo e uma implementação seria criada dentro da infraestrutura.

### Infrastructure

Essa camada contém classes para acessar recursos externos, como sistemas de arquivos, serviços da Web, 
 e assim por diante. Essas classes devem ser baseadas em interfaces definidas na camada de aplicativo.

### WebApi

Esta camada é um aplicativo de API da Web baseado em ASP.NET 5.0.x. Essa camada depende das camadas de aplicativo e de infraestrutura; no entanto, a dependência da infraestrutura é apenas para oferecer suporte à injeção de dependência. 
Portanto, apenas * Startup.cs * deve fazer referência a Infraestrutura.

### WebApp

Esta camada é uma web App baseado em ASP.NET 5.0.x. Esta camada depende somente da classe client gerada pelo nSwag.

### Logs

Logging Elasticsearch usando Serilog e visualização de logs em Kibana.
