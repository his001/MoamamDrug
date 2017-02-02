
CREATE TABLE [dbo].[Tbl_BokYongHooKi_DrugInfo](
	[seq] [bigint] IDENTITY(1,1) NOT NULL,
	[idx] [int] NOT NULL,
	[ITEM_SEQ] [bigint] NOT NULL,
	[ITEM_MEMO] [nvarchar](2000) NULL,
	[regdate] [datetime] NOT NULL,
	[regid] [nvarchar](100) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL,
	[modid] [nvarchar](100) NULL,
	[modip] [varchar](20) NULL,
 CONSTRAINT [PK_Tbl_BokYongHooKi_DrugInfo] PRIMARY KEY CLUSTERED 
(
	[seq] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Tbl_BokYongHooKi_DrugInfo] ADD  CONSTRAINT [DF_Tbl_BokYongHooKi_DrugInfo_regdate]  DEFAULT (getdate()) FOR [regdate]
GO

ALTER TABLE [dbo].[Tbl_BokYongHooKi_DrugInfo] ADD  CONSTRAINT [DF_Tbl_BokYongHooKi_DrugInfo_moddate]  DEFAULT (getdate()) FOR [moddate]
GO

