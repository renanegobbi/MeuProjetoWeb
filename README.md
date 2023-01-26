# MeuProjetoWeb

Uma aplicação Web desenvolvida em ASP.NET Core para visualizar as funcionalidades desenvolvidas pela API MinhaApi (https://github.com/renanegobbi/MinhaApi).

<h4 align="center"> 
  <a href="#Tecnologias-e-Ferramentas">Tecnologias e ferramentas</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; 
  <a href="#Sobre-o-projeto">Sobre o projeto</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#Demonstração">Demonstração</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#Licença">Licença</a>
</h4>

<br/>

<p align="center">
  <a href="https://opensource.org/licenses/MIT">
    <img src="https://img.shields.io/badge/License-MIT-blue.svg" alt="License MIT">
  </a>
</p>

<div id='Tecnologias-e-Ferramentas'/>

# Tecnologias e ferramentas 

O projeto foi desenvolvido com as seguintes tecnologias e ferramentas:

- [Visual Studio 2022 Community](https://visualstudio.microsoft.com/pt-br/vs/community/)
- [ASP.NET Core 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

<div id='Sobre-o-projeto'/>

# Sobre o projeto

Este é um projeto Web desenvolvido com ASP.NET Core 5.0 para consultar/criar/atualizar/excluir os dados disponibilizados por uma documentação de API, por Swagger, que realiza a gestão de fornecedores e produtos.

# Demonstração

A aplicação é composta de duas telas.   

1 - A tela abaixo, além de cadastrar/alterar/excluir um fornecedor, permite filtrar os fornecedores baseados nos seguintes parâmetros de procura: 

Nome do parâmetro   | Resumo
--------- | ------
Id | Id do fornecedor
Nome | Nome do fornecedor
Descrição | Descrição do fornecedor
Cnpj | CNPJ do fornecedor
Status | Status de ativação do fornecedor

<p align="center">
  <img src="https://github.com/renanegobbi/MeuProjetoWeb/blob/main/github/PrintTelaFornecedor.png">
</p>

2 - A tela abaixo, além de cadastrar/alterar/excluir um produto, permite filtrar os produtos baseados nos seguintes parâmetros de procura: 

Nome do parâmetro   | Resumo
--------- | ------
Id | Id do produto
Fornecedor | Id do fornecedor
Descrição | Descrição do produto
Fabricação | Data de fabricação do produto
Validade | Data de validade do produto
Status | Status de ativação do produto

<p align="center">
  <img src="https://github.com/renanegobbi/MeuProjetoWeb/blob/main/github/PrintTelaProduto.png">
</p>

# Licença
Este projeto está sob a licença do MIT. Consulte a [LICENÇA](https://github.com/TesteReteste/lim/blob/master/LICENSE) para obter mais informações.
