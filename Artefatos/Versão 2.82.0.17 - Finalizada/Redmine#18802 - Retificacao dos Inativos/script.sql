 use Dbpmas_quadrienal
 go
 
 ---------------------------------------------------------------------
 -- abaixo altera��es nas descricoes dos pareceres CMAS
 ---------------------------------------------------------------------
 --
 --
 -- Parecer CMAS
 --
 --
--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Conchas%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'Ap�s apresenta��o foi aprovado por unanimidade as altera��es para o ano de 2019 o PMAS para o exerc�cio 2018-2021.' 
WHERE ID_PREFEITURA = 7869 --Conchas
AND DATA BETWEEN '2019-01-18 10:29' AND '2019-01-18 10:30'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Itatinga%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'Ap�s a apresenta��o das altera��es para o ano de 2019 o PMAS foi aprovado.' 
WHERE ID_PREFEITURA = 7870 --Itatinga
AND DATA BETWEEN '2019-01-10 09:58' AND '2019-01-10 09:59'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Porangaba%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'Ap�s explana��o cada membro teve acesso ao PMAS e aprovaram a mat�ria em pauta.' 
WHERE ID_PREFEITURA = 7874 --Porangaba
AND DATA BETWEEN '2019-01-08 09:03' AND '2019-01-08 09:04'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Torre de Pedra%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'O CMAS aprova as altera��es efetuadas no PMAS uma vez que dever� ser adequado durante sua vig�ncia.' 
WHERE ID_PREFEITURA = 7877 --TORRE DE PEDRA
AND DATA BETWEEN '2019-01-09 08:46' AND '2019-01-09 08:47'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Guapiacu%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'O Conselho Municipal de Assist�ncia Social de Guapia�u reuniu-se na data de 09/01/2019, na Sala dos Conselhos - do Departamento de Assist�ncia Social �s 9:00 hs, conforme Ata n� 01/19, para an�lise e aprova��o do PMAS- Plano Municipal de Assist�ncia Social 2019/2021. Os membros do Conselho ap�s an�lise minuciosa das a��es e Projetos propostos no Plano Municipal de Assist�ncia Social para 2019/2021, e aprovaram por unanimidade.' 
WHERE ID_PREFEITURA = 8245 --GUAPIACU
AND DATA BETWEEN '2019-01-21 16:15' AND '2019-01-21 16:16'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Ipigu�%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'Favor�vel - Estamos de acordo com as informa��es registradas no PMAS 2018/2019 sobre a estrutura organizacional do �rg�o Gestor da Assist�ncia Social, sobre as a��es planejadas para o pr�ximo per�odo e sobre aloca��o dos recursos financeiros previstos para cofinanciamento dos servi�os da rede socioassistencial.' 
WHERE ID_PREFEITURA = 8248 --Ipigu�
AND DATA BETWEEN '2018-12-19 15:33' AND '2018-12-19 15:34'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Potirendaba%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'O Conselho Municipal de Assist�ncia Social atrav�s da reuni�o ordin�ria no dia 18 de Dezembro de 2018, �s 16:30 horas, na sede do CMAS, a pedido da Sra. Presidente  procederam an�lise dos dados frente ao PMAS 2018/2021, ano de referencia 2019; desde o item de identifica��o at� o processo de monitoramento e avalia��o que foram registrados de forma quantitativa e qualitativa pelos Profissionais do SUAS, demonstrando e assegurando as principais prioridades a serem atendidas atrav�s da Pol�tica de Cofianciamento com os tr�s Entes Federados pactuadas nas Pol�ticas de Prote��o Social B�sica e Especial de M�dia e Alta Complexidade, beneficiando a Rede Publica Direta e Rede Indireta. Constatou ainda que as propostas expressam os interesses e necessidades apresentadas pelas fam�lias, crian�as/adolescentes/jovens, idosos e pessoas com defici�ncia; raz�o pela qual, manifestamo-nos de forma consensual a emiss�o de Parecer Favor�vel.' 
WHERE ID_PREFEITURA = 8261 --POTIRENDABA
AND DATA BETWEEN '2018-12-18 16:34' AND '2018-12-18 16:35'


--
--
-- Retificacao dos Inativos
-- 
--
UPDATE TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PUBLICO 
		SET VALOR_ESTADUAL_ASSISTENCIA = 0 where ID = 2440

UPDATE TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PRIVADO 
		SET VALOR_MUNICIPAL_ASSISTENCIA = 0
		  , VALOR_ESTADUAL_ASSISTENCIA = 0
		  , VALOR_FEDERAL_ASSISTENCIA = 0
where ID = 4773






