
# 💳 Automação de Liquidação de Contas no VHSYS  

![GitHub repo size](https://img.shields.io/github/repo-size/S41T4M4/bot_liquidar_contas)  
![GitHub last commit](https://img.shields.io/github/last-commit/S41T4M4/bot_liquidar_contas)  

Automação responsável por **liquidar automaticamente contas a receber** no VHSYS, com base em boletos extraídos de um arquivo HTML. Desenvolvida em **C# + .NET**, integrando banco de dados e API VHSYS.

---

## 📌 Funcionalidades  

✅ Leitura de boletos exportados do VHSYS (.xls)  
✅ Extração e filtragem de títulos com desconto  
✅ Consulta ao banco de dados SQL Server  
✅ Consulta à API VHSYS  
✅ Liquidação automática de receitas válidas  

---

## ⚙️ Pré-requisitos  

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)  
- Banco de dados SQL Server com tabelas configuradas  
- Acesso à API do VHSYS  

---

## 🧩 Configuração  

### 1. `appsettings.json`  

Crie um arquivo `appsettings.json` na raiz do projeto com a seguinte estrutura:  

```json
{
  "ConnectionStrings": {
    "Default": "Server=...;Database=...;User Id=...;Password=...;"
  },
  "VHSYS": {
    "AccessToken": "seu_access_token",
    "SecretToken": "seu_secret_token"
  }
}
```

### 2. Caminho do arquivo de boletos  

No `Program.cs`, defina o caminho absoluto para o arquivo `.xls` exportado do VHSYS:

```csharp
string htmlFilePath = "C:\caminho\para\arquivo.xls";
```

---

## 🚀 Execução  

Compile e execute a aplicação com os comandos abaixo:

```bash
dotnet build
dotnet run
```

Durante a execução, o terminal exibirá o status de cada conta processada.


---


**Criado por:** [Vitor Ibraim](https://github.com/S41T4M4)
