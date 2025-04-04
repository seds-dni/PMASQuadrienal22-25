USE [Dbpmas_quadrienal]
GO

/****** Object:  Table [dbo].[TB_LOGxCREAS]    Script Date: 17/06/2019 17:27:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_LOGxCREAS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_LOG] [int] NOT NULL,
	[ID_CREAS] [int] NOT NULL,
	[ID_UNIDADE] [int] NOT NULL,
	[DATA_CRIACAO] [datetime] NOT NULL,
 CONSTRAINT [PK_LOG_CREAS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


