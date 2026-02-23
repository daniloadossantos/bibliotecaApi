# 📚 biblioteca API

> API RESTful para gerenciamento de livraria desenvolvida em C# com ASP.NET Core

[Adicione badges aqui - vou te passar depois]

## 📖 Sobre

Explique em 2-3 parágrafos:
- O que é o projeto
- Objetivo (estudo, desafio, etc.)
- Contexto (formação Rocketseat? Projeto pessoal?)

Exemplo:
"Este projeto é uma API REST para gerenciamento de uma livraria, 
desenvolvido como parte do desafio prático da Formação C# da Rocketseat. 
O objetivo foi aplicar conceitos de desenvolvimento web com ASP.NET Core, 
implementando um CRUD completo com validações de regras de negócio."

## ⚙️ Funcionalidades

Liste o que a API faz:

- [x] Cadastro de novos livros
- [x] Listagem de todos os livros
- [x] Busca de livro por ID
- [x] Atualização de informações de livros
- [x] Exclusão de livros
- [x] Validação de dados de entrada
- [x] Prevenção de livros duplicados (mesmo título + autor)
- [x] Documentação automática com Swagger

## 🛠️ Tecnologias Utilizadas

Liste as tecnologias:

- **[C#](https://docs.microsoft.com/dotnet/csharp/)** - Linguagem de programação
- **[ASP.NET Core 8.0](https://docs.microsoft.com/aspnet/core/)** - Framework web
- **[Swagger/OpenAPI](https://swagger.io/)** - Documentação da API
- **[Visual Studio](https://visualstudio.microsoft.com/)** - IDE

### Padrões e Conceitos:
- Arquitetura em Camadas
- Dependency Injection
- DTOs (Data Transfer Objects)
- RESTful API
- Validações com Data Annotations

## 📋 Pré-requisitos

Liste o que precisa instalar:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- (Opcional) [Postman](https://www.postman.com/) para testar os endpoints

## 🚀 Como Executar

### Clone o repositório
\`\`\`bash
git clone https://github.com/seu-usuario/bookstore-api.git
cd bookstore-api
\`\`\`

### Execute o projeto
\`\`\`bash
dotnet run
\`\`\`

Ou abra no Visual Studio e pressione F5.

### Acesse a documentação
Abra o navegador em: `https://localhost:PORTA/swagger`

(A porta varia, o Visual Studio mostra no console)


## 📍 Endpoints

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/books/new-book` | Cria um novo livro |
| GET | `/api/books` | Lista todos os livros |
| GET | `/api/books/{id}` | Busca livro por ID |
| PUT | `/api/books/{id}` | Atualiza um livro |
| DELETE | `/api/books/{id}` | Remove um livro |

### Exemplos de Requisições

#### Criar Livro
\`\`\`json
POST /api/books/new-book
{
  "title": "1984",
  "author": "George Orwell",
  "genre": "Ficção",
  "price": 45.90,
  "stock": 10
}
\`\`\`

#### Resposta (201 Created)
\`\`\`json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "1984",
  "author": "George Orwell",
  "genre": "Ficção",
  "price": 45.90,
  "stock": 10,
  "createdAt": "2026-02-23T12:00:00",
  "updatedAt": null
}
\`\`\`

## 📋 Regras de Negócio e Validações

### Campos Obrigatórios
- **title**: 2-120 caracteres
- **author**: 2-120 caracteres
- **genre**: Deve ser um dos valores: Ficção, Romance, Mistério, Terror
- **price**: Maior ou igual a 0
- **stock**: Maior ou igual a 0

### Validações Implementadas
- ✅ Não permite livros duplicados (mesmo título + autor)
- ✅ Preços e estoque não podem ser negativos
- ✅ Gênero deve estar na lista de valores permitidos
- ✅ Todos os campos obrigatórios devem ser preenchidos
- ✅ CreatedAt é preenchido automaticamente na criação
- ✅ UpdatedAt é atualizado em modificações

### Status Codes
- `200 OK` - Requisição bem-sucedida
- `201 Created` - Recurso criado com sucesso
- `400 Bad Request` - Dados inválidos
- `404 Not Found` - Recurso não encontrado
- `409 Conflict` - Conflito (livro duplicado)
- `500 Internal Server Error` - Erro no servidor

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas:

\`\`\`
📦 BookstoreRocketseat
 ┣ 📂 Controllers/        # Camada de apresentação (HTTP)
 ┣ 📂 Services/           # Camada de lógica de negócio
 ┣ 📂 Entities/           # Modelos de domínio
 ┣ 📂 Communications/
 ┃ ┣ 📂 Requests/         # DTOs de entrada
 ┃ ┗ 📂 Responses/        # DTOs de saída
 ┗ 📜 Program.cs          # Configuração da aplicação
\`\`\`

### Separação de Responsabilidades

**Controllers:**
- Recebem requisições HTTP
- Validam ModelState
- Retornam status codes apropriados

**Services:**
- Contêm lógica de negócio
- Validam regras complexas
- Manipulam dados

**DTOs:**
- Transferem dados entre camadas
- Protegem o modelo interno

## 🎓 Aprendizados

Durante o desenvolvimento deste projeto, foram aplicados os seguintes conceitos:

### ASP.NET Core
- Criação de Web APIs RESTful
- Controllers e Actions
- Model Binding e validação
- Retorno de status codes apropriados

### C# e Orientação a Objetos
- Classes, propriedades e métodos
- Modificadores de acesso
- Tipos nullable
- Comparação de strings (case-insensitive)

### Arquitetura de Software
- Separação em camadas (Controllers, Services, DTOs, Entities)
- Dependency Injection (Injeção de Dependência)
- Service Layer Pattern
- DTOs vs Entities

### Validações
- Data Annotations ([Required], [Range], [StringLength])
- ModelState.IsValid
- Validações de regras de negócio
- Tratamento de duplicações

### Boas Práticas
- Documentação com Swagger/OpenAPI
- XML Comments para documentação
- Nomenclatura consistente (PascalCase para propriedades)
- Uso de GUIDs para identificadores únicos

### Ferramentas
- Visual Studio 2022
- Swagger UI para testes
- Git para versionamento

## 🚧 Melhorias Futuras

Possíveis evoluções para o projeto:

- [ ] Implementar banco de dados
- [ ] Adicionar autenticação e autorização (JWT)
- [ ] Implementar testes unitários
- [ ] Adicionar paginação na listagem de livros
- [ ] Criar filtros de busca (por autor, gênero, etc.)
- [ ] Implementar soft delete (exclusão lógica)
- [ ] Adicionar logging estruturado
- [ ] Dockerizar a aplicação

## 👨‍💻 Autor

Desenvolvido por **DANILO ALVES DOS SANTOS**

- GitHub: [@daniloadossantos](https://github.com/seu-usuario)
- LinkedIn: [Danilo Santos](https://linkedin.com/in/daniloasantos/)

---

## 📄 Licença

Este projeto está sob a licença MIT.

---

⭐ Se este projeto te ajudou, considere dar uma estrela!
