use Dbpmas_quadrienal_DESENV
go
--############################################################################################################################
--# ORGAO GESTOR
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 10) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 10 as ID_REF_BLOQUEIO --BlocoI/FOrgaoGestor.aspx
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 10)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 10 as ID_REF_BLOQUEIO --Bloco V/FOrgaoGestor.aspx
		FROM TB_PREFEITURA PREF
	END
END
--##############################################################################################################################
--##############################################################################################################################
--##############################################################################################################################


--############################################################################################################################
--# REDE DIRETA 
--############################################################################################################################
--## : PUBLICO 
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 19) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 19 as ID_REF_BLOQUEIO --BlocoIII - Rede Direta
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 19)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 19 as ID_REF_BLOQUEIO --BlocoIII - Rede Direta
		FROM TB_PREFEITURA PREF
	END
END



--############################################################################################################################
--# REDE INDIRETA 
--############################################################################################################################
--## : PRIVADO
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 20) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 20 as ID_REF_BLOQUEIO --BlocoIII- Rede indireta
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 20)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 20 as ID_REF_BLOQUEIO --BlocoIII - Rede indireta
		FROM TB_PREFEITURA PREF
	END
END



--############################################################################################################################
--# REDE DIRETA 
--############################################################################################################################
--## : PUBLICO - REPROGRAMACAO
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 1019) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 1019 as ID_REF_BLOQUEIO --BlocoIII - Rede Direta - Reprog
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 1019)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 1019 as ID_REF_BLOQUEIO --BlocoIII - Rede Direta - Reprog
		FROM TB_PREFEITURA PREF
	END
END



--############################################################################################################################
--# REDE INDIRETA 
--############################################################################################################################
--## : PRIVADO - REPROGRAMACAO
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 1020) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 1020 as ID_REF_BLOQUEIO --BlocoIII- Rede indireta - Reprog
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 1020)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 1020 as ID_REF_BLOQUEIO --BlocoIII - Rede indireta - Reprog
		FROM TB_PREFEITURA PREF
	END
END






--############################################################################################################################
--## : BENEFICIO EVENTUAL
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 22) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 22 as ID_REF_BLOQUEIO --BlocoIII - Beneficio Eventual
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 22)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 22 as ID_REF_BLOQUEIO --BlocoIII - Beneficio Eventual
		FROM TB_PREFEITURA PREF
	END
END



--############################################################################################################################
--## : PROGRANMA E PROJETOS
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 23) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 23 as ID_REF_BLOQUEIO --BlocoIII - Beneficio Eventual
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 23)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 23 as ID_REF_BLOQUEIO --BlocoIII - Beneficio Eventual
		FROM TB_PREFEITURA PREF
	END
END



--############################################################################################################################
--## : Atualização do Diagnóstico
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 75) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 75 as ID_REF_BLOQUEIO --BlocoII - Atualização do Diagnóstico
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 75)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 75 as ID_REF_BLOQUEIO --BlocoII - Atualização do Diagnóstico
		FROM TB_PREFEITURA PREF
	END
END




--############################################################################################################################
--## : Cronograma PSB
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 26) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 26 as ID_REF_BLOQUEIO --BlocoV -  Cronograma PSB
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 26)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 26 as ID_REF_BLOQUEIO --BlocoV -  Cronograma PSB
		FROM TB_PREFEITURA PREF
	END
END


--############################################################################################################################
--## : Cronograma PSEMC
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 27) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 27 as ID_REF_BLOQUEIO --BlocoV -  Cronograma PSEMC
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 27)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 27 as ID_REF_BLOQUEIO --BlocoV -  Cronograma PSEMC
		FROM TB_PREFEITURA PREF
	END
END


--############################################################################################################################
--## : Cronograma PSEAC
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 28) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 28 as ID_REF_BLOQUEIO --BlocoV -  Cronograma PSEAC
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 28)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 28 as ID_REF_BLOQUEIO --BlocoV -  Cronograma PSEAC
		FROM TB_PREFEITURA PREF
	END
END


--############################################################################################################################
--## : Cronograma PP
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 29) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 29 as ID_REF_BLOQUEIO --BlocoV -   Lei Orçamentária
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 29)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 29 as ID_REF_BLOQUEIO --BlocoV -  Execução Financeira
		FROM TB_PREFEITURA PREF
	END
END


--############################################################################################################################
--## : Cronograma PP
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 29) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 29 as ID_REF_BLOQUEIO --Cronograma PP
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 29)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 29 as ID_REF_BLOQUEIO --Cronograma PP
		FROM TB_PREFEITURA PREF
	END
END


--############################################################################################################################
--## : TRANSFERENCIA RENDA
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 25) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 25 as ID_REF_BLOQUEIO --Bloco III - FTransferenciaRenda - Transferencia Renda
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 25)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 25 as ID_REF_BLOQUEIO --Bloco III - FTransferenciaRenda -  Transferencia Renda
		FROM TB_PREFEITURA PREF
	END
END


--############################################################################################################################
--## : Cronograma BENEFICIOS
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 30) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 30 as ID_REF_BLOQUEIO --Cronograma BENEFICIOS
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 30)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 30 as ID_REF_BLOQUEIO --Cronograma BENEFICIOS
		FROM TB_PREFEITURA PREF
	END
END



--############################################################################################################################
--## : Lei Orçamentária
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 78) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 78 as ID_REF_BLOQUEIO --BlocoV -   Lei Orçamentária
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 78)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 78 as ID_REF_BLOQUEIO --BlocoV -   Lei Orçamentária
		FROM TB_PREFEITURA PREF
	END
END


--############################################################################################################################
--## : Execução Financeira
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN
--select * from TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO where ID_PREFEITURA = 8297 AND EXERCICIO IN ( 2017,2018,2019,2020,2021) AND ID_REF_BLOQUEIO = 76
	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 76) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 76 as ID_REF_BLOQUEIO --BlocoV -   Lei Orçamentária
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 76)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 76 as ID_REF_BLOQUEIO --BlocoV -  Execução Financeira
		FROM TB_PREFEITURA PREF
	END
END


--############################################################################################################################
--## : Fontes de recursos do FMAS
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 70) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 70 as ID_REF_BLOQUEIO --BlocoV -   Fontes de recursos do FMAS
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 70)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 70 as ID_REF_BLOQUEIO --BlocoV -  Fontes de recursos do FMAS
		FROM TB_PREFEITURA PREF
	END
END


--############################################################################################################################
--## : Bloco Inicio - bloco0 - Início - Parecer Drads
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 40) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 40 as ID_REF_BLOQUEIO --Bloco0 -  Início - Parecer Drads
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 40)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 40 as ID_REF_BLOQUEIO --Bloco0 -  Início - Parecer Drads
		FROM TB_PREFEITURA PREF
	END
END


--############################################################################################################################
--## : Bloco Inicio - bloco0 - Início - Reprogramacao Drads
--############################################################################################################################
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO')
BEGIN

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO WHERE EXERCICIO = 2020 AND ID_REF_BLOQUEIO = 1040) 
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2020 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 1040 as ID_REF_BLOQUEIO --Bloco0 -  Início - Reprogramacao Drads
		FROM TB_PREFEITURA PREF
	END

	IF NOT EXISTS(SELECT 1 FROM TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO	WHERE EXERCICIO = 2021 AND ID_REF_BLOQUEIO = 1040)
	BEGIN
		INSERT INTO [dbo].[TB_PREFEITURA_EXERCICIO_BLOQUEIO_DESBLOQUEIO]
				   ([ID_PREFEITURA]
				   ,[EXERCICIO]
				   ,[DESBLOQUEADO]
				   ,[ID_REF_BLOQUEIO])
		SELECT 
		PREF.ID as ID_PREFEITURA
		, 2021 as EXERCICIO
		, 0 as DESBLOQUEADO 
		, 1040 as ID_REF_BLOQUEIO --Bloco0 -  Início - Reprogramacao Drads
		FROM TB_PREFEITURA PREF
	END
END
