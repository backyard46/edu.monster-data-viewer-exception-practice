/****** Object:  Table [dbo].[Monsters]    Script Date: 05/23/2018 15:08:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Monsters](
	[No] [int] NOT NULL,
	[NameJP] [nvarchar](50) NOT NULL,
	[NameEN] [nvarchar](50) NULL,
	[Type1] [nvarchar](10) NOT NULL,
	[Type2] [nvarchar](10) NULL,
	[EvolveTo] [int] NULL,
	[EvolveFrom] [int] NULL,
 CONSTRAINT [PK_Monsters_NameJP] PRIMARY KEY CLUSTERED 
(
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'和名インデックス' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monsters', @level2type=N'CONSTRAINT',@level2name=N'PK_Monsters_NameJP'
GO


