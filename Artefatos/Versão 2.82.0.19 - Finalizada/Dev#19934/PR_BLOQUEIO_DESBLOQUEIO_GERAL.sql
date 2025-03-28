USE [Dbpmas_quadrienal_DESENV]
GO
/****** Object:  StoredProcedure [dbo].[PR_BLOQUEIO_DESBLOQUEIO_GERAL]    Script Date: 21/05/2019 18:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prodesp
-- Create date: 21/05/2019
-- Description:	Bloqueia e desbloqueia TODOS OS QUADROS abaixo dos municípios:
--  > Execucao Financeira
--  > Lei Orçamentária
--  de acordo com exercício
-- =============================================
CREATE PROCEDURE [dbo].[PR_BLOQUEIO_DESBLOQUEIO_GERAL]
	  @ID_RECURSO int = 0
	, @ID_SITUACAO int = 0
	, @EXERCICIO int = 0
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TB_PREFEITURA_SITUACAO_QUADRO 
	SET ID_SITUACAO_QUADRO = @ID_SITUACAO 
	WHERE  ID_RECURSO = @ID_RECURSO AND EXERCICIO = @EXERCICIO 
	RETURN 1;
END
