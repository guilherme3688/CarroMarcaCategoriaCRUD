Entidades:
- Carro
- Marca
- Categoria

Tecnologias empregadas:
- MySQL Server Community v8.2.0
- ASP.NET MVC 7.0 (Core)
- Entity Framework Core v7.0.14
- Knockout.JS

Pacotes Nuget:
- Microsoft.EntityFrameworkCore.Design v7.0.14
- Microsoft.EntityFrameworkCore.Tools v7.0.14
- Pomelo.EntityFrameworkCore.MySql 7.0.0

Bibliotecas JS:
- jQuery-3.7.1
- Knockout.Mapping
- Moment.JS (Utilzado para as formatações de data e hora no frontend)

Como utilizar:

1) Acessar o arquivo appsettings.json e alterar a string de conexão conforme o banco de dados MySQL instalado, caso for utilizar uma outra versão, será necessário alterar a classe Program.cs na linha 11.

2) Após setado o banco de dados, será necessário executar a migration, para que o banco seja criado, utilizar o seguinte comando: "dotnet ef database update" no Console.

3) Uma vez executado os passos anteriores o projeto esta pronto para ser compilado, ao utilizar recomendo cadastrar as 'Categorias' e 'Marcas' primeiro, para que seja possível cadastrar o carro, visto que o botão 'Salvar' não irá habilitar caso estes campos não estejam preenchiidos.

Qualquer dúvida estou a disposição :)
