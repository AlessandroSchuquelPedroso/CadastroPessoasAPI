CREATE TABLE [dbo].[Cadastro] (
    [IdCadastro] INT            IDENTITY (1, 1) NOT NULL,
    [nome]       NVARCHAR (100) NOT NULL,
    [email]      NVARCHAR (80)  NOT NULL,
    [data]       NVARCHAR (10)  NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCadastro] ASC)
);

