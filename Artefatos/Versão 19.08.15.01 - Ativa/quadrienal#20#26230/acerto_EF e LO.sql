use Dbpmas_quadrienal_DESENV
go

PRINT 'DELETADOS OS EXERCICIOS IGUAIS A ZERO'
DELETE FROM TB_PREFEITURA_SITUACAO_QUADRO WHERE EXERCICIO = 0 

DECLARE @ID_PREFEITURA int
DECLARE lista CURSOR FOR Select ID FROM TB_PREFEITURA
OPEN lista
	FETCH NEXT FROM lista INTO @ID_PREFEITURA
	WHILE @@FETCH_STATUS = 0
	BEGIN

		PRINT 'INICIALIZADO EF - 2018'
		IF NOT EXISTS (Select 1 from TB_PREFEITURA_SITUACAO_QUADRO WHERE ID_PREFEITURA = @ID_PREFEITURA AND ID_RECURSO = 143 AND EXERCICIO = 2018)
		BEGIN
			PRINT 'ATUALIZADO EF EXERCICIO 2018: [ PREFEITURA' + CONVERT(VARCHAR(4), @ID_PREFEITURA ) + '] ';
			
				INSERT INTO [dbo].[TB_PREFEITURA_SITUACAO_QUADRO]
						   ([ID_PREFEITURA]
						   ,[ID_SITUACAO_QUADRO]
						   ,[ID_RECURSO]
						   ,[EXERCICIO])
					 VALUES
						   (@ID_PREFEITURA			--(ID_PREFEITURA, int,>
						    , 7						--,<ID_SITUACAO_QUADRO, int,>
						    , 143					--,<ID_RECURSO, int,>
						    , 2018 )				--,<EXERCICIO, int,>)
		END

		---------------------------------------------------------------------------------------------------------------------------------------------------------------
		PRINT 'INICIALIZADO EF - 2019'
		IF NOT EXISTS (Select 1 from TB_PREFEITURA_SITUACAO_QUADRO WHERE ID_PREFEITURA = @ID_PREFEITURA AND ID_RECURSO = 143 AND EXERCICIO = 2019)
		BEGIN
			PRINT 'ATUALIZADO EF EXERCICIO 2019: [ PREFEITURA' + CONVERT(VARCHAR(4), @ID_PREFEITURA ) + ' ] ';
			
				INSERT INTO [dbo].[TB_PREFEITURA_SITUACAO_QUADRO]
						   ([ID_PREFEITURA]
						   ,[ID_SITUACAO_QUADRO]
						   ,[ID_RECURSO]
						   ,[EXERCICIO])
					 VALUES
						   (@ID_PREFEITURA			--(ID_PREFEITURA, int,>
						    , 7						--,<ID_SITUACAO_QUADRO, int,>
						    , 143					--,<ID_RECURSO, int,>
						    , 2019 )				--,<EXERCICIO, int,>)
		END

		---------------------------------------------------------------------------------------------------------------------------------------------------------------
		PRINT 'INICIALIZADO EF - 2020'
		IF NOT EXISTS (Select 1 from TB_PREFEITURA_SITUACAO_QUADRO WHERE ID_PREFEITURA = @ID_PREFEITURA AND ID_RECURSO = 143 AND EXERCICIO = 2020)
		BEGIN
			PRINT 'ATUALIZADO EF EXERCICIO 2020: [ PREFEITURA' + CONVERT(VARCHAR(4), @ID_PREFEITURA) + ' ] ';
			
				INSERT INTO [dbo].[TB_PREFEITURA_SITUACAO_QUADRO]
						   ([ID_PREFEITURA]
						   ,[ID_SITUACAO_QUADRO]
						   ,[ID_RECURSO]
						   ,[EXERCICIO])
					 VALUES
						   (@ID_PREFEITURA			--(ID_PREFEITURA, int,>
						    , 7						--,<ID_SITUACAO_QUADRO, int,>
						    , 143					--,<ID_RECURSO, int,>
						    , 2020 )				--,<EXERCICIO, int,>)
		END

		---------------------------------------------------------------------------------------------------------------------------------------------------------------
		PRINT 'INICIALIZADO EF - 2021'
		IF NOT EXISTS (Select 1 from TB_PREFEITURA_SITUACAO_QUADRO WHERE ID_PREFEITURA = @ID_PREFEITURA AND ID_RECURSO = 143 AND EXERCICIO = 2021)
		BEGIN
			PRINT 'ATUALIZADO EF EXERCICIO 2021: [ PREFEITURA' + CONVERT(VARCHAR(4), @ID_PREFEITURA) + ' ] ';
			
				INSERT INTO [dbo].[TB_PREFEITURA_SITUACAO_QUADRO]
						   ([ID_PREFEITURA]
						   ,[ID_SITUACAO_QUADRO]
						   ,[ID_RECURSO]
						   ,[EXERCICIO])
					 VALUES
						   (@ID_PREFEITURA			--(ID_PREFEITURA, int,>
						    , 7						--,<ID_SITUACAO_QUADRO, int,>
						    , 143					--,<ID_RECURSO, int,>
						    , 2021 )				--,<EXERCICIO, int,>)
		END
		---------------------------------------------------------------------------------------------------------------------------------------------------------------
		---------------------------------------------------------------------------------------------------------------------------------------------------------------
		---------------------------------------------------------------------------------------------------------------------------------------------------------------
			PRINT 'INICIALIZADO LO - 2018'
		IF NOT EXISTS (Select 1 from TB_PREFEITURA_SITUACAO_QUADRO WHERE ID_PREFEITURA = @ID_PREFEITURA AND ID_RECURSO = 160 AND EXERCICIO = 2018)
		BEGIN
			PRINT 'ATUALIZADO LO EXERCICIO 2018: [ PREFEITURA' + CONVERT(VARCHAR(4), @ID_PREFEITURA) + '] ';
			
				INSERT INTO [dbo].[TB_PREFEITURA_SITUACAO_QUADRO]
						   ([ID_PREFEITURA]
						   ,[ID_SITUACAO_QUADRO]
						   ,[ID_RECURSO]
						   ,[EXERCICIO])
					 VALUES
						   (@ID_PREFEITURA			--(ID_PREFEITURA, int,>
						    , 7						--,<ID_SITUACAO_QUADRO, int,>
						    , 160					--,<ID_RECURSO, int,>
						    , 2018 )				--,<EXERCICIO, int,>)
		END

		---------------------------------------------------------------------------------------------------------------------------------------------------------------
		PRINT 'INICIALIZADO LO - 2019'
		IF NOT EXISTS (Select 1 from TB_PREFEITURA_SITUACAO_QUADRO WHERE ID_PREFEITURA = @ID_PREFEITURA AND ID_RECURSO = 160 AND EXERCICIO = 2019)
		BEGIN
			PRINT 'ATUALIZADO LO EXERCICIO 2019: [ PREFEITURA' + CONVERT(VARCHAR(4), @ID_PREFEITURA) + '] ';
			
				INSERT INTO [dbo].[TB_PREFEITURA_SITUACAO_QUADRO]
						   ([ID_PREFEITURA]
						   ,[ID_SITUACAO_QUADRO]
						   ,[ID_RECURSO]
						   ,[EXERCICIO])
					 VALUES
						   (@ID_PREFEITURA			--(ID_PREFEITURA, int,>
						    , 7						--,<ID_SITUACAO_QUADRO, int,>
						    , 160					--,<ID_RECURSO, int,>
						    , 2019 )				--,<EXERCICIO, int,>)
		END

		---------------------------------------------------------------------------------------------------------------------------------------------------------------
		PRINT 'INICIALIZADO LO - 2020'
		IF NOT EXISTS (Select 1 from TB_PREFEITURA_SITUACAO_QUADRO WHERE ID_PREFEITURA = @ID_PREFEITURA AND ID_RECURSO = 160 AND EXERCICIO = 2020)
		BEGIN
			PRINT 'ATUALIZADO LO EXERCICIO 2020: [ PREFEITURA' + CONVERT(VARCHAR(4), @ID_PREFEITURA) + '] ';
			
				INSERT INTO [dbo].[TB_PREFEITURA_SITUACAO_QUADRO]
						   ([ID_PREFEITURA]
						   ,[ID_SITUACAO_QUADRO]
						   ,[ID_RECURSO]
						   ,[EXERCICIO])
					 VALUES
						   (@ID_PREFEITURA			--(ID_PREFEITURA, int,>
						    , 7						--,<ID_SITUACAO_QUADRO, int,>
						    , 160					--,<ID_RECURSO, int,>
						    , 2020 )				--,<EXERCICIO, int,>)
		END

			---------------------------------------------------------------------------------------------------------------------------------------------------------------
		PRINT 'INICIALIZADO LO - 2021'
		IF NOT EXISTS (Select 1 from TB_PREFEITURA_SITUACAO_QUADRO WHERE ID_PREFEITURA = @ID_PREFEITURA AND ID_RECURSO = 160 AND EXERCICIO = 2021)
		BEGIN
			PRINT 'ATUALIZADO LO EXERCICIO 2021: [ PREFEITURA' + CONVERT(VARCHAR(4), @ID_PREFEITURA ) + '] ';
			
				INSERT INTO [dbo].[TB_PREFEITURA_SITUACAO_QUADRO]
						   ([ID_PREFEITURA]
						   ,[ID_SITUACAO_QUADRO]
						   ,[ID_RECURSO]
						   ,[EXERCICIO])
					 VALUES
						   (@ID_PREFEITURA			--(ID_PREFEITURA, int,>
						    , 7						--,<ID_SITUACAO_QUADRO, int,>
						    , 160					--,<ID_RECURSO, int,>
						    , 2021 )				--,<EXERCICIO, int,>)
		END

		FETCH NEXT FROM lista INTO @ID_PREFEITURA
	END   
CLOSE lista;  
DEALLOCATE lista;  
