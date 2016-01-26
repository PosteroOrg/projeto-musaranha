# Projeto Musaranha

**Instituição**: Instituto Federal do Rio Grande do Norte

**Curso**: Técnico em Informática para Internet

**Período**: 4º

**Disciplina**: Projeto de Desenvolvimento de Sistemas para Internet

**Professor**: Fabiano Papaiz

**Arquivo**: [projdesenvsist-projeto.pdf](http://diatinf.ifrn.edu.br/antigo/lib/exe/fetch.php?media=corpodocente:papaiz:09.0-projdesenvsist-projeto.pdf)

**Data de Início**: 30 nov. 2015

**Data de Término**: 14 mar. 2015

O sistema deverá oferecer funcionalidades eficientes e necessárias para o gerenciamento de uma loja.

##1 Gerenciamento de Clientes
O sistema deverá permitir cadastrar, atualizar e excluir Clientes além de oferecer listagem. 

**Os dados a serem cadastrados são**:

- Nome
- Telefone (s)
- Cidade
- Estado
- Endereço
- CPF ou CPNJ
- E-mail

##2 Gerenciamento de Funcionários
O sistema deverá permitir cadastrar, atualizar e excluir Funcionários além de oferecer listagem. 

**Os dados a serem cadastrados são**:

- Nome
- Telefone (s)
- Documento de Identidade
- Carteira de Trabalho
- Salário
- Categoria (Motorista ou Técnico)
- Observação

##3 Gerenciamento de Fornecedores
O sistema deverá permitir cadastrar, atualizar e excluir Fornecedores além de oferecer listagem. 

**Os dados a serem cadastrados são**:

- Nome
- Telefone (s)
- Cidade
- Estado
- Endereço
- CPF ou CNPJ
- E-mail

##4 Gerenciamento de Produtos
O sistema deverá permitir cadastrar, atualizar e excluir Produtos além de oferecer listagem. Apenas são cadastradas as descrições dos produtos.

##5 Gerenciamento de Pagamentos
O sistema deverá permitir cadastrar, atualizar e excluir Pagamentos além de oferecer listagem e cálculo de valor devido. 

**Os dados a serem cadastrados são**:

- Funcionário
- Data
- Mês e Ano de Referência
- Valor Pago

##6 Gerenciamento de Compras
O sistema deverá permitir cadastrar, atualizar e excluir Compras além de oferecer listagem, filtragem e cálculo de valor total. 

**Os dados a serem cadastrados são**:

- Fornecedor
- Data
- Itens (Produto, Unidade, Quantidade, Preço Unitário)
- Desconto

**Os dados oferecidos na filtragem são**:

- Fornecedor
- Período (Data Inicial e Data Final)
- Produto

##7 Gerenciamento de Vendas
O sistema deverá permitir cadastrar, atualizar e excluir Vendas além de oferecer listagem, filtragem e cálculo de valor total. 

**Os dados a serem cadastrados são**:

- Cliente
- Data
- Itens (Produto, Unidade, Quantidade, Preço Unitário)
- Desconto

**Os dados oferecidos na filtragem são**:

- Cliente
- Período (Data Inicial e Data Final)
- Produto

##8 Geração de Relatório
O sistema deverá permitir a geração de relatórios de Vendas e Compras utilizando de filtragens, além de permitir a emissão de recibos dos Pagamentos. 

**Os dados oferecidos na filtragem são**:

- Período (Data Inicial e Data Final)
- Produto
- Cliente (Vendas) ou Fornecedor (Compras)
