select * from tb_prefeitura where cidade like '%Adamantina%'


select * from TB_PREFEITURA_SITUACAO_QUADRO where ID_PREFEITURA = 8182

--Munic�pio			EF2017			LO2018
--1	Pendente
--2	Preenchido - EM AN�LISE DRADS
--3	Em an�lise CMAS
--4	Aprovado CMAS
--5	Devolvido DRADS
--6	Devolvido CMAS
--7	Bloqueio Administrativo

--Adamantina		APROVADO CMAS	APROVADO CMAS - FEITO
--select * from tb_prefeitura where cidade like '%Adamantina%'
UPDATE TB_PREFEITURA_SITUACAO_QUADRO SET ID_SITUACAO_QUADRO = 4 where ID_PREFEITURA = 8182 AND EXERCICIO = 2018 AND ID_RECURSO = 143
UPDATE TB_PREFEITURA_SITUACAO_QUADRO SET ID_SITUACAO_QUADRO = 4 where ID_PREFEITURA = 8182 AND EXERCICIO = 2018 AND ID_RECURSO = 160

select * from TB_SITUACAO_QUADRO
--Adolfo	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Adolfo%'
UPDATE TB_PREFEITURA_SITUACAO_QUADRO SET ID_SITUACAO_QUADRO = 4 where ID_PREFEITURA = 8241 AND EXERCICIO = 2018 AND ID_RECURSO = 143
UPDATE TB_PREFEITURA_SITUACAO_QUADRO SET ID_SITUACAO_QUADRO = 4 where ID_PREFEITURA = 8241 AND EXERCICIO = 2018 AND ID_RECURSO = 160

--Agua�	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Agua�%'
UPDATE TB_PREFEITURA_SITUACAO_QUADRO SET ID_SITUACAO_QUADRO = 2 where ID_PREFEITURA = 7986 AND EXERCICIO = 2018 AND ID_RECURSO = 143
UPDATE TB_PREFEITURA_SITUACAO_QUADRO SET ID_SITUACAO_QUADRO = 4 where ID_PREFEITURA = 7986 AND EXERCICIO = 2018 AND ID_RECURSO = 160

--�guas da Prata	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%guas da Prata%'

--�guas de Lind�ia	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%guas de Lind%ia%'

--�guas de Santa B�rbara	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%guas de Santa B%rbara%'

--�guas de S�o Pedro	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%guas de S�o Pedro%'

--Agudos	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Agudos%'

--Alambari	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Alambari%'

--Alfredo Marcondes	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Alfredo Marcondes%'

--Altair	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Altair%'

--Altin�polis	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Altin%polis%'

--Alto Alegre	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Alto Alegre%'

--Alum�nio	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Alum�nio%'

--�lvares Florence	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '�lvares Florence%'

--�lvares Machado	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '�lvares Machado%'

--�lvaro de Carvalho	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '�lvaro de Carvalho%'

--Alvinl�ndia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Alvinl%ndia%'

--Americana	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Americana%'

--Am�rico Brasiliense	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Am�rico Brasiliense%'

--Am�rico de Campos	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Am�rico de Campos%'

--Amparo	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Amparo%'

--Anal�ndia	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Anal%ndia%'

--Andradina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Andradina%'

--Angatuba	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Angatuba%'

--Anhembi	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Anhembi%'

--Anhumas	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Anhumas%'

--Aparecida	APROVADO CMAS	PENDENTE
select * from tb_prefeitura where cidade like 'Aparecida%'

--Aparecida d'Oeste	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Aparecida %Oeste%'

--Apia�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Apia�%'

--Ara�ariguama	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Ara%ariguama%'

--Ara�atuba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ara%atuba%'

--Ara�oiaba da Serra	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Ara%oiaba da Serra%'

--Aramina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Aramina%'

--Arandu	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Arandu%'

--Arape�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Arape�%'

--Araraquara	PENDENTE	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Araraquara%'

--Araras	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Araras%'

--Arco-�ris	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Arco%ris%'

--Arealva	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Arealva%'

--Areias	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Areias%'

--Arei�polis	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Arei%polis%'

--Ariranha	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ariranha%'

--Artur Nogueira	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Artur%Nogueira%'
--Aruj�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Aruj�%'
--Asp�sia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Asp�sia%'
--Assis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Assis%'
--Atibaia	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Atibaia%'
--Auriflama	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Auriflama%'
--Ava�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Avai%'
--Avanhandava	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Avanhandava%'
--Avar�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Avare%'
--Bady Bassitt	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bady Bassitt%'
--Balbinos	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Balbinos%'
--B�lsamo	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%B�lsamo%'
--Bananal	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bananal%'
--Bar�o de Antonina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bar�o%'
--Barbosa	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Barbosa%'
--Bariri	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bariri%'
--Barra Bonita	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Barra Bonita%'
--Barra do Chap�u	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Barra do Chapeu%'
--Barra do Turvo	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Barra do Turvo%'
--Barretos	PENDENTE	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Barretos%'
--Barrinha	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Barrinha%'
--Barueri	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Barueri%'
--Bastos	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bastos%'
--Batatais	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Batatais%'
--Bauru	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bauru%'
--Bebedouro	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bebedouro%'
--Bento de Abreu	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bento de Abreu%'
--Bernardino de Campos	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bernardino de Campos%'
--Bertioga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bertioga%'
--Bilac	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bilac%'
--Birigui	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Birigui%'
--Biritiba Mirim	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Biritiba Mirim%'
--Boa Esperan�a do Sul	PENDENTE	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Boa Esperanca%'
--Bocaina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bocaina%'
--Bofete	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bofete%'
--Boituva	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Boituva%'
--Bom Jesus dos Perd�es	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Bom Jesus dos %'
--Bom Sucesso de Itarar�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bom Sucesso de Itarare%'
--Bor�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Bor�%'
--Borac�ia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Boraceia%'
--Borborema	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Borborema%'
--Borebi	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Borebi%'
--Botucatu	EM AN�LISE CMAS	EM AN�LISE CMAS
select * from tb_prefeitura where cidade like '%Botucatu%'
--Bragan�a Paulista	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Braganca Paulista%'
--Bra�na	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Brauna%'
--Brejo Alegre	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Brejo Alegre%'
--Brodowski	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Brodowski%'
--Brotas	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Brotas%'
--Buri	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Buri'
--Buritama	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Buritama%'
--Buritizal	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Buritizal%'
--Cabr�lia Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Cabralia Paulista%'
--Cabre�va	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Cabreuva%'
--Ca�apava	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cacapava%'
--Cachoeira Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cachoeira Paulista%'
--Caconde	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Caconde%'
--Cafel�ndia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cafel�ndia%'
--Caiabu	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Caiabu%'
--Caieiras	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Caieiras%'1
--Caiu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Caiua%'
--Cajamar	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cajamar%'
--Cajati	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cajati%'
--Cajobi	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cajobi%'
--Cajuru	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Cajuru%'
--Campina do Monte Alegre	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Campina do Monte %'
--Campinas	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Campinas%'
--Campo Limpo Paulista	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Campo Limpo %'
--Campos do Jord�o	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Campos do Jord�o%'
--Campos Novos Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Campos Novos Paulista%'
--Canan�ia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cananeia%'
--Canas	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Canas'
--C�ndido Mota	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'C�ndido Mota%'
--C�ndido Rodrigues	PENDENTE	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Candido Rodrigues%'
--Canitar	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Canitar%'
--Cap�o Bonito	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cap�o Bonito%'
--Capela do Alto	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Capela do Alto%'
--Capivari	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Capivari%'
--Caraguatatuba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Caraguatatuba%'
--Carapicu�ba	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Carapicuiba%'
--Cardoso	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cardoso%'
--Casa Branca	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Casa Branca%'
--C�ssia dos Coqueiros	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Cassia dos Coqueiros%'
--Castilho	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Castilho%'
--Catanduva	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Catanduva%'
--Catigu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Catigu�%'
--Cedral	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cedral%'
--Cerqueira C�sar	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cerqueira C�sar%'
--Cerquilho	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Cerquilho%'
--Ces�rio Lange	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Cesario%'
--Charqueada	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Charqueada%'
--Chavantes	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Chavantes%'
--Clementina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Clementina%'
--Colina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Colina%'
--Col�mbia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Colombia%'
--Conchal	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Conchal%'
--Conchas	APROVADO CMAS	PENDENTE
select * from tb_prefeitura where cidade like 'Conchas%'
--Cordeir�polis	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Cordeir�polis%'
--Coroados	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Coroados%'
--Coronel Macedo	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Coronel Macedo%'
--Corumbata�	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Corumbatai%'
--Cosm�polis	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Cosmopolis%'
--Cosmorama	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cosmorama%'
--Cotia	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Cotia%'
--Cravinhos	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Cravinhos%'
--Cristais Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cristais Paulista%'
--Cruz�lia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cruzalia%'
--Cruzeiro	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Cruzeiro%'
--Cubat�o	DEVOLVIDO DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Cubatao%'
--Cunha	APROVADO CMAS	PENDENTE
select * from tb_prefeitura where cidade like 'Cunha'
--Descalvado	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Descalvado%'
--Diadema	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Diadema%'
--Dirce Reis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Dirce Reis%'
--Divinol�ndia	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Divinol�ndia%'
--Dobrada	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Dobrada%'
--Dois C�rregos	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Dois C�rregos%'
--Dolcin�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Dolcin�polis%'
--Dourado	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Dourado%'
--Dracena	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Dracena%'
--Duartina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Duartina%'
--Dumont	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Dumont%'
--Echapor�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Echapor�%'
--Eldorado	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Eldorado%'
--Elias Fausto	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%Elias Fausto%'
--Elisi�rio	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Elisi�rio%'
--Emba�ba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Emba�ba%'
--Embu das Artes	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Embu das Artes%'
--Embu-Gua�u	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Embu%Guacu%'
--Emilian�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Emilianopolis%'
--Engenheiro Coelho	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Engenheiro Coelho%'
--Esp�rito Santo do Pinhal	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Adamantina%'
--Esp�rito Santo do Turvo	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Espirito Santo do Turvo%'
--Estiva Gerbi	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Estiva Gerbi%'
--Estrela do Norte	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Estrela do Norte%'
--Estrela d'Oeste	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Estrela %Oeste%'
--Euclides da Cunha Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Euclides da Cunha Paulista%'
--Fartura	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Fartura%'
--Fernando Prestes	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Fernando Prestes%'
--Fernand�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Fernandopolis%'
--Fern�o	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Fernao'
--Ferraz de Vasconcelos	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ferraz de Vasconcelos%'
--Flora Rica	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Flora Rica%'
--Floreal	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Floreal%'
--Fl�rida Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Fl�rida Paulista%'
--Flor�nea	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Florinia%'
--Franca	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Franca%'
--Francisco Morato	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Francisco Morato%'
--Franco da Rocha	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Franco da Rocha%'
--Gabriel Monteiro	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Gabriel Monteiro%'
--G�lia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'G�lia%'
--Gar�a	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Garca'
--Gast�o Vidigal	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Gastao Vidigal%'
--Gavi�o Peixoto	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Gavi�o Peixoto%'
--General Salgado	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'General Salgado%'
--Getulina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Getulina%'
--Glic�rio	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Glicerio%'
--Guai�ara	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guai�ara%'
--Guaimb�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guaimb�%'
--Gua�ra	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guaira%'
--Guapia�u	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guapiacu%'
--Guapiara	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guapiara%'
--Guar�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guar�%'
--Guara�a�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guara�a�%'
--Guaraci	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guaraci%'
--Guarani d'Oeste	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guarani %Oeste%'
--Guarant�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guaranta%'
--Guararapes	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guararapes%'
--Guararema	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guararema%'
--Guaratinguet�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guaratinguet�%'
--Guare�	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Guare�%'
--Guariba	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Guariba%'
--Guaruj�	DEVOLVIDO DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guaruj�%'
--Guarulhos	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guarulhos%'
--Guatapar�	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Guatapar�%'
--Guzol�ndia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Guzolandia%'
--Hercul�ndia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Herculandia%'
--Holambra	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Holambra%'
--Hortol�ndia	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Hortolandia%'
--Iacanga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Iacanga%'
--Iaras	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Iaras%'
--Ibat�	PENDENTE	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Ibat�%'
--Ibir�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ibira%'
--Ibirarema	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ibirarema%'
--Ibitinga	PENDENTE	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Ibitinga%'
--Ibi�na	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Ibi�na%'
--Ic�m	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Icem%'
--Iep�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Iepe%'
--Igara�u do Tiet�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Igara�u%'
--Igarapava	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Igarapava%'
--Igarat�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Igarata%'
--Iguape	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Iguape%'
--Ilha Comprida	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ilha Comprida%'
--Ilha Solteira	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ilha Solteira%'
--Ilhabela	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ilhabela%'
--Indaiatuba	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Indaiatuba%'
--Indiana	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Indiana%'
--Indiapor�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Indiapora%'
--In�bia Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Inubia Paulista%'
--Ipaussu	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ipaussu%'
--Iper�	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Ipero%'
--Ipe�na	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Ipe�na%'
--Ipigu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ipigu�%'
--Iporanga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Iporanga%'
--Ipu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ipua%'
--Iracem�polis	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Iracem�polis%'
--Irapu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Irapu�%'
--Irapuru	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Irapuru%'
--Itaber�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itabera%'
--Ita�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itai%'
--Itajobi	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itajobi%'
--Itaju	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itaju%'
--Itanha�m	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itanhaem%'
--Itaoca	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itaoca%'
--Itapecerica da Serra	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Itapecerica da Serra%'
--Itapetininga	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Itapetininga%'
--Itapeva	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itapeva%'
--Itapevi	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Itapevi%'
--Itapira	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itapira'
--Itapirapu� Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itapirapua Paulista%'
--It�polis	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Itapolis%'
--Itaporanga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itaporanga%'
--Itapu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itapui%'
--Itapura	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itapura%'
--Itaquaquecetuba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itaquaquecetuba%'
--Itarar�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itarar�%'
--Itariri	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itariri%'
--Itatiba	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Itatiba%'
--Itatinga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itatinga%'
--Itirapina	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like 'Itirapina%'
--Itirapu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itirapu�%'
--Itobi	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Itobi%'
--Itu	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Itu'
--Itupeva	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Itupeva%'
--Ituverava	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ituverava%'
--Jaborandi	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Jaborandi%'
--Jaboticabal	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Jaboticabal%'
--Jacare�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Jacare�%'
--Jaci	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Jaci%'
--Jacupiranga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Jacupiranga%'
--Jaguari�na	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Jaguariuna%'
--Jales	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Jales%'
--Jambeiro	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Jambeiro%'
--Jandira	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Jandira%'
--Jardin�polis	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Jardinopolis%'
--Jarinu	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like 'Jarinu%'
--Ja�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like 'Ja�%'
--Jeriquara	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Jeriquara%'
--Joan�polis	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Joan�polis%'
--Jo�o Ramalho	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Jos� Bonif�cio	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--J�lio Mesquita	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Jumirim	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Jundia�	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Junqueir�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Juqui�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Juquitiba	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Lagoinha	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Laranjal Paulista	APROVADO CMAS	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Lav�nia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Lavrinhas	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Leme	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Len��is Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Limeira	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Lind�ia	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Lins	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Lorena	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Lourdes	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Louveira	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Luc�lia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Lucian�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Luiz Ant�nio	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Luizi�nia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Lup�rcio	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Lut�cia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Macatuba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Macaubal	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Maced�nia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Magda	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mairinque	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mairipor�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Manduri	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Marab� Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Maraca�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Marapoama	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mari�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mar�lia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Marin�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Martin�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mat�o	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mau�	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mendon�a	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Meridiano	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mes�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Miguel�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mineiros do Tiet�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mira Estrela	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Miracatu	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mirand�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mirante do Paranapanema	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mirassol	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mirassol�ndia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mococa	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mogi das Cruzes	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mogi Gua�u	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mogi Mirim	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mombuca	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mon��es	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Mongagu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Monte Alegre do Sul	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Monte Alto	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Monte Apraz�vel	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Monte Azul Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Monte Castelo	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Monte Mor	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Monteiro Lobato	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Morro Agudo	EM AN�LISE CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Morungaba	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Motuca	PENDENTE	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Murutinga do Sul	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nantes	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Narandiba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Natividade da Serra	PENDENTE	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nazar� Paulista	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Neves Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nhandeara	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nipo�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nova Alian�a	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nova Campina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nova Cana� Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nova Castilho	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nova Europa	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nova Granada	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nova Guataporanga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nova Independ�ncia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nova Luzit�nia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nova Odessa	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Novais	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Novo Horizonte	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Nuporanga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ocau�u	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--�leo	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ol�mpia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Onda Verde	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Oriente	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Orindi�va	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Orl�ndia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Osasco	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Oscar Bressane	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Osvaldo Cruz	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ourinhos	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ouro Verde	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ouroeste	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pacaembu	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Palestina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Palmares Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Palmeira d'Oeste	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Palmital	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Panorama	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Paragua�u Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Paraibuna	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Para�so	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Paranapanema	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Paranapu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Parapu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pardinho	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pariquera-A�u	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Parisi	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Patroc�nio Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Paulic�ia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Paul�nia	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Paulist�nia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Paulo de Faria	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pederneiras	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pedra Bela	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pedran�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pedregulho	EM AN�LISE CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pedreira	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pedrinhas Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pedro de Toledo	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pen�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pereira Barreto	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pereiras	APROVADO CMAS	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Peru�be	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Piacatu	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Piedade	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pilar do Sul	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pindamonhangaba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pindorama	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pinhalzinho	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Piquerobi	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Piquete	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Piracaia	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Piracicaba	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Piraju	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Piraju�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pirangi	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pirapora do Bom Jesus	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pirapozinho	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pirassununga	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Piratininga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pitangueiras	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Planalto	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Platina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Po�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Poloni	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pomp�ia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ponga�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pontal	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pontalinda	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pontes Gestal	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Populina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Porangaba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Porto Feliz	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Porto Ferreira	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Potim	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Potirendaba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Pracinha	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Prad�polis	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Praia Grande	DEVOLVIDO DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Prat�nia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Presidente Alves	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Presidente Bernardes	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Presidente Epit�cio	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Presidente Prudente	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Presidente Venceslau	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Promiss�o	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Quadra	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Quat�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Queiroz	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Queluz	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Quintana	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Rafard	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Rancharia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Reden��o da Serra	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Regente Feij�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Regin�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Registro	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Restinga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ribeira	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ribeir�o Bonito	PENDENTE	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ribeir�o Branco	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ribeir�o Corrente	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ribeir�o do Sul	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ribeir�o dos �ndios	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ribeir�o Grande	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ribeir�o Pires	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ribeir�o Preto	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Rifaina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Rinc�o	PENDENTE	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Rin�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Rio Claro	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Rio das Pedras	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Rio Grande da Serra	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Riol�ndia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Riversul	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Rosana	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Roseira	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Rubi�cea	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Rubin�ia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sabino	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sagres	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sales	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sales Oliveira	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sales�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Salmour�o	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Saltinho	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Salto	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Salto de Pirapora	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Salto Grande	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sandovalina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Ad�lia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Albertina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa B�rbara d'Oeste	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Branca	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Clara d'Oeste	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Cruz da Concei��o	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Cruz da Esperan�a	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Cruz das Palmeiras	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Cruz do Rio Pardo	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Ernestina	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa F� do Sul	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Gertrudes	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Isabel	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa L�cia	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Maria da Serra	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Mercedes	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Rita do Passa Quatro	PENDENTE	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Rita d'Oeste	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Rosa de Viterbo	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santa Salete	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santana da Ponte Pensa	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santana de Parna�ba	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santo Anast�cio	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santo Andr�	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santo Ant�nio da Alegria	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santo Ant�nio de Posse	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santo Ant�nio do Aracangu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santo Ant�nio do Jardim	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santo Ant�nio do Pinhal	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santo Expedito	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sant�polis do Aguape�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Santos	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Bento do Sapuca�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Bernardo do Campo	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Caetano do Sul	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Carlos	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Francisco	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Jo�o da Boa Vista	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Jo�o das Duas Pontes	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Jo�o de Iracema	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Jo�o do Pau d'Alho	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Joaquim da Barra	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Jos� da Bela Vista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Jos� do Barreiro	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Jos� do Rio Pardo	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Jos� do Rio Preto	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Jos� dos Campos	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Louren�o da Serra	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Luiz do Paraitinga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Manuel	APROVADO CMAS	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Miguel Arcanjo	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Paulo	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Pedro	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Pedro do Turvo	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Roque	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Sebasti�o	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Sebasti�o da Grama	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Sim�o	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--S�o Vicente	DEVOLVIDO DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sarapu�	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sarutai�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sebastian�polis do Sul	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Serra Azul	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Serra Negra	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Serrana	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sert�ozinho	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sete Barras	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sever�nia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Silveiras	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Socorro	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sorocaba	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sud Mennucci	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Sumar�	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Suzan�polis	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Suzano	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tabapu�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tabatinga	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tabo�o da Serra	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Taciba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tagua�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Taia�u	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tai�va	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tamba�	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tanabi	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tapira�	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tapiratiba	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Taquaral	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Taquaritinga	PENDENTE	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Taquarituba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Taquariva�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tarabai	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tarum�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tatu�	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Taubat�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tejup�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Teodoro Sampaio	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Terra Roxa	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tiet�	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Timburi	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Torre de Pedra	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Torrinha	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Trabiju	PENDENTE	PENDENTE
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Trememb�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tr�s Fronteiras	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tuiuti	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tup�	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Tupi Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Turi�ba	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Turmalina	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ubarana	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ubatuba	PENDENTE	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ubirajara	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Uchoa	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Uni�o Paulista	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Ur�nia	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Uru	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Urup�s	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Valentim Gentil	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Valinhos	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Valpara�so	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Vargem	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%XXXXXXXXXXXX%'
--Vargem Grande do Sul	EM AN�LISE DRADS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Adamantina%'
--Vargem Grande Paulista	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%-Vargem Grande Paulista%'
--V�rzea Paulista	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%V�rzea Paulista%'
--Vera Cruz	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Vera Cruz%'
--Vinhedo	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Vinhedo%'
--Viradouro	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Viradouro%'
--Vista Alegre do Alto	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Vista Alegre do Alto%'
--Vit�ria Brasil	APROVADO CMAS	APROVADO Vit�ria Brasil
select * from tb_prefeitura where cidade like '%Adamantina%'
--Votorantim	EM AN�LISE DRADS	EM AN�LISE DRADS
select * from tb_prefeitura where cidade like '%Votorantim%'
--Votuporanga	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Votuporanga%'
--Zacarias	APROVADO CMAS	APROVADO CMAS
select * from tb_prefeitura where cidade like '%Zacarias%'
