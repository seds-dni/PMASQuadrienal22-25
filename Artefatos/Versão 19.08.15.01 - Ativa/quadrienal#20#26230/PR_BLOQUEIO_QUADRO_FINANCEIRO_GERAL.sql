-- ================================================
-- Procedimento para desbloquear e desbloquear 
-- todos os Quadros de LO e EF
-- 
--
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prodesp
-- Create date: 16/09/2019
-- =============================================
CREATE PROCEDURE PR_PREFEITURA_BLOQUEIO_QUADRO_FINANCEIRO_GERAL
	-- Add the parameters for the stored procedure here
	  @ID_SITUACAO_QUADRO int
	, @ID_RECURSO int
	, @EXERCICIO int
	
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TB_PREFEITURA_SITUACAO_QUADRO SET ID_SITUACAO_QUADRO = @ID_SITUACAO_QUADRO WHERE ID_RECURSO = @ID_RECURSO
END
GO

