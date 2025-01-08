<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p><b>Sobre o PMASweb</b></p>
    <p>Em 2004, a Secretaria Estadual de Desenvolvimento Social (SEDS) desenvolveu e implantou no Estado de São Paulo a primeira versão do sistema PMASweb, com o objetivo de orientar o planejamento das ações da política de assistência social dos municípios paulistas.</p>
    <p>Nos anos subsequentes, com apoio do Banco Interamericano de Desenvolvimento (BID) e em parceria com a Companhia de Processamento de Dados do Estado de São Paulo (PRODESP), foram agregados ao sistema avanços tecnológicos e conceituais, tornando-o uma valiosa ferramenta para planejamento e gestão, retratando a realidade de cada município e das regiões do Estado.</p>
    <p>O sistema disponibiliza acesso em tempo real às sínteses dos 645 planos municipais numa única base de dados, integrando e disponibilizando estas informações de maneira rápida aos gestores, conselheiros, técnicos e quaisquer outros interessados na política de assistência social, através de dezenas de relatórios com níveis de abrangência municipal, regional e estadual.</p>
    <p>Até 2017, o preenchimento das informações foi realizado anualmente pelos órgãos gestores municipais e pelos conselhos municipais de Assistência Social. A partir de 2018, o preenchimento ocorre a cada 4 anos, em períodos compatíveis com a elaboração dos planos plurianuais municipais, sendo que a atualização das informações continuará sendo realizada de maneira continuada durante todo este período.</p>
    <p>O PMASweb disponibiliza as informações através de oito blocos:</p>
    <p><b>1 – Identificação:</b> identificação da prefeitura, do órgão gestor municipal da assistência social e do fundo municipal de assistência social, além da relação dos conselhos de direitos existentes no município;</p>
    <p><b>2 – Diagnóstico socioterritorial: </b>com indicadores demográficos, de vulnerabilidade social e sobre a rede de atendimento socioassistencial, além da análise e interpretação destes dados feita pelo município;</p>
    <p><b>3 - Rede de proteção social: </b>informações sobre cada uma das unidades e serviços socioassistenciais que compõem a rede de proteção social, pública e privada, além de informações específicas sobre programas, projetos, transferência direta de renda, benefícios eventuais, e da integração destas ofertas;</p>
    <p><b>4 – Interfaces com outras políticas públicas:</b> informações acerca das interfaces entre a Assistência Social e outras políticas públicas, através de ações, programas ou projetos, apresentando um panorama geral sobre as articulações existentes com as políticas de Educação, Saúde, Segurança alimentar e nutricional, e Emprego, trabalho e renda.</p>
    <p><b>5 – Financiamento:</b> apresenta um quadro resumo sobre os valores dos recursos financeiros alocados na política de Assistência Social, detalhando a previsão de utilização dos recursos financeiros repassados pelo Estado ao Município através do sistema Fundo a Fundo;</p>
    <p><b>6 - Planejamento:</b> elenca as principais ações que o órgão gestor planeja realizar no próximo ano, incluindo uma breve descrição, seus objetivos, etapas, metas e previsão de custo de cada uma delas;</p>
    <p><b>7 – Vigilância, monitoramento e avaliação:</b> dá uma visão geral sobre as ações de monitoramento, avaliação e vigilância socioassistencial realizadas no município;</p>
    <p><b>8 – CMAS :</b> informações cadastrais do Conselho Municipal de Assistência Social, e o registro do parecer final e das deliberações do CMAS sobre as informações registradas no sistema.</p>
    <p>Em caso de sugestões ou comentários, fale conosco.</p>
    <p style="text-align: center">
        <%--<a href="mailto:pmas@sp.gov.br">pmas@sp.gov.br</a>--%>
    </p>

    <p>
        <asp:Label ID="lblResult" runat="server" />
    </p>
</asp:Content>
