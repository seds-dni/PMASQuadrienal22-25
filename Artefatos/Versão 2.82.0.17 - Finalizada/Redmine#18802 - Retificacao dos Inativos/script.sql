 use Dbpmas_quadrienal
 go
 
 ---------------------------------------------------------------------
 -- abaixo alterações nas descricoes dos pareceres CMAS
 ---------------------------------------------------------------------
 --
 --
 -- Parecer CMAS
 --
 --
--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Conchas%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'Após apresentação foi aprovado por unanimidade as alterações para o ano de 2019 o PMAS para o exercício 2018-2021.' 
WHERE ID_PREFEITURA = 7869 --Conchas
AND DATA BETWEEN '2019-01-18 10:29' AND '2019-01-18 10:30'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Itatinga%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'Após a apresentação das alterações para o ano de 2019 o PMAS foi aprovado.' 
WHERE ID_PREFEITURA = 7870 --Itatinga
AND DATA BETWEEN '2019-01-10 09:58' AND '2019-01-10 09:59'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Porangaba%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'Após explanação cada membro teve acesso ao PMAS e aprovaram a matéria em pauta.' 
WHERE ID_PREFEITURA = 7874 --Porangaba
AND DATA BETWEEN '2019-01-08 09:03' AND '2019-01-08 09:04'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Torre de Pedra%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'O CMAS aprova as alterações efetuadas no PMAS uma vez que deverá ser adequado durante sua vigência.' 
WHERE ID_PREFEITURA = 7877 --TORRE DE PEDRA
AND DATA BETWEEN '2019-01-09 08:46' AND '2019-01-09 08:47'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Guapiacu%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'O Conselho Municipal de Assistência Social de Guapiaçu reuniu-se na data de 09/01/2019, na Sala dos Conselhos - do Departamento de Assistência Social às 9:00 hs, conforme Ata nº 01/19, para análise e aprovação do PMAS- Plano Municipal de Assistência Social 2019/2021. Os membros do Conselho após análise minuciosa das ações e Projetos propostos no Plano Municipal de Assistência Social para 2019/2021, e aprovaram por unanimidade.' 
WHERE ID_PREFEITURA = 8245 --GUAPIACU
AND DATA BETWEEN '2019-01-21 16:15' AND '2019-01-21 16:16'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Ipiguá%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'Favorável - Estamos de acordo com as informações registradas no PMAS 2018/2019 sobre a estrutura organizacional do Órgão Gestor da Assistência Social, sobre as ações planejadas para o próximo período e sobre alocação dos recursos financeiros previstos para cofinanciamento dos serviços da rede socioassistencial.' 
WHERE ID_PREFEITURA = 8248 --Ipiguá
AND DATA BETWEEN '2018-12-19 15:33' AND '2018-12-19 15:34'


--select ID, CIDADE from TB_PREFEITURA WHERE CIDADE LIKE '%Potirendaba%'
Update TB_PLANO_MUNICIPAL_HISTORICO 
SET DESCRICAO = 'O Conselho Municipal de Assistência Social através da reunião ordinária no dia 18 de Dezembro de 2018, ás 16:30 horas, na sede do CMAS, a pedido da Sra. Presidente  procederam análise dos dados frente ao PMAS 2018/2021, ano de referencia 2019; desde o item de identificação até o processo de monitoramento e avaliação que foram registrados de forma quantitativa e qualitativa pelos Profissionais do SUAS, demonstrando e assegurando as principais prioridades a serem atendidas através da Política de Cofianciamento com os três Entes Federados pactuadas nas Políticas de Proteção Social Básica e Especial de Média e Alta Complexidade, beneficiando a Rede Publica Direta e Rede Indireta. Constatou ainda que as propostas expressam os interesses e necessidades apresentadas pelas famílias, crianças/adolescentes/jovens, idosos e pessoas com deficiência; razão pela qual, manifestamo-nos de forma consensual a emissão de Parecer Favorável.' 
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






