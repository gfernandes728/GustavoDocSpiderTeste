# GustavoDocSpiderTeste

Projeto ASP.NET Core MVC para gerenciamento de documentos com upload, edição e visualização. Desenvolvido como parte de um teste técnico.

---

## 📁 Estrutura do Projeto

```
GustavoDocSpiderTeste/
├── GustavoDocSpiderTeste/              # Projeto principal MVC
├── GustavoDocSpiderTeste.Business/     # Regras de negócio
├── GustavoDocSpiderTeste.Data/         # Repositórios e DbContext
├── GustavoDocSpiderTeste.Models/       # Entidades
├── GustavoDocSpiderTeste.ViewModels/   # ViewModels
├── GustavoDocSpiderTeste.UnitTests/    # Testes automatizados
└── appsettings.json                    # Configurações
```

---

## 🚀 Como rodar o projeto localmente

### 1. Clonar o repositório

```bash
git clone https://github.com/gfernandes728/GustavoDocSpiderTeste.git
cd GustavoDocSpiderTeste
```

### 2. Restaurar os pacotes

```bash
dotnet restore
```

### 3. Configurar o banco de dados

Edite o arquivo `GustavoDocSpiderTeste/appsettings.json` com sua string de conexão:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DocSpiderDb;Trusted_Connection=True;"
}
```

### 4. Aplicar as migrations

Para aplicar a migration existente:

```bash
dotnet ef database update --project GustavoDocSpiderTeste.Data --startup-project GustavoDocSpiderTeste
```

Para criar uma nova migration inicial (se necessário):

```bash
dotnet ef migrations add InitialCreate --project GustavoDocSpiderTeste.Data --startup-project GustavoDocSpiderTeste
```

> 💡 Certifique-se de que o pacote `Microsoft.EntityFrameworkCore.Tools` esteja instalado.

### 5. Rodar o projeto

```bash
dotnet run --project GustavoDocSpiderTeste
```

Acesse no navegador: [https://localhost:44342](https://localhost:44342)

---

## ✅ Rodar os Testes

```bash
dotnet test GustavoDocSpiderTeste.UnitTests
```

---

## 📦 Publicação

```bash
dotnet publish -c Release -o ./publish
```

---

## 👤 Autor

**Gustavo Fernandes**  
📧 guga.728@gmail.com  
