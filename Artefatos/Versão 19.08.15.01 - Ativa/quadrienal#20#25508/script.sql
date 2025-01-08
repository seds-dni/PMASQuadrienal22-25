use Dbpmas_quadrienal
go 

update TB_RECURSO set nome = '<span class="mif-home mif-2x fg-lightGreen"></span> Início' where id = 1
update TB_RECURSO set nome = '<span class="mif-home mif-2x fg-lightGreen"></span> Início' where id = 8




IF NOT EXISTS(SELECT 1
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'TB_PREFEITURA_ACAO_PLANEJAMENTO'
                 AND COLUMN_NAME = 'SITUACAO') 
BEGIN
	ALTER TABLE TB_PREFEITURA_ACAO_PLANEJAMENTO ADD SITUACAO integer
	ALTER TABLE TB_PREFEITURA_ACAO_PLANEJAMENTO ADD COMENTARIO_SITUACAO varchar(max)
	EXEC('UPDATE TB_PREFEITURA_ACAO_PLANEJAMENTO set SITUACAO = 0')
	EXEC('UPDATE TB_PREFEITURA_ACAO_PLANEJAMENTO set COMENTARIO_SITUACAO = ''''')
END