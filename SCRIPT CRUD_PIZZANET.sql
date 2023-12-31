CREATE DATABASE PizzaNet
GO

USE PizzaNet
GO

CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Sexo] [varchar](20) NOT NULL,
	[Admissao] [date] NOT NULL,
	[Telefone] [varchar](15) NOT NULL,
	[Celular] [varchar] (15) NOT NULL,
	[Endereco] [varchar](100) NOT NULL,
	[Numero] [varchar](10) NOT NULL,
	[CPF] [varchar] (20) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Senha] [varchar](60) NOT NULL,
	[Admin] [varchar](5) NOT NULL
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Pizza](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Sabor] [varchar](100) NOT NULL,
	[Tamanho] [varchar](30) NOT NULL,
	[NumeroFatia] [varchar](30) NOT NULL,
	[Preco] [decimal] (10,2) NOT NULL,	
	[Disponibilidade] [varchar] (30) NOT NULL,
	[Imagem] [varchar] (150) NOT NULL
)ON [PRIMARY]

CREATE TABLE [dbo].[Pedidos](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Valor] [decimal] (10,2) NOT NULL,
	[Quantidade] [int] NOT NULL,
	[Id_Pizza] [int] FOREIGN KEY REFERENCES [dbo].[Pizza]([Id]) NOT NULL,
)ON [PRIMARY]

CREATE TABLE [dbo].[FimPedido](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[QuantItens] [int] NOT NULL,
	[ValorTotal] [decimal] (10,2) NOT NULL,
	[MetodoPag] [varchar] (50) NOT NULL,
	[Troco] [decimal] (10,2) NOT NULL, 
	[Data] [date] NOT NULL
)ON [PRIMARY]

CREATE TABLE [dbo].[Relatorio](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[QuantItens] [int] NOT NULL,
	[ValorTotal] [decimal](10,2) NOT NULL,
	[TotalPago] [decimal](10,2) NOT NULL,
	[MetodoPag] [varchar](50) NOT NULL,
	[Troco] [decimal](10,2) NOT NULL,
	[Data] [date] NOT NULL,
	[Horario] [time] NOT NULL
) ON [PRIMARY]



--PRÉ-CADASTRO
--Usuário: admin Senha: admin (CONTA DE ADMINISTRADOR)
INSERT INTO [dbo].[Usuarios] VALUES ('Daniel','Masculino',GETDATE(),'1299223275','12992232750','R. Primeiro de Janeiro','231','38121841844','admin','YWRtaW4=','SIM')
GO









































