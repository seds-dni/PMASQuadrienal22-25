
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	
	--###############################--###############################--###############################--###############################
	-- BlocoV/Cronograma Desembolo Programas Projetos
	--############################### TB_RECURSO
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2018 AND ID_REF_BLOQUEIO = 29) --Bloco V 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
					([ID_PREFEITURA]
					,[EXERCICIO]
					,[DESBLOQUEADO]
					,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2018 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 29 as ID_REF_BLOQUEIO --Bloco V/FCronogramaDesembolso.aspx
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2019 AND ID_REF_BLOQUEIO = 29)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
					([ID_PREFEITURA]
					,[EXERCICIO]
					,[DESBLOQUEADO]
					,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2019 as EXERCICIO
		, 1 as DESBLOQUEADO 
		, 29 as ID_REF_BLOQUEIO --Bloco V/FCronogramaDesembolso.aspx
		FROM TB_PREFEITURA PREF
	END
END