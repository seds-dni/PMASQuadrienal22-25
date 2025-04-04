		ALTER TABLE TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO NOCHECK CONSTRAINT FK_TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO_TB_REF_RECURSO
		IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
		BEGIN
	
			--###############################--###############################--###############################--###############################
			-- BlocoV/Cronograma Desembolo Beneficio Eventual
			--############################### TB_RECURSO
			IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2018 AND ID_REF_BLOQUEIO = 1019) --Bloco V e nao III
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
				, 1019 as ID_REF_BLOQUEIO --BlocoV/CBeneficioEventual.aspx
				FROM TB_PREFEITURA PREF
			END

			IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2019 AND ID_REF_BLOQUEIO = 1019)
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
				, 1019 as ID_REF_BLOQUEIO --Bloco V/Beneficios eventuais.aspx
				FROM TB_PREFEITURA PREF
			END
		END

		IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
		BEGIN
	
			--###############################--###############################--###############################--###############################
			-- BlocoV/Cronograma Desembolo Beneficio Eventual
			--############################### TB_RECURSO
			IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2018 AND ID_REF_BLOQUEIO = 1020) --Bloco V e nao III
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
				, 1020 as ID_REF_BLOQUEIO --BlocoV/CBeneficioEventual.aspx
				FROM TB_PREFEITURA PREF
			END

			IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2019 AND ID_REF_BLOQUEIO = 1020)
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
				, 1020 as ID_REF_BLOQUEIO --Bloco V/Beneficios eventuais.aspx
				FROM TB_PREFEITURA PREF
			END
		END

		