/****** Script for SelectTopNRows command from SSMS  ******/
INSERT INTO  [DBPMAS2016].[dbo].[TB_USUARIO]


SELECT [ID_USUARIO]
      ,[ID_PREFEITURA]
      ,[CPF]
      ,[ID_DRADS]
      ,[ID_STATUS]
      ,[ATIVO]
      ,[INSTITUICAO]
      ,[CARGO]
  FROM [DBPMAS2016_CAPACITACAO].[dbo].[TB_USUARIO] WHERE CARGO = 'Tester'