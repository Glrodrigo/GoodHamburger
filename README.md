# 🍔 Good Hamburger

Sistema de gerenciamento de pedidos para lanchonete, desenvolvido com **ASP.NET Core Web API**, **Blazor WebAssembly** e **xUnit** para testes automatizados.

---

## 📋 Sobre o Projeto

O **Good Hamburger** é uma aplicação fullstack que simula o sistema de pedidos de uma hamburgueria artesanal. O cliente pode visualizar o cardápio, criar pedidos, editá-los e removê-los, com cálculo automático de descontos baseado nos itens selecionados.

### Regras de Negócio

- Cada pedido pode conter **apenas um sanduíche**
- Acompanhamentos (Batata Frita e Refrigerante) podem ser combinados livremente
- Itens duplicados (mesmo produto duas vezes) não são permitidos
- Descontos aplicados automaticamente:

| Combinação | Desconto |
|---|---|
| Sanduíche + Batata + Refrigerante | 20% |
| Sanduíche + Refrigerante | 15% |
| Sanduíche + Batata | 10% |

### Cardápio pré-carregado

| Categoria | Produto | Preço |
|---|---|---|
| Sanduíche | X Burger | R$ 5,00 |
| Sanduíche | X Egg | R$ 4,50 |
| Sanduíche | X Bacon | R$ 7,00 |
| Acompanhamento | Batata Frita | R$ 2,00 |
| Acompanhamento | Refrigerante | R$ 2,50 |

---

## 🏗️ Estrutura da Solution

```
GoodHamburguer/
├── GoodHamburguer.API        # ASP.NET Core Web API (backend)
├── GoodHamburguer.Blazor     # Blazor WebAssembly (frontend)
├── GoodHamburguer.Shared     # Biblioteca compartilhada (DTOs, Result)
└── GoodHamburguer.Tests      # Testes automatizados com xUnit
```

---

## ⚙️ Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8) ou superior
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (versão 17.8+) **ou** [Visual Studio Code](https://code.visualstudio.com/) com a extensão C#
- Git

Você pode verificar sua versão do .NET com:

```bash
dotnet --version
```

---

## 🚀 Como Rodar o Projeto

### 1. Clone o repositório

```bash
git clone https://github.com/Glrodrigo/GoodHamburger.git
cd GoodHamburger
```

### 2. Restaure as dependências

```bash
dotnet restore
```

---

## ▶️ Rodando no Visual Studio 2022

Como a solution possui **API e Blazor rodando juntos**, é necessário configurar múltiplos projetos de inicialização. Siga os passos abaixo:

### Passo 1 — Abrir as configurações de Startup

Clique com o botão direito na **Solution** (não em um projeto específico) no Solution Explorer e selecione:

```
Properties → Configure Startup Projects...
```

Ou acesse pelo menu:

```
Menu → Debug → Configure Startup Projects...
```

### Passo 2 — Configurar Multiple Startup Projects

Na janela que abrir:

1. Selecione a opção **"Multiple startup projects"**
2. Localize os projetos **GoodHamburguer.API** e **GoodHamburguer.Blazor**
3. Para ambos, altere a coluna **Action** de `None` para **`Start`**
4. Clique em **OK** para salvar

> ⚠️ O projeto `GoodHamburguer.Tests` deve permanecer como `None` — ele não é uma aplicação executável.

### Passo 3 — Verificar a URL da API no Blazor

Abra o arquivo `GoodHamburguer.Blazor/Program.cs` e confirme que a `BaseAddress` aponta para a URL da sua API:

```csharp
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7168/") // deve bater com a porta da API
});
```

Para verificar a porta da API, abra `GoodHamburguer.API/Properties/launchSettings.json` e veja o valor de `applicationUrl`.

### Passo 4 — Rodar a aplicação

Pressione **F5** ou clique em **Start**. Duas janelas do navegador serão abertas:

| Aplicação | URL padrão |
|---|---|
| API (Swagger) | `https://localhost:7168/swagger` |
| Blazor (Frontend) | `https://localhost:7296` |

---

## ▶️ Rodando via Terminal (sem Visual Studio)

Abra **dois terminais separados**:

**Terminal 1 — API:**
```bash
cd GoodHamburguer.API
dotnet run
```

**Terminal 2 — Blazor:**
```bash
cd GoodHamburguer.Blazor
dotnet run
```

---

## 🧪 Rodando os Testes

### Via Visual Studio

Acesse o menu:
```
Menu → Test → Run All Tests
```

Ou abra o **Test Explorer** com:
```
Menu → Test → Test Explorer
```

### Via Terminal

```bash
cd GoodHamburguer.Tests
dotnet test
```

Para ver o resultado detalhado:

```bash
dotnet test --verbosity normal
```

---

## 🗄️ Banco de Dados

O projeto utiliza **banco de dados em memória** (InMemory), o que significa que:

- ✅ Nenhuma configuração de banco de dados é necessária
- ✅ Os dados são pré-carregados automaticamente ao iniciar a API
- ⚠️ Os dados são resetados a cada vez que a API é reiniciada

Não é necessário rodar migrations ou configurar connection strings.

---

## 📡 Endpoints da API

A documentação completa dos endpoints está disponível via **Swagger** ao rodar a API:

```
https://localhost:7168/swagger
```

### Resumo dos endpoints

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/cardapio` | Lista todos os produtos do cardápio |
| `POST` | `/api/cardapio` | Cadastra um novo produto |
| `GET` | `/api/pedido` | Lista todos os pedidos |
| `GET` | `/api/pedido/{id}` | Busca um pedido por ID |
| `POST` | `/api/pedido` | Cria um novo pedido |
| `PUT` | `/api/pedido/{id}` | Atualiza um pedido existente |
| `DELETE` | `/api/pedido/{id}` | Remove um pedido |

---

## 🛠️ Tecnologias Utilizadas

| Camada | Tecnologia |
|---|---|
| Backend | ASP.NET Core 8 Web API |
| Frontend | Blazor WebAssembly |
| ORM | Entity Framework Core (InMemory) |
| Mediator | MediatR |
| Testes | xUnit + Moq + FluentAssertions |
| Documentação | Swagger / Swashbuckle |

---

## 📁 Arquitetura

O projeto segue os princípios de **Clean Architecture** com separação em camadas:

```
GoodHamburguer.API/
├── Domain/          # Entidades e regras de negócio
├── Application/     # Commands, Queries e Handlers (CQRS com MediatR)
├── Infrastructure/  # Repositórios, DbContext e Seeders
└── Presentation/    # Controllers e Request models
```

---

## 🤝 Contribuindo

1. Faça um fork do projeto
2. Crie uma branch para sua feature: `git checkout -b feature/minha-feature`
3. Commit suas alterações: `git commit -m 'feat: adiciona minha feature'`
4. Push para a branch: `git push origin feature/minha-feature`
5. Abra um Pull Request
