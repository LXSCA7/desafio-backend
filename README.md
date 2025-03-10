# Banco Simplificado

API RESTful para gestão de usuários e transferências de saldo, desenvolvida como parte de um desafio backend.

## Tecnologias

- **Docker**: Containerização do banco de dados PostgreSQL.
- **PostgreSQL**: Banco de dados relacional.
- **ASP.NET Core**: Framework para desenvolvimento da API.
- **Entity Framework**: ORM para interação com o banco de dados.
- **Swagger**: Documentação e testes da API.
- **BCrypt.Net**: Hash de senhas.

## Funcionalidades

### Registro de Usuários
- Cadastro de clientes e lojistas.
- Validação de CPF e e-mail únicos.

### Transferência de Saldo
- Transferência entre usuários.
- Validações:
  - Saldo suficiente.
  - Apenas clientes podem transferir.
  - Reversão da operação em caso de falha.

### Integração com Serviços Externos
- **Autorizador**: Consulta externa para validar a transferência.
- **Notificação**: Envio de notificações aos usuários envolvidos.


---

## Como Executar

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/LXSCA7/desafio-backend.git
   
   cd desafio-backend/Desafio.Api
   ```
   
2. **Suba o contêiner postgres**:
   ```bash
   docker-compose up -d
   ```

3. **Execute a API**:
   ```bash
   dotnet watch run
   ```

4. **Acesse a documentação no link indicado no terminal, normalmente: `https://localhost:5000/swagger`**.
