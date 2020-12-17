/* --------------------------------------------------------
	CREATE DATABASE Receitas_P7
-------------------------------------------------------- */
USE [master]
GO

CREATE DATABASE [Receitas_P7_v2]
 CONTAINMENT = NONE
 ON PRIMARY
( NAME = N'Receitas_P7', FILENAME = N'D:\APC-2020-11-P7\Database\Receitas_P7_v2.mdf', SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Receitas_P7_log', FILENAME = N'D:\APC-2020-11-P7\Database\Receitas_P7_v2_log.ldf', SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO


/* --------------------------------------------------------
	CREATE TABLE CATEGORIA
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO

CREATE TABLE [dbo].[Categoria]
(
	[CategoriaID] INT IDENTITY NOT NULL PRIMARY KEY, 
    [NomeCategoria] NVARCHAR(20) NOT NULL
)

/* --------------------------------------------------------
	CREATE TABLE RECEITA
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO

CREATE TABLE [dbo].[Receita] (
    [ReceitaID]    INT             IDENTITY (1, 1) NOT NULL,
    [CategoriaID]  INT             NOT NULL,
    [NomeReceita]  NVARCHAR (50)   NOT NULL,
    [ModoPreparo]  NVARCHAR (1000) NOT NULL,
    [Dificuldade]  NVARCHAR (10)   NOT NULL,
    [TempoPreparo] INT             NOT NULL,
    [DataRegisto]  DATE            NOT NULL,
    PRIMARY KEY CLUSTERED ([ReceitaID] ASC),
	CONSTRAINT [FK_dbo.Receita_dbo.Categoria_CategoriaID] FOREIGN KEY ([CategoriaID]) REFERENCES [dbo].[Categoria] ([CategoriaID]) ON DELETE CASCADE
);

/* --------------------------------------------------------
	CREATE TABLE INGREDIENTE
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO

CREATE TABLE [dbo].[Ingrediente] (
    [IngredienteID]     INT             IDENTITY (1,1) NOT NULL, 
    [ReceitaID]         INT             NOT NULL,
    [NomeIngrediente]   NVARCHAR (50)   NOT NULL,
    [Quantidade]        NVARCHAR (50)   NOT NULL,
    PRIMARY KEY CLUSTERED ([IngredienteID] ASC),
    CONSTRAINT [FK_dbo.Ingrediente_dbo.Receita_ReceitaID] FOREIGN KEY ([ReceitaID]) REFERENCES [dbo].Receita ([ReceitaID]) ON DELETE CASCADE
);

/* --------------------------------------------------------
	uspCategoriaViewAll
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO

CREATE PROCEDURE uspCategoriaViewAll
AS
    BEGIN
        SELECT * FROM [dbo].[Categoria]
    END
GO

/* --------------------------------------------------------
	uspReceitaIDViewAll
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO

CREATE PROCEDURE uspReceitaIDViewAll
AS
    BEGIN
        SELECT * FROM [dbo].[Receita]
    END
GO

/* --------------------------------------------------------
	uspAddOrUpdateReceita
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO

CREATE PROCEDURE uspAddOrUpdateReceita
    @receitaId int,
    @categoriaId int,
    @nomeReceita nvarchar (50),
    @modoPreparo nvarchar(1000),
    @dificuldade nvarchar(10),
    @tempoPreparo int,
    @dataRegisto date
AS
    BEGIN
        IF(@receitaId = 0)
            BEGIN
                INSERT INTO [dbo].[Receita]([CategoriaID], [NomeReceita], [ModoPreparo], [Dificuldade], [TempoPreparo], [DataRegisto])
                VALUES (@categoriaId, @nomeReceita, @modoPreparo, @dificuldade, @tempoPreparo, @dataRegisto)                 
            END
        ELSE
            BEGIN
                UPDATE [dbo].[Receita]
                SET [CategoriaID] = @categoriaId, [NomeReceita] = @nomeReceita, [ModoPreparo] = @modoPreparo, [Dificuldade] = @dificuldade, [TempoPreparo] = @tempoPreparo, [DataRegisto] = @dataRegisto
                WHERE [ReceitaID] = @receitaId
            END
        SELECT
            MAX([ReceitaID])
        FROM [dbo].[Receita]
    END
GO

/* --------------------------------------------------------
	uspAddIngrediente
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO

CREATE PROCEDURE uspAddIngrediente
    @receitaId int,
    @nomeIngrediente nvarchar (50),
    @quantidade nvarchar(50)
AS
    BEGIN
        INSERT INTO [dbo].[Ingrediente]([ReceitaID], [NomeIngrediente], [Quantidade])
        VALUES (@receitaId, @nomeIngrediente, @quantidade)
    END
GO
        
/* --------------------------------------------------------
	uspReceitaSearchById
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO

CREATE PROCEDURE uspReceitaSearchById
    @receitaId int
AS
    BEGIN
        SELECT * FROM [dbo].[Receita]
        WHERE [ReceitaID] = @receitaId
    END
GO

/* --------------------------------------------------------
	uspCategoriaSearchById
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO

CREATE PROCEDURE uspCategoriaSearchById
    @categoriaId int
AS
    BEGIN
        SELECT 
            [NomeCategoria]
        FROM [dbo].[Categoria]
        WHERE [CategoriaID] = @categoriaId
    END
GO

/* --------------------------------------------------------
	uspDeleteReceitaById
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO

CREATE PROCEDURE uspDeleteReceitaById
    @receitaId int
AS
    BEGIN
        DELETE FROM [dbo].[Receita]
        WHERE [ReceitaID] = @receitaId
    END
GO
       
  /* --------------------------------------------------------
	uspReceitaViewAll
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO    

CREATE PROCEDURE uspReceitaViewAll
AS
    BEGIN
        SELECT
            Receita.ReceitaID,
            Receita.NomeReceita,
            Categoria.NomeCategoria,
            Receita.ModoPreparo,
            Receita.Dificuldade,
            Receita.TempoPreparo,
            Receita.DataRegisto
        FROM [dbo].[Receita] AS Receita
        INNER JOIN [dbo].[Categoria] AS Categoria
        ON Receita.CategoriaID = Categoria.CategoriaID
    END
GO

/* --------------------------------------------------------
	
uspIngredientesByReceitaId
-------------------------------------------------------- */
USE [Receitas_P7_v2]
GO    

CREATE PROCEDURE uspIngredientesByReceitaId
    @receitaId int
AS
    BEGIN
        SELECT
            [NomeIngrediente],
            [Quantidade]
        FROM [dbo].[Ingrediente]
        WHERE ReceitaId = @receitaId
    END
GO