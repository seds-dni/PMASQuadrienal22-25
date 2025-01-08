 use Dbpmas_quadrienal
 go
 ---------------------------------------------------------------------
 -- abaixo alterações nas descricoes dos pareceres CMAS
 ---------------------------------------------------------------------
--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Pindamonhangaba%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'O município de Pindamonhangaba não pode realizar a reprogramação do valor que seria destinado à Associação do Centro de Convivência para Idosos - "Cônego Nestor José de Azevedo" em 2019, pois em 2018 havia pendência de prestação de contas e certidões, motivo pelo qual, o valor de R$ 10.254,40, referente às parcelas de janeiro a maio de 2018 será devolvido ao Governo do Estado. Submetida a questão à apreciação dos conselheiros, foi aprovada por unanimidade a não reprogramação da verba, com a consequente devolução ao Governo do Estado.' 
WHERE ID_PREFEITURA = 8365 --Pindamonhangaba
AND DATA BETWEEN '2019-05-30 15:23' AND '2019-06-01 10:30'

--SELECT VALOR_ESTADUAL_ASSISTENCIA FROM TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PRIVADO WHERE ID = 5668 AND EXERCICIO = 2019
--Fundo estadual > Valor de R$7.735,36 deve ser retificado para R$7.775,06.
UPDATE TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PRIVADO SET VALOR_ESTADUAL_ASSISTENCIA = 7775.06  WHERE ID = 5668

--SELECT * FROM TB_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO WHERE ID = 22664
update TB_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO SET DESATIVADO = 0 WHERE ID = 22664