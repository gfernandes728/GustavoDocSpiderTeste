# Estrutura do Projeto

GustavoDocSpiderTeste/
├── GustavoDocSpiderTeste/              # Projeto principal MVC
├── GustavoDocSpiderTeste.Business/     # Regras de negócio
├── GustavoDocSpiderTeste.Data/         # Repositórios e DbContext
├── GustavoDocSpiderTeste.Models/       # Entidades
├── GustavoDocSpiderTeste.ViewModels/   # ViewModels
├── GustavoDocSpiderTeste.UnitTests/    # Testes automatizados
└── appsettings.json                    # Configurações

## Como rodar o projeto localmente

## 1. Clonar o repositório

git clone <url-do-repositorio>
cd GustavoDocSpiderTeste

## 2. Restaurar pacotes

Abra o terminal/prompt de comando na pasta da solução e execute:

dotnet restore

## 3. Configurar o banco de dados

Abra o arquivo:

GustavoDocSpiderTeste/appsettings.json

E edite a DefaultConnection com sua string de conexão local, por exemplo:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DocSpiderDb;Trusted_Connection=True;"
}

## 4. Aplicar as Migrations

Crie e/ou aplique as migrations com os comandos:

dotnet ef database update --project GustavoDocSpiderTeste.Data --startup-project GustavoDocSpiderTeste

Caso precise criar a migration inicialmente:

dotnet ef migrations add InitialCreate --project GustavoDocSpiderTeste.Data --startup-project GustavoDocSpiderTeste

Obs.: Certifique-se de que o pacote Microsoft.EntityFrameworkCore.Tools esteja referenciado.

## 5. Rodar o projeto

dotnet run --project GustavoDocSpiderTeste

Acesse: https://localhost:44342

## Rodar os Testes

dotnet test GustavoDocSpiderTeste.UnitTests

## Publicação

dotnet publish -c Release -o ./publish

## Autor

Gustavo Fernandes
guga.728@gmail.com
