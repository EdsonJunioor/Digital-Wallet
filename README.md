## Digital Wallet API

API para gerenciamento de transações financeiras, incluindo autenticação JWT, operações de depósito e transferência.

### Tecnologias Utilizadas

.NET 8

C#

Entity Framework Core (EF Core)

SQL Server

JWT (JSON Web Token) para autenticação

AutoMapper

Swagger (para documentação)

### Pré-requisitos

Antes de rodar o projeto, você precisa ter instalado:

.NET 8 SDK

SQL Server

Postman ou outra ferramenta para testar a API

### Configuração do Banco de Dados


1. Configurar SQL Server
   
Crie um banco de dados no SQL Server e copie a string de conexão.

Exemplo de string de conexão no appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=DigitalWallet;TrustServerCertificate=True;Integrated Security=True""
}


2. Configurar JWT
   
No appsettings.json, configure as rotas dadas por sua maquina ao rodar o projeto e insira as mesmas nas credenciais do JWT:

Obs: se a rota for a mesmo que já disponivel no projeto não será necessário alterar.

"JwtSettings": {
  "Issuer": " SUA ROTA ",
  "Audience": "A MESMA ROTA QUE A DE CIMA"
}


4. Aplicar Migrations e Criar o Banco
   
Abra o terminal na raiz do projeto e execute:

dotnet ef migrations add InitialCreate

dotnet ef database update

## Rodando a API

Para rodar o projeto, use:

### dotnet run 

Ou rode pelo Visual Studio mesmo

![image](https://github.com/user-attachments/assets/00d7aab8-e8cd-4e60-81b6-4ecfd9135caa)


```sql
-- Inserindo usuários na tabela [User]
INSERT INTO [User] (Name, Email, Password, CPF, Bank, WalletNumber, Balance)
VALUES 
('João Silva', 'joao@email.com', 'senha123', '12345678901', 1, 'WALLET123', 1000.00),
('Maria Souza', 'maria@email.com', 'senha456', '98765432100', 2, 'WALLET456', 1500.50);

-- Inserindo transações na tabela [Transaction]
INSERT INTO [Transaction] (UserId, CPFDestination, WalletNumberDestination, Bank, Value, TransactionTime)
VALUES 
(1, '98765432100', 'WALLET456', 2, 200.00, GETUTCDATE()),
(2, '12345678901', 'WALLET123', 1, 350.75, GETUTCDATE());




