# Banco Simplificado

Este projeto é parte de um desafio de desenvolvimento backend promovido pelo PicPay que envolve a criação de uma API RESTful para gestão de usuários (clientes e lojistas) com funcionalidades de registro, transferência de saldo e integração com serviço autorizador externo.

## Tecnologias Utilizadas

- PostgreSQL: Banco de Dados do sistema.
- ASP.NET Core: Framework utilizado para desenvolver a API RESTful.
- Entity Framework: ORM utilizado para interagir com o banco de dados PostgreSQL.
- Swagger: Utilizado para documentar e testar a API.
- BCrypt.Net: Biblioteca para hash de senhas.
- Newtonsoft.Json: Biblioteca para manipulação de JSON.
- Npgsql: Driver Npgsql para PostgreSQL.

## Funcionalidades Implementadas

- Registro de Usuários:
    - Endpoint para registrar novos clientes e lojistas na API.
    - Validação de CPF único e e-mail único durante o registro.

- Transferência de Saldo:
    - Endpoint para transferir saldo entre usuários.
    - Validações de saldo suficiente e tipo de usuário (apenas _clientes_ podem transferir).

- Integração com Serviço Autorizador Externo:
    - Antes de completar a transferência, a API consulta um serviço autorizador externo para validar a operação.


## Executar o projeto: 

1. Clone o repositório:

```bash
git clone https://github.com/LXSCA7/desafio-backend.git
```

2. Configure a conexão com o banco de dados no arquivo appsettings.json.

3. Execute as migrações do Entity Framework para criar o banco de dados e suas tabelas.
    - Adicionar Migração: <br> <br>
        ```bash
        dotnet ef migrations add CriacaoTabela
        ```
    - Aplicar Migração: <br> <br>
        ```bash
        dotnet-ef database update
        ```


1. Execute o projeto:

```bash
dotnet watch run
```
