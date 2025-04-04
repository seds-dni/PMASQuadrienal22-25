USE [Dbpmas_quadrienal_PRODUCAO_OUTUBRO_2018]
GO

/****** Object:  View [dbo].[VW_PREFEITURA_ACAO_PLANEJAMENTO]    Script Date: 14/11/2018 14:56:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER VIEW [dbo].[VW_PREFEITURA_ACAO_PLANEJAMENTO]
AS
SELECT P.ID
	   ,P.ID_PREFEITURA
	   ,E.ID AS ID_EIXO_ACAO_PLANEJAMENTO
	   ,A.ID AS ID_ACAO_PLANEJAMENTO
	   ,A.NOME AS IDENTIFICACAO
	   ,E.NOME AS EIXO
	   ,P.NOME
	   ,isnull(P.VALOR_ESTIMATIVA_CUSTO, 0.0) AS PREVISAO_ORCAMENTARIA
	   ,dbo.FORMATAR_MES_ANO(P.MES_PREVISTO_INICIO, P.ANO_PREVISTO_INICIO) + ' - ' +dbo.FORMATAR_MES_ANO(P.MES_PREVISTO_TERMINO, p.ANO_PREVISTO_TERMINO) as PREVISAO_EXECUCAO
	   ,P.EXERCICIO
 FROM TB_PREFEITURA_ACAO_PLANEJAMENTO P
INNER JOIN TB_ACAO_PLANEJAMENTO A ON(A.ID = P.ID_ACAO_PLANEJAMENTO)
INNER JOIN TB_EIXO_ACAO_PLANEJAMENTO E ON(E.ID = A.ID_EIXO_ACAO_PLANEJAMENTO)




GO


