## API de Autenticação de Usuários

Esta API RESTful implementa autenticação e gerenciamento de usuários utilizando .NET 6 e Identity.

**Funcionalidades:**

* **Login:** Autentica usuários com credenciais (usuário e senha) e retorna um token JWT.
* **Registro:** Permite que novos usuários se registrem na plataforma, criando uma nova conta.
* **Geração de Token JWT:** Gera tokens JWT para usuários autenticados, contendo informações como nome de usuário, ID e data de nascimento.

**Tecnologias:**

* .NET 6
* Identity
* JWT (JSON Web Tokens)

**Recursos:**

* **UsuarioService:** Classe responsável por gerenciar as operações de login, registro e geração de tokens.
* **TokenService:** Classe responsável por gerar tokens JWT.

**Como usar a API:**

1. **Instale o projeto:** Clone o repositório do GitHub e execute o comando `dotnet restore` para instalar as dependências.
2. **Execute a API:** Execute o comando `dotnet run` para iniciar a API.
3. **Utilize as endpoints:** Utilize as endpoints da API para realizar as operações de login, registro e geração de tokens.

**Endpoints:**

* **POST /login:** Autentica um usuário e retorna um token JWT.
* **POST /register:** Registra um novo usuário.

**Exemplo de uso:**

```json
// Login
{
  "username": "usuario",
  "password": "senha"
}

// Registro
{
  "username": "novousuario",
  "password": "novasenha",
  "email": "novousuario@email.com"
}
