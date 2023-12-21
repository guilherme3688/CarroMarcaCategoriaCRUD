Entidades:
- Carro

{
  Id: int primaryKey
  Modelo: varChar(50)
  AnoLancamento: varChar(4)
  Idmarca: int foreignKey
  IdCategoria: int foreignKey
  DataCadastro: DateTime
  DataExclusao: DateTime
}

- Marca

{
  Id: int primaryKey
  NomeCategoria: varChar(50)
  DataCadastro: DateTime
  DataExclusao: DateTime
}

- Categoria

{
  Id: int primaryKey
  NomeMarca: varChar(50)
  DataCadastro: DateTime
  DataExclusao: DateTime
}

Tecnologias empregadas:
- MySQL Server Community v8.2.0
- ASP.NET MVC 7.0 (Core)
- Entity Framework Core v7.0.14
- Knockout.JS v3.5.1

Pacotes Nuget:
- Microsoft.EntityFrameworkCore.Design v7.0.14
- Microsoft.EntityFrameworkCore.Tools v7.0.14
- Pomelo.EntityFrameworkCore.MySql v7.0.0

Bibliotecas:
- jQuery-3.7.1
- Knockout.Mapping
- Moment.JS (Utilzado para as formatações de data e hora no frontend)
- Fonts Awasome (Para os icones)
- Bootstrap (Ja incluido por padrão no ASP.NET)

Como utilizar:

1) Acessar o arquivo appsettings.json e alterar a string de conexão conforme o banco de dados MySQL instalado, caso for utilizar uma outra versão, será necessário alterar na classe Program.cs na linha 11 (Por padrão esta setado para usar a versão 8.2.0)

2) Após setado o banco de dados, será necessário executar a migration, para que o banco seja criado, utilizar o seguinte comando: "dotnet ef database update" no Console.

3) Uma vez executado os passos anteriores o projeto esta pronto para ser compilado, ao utilizar recomendo cadastrar as 'Categorias' e 'Marcas' primeiro, para que seja possível cadastrar o carro, visto que o botão 'Salvar' possui uma validação, onde o mesmo não irá habilitar caso estes campos não estejam preenchiidos.

Att. Guilherme Silva
