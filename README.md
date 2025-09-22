
# 🚀 API de Produtos de Empréstimo com Cálculo de Juros v1

**Versão 1 do Desafio Técnico do Caixaverso - Dev Back-end C#**  
**Funcionário:** Artur Borges Cerqueira de Andrade  
**Cargo:** Técnico Bancário Novo  
**Local:** Rio de Janeiro, RJ  
**Gestor:** Alexandre Priori Rech  
**Box de Gestão Arquivística**  
**Contato:** artur.andrade@caixa.gov.br / (21) 98167-2720

---

## 📌 Objetivo

O sistema consiste em uma WebApi que cadastra os produtos de empréstimos personalizados e realiza uma simulação de empréstimo, detalhando mês a mês a evolução da dívida.

---

## ✨ Funcionalidades

- Cadastro de produtos de empréstimo com parâmetros personalizados(CRUD de produtos)
- Simulação de empréstimo com cálculo de juros composto
- Retorno detalhado da evolução da dívida mês a mês
- Testes automatizados para validação das regras de negócio

---

## ⚙️ Tecnologias Utilizadas

- .NET 9
- C#
- xUnit
- Sqlite

---

## ▶️ Como Executar o Projeto

### Pré-requisitos

- .NET 9 SDK instalado
- Git instalado

### Passos

1. **Clone o repositório:**

   O projeto estará na pasta "c159529-DESAFIO-BACKEND-CSHARP-CAIXAVERSO"

   ```bash
   git clone https://github.com/ArturBorges1/webapi-simulacao-emprestimo-caixaverso.git
   cd webapi-simulacao-emprestimo-caixaverso
   ```

2. **Restaure os pacotes:**
   ```bash
   dotnet restore src/Caixaverso.Backend.csproj
   dotnet restore tests/Caixaverso.Backend.Tests.csproj
   ```

3. **Instale as ferramentas do Entity Framework Core (se necessário):**
   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. **Execute a migration:**
   ```bash
   dotnet ef database update --project src/Caixaverso.Backend.csproj
   ```

5. **Execute a aplicação:**
   ```bash
   dotnet run --project src/Caixaverso.Backend.csproj
   ```

---

## 📚 Documentação da API

Após iniciar o projeto, acesse:

```
http://localhost:5046
```

para visualizar e testar os endpoints da API via Swagger.

---

## 🧪 Executando os Testes

Os testes estão localizados na pasta `tests/`.

Para executá-los:
```bash
cd tests

dotnet test
```

---

## 📄 Licença

Este projeto é privado e destinado exclusivamente para fins internos.  
**Todos os direitos reservados.**
