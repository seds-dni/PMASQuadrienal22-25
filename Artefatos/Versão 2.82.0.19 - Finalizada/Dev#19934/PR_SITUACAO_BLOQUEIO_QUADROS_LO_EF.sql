SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prodesp
-- Create date: 22-05-2019
-- Description:	Obtem a situacao do quadro de um determinado municipio
-- =============================================
CREATE PROCEDURE PR_SITUACAO_BLOQUEIO_QUADROS_LO_EF
	
	@ID_PREFEITURA int = 0
	, @ID_RECURSO int = 0
	, @EXERCICIO int = 0
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	SELECT ID_SITUACAO_QUADRO FROM TB_PREFEITURA_SITUACAO_QUADRO 
	WHERE  ID_PREFEITURA = @ID_PREFEITURA AND ID_RECURSO = @ID_RECURSO AND EXERCICIO = @EXERCICIO 
	RETURN 1;
END
GO
