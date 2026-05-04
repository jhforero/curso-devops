-Material para la clase 6

dotnet new xunit -o ApiContactos.Tests

dotnet add ApiContactos.Tests reference ContactosAPI

dotnet new sln -n ApiContactos

dotnet sln ApiContactos.sln add ContactosAPI ApiContactos.tests

dotnet add ApiContactos.Tests package Microsoft.AspNetCore.Mvc.Testing --version 8.0.15

-Agrega este código a tu proyecto de pruebas
