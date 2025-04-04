USE [Dbpmas_quadrienal_PRODUCAO_OUTUBRO_2018]
GO

/****** Object:  View [dbo].[VW_UNIDADE_PUBLICA]    Script Date: 24/10/2018 19:21:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[VW_UNIDADE_PUBLICA]  
AS      
SELECT   T.ID  
		,T.RAZAO_SOCIAL  
		,T.CNPJ  
		,T.ID_PREFEITURA  
	    ,(select count(C.ID) from TB_LOCAL_EXECUCAO_PUBLICO C where C.ID_UNIDADE_PUBLICA = T.ID and C.DESATIVADO = 'False') AS TOTAL_LOCAIS  
	    ,(select count(C.ID) from TB_LOCAL_EXECUCAO_PUBLICO C where C.ID_UNIDADE_PUBLICA = T.ID and C.DESATIVADO = 'TRUE') AS TOTAL_LOCAIS_DESATIVADOS  
	    ,CASE (SELECT  COUNT(A.ID)    
				FROM TB_LOCAL_EXECUCAO_PUBLICO A  
					INNER JOIN TB_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO B ON(A.ID = B.ID_LOCAL_EXECUCAO_PUBLICO)                  
					INNER JOIN TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PUBLICO B1 
						ON(B.ID = B1.ID_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO AND B1.EXERCICIO = 2018)
				WHERE (
					       --(B.VALOR_ESTADUAL_ASSISTENCIA IS NOT NULL AND B.VALOR_ESTADUAL_ASSISTENCIA > 0) 
						   (B1.VALOR_ESTADUAL_ASSISTENCIA IS NOT NULL AND B1.VALOR_ESTADUAL_ASSISTENCIA > 0) 
					    --OR (B.VALOR_ESTADUALIZADO IS NOT NULL AND B.VALOR_ESTADUALIZADO > 0)
					   )  
					   AND A.ID_UNIDADE_PUBLICA = T.ID)     
   WHEN 0 THEN 'N�o'     
   ELSE 'Sim'     
   END 'COFINANCIAMENTO'    
  --,CASE (SELECT COUNT(B.SERVICO_ESTADUALIZADO)  
		-- FROM TB_LOCAL_EXECUCAO_PUBLICO A  
		--			INNER JOIN TB_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO B  
		--				ON A.ID = B.ID_LOCAL_EXECUCAO_PUBLICO  
	 --    WHERE A.ID_UNIDADE_PUBLICA = T.ID  and B.SERVICO_ESTADUALIZADO = 1)  
  ,CASE (SELECT 0)
  WHEN 0  
		THEN 'N�o'  
		ELSE 'Sim'     
		END 'ESTADUALIZADO'  
 ,(
		--SELECT COALESCE(SUM(COALESCE(S.VALOR_ESTADUAL_ASSISTENCIA ,0) + COALESCE(S.VALOR_ESTADUALIZADO,0)),0) 
		--SELECT COALESCE(SUM(COALESCE(S1.VALOR_ESTADUAL_ASSISTENCIA ,0) + COALESCE(S.VALOR_ESTADUALIZADO,0)),0) 
		SELECT COALESCE(SUM(COALESCE(S1.VALOR_ESTADUAL_ASSISTENCIA ,0) ),0) 
		FROM TB_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO S  
		INNER JOIN TB_LOCAL_EXECUCAO_PUBLICO L ON(L.ID = S.ID_LOCAL_EXECUCAO_PUBLICO AND L.ID_UNIDADE_PUBLICA = T.ID)
		INNER JOIN TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PUBLICO S1 
			ON(S.ID = S1.ID_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO AND S1.EXERCICIO = 2018)
  ) +  
  (
		--SELECT COALESCE(SUM(COALESCE(S.VALOR_ESTADUAL_ASSISTENCIA ,0) + COALESCE(S.VALOR_ESTADUALIZADO,0)),0) 
		--SELECT COALESCE(SUM(COALESCE(S1.VALOR_ESTADUAL_ASSISTENCIA ,0) + COALESCE(S.VALOR_ESTADUALIZADO,0)),0) 
		SELECT COALESCE(SUM(COALESCE(S1.VALOR_ESTADUAL_ASSISTENCIA ,0) ),0) 
		FROM TB_SERVICOS_RECURSOS_FINANCEIROS_CRAS S  
		INNER JOIN TB_CRAS L ON(L.ID = S.ID_CRAS AND L.ID_UNIDADE_PUBLICA = T.ID)
		INNER JOIN TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CRAS S1 
			ON(S.ID = S1.ID_SERVICOS_RECURSOS_FINANCEIROS_CRAS AND S1.EXERCICIO = 2018)
  ) +  
  (
		--SELECT COALESCE(SUM(COALESCE(S.VALOR_ESTADUAL_ASSISTENCIA ,0) + COALESCE(S.VALOR_ESTADUALIZADO,0)),0) 
		--SELECT COALESCE(SUM(COALESCE(S1.VALOR_ESTADUAL_ASSISTENCIA ,0) + COALESCE(S.VALOR_ESTADUALIZADO,0)),0) 
		SELECT COALESCE(SUM(COALESCE(S1.VALOR_ESTADUAL_ASSISTENCIA ,0) ),0) 
		FROM TB_SERVICOS_RECURSOS_FINANCEIROS_CREAS S  
		INNER JOIN TB_CREAS L ON(L.ID = S.ID_CREAS AND L.ID_UNIDADE_PUBLICA = T.ID)
		INNER JOIN TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CREAS S1 
			ON(S.ID = S1.ID_SERVICOS_RECURSOS_FINANCEIROS_CREAS AND S1.EXERCICIO = 2018)
  ) +  
  (
		--SELECT COALESCE(SUM(COALESCE(S.VALOR_ESTADUAL_ASSISTENCIA ,0) + COALESCE(S.VALOR_ESTADUALIZADO,0)),0) 
		--SELECT COALESCE(SUM(COALESCE(S1.VALOR_ESTADUAL_ASSISTENCIA ,0) + COALESCE(S.VALOR_ESTADUALIZADO,0)),0) 
		SELECT COALESCE(SUM(COALESCE(S1.VALOR_ESTADUAL_ASSISTENCIA ,0) ),0) 
		FROM TB_SERVICOS_RECURSOS_FINANCEIROS_CENTRO_POP S  
		INNER JOIN TB_CENTRO_POP L ON(L.ID = S.ID_CENTRO_POP AND L.ID_UNIDADE_PUBLICA = T.ID)
		INNER JOIN TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CENTRO_POP S1 
			ON(S.ID = S1.ID_SERVICOS_RECURSOS_FINANCEIROS_CENTRO_POP AND S1.EXERCICIO = 2018)
  )  AS VALOR_COFINANCIAMENTO_ESTADUAL    

 ,(
	SELECT COALESCE(SUM(S.NUMERO_ATENDIDOS),0) 
	FROM TB_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO S  
	INNER JOIN TB_LOCAL_EXECUCAO_PUBLICO L ON(L.ID = S.ID_LOCAL_EXECUCAO_PUBLICO AND L.ID_UNIDADE_PUBLICA = T.ID)
	) AS NUMERO_ATENDIDOS  
 ,(
	SELECT COALESCE(
						SUM(R1.VALOR_MUNICIPAL_ASSISTENCIA 
						  + R1.VALOR_MUNICIPAL_FMDCA 
						  + R1.VALOR_ESTADUAL_ASSISTENCIA 
						  + R1.VALOR_ESTADUAL_FEDCA 
						  + R1.VALOR_FEDERAL_ASSISTENCIA 
						  + R1.VALOR_FEDERAL_FNDCA 
						  + ISNULL(R1.VALOR_MUNICIPAL_FMI, 0.0) 
						  + ISNULL(R1.VALOR_ESTADUAL_FEI, 0.0) 
						  + ISNULL(R1.VALOR_FEDERAL_FNI, 0) 
						  --+ COALESCE(R.VALOR_ESTADUALIZADO,0)
				 ), 0)   
 FROM TB_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO R  
 INNER JOIN TB_LOCAL_EXECUCAO_PUBLICO L ON(L.ID = R.ID_LOCAL_EXECUCAO_PUBLICO AND L.ID_UNIDADE_PUBLICA = T.ID)
 INNER JOIN TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PUBLICO R1 
	ON(R.ID = R1.ID_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO AND R1.EXERCICIO = 2018)
 ) AS PREVISAO_ORCAMENTARIA,  
			 (select count(C.ID) from TB_CRAS C where C.ID_UNIDADE_PUBLICA = T.ID and C.DESATIVADO = 'False') as TOTAL_CRAS,  
			 (select count(C.ID) from TB_CREAS C where C.ID_UNIDADE_PUBLICA = T.ID and C.DESATIVADO= 'False') as TOTAL_CREAS,  
			 (select count(C.ID) from TB_CENTRO_POP C where C.ID_UNIDADE_PUBLICA = T.ID and C.DESATIVADO = 'False') as TOTAL_CENTRO_POP,  
			 (select count(C.ID) from TB_CRAS C where C.ID_UNIDADE_PUBLICA = T.ID and C.DESATIVADO = 'TRUE') as TOTAL_CRAS_DESATIVADOS,  
			 (select count(C.ID) from TB_CREAS C where C.ID_UNIDADE_PUBLICA = T.ID and C.DESATIVADO= 'TRUE') as TOTAL_CREAS_DESATIVADOS,  
			 (select count(C.ID) from TB_CENTRO_POP C where C.ID_UNIDADE_PUBLICA = T.ID and C.DESATIVADO = 'TRUE') as TOTAL_CENTRO_POP_DESATIVADOS  
  ,T.[DESATIVADO]  
     ,T.[ID_MOTIVO_DESATIVACAO]  
     ,T.[DATA_DESATIVACAO]  
     ,T.[DETALHAMENTO]  
     ,T.[DATA_REGISTRO_LOG]  
 FROM [TB_UNIDADE_PUBLICA] AS T  
 LEFT JOIN [TB_LOCAL_EXECUCAO_PUBLICO] L ON(L.ID_UNIDADE_PUBLICA = T.ID)  
 GROUP BY T.ID,T.RAZAO_SOCIAL,T.CNPJ,T.ID_PREFEITURA, T.DESATIVADO,T.[ID_MOTIVO_DESATIVACAO]  
     ,T.[DATA_DESATIVACAO] ,T.[DETALHAMENTO] ,T.[DATA_REGISTRO_LOG]  
GO


