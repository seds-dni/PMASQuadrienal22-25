 use Dbpmas_quadrienal
 go
 ---------------------------------------------------------------------
 -- abaixo altera��es nas descricoes dos pareceres CMAS
 ---------------------------------------------------------------------
--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Pindamonhangaba%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'O munic�pio de Pindamonhangaba n�o pode realizar a reprograma��o do valor que seria destinado � Associa��o do Centro de Conviv�ncia para Idosos - "C�nego Nestor Jos� de Azevedo" em 2019, pois em 2018 havia pend�ncia de presta��o de contas e certid�es, motivo pelo qual, o valor de R$ 10.254,40, referente �s parcelas de janeiro a maio de 2018 ser� devolvido ao Governo do Estado. Submetida a quest�o � aprecia��o dos conselheiros, foi aprovada por unanimidade a n�o reprograma��o da verba, com a consequente devolu��o ao Governo do Estado.' 
WHERE ID_PREFEITURA = 8365 --Pindamonhangaba
AND DATA BETWEEN '2019-05-30 15:23' AND '2019-06-01 10:30'

--SELECT VALOR_ESTADUAL_ASSISTENCIA FROM TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PRIVADO WHERE ID = 5668 AND EXERCICIO = 2019
--Fundo estadual > Valor de R$7.735,36 deve ser retificado para R$7.775,06.
UPDATE TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PRIVADO SET VALOR_ESTADUAL_ASSISTENCIA = 7775.06  WHERE ID = 5668

--SELECT * FROM TB_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO WHERE ID = 22664
update TB_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO SET DESATIVADO = 0 WHERE ID = 22664