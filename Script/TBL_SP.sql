USE [moamam]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tbl_BokYongHooKi', @level2type=N'COLUMN',@level2name=N'HangSaengJeEat'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tbl_BokYongHooKi', @level2type=N'COLUMN',@level2name=N'HangSaengJeBokan'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tbl_BokYongHooKi', @level2type=N'COLUMN',@level2name=N'Yak_iLbun'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tbl_BokYongHooKi', @level2type=N'COLUMN',@level2name=N'HaeYeolJe'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tbl_BokYongHooKi', @level2type=N'COLUMN',@level2name=N'Feber'

GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_member_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_member_S]
GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_member_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_member_R]
GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_member_Pwd_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_member_Pwd_S]
GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_DrugOrg_Info_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_DrugOrg_Info_S]
GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_DrugOrg_Info_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_DrugOrg_Info_R]
GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_Drug_Info_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_Drug_Info_S]
GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_Drug_Info_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_Drug_Info_R]
GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_Code_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_Code_R]
GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_BokYongHooKi_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_BokYongHooKi_S]
GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_BokYongHooKi_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_BokYongHooKi_R]
GO
/****** Object:  StoredProcedure [dbo].[SP_WEB_Common_Master_Site_UpdateDate_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SP_WEB_Common_Master_Site_UpdateDate_R]
GO
/****** Object:  StoredProcedure [dbo].[SP_U_PHOTOALBUM_BEST]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SP_U_PHOTOALBUM_BEST]
GO
/****** Object:  StoredProcedure [dbo].[SP_U_PC_MYALBUM_SCORE_U]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SP_U_PC_MYALBUM_SCORE_U]
GO
/****** Object:  StoredProcedure [dbo].[SP_U_GUBUN_CD]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SP_U_GUBUN_CD]
GO
/****** Object:  StoredProcedure [dbo].[SP_C_PAGE_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SP_C_PAGE_S]
GO
/****** Object:  StoredProcedure [dbo].[SP_C_PAGE_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SP_C_PAGE_R]
GO
/****** Object:  StoredProcedure [dbo].[SP_C_NOW_CONNECT_PROCESS]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SP_C_NOW_CONNECT_PROCESS]
GO
/****** Object:  StoredProcedure [dbo].[SP_C_MENU_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SP_C_MENU_R]
GO
/****** Object:  StoredProcedure [dbo].[SP_C_COMMON_GUBUN_CDALL_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SP_C_COMMON_GUBUN_CDALL_R]
GO
/****** Object:  StoredProcedure [dbo].[SP_C_COMMON_GUBUN_CD_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SP_C_COMMON_GUBUN_CD_R]
GO
/****** Object:  StoredProcedure [dbo].[SP_A_LOGIN]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP PROCEDURE [dbo].[SP_A_LOGIN]
GO
ALTER TABLE [dbo].[pc_mydiary_comm] DROP CONSTRAINT [FK_pc_mydiary_comm_pc_mydiary]
GO
ALTER TABLE [dbo].[pc_myalbum_score] DROP CONSTRAINT [FK_pc_myalbum_score_pc_myalbum]
GO
ALTER TABLE [dbo].[pc_myalbum_comm] DROP CONSTRAINT [FK_pc_myalbum_comm_pc_myalbum]
GO
ALTER TABLE [dbo].[pc_myalbum] DROP CONSTRAINT [FK_pc_myalbum_pc_myalbum_admin]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Tbl_memberChild] DROP CONSTRAINT [DF_Tbl_memberChild_moddate]
GO
ALTER TABLE [dbo].[Tbl_memberChild] DROP CONSTRAINT [DF_Tbl_memberChild_regdate]
GO
ALTER TABLE [dbo].[pc_notice] DROP CONSTRAINT [DF_pc_notice_regdate]
GO
ALTER TABLE [dbo].[pc_notice] DROP CONSTRAINT [DF_pc_notice_readcnt]
GO
ALTER TABLE [dbo].[pc_myfriends] DROP CONSTRAINT [DF_pc_myfriends_regdate]
GO
ALTER TABLE [dbo].[pc_mydiary_comm] DROP CONSTRAINT [DF__pc_diary___regda__630F92C5]
GO
ALTER TABLE [dbo].[pc_mydiary] DROP CONSTRAINT [DF__pc_mydiar__regda__2235F3A1]
GO
ALTER TABLE [dbo].[pc_mycash] DROP CONSTRAINT [DF_pc_mycash_regdate]
GO
ALTER TABLE [dbo].[pc_mycash] DROP CONSTRAINT [DF_pc_mycash_use_amt]
GO
ALTER TABLE [dbo].[pc_mycash] DROP CONSTRAINT [DF_pc_mycash_tot_amt]
GO
ALTER TABLE [dbo].[pc_myalbum_score] DROP CONSTRAINT [DF_pc_myalbum_score_regdate]
GO
ALTER TABLE [dbo].[pc_myalbum_score] DROP CONSTRAINT [DF_pc_myalbum_score_score]
GO
ALTER TABLE [dbo].[pc_myalbum_comm] DROP CONSTRAINT [DF_pc_myalbum_comm_regdate]
GO
ALTER TABLE [dbo].[block_ip] DROP CONSTRAINT [DF_block_ip_access_on]
GO
ALTER TABLE [dbo].[block_ip] DROP CONSTRAINT [DF_block_ip_block_date]
GO
/****** Object:  Index [IX_Tbl_member_idx]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP INDEX [IX_Tbl_member_idx] ON [dbo].[Tbl_member]
GO
/****** Object:  Index [IX_Tbl_DrugOrg_Info_ItemName]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP INDEX [IX_Tbl_DrugOrg_Info_ItemName] ON [dbo].[Tbl_DrugOrg_Info]
GO
/****** Object:  Index [IX_Tbl_DrugOrg_Info_EntpNae]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP INDEX [IX_Tbl_DrugOrg_Info_EntpNae] ON [dbo].[Tbl_DrugOrg_Info]
GO
/****** Object:  Index [UserNameIndex]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
GO
/****** Object:  Index [IX_UserId]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_RoleId]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_UserId]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
GO
/****** Object:  Index [IX_UserId]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[Tbl_memberChild]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[Tbl_memberChild]
GO
/****** Object:  Table [dbo].[Tbl_member]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[Tbl_member]
GO
/****** Object:  Table [dbo].[Tbl_DrugOrg_Info]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[Tbl_DrugOrg_Info]
GO
/****** Object:  Table [dbo].[Tbl_Drug_Info]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[Tbl_Drug_Info]
GO
/****** Object:  Table [dbo].[Tbl_Code]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[Tbl_Code]
GO
/****** Object:  Table [dbo].[Tbl_BokYongHooKi]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[Tbl_BokYongHooKi]
GO
/****** Object:  Table [dbo].[sfbbs_board_config]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[sfbbs_board_config]
GO
/****** Object:  Table [dbo].[sfbbs_board_comment]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[sfbbs_board_comment]
GO
/****** Object:  Table [dbo].[sfbbs_board]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[sfbbs_board]
GO
/****** Object:  Table [dbo].[sfbbs_AdminBoard]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[sfbbs_AdminBoard]
GO
/****** Object:  Table [dbo].[pc_now_connect]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[pc_now_connect]
GO
/****** Object:  Table [dbo].[pc_notice]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[pc_notice]
GO
/****** Object:  Table [dbo].[pc_myfriends]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[pc_myfriends]
GO
/****** Object:  Table [dbo].[pc_mydiary_comm]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[pc_mydiary_comm]
GO
/****** Object:  Table [dbo].[pc_mydiary]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[pc_mydiary]
GO
/****** Object:  Table [dbo].[pc_mycash]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[pc_mycash]
GO
/****** Object:  Table [dbo].[pc_myalbum_score]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[pc_myalbum_score]
GO
/****** Object:  Table [dbo].[pc_myalbum_comm]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[pc_myalbum_comm]
GO
/****** Object:  Table [dbo].[pc_myalbum_admin]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[pc_myalbum_admin]
GO
/****** Object:  Table [dbo].[pc_myalbum]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[pc_myalbum]
GO
/****** Object:  Table [dbo].[block_ip]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[block_ip]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2017-01-23 오후 12:56:22 ******/
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[block_ip]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[block_ip](
	[ip_idx] [int] IDENTITY(1,1) NOT NULL,
	[vIP] [varchar](15) NOT NULL,
	[user_id] [varchar](30) NULL,
	[user_pass] [varchar](30) NULL,
	[block_date] [datetime] NOT NULL,
	[access_on] [int] NOT NULL,
	[b_content] [varchar](100) NULL,
	[access_cnt] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pc_myalbum]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pc_myalbum](
	[seqno] [int] IDENTITY(1,1) NOT NULL,
	[userid] [varchar](30) NOT NULL,
	[idno] [varchar](20) NOT NULL,
	[title] [varchar](200) NOT NULL,
	[contents] [text] NULL,
	[category] [varchar](2) NULL,
	[main_img] [varchar](1) NOT NULL,
	[filename] [varchar](200) NOT NULL,
	[score] [int] NOT NULL CONSTRAINT [DF_pc_myalbum_score]  DEFAULT ((0)),
	[readcnt] [int] NULL CONSTRAINT [DF_pc_myalbum_readcnt]  DEFAULT ((0)),
	[week_rank] [int] NULL CONSTRAINT [DF_pc_myalbum_week_rank]  DEFAULT ((0)),
	[month_rank] [int] NULL CONSTRAINT [DF_pc_myalbum_month_rank]  DEFAULT ((0)),
	[all_rank] [int] NULL CONSTRAINT [DF_pc_myalbum_all_rank]  DEFAULT ((0)),
	[regdate] [datetime] NOT NULL CONSTRAINT [DF_pc_myalbum_regdate]  DEFAULT (getdate()),
	[regid] [varchar](30) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL,
	[modid] [varchar](30) NULL,
	[modip] [varchar](20) NULL,
 CONSTRAINT [PK_pc_myalbum] PRIMARY KEY CLUSTERED 
(
	[seqno] ASC,
	[idno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pc_myalbum_admin]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pc_myalbum_admin](
	[seqno] [int] IDENTITY(1,1) NOT NULL,
	[userid] [varchar](30) NOT NULL,
	[idno] [varchar](20) NOT NULL,
	[idnm] [varchar](30) NOT NULL,
	[use_comm] [varchar](1) NULL,
	[pagesize] [int] NOT NULL CONSTRAINT [DF_pc_myalbum_admin_pagesize]  DEFAULT ((0)),
	[read_grade] [varchar](1) NULL,
	[regdate] [datetime] NULL CONSTRAINT [DF_pc_myalbum_admin_regdate]  DEFAULT (getdate()),
	[regid] [varchar](30) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL,
	[modid] [varchar](30) NULL,
	[modip] [varchar](20) NULL,
 CONSTRAINT [PK_pc_myalbum_admin] PRIMARY KEY CLUSTERED 
(
	[idno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pc_myalbum_comm]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pc_myalbum_comm](
	[seqno] [int] NOT NULL,
	[idno] [varchar](20) NOT NULL,
	[comm_no] [int] NOT NULL,
	[userid] [varchar](30) NOT NULL,
	[contents] [text] NULL,
	[regdate] [datetime] NULL,
	[regid] [varchar](30) NULL,
	[regip] [varchar](20) NULL,
 CONSTRAINT [PK_pc_myalbum_comm] PRIMARY KEY CLUSTERED 
(
	[seqno] ASC,
	[idno] ASC,
	[comm_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pc_myalbum_score]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pc_myalbum_score](
	[seqno] [int] NOT NULL,
	[idno] [varchar](20) NOT NULL,
	[score_no] [int] IDENTITY(1,1) NOT NULL,
	[userid] [varchar](30) NOT NULL,
	[score] [int] NOT NULL,
	[regdate] [datetime] NULL,
	[regid] [varchar](30) NULL,
	[regip] [varchar](20) NULL,
 CONSTRAINT [PK_pc_myalbum_score] PRIMARY KEY CLUSTERED 
(
	[seqno] ASC,
	[idno] ASC,
	[score_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pc_mycash]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pc_mycash](
	[seqno] [int] IDENTITY(1,1) NOT NULL,
	[userid] [varchar](30) NOT NULL,
	[tot_amt] [int] NOT NULL,
	[use_amt] [int] NOT NULL,
	[use_item] [varchar](1000) NULL,
	[regdate] [datetime] NULL,
	[regid] [varchar](30) NULL,
	[regip] [varchar](20) NULL,
 CONSTRAINT [PK_pc_mycash] PRIMARY KEY CLUSTERED 
(
	[seqno] ASC,
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pc_mydiary]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pc_mydiary](
	[seqno] [int] IDENTITY(1,1) NOT NULL,
	[userid] [varchar](30) NOT NULL,
	[writedt] [varchar](8) NOT NULL,
	[contents] [text] NULL,
	[filename] [varchar](200) NULL,
	[shareyn] [varchar](1) NULL,
	[regdate] [datetime] NULL,
	[regid] [varchar](30) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL,
	[modid] [varchar](30) NULL,
	[modip] [varchar](20) NULL,
 CONSTRAINT [PK_pc_mydiary] PRIMARY KEY CLUSTERED 
(
	[seqno] ASC,
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pc_mydiary_comm]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pc_mydiary_comm](
	[seqno] [int] NOT NULL,
	[userid] [varchar](30) NOT NULL,
	[comm_no] [int] IDENTITY(1,1) NOT NULL,
	[contents] [text] NULL,
	[writer] [varchar](30) NULL,
	[regdate] [datetime] NULL,
	[regid] [varchar](30) NULL,
	[regip] [varchar](20) NULL,
 CONSTRAINT [PK_pc_mydiary_comm] PRIMARY KEY CLUSTERED 
(
	[seqno] ASC,
	[userid] ASC,
	[comm_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pc_myfriends]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pc_myfriends](
	[seqno] [int] IDENTITY(1,1) NOT NULL,
	[userid] [nvarchar](50) NOT NULL,
	[friendsid] [nvarchar](50) NOT NULL,
	[regdate] [datetime] NULL,
	[regid] [varchar](30) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL,
	[modid] [varchar](30) NULL,
	[modip] [varchar](20) NULL,
 CONSTRAINT [PK_pc_myfriends] PRIMARY KEY CLUSTERED 
(
	[seqno] ASC,
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pc_notice]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pc_notice](
	[seqno] [int] IDENTITY(1,1) NOT NULL,
	[idno] [varchar](20) NOT NULL,
	[title] [varchar](200) NOT NULL,
	[contents] [text] NULL,
	[writer] [varchar](20) NULL,
	[readcnt] [int] NULL,
	[regdate] [datetime] NULL,
	[regid] [varchar](30) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL,
	[modid] [varchar](30) NULL,
	[modip] [varchar](20) NULL,
 CONSTRAINT [PK_pc_notice] PRIMARY KEY CLUSTERED 
(
	[seqno] ASC,
	[idno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pc_now_connect]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pc_now_connect](
	[seqno] [int] IDENTITY(1,1) NOT NULL,
	[userid] [varchar](30) NOT NULL,
	[regdate] [datetime] NOT NULL CONSTRAINT [DF_pc_now_connect_regdate]  DEFAULT (getdate()),
	[regip] [varchar](20) NULL,
 CONSTRAINT [PK_pc_now_connect] PRIMARY KEY CLUSTERED 
(
	[seqno] ASC,
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sfbbs_AdminBoard]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sfbbs_AdminBoard](
	[grp] [nvarchar](128) NOT NULL,
	[board] [nvarchar](50) NOT NULL,
	[num] [int] NOT NULL,
	[idx] [int] NULL,
	[re_step] [int] NULL,
	[re_level] [int] NULL,
	[bbs_name] [nvarchar](50) NULL,
	[bbs_userid] [nvarchar](20) NULL,
	[bbs_email] [nvarchar](50) NULL,
	[bbs_homepage] [nvarchar](250) NULL,
	[bbs_title] [nvarchar](100) NULL,
	[bbs_pwd] [nvarchar](50) NULL,
	[bbs_day] [nvarchar](50) NULL,
	[bbs_hour] [nvarchar](50) NULL,
	[bbs_ip] [nvarchar](50) NULL,
	[bbs_hit] [int] NULL,
	[bbs_content] [ntext] NULL,
	[bbs_html] [nvarchar](50) NULL,
	[bbs_file] [nvarchar](256) NULL,
	[bbs_filehit] [int] NULL,
	[bbs_filesize] [int] NULL,
	[bbs_img] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sfbbs_board]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sfbbs_board](
	[grp] [nvarchar](128) NOT NULL,
	[board] [nvarchar](50) NOT NULL,
	[num] [int] NOT NULL,
	[idx] [int] NULL,
	[re_step] [int] NULL,
	[re_level] [int] NULL,
	[bbs_name] [nvarchar](50) NULL,
	[bbs_userid] [nvarchar](20) NULL,
	[bbs_email] [nvarchar](50) NULL,
	[bbs_homepage] [nvarchar](250) NULL,
	[bbs_title] [nvarchar](100) NULL,
	[bbs_pwd] [nvarchar](50) NULL,
	[bbs_day] [nvarchar](50) NULL,
	[bbs_hour] [nvarchar](50) NULL,
	[bbs_ip] [nvarchar](50) NULL,
	[bbs_hit] [int] NULL,
	[bbs_content] [ntext] NULL,
	[bbs_html] [nvarchar](50) NULL,
	[bbs_file] [nvarchar](256) NULL,
	[bbs_filehit] [int] NULL,
	[bbs_filesize] [int] NULL,
	[bbs_img] [ntext] NULL,
	[admin_notice] [bit] NULL,
	[adult_check] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sfbbs_board_comment]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sfbbs_board_comment](
	[grp] [nvarchar](128) NOT NULL,
	[board] [nvarchar](50) NOT NULL,
	[num] [int] NOT NULL,
	[comment_idx] [int] NOT NULL,
	[comment_name] [nvarchar](20) NULL,
	[comment_userid] [nvarchar](30) NULL,
	[comment_content] [ntext] NULL,
	[comment_ip] [nvarchar](19) NULL,
	[comment_day] [nvarchar](30) NULL,
	[comment_hour] [nvarchar](30) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sfbbs_board_config]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sfbbs_board_config](
	[grp] [nvarchar](128) NOT NULL,
	[board] [nvarchar](50) NOT NULL,
	[idx] [int] NULL,
	[grpnm] [nvarchar](256) NOT NULL,
	[admin_userid] [nvarchar](20) NULL,
	[use_admin] [nvarchar](5) NULL,
	[board_date] [nvarchar](30) NULL,
	[browser_title] [nvarchar](100) NULL,
	[board_title] [nvarchar](150) NULL,
	[table_size] [nvarchar](4) NULL,
	[use_html] [nvarchar](5) NULL,
	[use_comment] [nvarchar](5) NULL,
	[flash_width] [nvarchar](4) NULL,
	[flash_height] [nvarchar](4) NULL,
	[use_img] [nvarchar](5) NULL,
	[use_img_size] [int] NULL,
	[use_file] [nvarchar](5) NULL,
	[use_sound] [nvarchar](5) NULL,
	[reply_size] [int] NULL,
	[page_size] [int] NULL,
	[gopage_size] [int] NULL,
	[use_search] [nvarchar](5) NULL,
	[use_list] [nvarchar](5) NULL,
	[cell_sort] [nvarchar](6) NULL,
	[board_bgcolor] [nvarchar](20) NULL,
	[board_font] [nvarchar](20) NULL,
	[board_cbgcolor] [nvarchar](20) NULL,
	[board_tfont] [nvarchar](20) NULL,
	[board_tbgcolor] [nvarchar](20) NULL,
	[board_tfcolor] [nvarchar](20) NULL,
	[board_pagecolor] [nvarchar](20) NULL,
	[content_tfcolor] [nvarchar](20) NULL,
	[content_tbgcolor] [nvarchar](20) NULL,
	[content_cfcolor] [nvarchar](20) NULL,
	[content_cbgcolor] [nvarchar](20) NULL,
	[content_bgcolor] [nvarchar](20) NULL,
	[content_font] [nvarchar](20) NULL,
	[skin_board] [nvarchar](50) NULL,
	[use_19] [varchar](50) NULL,
	[board_title_img] [varchar](70) NULL,
	[board_title_img_w] [int] NULL,
	[board_title_img_h] [int] NULL,
	[board_title_img_use] [int] NULL CONSTRAINT [DF_sfbbs_board_config_board_title_img_use]  DEFAULT ((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tbl_BokYongHooKi]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tbl_BokYongHooKi](
	[idx] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](100) NOT NULL,
	[VisitDate] [varchar](8) NOT NULL,
	[CureDate] [varchar](8) NULL,
	[JungSang] [varchar](max) NULL,
	[tempC] [varchar](4) NULL,
	[Feber] [varchar](1) NULL,
	[HaeYeolJe] [varchar](1) NULL,
	[ChouBang] [varchar](max) NULL,
	[Yak_iLbun] [varchar](3) NULL,
	[HangSaengJeBokan] [varchar](1) NULL,
	[HangSaengJeEat] [varchar](1) NULL,
	[ChamGoSaHang] [varchar](max) NULL,
	[BokYongHooKi] [varchar](max) NULL,
	[regdate] [datetime] NOT NULL CONSTRAINT [DF_Tbl_BokYongHooKi_regdate]  DEFAULT (getdate()),
	[regid] [nvarchar](100) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL CONSTRAINT [DF_Tbl_BokYongHooKi_moddate]  DEFAULT (getdate()),
	[modid] [nvarchar](100) NULL,
	[modip] [varchar](20) NULL,
 CONSTRAINT [PK_Tbl_BokYongHooKi_1] PRIMARY KEY CLUSTERED 
(
	[idx] ASC,
	[UserID] ASC,
	[VisitDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tbl_Code]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tbl_Code](
	[CodeGroup] [varchar](50) NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[CodeName] [nvarchar](50) NULL,
	[images] [varchar](200) NULL,
	[regdate] [datetime] NOT NULL CONSTRAINT [DF_Tbl_Code_regdate]  DEFAULT (getdate()),
	[regid] [nvarchar](100) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL CONSTRAINT [DF_Tbl_Code_moddate]  DEFAULT (getdate()),
	[modid] [nvarchar](100) NULL,
	[modip] [varchar](20) NULL,
 CONSTRAINT [PK_Tbl_Code] PRIMARY KEY CLUSTERED 
(
	[CodeGroup] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tbl_Drug_Info]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tbl_Drug_Info](
	[idx] [bigint] NOT NULL,
	[drugName] [nvarchar](100) NULL,
	[drugCode] [varchar](13) NULL,
	[drugDanPoomYN] [char](1) NULL CONSTRAINT [DF_Tbl_Drug_Info_drugDanPoomYN]  DEFAULT ('Y'),
	[drugSungBun] [nvarchar](200) NULL,
	[drugCompany] [nvarchar](100) NULL,
	[drugBunRu] [varchar](6) NULL,
	[drugToYeo] [nvarchar](1000) NULL,
	[drugJeHyong] [nvarchar](10) NULL,
	[drugGubun] [nvarchar](10) NULL,
	[drugInsure] [nvarchar](100) NULL,
	[drugImage] [nvarchar](150) NULL,
	[regdate] [datetime] NOT NULL CONSTRAINT [DF_Tbl_Drug_Info_regdate]  DEFAULT (getdate()),
	[regid] [nvarchar](100) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL CONSTRAINT [DF_Tbl_Drug_Info_moddate]  DEFAULT (getdate()),
	[modid] [nvarchar](100) NULL,
	[modip] [varchar](20) NULL,
 CONSTRAINT [PK_Tbl_Drug_Info] PRIMARY KEY CLUSTERED 
(
	[idx] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tbl_DrugOrg_Info]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tbl_DrugOrg_Info](
	[ITEM_SEQ] [bigint] NOT NULL,
	[ITEM_NAME] [nvarchar](100) NULL,
	[ENTP_SEQ] [bigint] NULL,
	[ENTP_NAME] [nvarchar](100) NULL,
	[CHART] [nvarchar](100) NULL,
	[ITEM_IMAGE] [nvarchar](300) NULL,
	[PRINT_FRONT] [nvarchar](100) NULL,
	[PRINT_BACK] [nvarchar](100) NULL,
	[DRUG_SHAPE] [nvarchar](100) NULL,
	[COLOR_CLASS1] [nvarchar](100) NULL,
	[COLOR_CLASS2] [nvarchar](100) NULL,
	[LINE_FRONT] [nvarchar](100) NULL,
	[LINE_BACK] [nvarchar](100) NULL,
	[LENG_LONG] [nvarchar](100) NULL,
	[LENG_SHORT] [nvarchar](100) NULL,
	[THICK] [nvarchar](100) NULL,
	[IMG_REGIST_TS] [nvarchar](100) NULL,
	[CLASS_NO] [nvarchar](100) NULL,
	[CLASS_NAME] [nvarchar](100) NULL,
	[ETC_OTC_NAME] [nvarchar](100) NULL,
	[ITEM_PERMIT_DATE] [nvarchar](100) NULL,
	[FORM_CODE_NAME] [nvarchar](100) NULL,
	[MARK_CODE_FRONT_ANAL] [nvarchar](100) NULL,
	[MARK_CODE_BACK_ANAL] [nvarchar](100) NULL,
	[MARK_CODE_FRONT_IMG] [nvarchar](100) NULL,
	[MARK_CODE_BACK_IMG] [nvarchar](100) NULL,
	[regdate] [datetime] NOT NULL CONSTRAINT [DF_Tbl_DrugOrg_Info_regdate]  DEFAULT (getdate()),
	[regid] [nvarchar](100) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL CONSTRAINT [DF_Tbl_DrugOrg_Info_moddate]  DEFAULT (getdate()),
	[modid] [nvarchar](100) NULL,
	[modip] [varchar](20) NULL,
 CONSTRAINT [PK_Tbl_DrugOrg_Info] PRIMARY KEY CLUSTERED 
(
	[ITEM_SEQ] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tbl_member]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tbl_member](
	[idx] [int] IDENTITY(10000000,1) NOT NULL,
	[UserID] [nvarchar](100) NOT NULL,
	[UserPwd] [varchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[UserGroup] [char](1) NOT NULL CONSTRAINT [DF_Tbl_member_UserGroup]  DEFAULT ('U'),
	[UserHP] [varchar](13) NULL,
	[UserRegIP] [varchar](15) NULL,
	[regdate] [datetime] NOT NULL CONSTRAINT [DF_Tbl_member_regdate_1]  DEFAULT (getdate()),
	[regid] [nvarchar](100) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL CONSTRAINT [DF_Tbl_member_moddate]  DEFAULT (getdate()),
	[modid] [nvarchar](100) NULL,
	[modip] [varchar](20) NULL,
 CONSTRAINT [PK_Tbl_member] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tbl_memberChild]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tbl_memberChild](
	[idx] [int] IDENTITY(1,1) NOT NULL,
	[Pidx] [int] NULL,
	[PUserID] [nvarchar](100) NULL,
	[PUserName] [nvarchar](50) NULL,
	[PUserSchoolCd] [varchar](20) NULL,
	[PEmergencyHP] [varchar](13) NULL,
	[PUserPic] [nvarchar](200) NULL,
	[regdate] [datetime] NOT NULL,
	[regid] [nvarchar](100) NULL,
	[regip] [varchar](20) NULL,
	[moddate] [datetime] NULL,
	[modid] [nvarchar](100) NULL,
	[modip] [varchar](20) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 2017-01-23 오후 12:56:22 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 2017-01-23 오후 12:56:22 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 2017-01-23 오후 12:56:22 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 2017-01-23 오후 12:56:22 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 2017-01-23 오후 12:56:22 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 2017-01-23 오후 12:56:22 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl_DrugOrg_Info_EntpNae]    Script Date: 2017-01-23 오후 12:56:22 ******/
CREATE NONCLUSTERED INDEX [IX_Tbl_DrugOrg_Info_EntpNae] ON [dbo].[Tbl_DrugOrg_Info]
(
	[ITEM_SEQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Tbl_DrugOrg_Info_ItemName]    Script Date: 2017-01-23 오후 12:56:22 ******/
CREATE NONCLUSTERED INDEX [IX_Tbl_DrugOrg_Info_ItemName] ON [dbo].[Tbl_DrugOrg_Info]
(
	[ITEM_NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl_member_idx]    Script Date: 2017-01-23 오후 12:56:22 ******/
CREATE NONCLUSTERED INDEX [IX_Tbl_member_idx] ON [dbo].[Tbl_member]
(
	[idx] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[block_ip] ADD  CONSTRAINT [DF_block_ip_block_date]  DEFAULT (getdate()) FOR [block_date]
GO
ALTER TABLE [dbo].[block_ip] ADD  CONSTRAINT [DF_block_ip_access_on]  DEFAULT ((0)) FOR [access_on]
GO
ALTER TABLE [dbo].[pc_myalbum_comm] ADD  CONSTRAINT [DF_pc_myalbum_comm_regdate]  DEFAULT (getdate()) FOR [regdate]
GO
ALTER TABLE [dbo].[pc_myalbum_score] ADD  CONSTRAINT [DF_pc_myalbum_score_score]  DEFAULT ((0)) FOR [score]
GO
ALTER TABLE [dbo].[pc_myalbum_score] ADD  CONSTRAINT [DF_pc_myalbum_score_regdate]  DEFAULT (getdate()) FOR [regdate]
GO
ALTER TABLE [dbo].[pc_mycash] ADD  CONSTRAINT [DF_pc_mycash_tot_amt]  DEFAULT ((0)) FOR [tot_amt]
GO
ALTER TABLE [dbo].[pc_mycash] ADD  CONSTRAINT [DF_pc_mycash_use_amt]  DEFAULT ((0)) FOR [use_amt]
GO
ALTER TABLE [dbo].[pc_mycash] ADD  CONSTRAINT [DF_pc_mycash_regdate]  DEFAULT (getdate()) FOR [regdate]
GO
ALTER TABLE [dbo].[pc_mydiary] ADD  CONSTRAINT [DF__pc_mydiar__regda__2235F3A1]  DEFAULT (getdate()) FOR [regdate]
GO
ALTER TABLE [dbo].[pc_mydiary_comm] ADD  CONSTRAINT [DF__pc_diary___regda__630F92C5]  DEFAULT (getdate()) FOR [regdate]
GO
ALTER TABLE [dbo].[pc_myfriends] ADD  CONSTRAINT [DF_pc_myfriends_regdate]  DEFAULT (getdate()) FOR [regdate]
GO
ALTER TABLE [dbo].[pc_notice] ADD  CONSTRAINT [DF_pc_notice_readcnt]  DEFAULT ((0)) FOR [readcnt]
GO
ALTER TABLE [dbo].[pc_notice] ADD  CONSTRAINT [DF_pc_notice_regdate]  DEFAULT ((0)) FOR [regdate]
GO
ALTER TABLE [dbo].[Tbl_memberChild] ADD  CONSTRAINT [DF_Tbl_memberChild_regdate]  DEFAULT (getdate()) FOR [regdate]
GO
ALTER TABLE [dbo].[Tbl_memberChild] ADD  CONSTRAINT [DF_Tbl_memberChild_moddate]  DEFAULT (getdate()) FOR [moddate]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[pc_myalbum]  WITH CHECK ADD  CONSTRAINT [FK_pc_myalbum_pc_myalbum_admin] FOREIGN KEY([idno])
REFERENCES [dbo].[pc_myalbum_admin] ([idno])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[pc_myalbum] CHECK CONSTRAINT [FK_pc_myalbum_pc_myalbum_admin]
GO
ALTER TABLE [dbo].[pc_myalbum_comm]  WITH CHECK ADD  CONSTRAINT [FK_pc_myalbum_comm_pc_myalbum] FOREIGN KEY([seqno], [idno])
REFERENCES [dbo].[pc_myalbum] ([seqno], [idno])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[pc_myalbum_comm] CHECK CONSTRAINT [FK_pc_myalbum_comm_pc_myalbum]
GO
ALTER TABLE [dbo].[pc_myalbum_score]  WITH CHECK ADD  CONSTRAINT [FK_pc_myalbum_score_pc_myalbum] FOREIGN KEY([seqno], [idno])
REFERENCES [dbo].[pc_myalbum] ([seqno], [idno])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[pc_myalbum_score] CHECK CONSTRAINT [FK_pc_myalbum_score_pc_myalbum]
GO
ALTER TABLE [dbo].[pc_mydiary_comm]  WITH CHECK ADD  CONSTRAINT [FK_pc_mydiary_comm_pc_mydiary] FOREIGN KEY([seqno], [userid])
REFERENCES [dbo].[pc_mydiary] ([seqno], [userid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[pc_mydiary_comm] CHECK CONSTRAINT [FK_pc_mydiary_comm_pc_mydiary]
GO
/****** Object:  StoredProcedure [dbo].[SP_A_LOGIN]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: Web Project(Dong Jin)
*          명칭		: 지점 로그인
*          인수		:
*          Output    : 
*
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2003/09/15             장동진              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SP_A_LOGIN]
	@USERID VARCHAR(30),
	@PASSWD VARCHAR(30),
	@REGIP VARCHAR(20)
 AS
	DECLARE @SQL VARCHAR(2000)
	DECLARE @USERID_CHK INT
	DECLARE @CONNECT_CHK INT
	DECLARE @OPEN_CHK VARCHAR(10)
	DECLARE @CLOSE_CHK VARCHAR(10)
	
	SELECT	USERID, USERNM, NICKNM, PASSWD, SOCNO, 
		EMAIL, GRADE, STATES, LEVEL, CLOSE_DATE
	FROM PV_USER WITH(NOLOCK)
	WHERE USERID = @USERID
		AND PASSWD = @PASSWD
		AND STATES = '정상'


	SELECT @USERID_CHK = LEN(USERID),@OPEN_CHK = OPEN_DATE,@CLOSE_CHK = CLOSE_DATE 
	FROM PV_USER WITH(NOLOCK) 
	WHERE USERID = @USERID 
		AND PASSWD = @PASSWD
	
	IF @USERID_CHK > 0 
	BEGIN
		UPDATE	PC_AGENCY
		SET		CONNECT_CNT	= ISNULL(CONNECT_CNT,0) + 1	,
				CONNECT_DATE	= GETDATE()
		WHERE	USERID		= @USERID
 
		--접속자 입력
		EXEC SP_C_NOW_CONNECT_PROCESS 'Insert',@USERID,@REGIP
	END



GO
/****** Object:  StoredProcedure [dbo].[SP_C_COMMON_GUBUN_CD_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 공통코드 SELECTBOX 용(조회)
*          인수		:
*          Output    :
* exec SP_C_COMMON_GUBUN_CD_R @gubun_id=''
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2016-06-27             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SP_C_COMMON_GUBUN_CD_R]
(
	@gubun_id varchar(20) =''
)
AS
BEGIN


	SELECT gubun_id
	FROM pc_gubun_cd WITH(NOLOCK)
	WHERE gubun_id like '%'+isnull(@gubun_id ,'')+'%'
	GROUP BY gubun_id
	ORDER BY gubun_id


END




GO
/****** Object:  StoredProcedure [dbo].[SP_C_COMMON_GUBUN_CDALL_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 공통코드 SELECTBOX 용(조회)
*          인수		:
*          Output    :
* exec SP_C_COMMON_GUBUN_CDALL_R @gubun_id=''
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2016-06-27             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SP_C_COMMON_GUBUN_CDALL_R]
(
	@gubun_id varchar(20) =''
)
AS
BEGIN

	SELECT seqno, gubun_id, gubun_cd, gubun_nm, images, regdate, regid, regip, moddate, modid, modip
	FROM pc_gubun_cd with(nolock)
	WHERE gubun_id like '%'+isnull(@gubun_id ,'')+'%'


END




GO
/****** Object:  StoredProcedure [dbo].[SP_C_MENU_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 메뉴 관리 (조회)
*          인수		:
*          Output    :
* exec SP_C_MENU_R @MENUGRUP=''
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2016-06-24             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SP_C_MENU_R]
(
	@MENUGRUP VARCHAR(4) ='1000'
)
AS
BEGIN

	SELECT CM.MENUCODE, CM.MENUNAME, CM.MENUGRUP, CM.MENULEVEL, CM.ORDERNUM,
		CM.USECHK, CM.TOPIMG1, CM.TOPIMG2, CM.TITLEIMG, CM.PAGECODE,
		CM.MDATE, CM.USERID , CP.PAGEPATH
	FROM C_MENU CM WITH(NOLOCK)
	LEFT OUTER JOIN C_PAGE CP WITH(NOLOCK) on CM.PAGECODE = CP.PAGECODE AND CP.DelChk='N'
	WHERE CM.USECHK = 'Y'
		AND MENUGRUP = @MENUGRUP
	ORDER BY ORDERNUM

END




GO
/****** Object:  StoredProcedure [dbo].[SP_C_NOW_CONNECT_PROCESS]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: Web Project(Dong Jin)
*          명칭		: 현제 접속자
*          인수		:
*          Output   		: 
*
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2003/07/07             장동진              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SP_C_NOW_CONNECT_PROCESS]
	@JOB VARCHAR(10),
	@USERID VARCHAR(30),
	@REGIP VARCHAR(20)
 AS
	DECLARE @USERID_CHK INT
	IF @JOB = 'Insert'
		BEGIN
			DELETE
			FROM		PC_NOW_CONNECT
			WHERE	DATEADD(DAY,1,REGDATE)	< GETDATE()
			SELECT @USERID_CHK = ISNULL(COUNT(USERID),0) FROM PC_NOW_CONNECT WHERE USERID = @USERID
			
			IF @USERID_CHK = 0
				BEGIN
					INSERT INTO PC_NOW_CONNECT(USERID,REGDATE,REGIP)
						VALUES(@USERID,GETDATE(),@REGIP)
				END
		END
	ELSE IF @JOB = 'Delete'
		BEGIN
			DELETE
			FROM		PC_NOW_CONNECT
			WHERE	USERID	= @USERID
		END



GO
/****** Object:  StoredProcedure [dbo].[SP_C_PAGE_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 페이지 관리 (조회)
*          인수		:
*          Output    :
* exec SP_C_PAGE_R @UPPAGECODE='', @PAGENAME='', @PAGEPATH='', @EXPLAIN='',@DelChk=''
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2016-06-23             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SP_C_PAGE_R]
(
	@UPPAGECODE		VARCHAR(4),
	@PAGENAME		VARCHAR(50),
	@PAGEPATH		VARCHAR(200),
	@EXPLAIN		VARCHAR(200),
	@DelChk		VARCHAR(1)
)
AS
BEGIN

	SELECT PAGECODE, UPPAGECODE, PAGENAME, PAGEPATH, EXPLAIN, DelChk, MDATE, ModID
	FROM C_PAGE WITH(NOLOCK)
	WHERE UPPAGECODE LIKE '%' + ISNULL(@UPPAGECODE , '') + '%'
		AND PAGENAME LIKE '%' + ISNULL(@PAGENAME , '') + '%'
		AND PAGEPATH LIKE '%' + ISNULL(@PAGEPATH , '') + '%'
		AND EXPLAIN LIKE '%' + ISNULL(@EXPLAIN , '') + '%'
		AND DelChk LIKE '%' + ISNULL(@DelChk , '') + '%'
	ORDER BY PAGECODE, UPPAGECODE


END


GO
/****** Object:  StoredProcedure [dbo].[SP_C_PAGE_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 페이지 관리 (조회)
*          인수		:
*          Output    :
* exec SP_C_PAGE_S @PAGECODE='',@UPPAGECODE='', @PAGENAME='', @PAGEPATH='', @EXPLAIN='',@DelChk='',@ModID=''
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2016-06-23             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SP_C_PAGE_S]
(
	@PAGECODE		VARCHAR(4),
	@UPPAGECODE		VARCHAR(4),
	@PAGENAME		VARCHAR(50),
	@PAGEPATH		VARCHAR(200),
	@EXPLAIN		VARCHAR(200),
	@DelChk			VARCHAR(1),
	@ModID			VARCHAR(30)
)
AS
BEGIN

	DECLARE @Cnt int
	SELECT @Cnt = COUNT(PAGECODE)
	FROM C_PAGE WITH(NOLOCK)
	WHERE PAGECODE = @PAGECODE

	IF @Cnt = 0
	BEGIN

		INSERT INTO C_PAGE
		(
			PAGECODE
			,UPPAGECODE
			,PAGENAME
			,PAGEPATH
			,EXPLAIN
			,DelChk
			,MDATE
			,ModID
		)
		SELECT
			@PAGECODE
			,@UPPAGECODE
			,@PAGENAME
			,@PAGEPATH
			,@EXPLAIN
			,@DelChk
			,GETDATE()
			,@ModID

	END
	ELSE
	BEGIN

		UPDATE C_PAGE SET
			PAGENAME = @PAGENAME,
			PAGEPATH = @PAGEPATH,
			EXPLAIN = @EXPLAIN,
			DelChk	= @DelChk,
			MDATE	= GETDATE(),
			ModID = @ModID
		WHERE PAGECODE = @PAGECODE

	END


END


GO
/****** Object:  StoredProcedure [dbo].[SP_U_GUBUN_CD]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: Web Project(Dong Jin)
*          명칭		: 구분코드 출력
*          인수		:
*          Output    : 
*
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2003/07/28             장동진              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SP_U_GUBUN_CD]
	@GUBUN_ID VARCHAR(50)
 AS
	SELECT	GUBUN_CD,GUBUN_NM,GUBUN_ID
	FROM		PC_GUBUN_CD
	WHERE	GUBUN_ID 	= @GUBUN_ID
	ORDER BY 	GUBUN_CD ASC



GO
/****** Object:  StoredProcedure [dbo].[SP_U_PC_MYALBUM_SCORE_U]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_U_PC_MYALBUM_SCORE_U]
	@SCORE int,
	@SEQNO int,
	@USERID VARCHAR(30),
	@AREA VARCHAR(3),
	@TEMA VARCHAR(3)

AS
BEGIN
	UPDATE PC_MYALBUM SET SCORE = @SCORE WHERE SEQNO = @SEQNO

	UPDATE PC_USER SET AREA = @AREA, TEMA = @TEMA WHERE USERID = @USERID

END




GO
/****** Object:  StoredProcedure [dbo].[SP_U_PHOTOALBUM_BEST]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: Web Project(Dong Jin)
*          명칭		: 앨범 베스트
*          인수		:
*          Output    : 
*
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2003/09/05             장동진              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SP_U_PHOTOALBUM_BEST]
	@JOB VARCHAR(10)
 AS
	DECLARE @SQL VARCHAR(2000)
	IF @JOB = 'ALL'
		BEGIN
			SELECT	TOP 500 A.SEQNO,A.USERID,A.TITLE,A.CATEGORY,A.SCORE,A.FILENAME,B.NICKNM,
					A.IDNO,DBO.UF_AGE(B.SOCNO,GETDATE()) AS AGE_HAN,DBO.UF_SEX(B.SOCNO) AS SEX,
					B.AVATAR,DBO.UF_GUBUN_CD('TEMA',B.TEMA) AS TEMA_HAN,
					DBO.UF_GUBUN_CD('AREA',B.AREA) AS AREA_HAN
					,B.TEMA, B.AREA
			FROM		PC_MYALBUM A,PC_USER B
			WHERE	A.USERID	= B.USERID
			AND		B.STATES	= '정상'
			ORDER BY 	A.SCORE DESC,A.SEQNO DESC
		END
	ELSE IF @JOB = 'MONTH'
		BEGIN
			SELECT	TOP 10 A.SEQNO,A.USERID,A.TITLE,A.CATEGORY,A.SCORE,A.FILENAME,B.NICKNM,
					A.IDNO,DBO.UF_AGE(B.SOCNO,GETDATE()) AS AGE_HAN,DBO.UF_SEX(B.SOCNO) AS SEX,
					B.AVATAR,DBO.UF_GUBUN_CD('TEMA',B.TEMA) AS TEMA_HAN,
					DBO.UF_GUBUN_CD('AREA',B.AREA) AS AREA_HAN
			FROM		PC_MYALBUM A,PC_USER B
			WHERE	A.USERID	= B.USERID
			AND		B.STATES	= '정상'	
			AND		DATEPART(YEAR,A.REGDATE)		= DATEPART(YEAR,GETDATE())
			AND		DATEPART(MONTH,A.REGDATE)	= DATEPART(MONTH,GETDATE())
			ORDER BY	A.SCORE DESC,A.SEQNO DESC
		END
	ELSE IF @JOB = 'WEEK'
		BEGIN
			SELECT	TOP 10 A.SEQNO,A.USERID,A.TITLE,A.CATEGORY,A.SCORE,A.FILENAME,B.NICKNM,
					A.IDNO,DBO.UF_AGE(B.SOCNO,GETDATE()) AS AGE_HAN,DBO.UF_SEX(B.SOCNO) AS SEX,
					B.AVATAR,DBO.UF_GUBUN_CD('TEMA',B.TEMA) AS TEMA_HAN,
					DBO.UF_GUBUN_CD('AREA',B.AREA) AS AREA_HAN
			FROM		PC_MYALBUM A,PC_USER B
			WHERE	A.USERID	= B.USERID
			AND		B.STATES	= '정상'	
			AND		DATEPART(YEAR,A.REGDATE)		= DATEPART(YEAR,GETDATE())
			AND		DATEPART(MONTH,A.REGDATE)	= DATEPART(MONTH,GETDATE())
			AND		DATEPART(WEEK,A.REGDATE)	= DATEPART(WEEK,GETDATE())
			ORDER BY	 A.SCORE DESC,A.SEQNO DESC
		END




GO
/****** Object:  StoredProcedure [dbo].[SP_WEB_Common_Master_Site_UpdateDate_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_WEB_Common_Master_Site_UpdateDate_R]
AS
    SELECT GETDATE()AS UPDATEDATE


GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_BokYongHooKi_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************
개요        : 복용후기 리스트
생성자      : 박희수
생성일      : 2017-01-05
로직        : 
최종수정자  : 
최종수정일  : 
exec SPM_Web_COMMON_Tbl_BokYongHooKi_R @UserID='his001'
***********************************************************************************************/ 
CREATE PROC [dbo].[SPM_Web_COMMON_Tbl_BokYongHooKi_R]
	@UserID	NVARCHAR(100)  =''
AS
BEGIN
	--SELECT * FROM (
	--	SELECT 
	--		FLOOR((ROW_NUMBER() OVER (ORDER BY A.ITEM) - 1) / @ROWCNT + 1) PAGE
	--		, COUNT(*) OVER() AS TOTAL_COUNT
	--		,A.ORDER_GROUP,
	--	FROM ADO_ITEM AS A WITH(NOLOCK)
	--	WHERE A.SECTION BETWEEN @SECTIONFROM AND @SECTIONTO
	--		--AND B.SUP_TERM_ID LIKE '%'+ISNULL(@RUD_ID,'')+'%'
	--) T
	--WHERE T.PAGE=@PAGENUM

	SELECT 
		idx, UserID, VisitDate, CureDate, JungSang, 
		tempC, Feber, HaeYeolJe, ChouBang, Yak_iLbun, 
		HangSaengJeBokan, HangSaengJeEat, ChamGoSaHang, BokYongHooKi, regdate, 
		regid, regip, moddate, modid, modip
	FROM Tbl_BokYongHooKi with(nolock)
	WHERE @UserID like '%'+ISNULL(@UserID,'')+'%'
	ORDER BY idx DESC
END


GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_BokYongHooKi_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************
개요        : 복용후기 작성
생성자      : 박희수
생성일      : 2017-01-03
로직        :
최종수정자  :
최종수정일  :
2017-01-06 @idx 추가로 수정 추가
exec SPM_Web_COMMON_Tbl_BokYongHooKi_S 
***********************************************************************************************/ 
CREATE PROC [dbo].[SPM_Web_COMMON_Tbl_BokYongHooKi_S]
	@UserID		nvarchar(100)
	,@VisitDate varchar(8)	--txt_Visit_Date
	,@CureDate	varchar(8)	--txt_NoPain_Date
	,@JungSang	varchar(max)	--txt_JngSang
	,@tempC		varchar(4)		--txt_tempC
	,@Feber		varchar(1)		-- rdoFeber G Y R
	,@HaeYeolJe varchar(1)		-- HaeYeolJeOX
	,@ChouBang	varchar(max)	-- txt_ChouBang
	,@Yak_iLbun varchar(3)		-- txt_Yak_iLbun
	,@HangSaengJeBokan varchar(1)	-- rdoHangSaengJeBokan C,S
	,@HangSaengJeEat varchar(1)		-- rdoHangSaengJeEat A,B,C
	,@ChamGoSaHang varchar(max)	-- txt_ChamGoSaHang
	,@BokYongHooKi varchar(max)	-- txt_BokYongHooKi
	,@regip varchar(20)
	,@idx int = 0
AS
BEGIN TRAN 
	
	IF @idx>0
	BEGIN
		UPDATE Tbl_BokYongHooKi set 
			VisitDate = @VisitDate,
			CureDate = @CureDate,
			JungSang = @JungSang,
			tempC = @tempC,
			Feber = @Feber,
			HaeYeolJe = @HaeYeolJe,
			ChouBang = @ChouBang,
			Yak_iLbun = @Yak_iLbun,
			HangSaengJeBokan = @HangSaengJeBokan,
			HangSaengJeEat = @HangSaengJeEat,
			ChamGoSaHang = @ChamGoSaHang,
			BokYongHooKi = @BokYongHooKi,
			moddate = getdate(),
			modid = @UserID,
			modip = @regip
		WHERE UserID = @UserID AND idx=@idx
	END
	ELSE
	BEGIN
		INSERT INTO Tbl_BokYongHooKi
		(
			UserID ,VisitDate ,CureDate ,JungSang ,tempC
			,Feber ,HaeYeolJe ,ChouBang ,Yak_iLbun ,HangSaengJeBokan
			,HangSaengJeEat ,ChamGoSaHang ,BokYongHooKi ,regip 
		)
		SELECT 
			@UserID ,@VisitDate ,@CureDate ,@JungSang ,@tempC
			,@Feber ,@HaeYeolJe ,@ChouBang ,@Yak_iLbun ,@HangSaengJeBokan
			,@HangSaengJeEat ,@ChamGoSaHang ,@BokYongHooKi, @regip
	END

IF(@@ERROR<>0)
BEGIN
	ROLLBACK TRAN
	SELECT '' AS RESULT
END
ELSE
BEGIN
	COMMIT TRAN
	SELECT 'OK' AS RESULT
END

GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_Code_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 공통코드 SELECTBOX 용(조회)
*          인수		:
*          Output    :
* exec SPM_Web_COMMON_Tbl_Code_R @CodeGroup=''
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2016-12-29             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_Code_R]
(
	@CodeGroup varchar(50) =''
)
AS
BEGIN

	SELECT CodeGroup, Code, CodeName, images, regdate, regid, regip, moddate, modid, modip
	FROM Tbl_Code WITH(NOLOCK)
	WHERE CodeGroup like '%'+isnull(@CodeGroup ,'')+'%'
	Order by Code

END
GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_Drug_Info_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************
개요        : 약정보 조회
생성자      : 박희수
생성일      : 2017-01-18
로직        : 
최종수정자  : 
최종수정일  :  
exec SPM_Web_COMMON_Tbl_Drug_Info_R @idx ='34200', @drugCode='A11AKP08C0025',@drugName='가스핀정',@drugDanPoomYN='Y',@drugSungBun='Mosapride Citrate Hydrate 5mg'
,@drugCompany='위더스제약',@drugBunRu='239',@drugToYeo='경구(내용고형)',@drugJeHyong='정제',@drugGubun='전문',@drugInsure='103원/1정',@drugImage='',@regip='127.0.0.1'
,@userId='his001_ccp'
***********************************************************************************************/ 
CREATE PROC [dbo].[SPM_Web_COMMON_Tbl_Drug_Info_R]
	@idx			VARCHAR(10) = ''
	,@drugCode		VARCHAR(13) = ''
	,@drugName		NVARCHAR(100) = ''
	,@drugDanPoomYN VARCHAR(1) = ''
	,@drugSungBun	NVARCHAR(200) = ''
	,@drugCompany	NVARCHAR(100) = ''
	,@drugBunRu		VARCHAR(6) = ''
	,@drugToYeo		NVARCHAR(1000) = ''
	,@drugJeHyong	NVARCHAR(10) = ''
	,@drugGubun		NVARCHAR(10) = ''
	,@drugInsure	NVARCHAR(100) = ''
AS
BEGIN

	SELECT idx
		,drugName,drugCode,drugDanPoomYN,drugSungBun,drugCompany
		,drugBunRu,drugToYeo,drugJeHyong,drugGubun,drugInsure
		,drugImage,regdate,regid,regip,moddate
		,modid,modip
	FROM Tbl_Drug_Info WITH(NOLOCK)
	WHERE idx like '%'+ isnull(@idx,'') +'%'
		AND drugCode like '%'+ isnull(@drugCode,'') +'%'
		AND drugName like '%'+ isnull(@drugName,'') +'%'
		AND drugDanPoomYN like '%'+ isnull(@drugDanPoomYN,'') +'%'
		AND drugSungBun like '%'+ isnull(@drugSungBun,'') +'%'
		AND drugCompany like '%'+ isnull(@drugCompany,'') +'%'
		AND drugBunRu like '%'+ isnull(@drugBunRu,'') +'%'
		AND drugToYeo like '%'+ isnull(@drugToYeo,'') +'%'
		AND drugGubun like '%'+ isnull(@drugGubun,'') +'%'
		AND drugJeHyong like '%'+ isnull(@drugJeHyong,'') +'%'
		AND drugInsure like '%'+ isnull(@drugInsure,'') +'%'
END


GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_Drug_Info_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************
개요        : 약정보 저장
생성자      : 박희수
생성일      : 2017-01-18
로직        : 
최종수정자  : 
최종수정일  :  
exec SPM_Web_COMMON_Tbl_Drug_Info_S @idx ='34200', @drugCode='A11AKP08C0025',@drugName='가스핀정',@drugDanPoomYN='Y',@drugSungBun='Mosapride Citrate Hydrate 5mg'
,@drugCompany='위더스제약',@drugBunRu='239',@drugToYeo='경구(내용고형)',@drugJeHyong='정제',@drugGubun='전문',@drugInsure='103원/1정',@drugImage='',@regip='127.0.0.1'
,@userId='his001_ccp'
***********************************************************************************************/ 
CREATE PROC [dbo].[SPM_Web_COMMON_Tbl_Drug_Info_S]
	@idx			BIGINT
	,@drugCode		VARCHAR(13)
	,@drugName		NVARCHAR(100)
	,@drugDanPoomYN CHAR(1)
	,@drugSungBun	NVARCHAR(200)
	,@drugCompany	NVARCHAR(100)
	,@drugBunRu		VARCHAR(6)
	,@drugToYeo		NVARCHAR(1000)
	,@drugJeHyong	NVARCHAR(10)
	,@drugGubun		NVARCHAR(10)
	,@drugInsure	NVARCHAR(100)
	,@drugImage		NVARCHAR(150)
	,@regip			VARCHAR(20)
	,@userId		VARCHAR(100)
AS
BEGIN

	BEGIN TRAN

	DECLARE @Cnt int
	SELECT @Cnt = COUNT(*) FROM Tbl_Drug_Info WITH(NOLOCK) WHERE idx = @idx

	IF @Cnt=0
	BEGIN
		INSERT INTO Tbl_Drug_Info (
			idx, drugCode, drugName ,drugDanPoomYN ,drugSungBun ,drugCompany
			,drugBunRu ,drugToYeo ,drugJeHyong ,drugGubun ,drugInsure
			,drugImage ,regid ,regip
		)
		SELECT 
			@idx ,@drugCode, @drugName ,@drugDanPoomYN ,@drugSungBun ,@drugCompany
			,@drugBunRu ,@drugToYeo ,@drugJeHyong ,@drugGubun ,@drugInsure
			,@drugImage ,@userId ,@regip
	END

	--MERGE Tbl_Drug_Info AS tgt
	--USING (SELECT TOP 1 group_cd, menu_cd, user_group FROM Tbl_Drug_Info WITH(NOLOCK) WHERE group_cd=@group_cd AND menu_cd=@menuCd AND user_group=@usergroup) AS src
	--ON (tgt.user_group=src.user_group and tgt.group_cd=src.group_cd and tgt.menu_cd = src.menu_cd)
	--WHEN MATCHED THEN UPDATE SET grant_v=@grant_v, grant_i=@grant_i, grant_s=@grant_s, grant_r=@grant_r, grant_a=@grant_a, update_user=@userId, update_date = getdate()
	--WHEN NOT MATCHED THEN
	--	INSERT ( user_group, group_cd, menu_cd, grant_v, grant_i, grant_s, grant_r, grant_a, create_user, create_date) 
	--	VALUES (@usergroup,@group_cd,@menuCd,@grant_v,@grant_i,@grant_s,@grant_r,@grant_a,@userId,GETDATE()) ;


	IF(@@ERROR<>0)
		BEGIN
			ROLLBACK TRAN
			SELECT '' AS RESULT
		END
	ELSE
		BEGIN
			COMMIT TRAN
			SELECT 'OK' AS RESULT
		END
END

GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_DrugOrg_Info_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************
개요        : 약정보 조회
생성자      : 박희수
생성일      : 2017-01-18
로직        : 
최종수정자  : 
최종수정일  :  
exec SPM_Web_COMMON_Tbl_DrugOrg_Info_R @idx ='34200', @drugCode='A11AKP08C0025',@drugName='가스핀정',@drugDanPoomYN='Y',@drugSungBun='Mosapride Citrate Hydrate 5mg'
,@drugCompany='위더스제약',@drugBunRu='239',@drugToYeo='경구(내용고형)',@drugJeHyong='정제',@drugGubun='전문',@drugInsure='103원/1정',@drugImage='',@regip='127.0.0.1'
,@userId='his001_ccp'
***********************************************************************************************/ 
CREATE PROC [dbo].[SPM_Web_COMMON_Tbl_DrugOrg_Info_R]
	@PageNo	INT
	,@RowCount	INT
	,@ITEM_SEQ nvarchar(10)					=''--품목일련번호
	,@ITEM_NAME nvarchar(100)				=''--품목명
	,@ENTP_SEQ nvarchar(10)					=''--업체일련번호
	,@ENTP_NAME nvarchar(100)				=''--업체명
	,@CHART nvarchar(100)					=''--성상
	,@CLASS_NO nvarchar(100)				=''--분류번호
	,@CLASS_NAME nvarchar(100)				=''--분류명
	,@ETC_OTC_NAME nvarchar(100)			=''--전문/일반
	,@ITEM_PERMIT_DATE nvarchar(100)		=''--품목허가일자
	,@FORM_CODE_NAME nvarchar(100)			=''--제형코드이름
	,@MARK_CODE_FRONT_ANAL nvarchar(100)	=''--마크내용(앞)
	,@MARK_CODE_BACK_ANAL nvarchar(100)		=''--마크내용(뒤)
	,@MARK_CODE_FRONT_IMG nvarchar(100)		=''--마크이미지(앞)
	,@MARK_CODE_BACK_IMG nvarchar(100)		=''--마크이미지(뒤)
AS
BEGIN

	SELECT * FROM 
	(
		SELECT 
			FLOOR((ROW_NUMBER() OVER (ORDER BY ITEM_NAME) - 1) / @RowCount + 1) PAGE
			, COUNT(*) OVER() AS TOTAL_COUNT
			,ITEM_SEQ ,ITEM_NAME ,ENTP_SEQ ,ENTP_NAME ,CHART
			,ITEM_IMAGE ,PRINT_FRONT ,PRINT_BACK ,DRUG_SHAPE ,COLOR_CLASS1
			,COLOR_CLASS2 ,LINE_FRONT ,LINE_BACK ,LENG_LONG ,LENG_SHORT
			,THICK ,IMG_REGIST_TS ,CLASS_NO ,CLASS_NAME ,ETC_OTC_NAME
			,ITEM_PERMIT_DATE ,FORM_CODE_NAME ,MARK_CODE_FRONT_ANAL ,MARK_CODE_BACK_ANAL ,MARK_CODE_FRONT_IMG
			,MARK_CODE_BACK_IMG ,regdate ,regid ,regip ,moddate
			,modid ,modip
		FROM Tbl_DrugOrg_Info WITH(NOLOCK)
		WHERE ITEM_SEQ like '%'+ isnull(@ITEM_SEQ,'') +'%'
			AND ITEM_NAME like '%'+ isnull(@ITEM_NAME,'') +'%'
			AND ENTP_SEQ like '%'+ isnull(@ENTP_SEQ,'') +'%'
			AND ENTP_NAME like '%'+ isnull(@ENTP_NAME,'') +'%'
			AND CHART like '%'+ isnull(@CHART,'') +'%'
			AND CLASS_NO like '%'+ isnull(@CLASS_NO,'') +'%'
			AND CLASS_NAME like '%'+ isnull(@CLASS_NAME,'') +'%'
	) T
	WHERE T.PAGE=@PageNo


END


GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_DrugOrg_Info_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************
개요        : 공공DB 의약정보 저장
생성자      : 박희수
생성일      : 2017-01-19
로직        : 
최종수정자  : 
최종수정일  :  
exec SPM_Web_COMMON_Tbl_member_S @ITEM_SEQ=N'8760',@drugCode=N'A11A0030A0285',@drugName=N'신풍아스피린리신주',@drugDanPoomYN=N'Y'
,@drugSungBun=N'Aspirin Lysine 900mg',@drugCompany=N'신풍제약',@drugBunRu=N'114',@drugToYeo=N'주사'
,@drugJeHyong=N'주사제',@drugGubun=N'전문',@drugInsure=N'300원/1병',@drugImage=N'/drug_info/pop_sb.asp?sbcode=A11A0030A028501',@userId=N'system',@regip=N'10.199.21.67'
***********************************************************************************************/ 
CREATE PROC [dbo].[SPM_Web_COMMON_Tbl_DrugOrg_Info_S]
	@ITEM_SEQ bigint
	,@ITEM_NAME nvarchar(100)
	,@ENTP_SEQ bigint
	,@ENTP_NAME nvarchar(100)
	,@CHART nvarchar(100)
	,@ITEM_IMAGE nvarchar(300)
	,@PRINT_FRONT nvarchar(100)
	,@PRINT_BACK nvarchar(100)
	,@DRUG_SHAPE nvarchar(100)
	,@COLOR_CLASS1 nvarchar(100)
	,@COLOR_CLASS2 nvarchar(100)
	,@LINE_FRONT nvarchar(100)
	,@LINE_BACK nvarchar(100)
	,@LENG_LONG nvarchar(100)
	,@LENG_SHORT nvarchar(100)
	,@THICK nvarchar(100)
	,@IMG_REGIST_TS nvarchar(100)
	,@CLASS_NO nvarchar(100)
	,@CLASS_NAME nvarchar(100)
	,@ETC_OTC_NAME nvarchar(100)
	,@ITEM_PERMIT_DATE nvarchar(100)
	,@FORM_CODE_NAME nvarchar(100)
	,@MARK_CODE_FRONT_ANAL nvarchar(100)
	,@MARK_CODE_BACK_ANAL nvarchar(100)
	,@MARK_CODE_FRONT_IMG nvarchar(100)
	,@MARK_CODE_BACK_IMG nvarchar(100)
	,@regip			VARCHAR(20)
	,@userId		VARCHAR(100)
AS
BEGIN

	BEGIN TRAN

	DECLARE @Cnt int
	SELECT @Cnt = COUNT(*) FROM Tbl_DrugOrg_Info WITH(NOLOCK) WHERE ITEM_SEQ = @ITEM_SEQ

	IF @Cnt=0
	BEGIN
		INSERT INTO dbo.Tbl_DrugOrg_Info
		(
			ITEM_SEQ ,ITEM_NAME ,ENTP_SEQ ,ENTP_NAME ,CHART
			,ITEM_IMAGE ,PRINT_FRONT ,PRINT_BACK ,DRUG_SHAPE ,COLOR_CLASS1
			,COLOR_CLASS2 ,LINE_FRONT ,LINE_BACK ,LENG_LONG ,LENG_SHORT
			,THICK ,IMG_REGIST_TS ,CLASS_NO ,CLASS_NAME ,ETC_OTC_NAME
			,ITEM_PERMIT_DATE ,FORM_CODE_NAME ,MARK_CODE_FRONT_ANAL ,MARK_CODE_BACK_ANAL ,MARK_CODE_FRONT_IMG
			,MARK_CODE_BACK_IMG,regid ,regip
		)
		VALUES
		(
			@ITEM_SEQ , @ITEM_NAME , @ENTP_SEQ , @ENTP_NAME , @CHART
			, @ITEM_IMAGE , @PRINT_FRONT , @PRINT_BACK , @DRUG_SHAPE , @COLOR_CLASS1
			, @COLOR_CLASS2 , @LINE_FRONT , @LINE_BACK , @LENG_LONG , @LENG_SHORT
			, @THICK , @IMG_REGIST_TS , @CLASS_NO , @CLASS_NAME , @ETC_OTC_NAME
			, @ITEM_PERMIT_DATE , @FORM_CODE_NAME , @MARK_CODE_FRONT_ANAL , @MARK_CODE_BACK_ANAL , @MARK_CODE_FRONT_IMG
			, @MARK_CODE_BACK_IMG,@userId ,@regip
		)

	END

	IF(@@ERROR<>0)
		BEGIN
			ROLLBACK TRAN
			SELECT '' AS RESULT
		END
	ELSE
		BEGIN
			COMMIT TRAN
			SELECT 'OK' AS RESULT
		END
END

GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_member_Pwd_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 회원가입 저장
*          인수		:
*          Output    :
* exec SPM_Web_COMMON_Tbl_member_Pwd_S @UserID='his001',@UserPwd='545434',@UserName='test1234'
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2017-01-12             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_member_Pwd_S]
(
	@UserID nvarchar(100)
	,@UserPwd varchar(50)
	,@UserPwdNew nvarchar(50)
)
AS
BEGIN TRAN
DECLARE @DBPwd VARCHAR(50)
, @msg VARCHAR(50) = ''

SELECT @DBPwd = UserPwd FROM Tbl_member WITH(NOLOCK) WHERE UserID =@UserID 

IF (@DBPwd = @UserPwd)
BEGIN
	UPDATE Tbl_member set UserPwd = @UserPwdNew WHERE UserID = @UserID
END
ELSE
BEGIN
	SET @msg = 'pwdDiff'
END


IF(@@ERROR<>0)
	BEGIN
		ROLLBACK TRAN
		SELECT @msg AS RESULT
	END
ELSE
	BEGIN
		COMMIT TRAN
		SELECT 'OK' AS RESULT
	END

GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_member_R]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 로그인 조회
*          인수		:
*          Output    :
* exec SPM_Web_COMMON_Tbl_member_R @UserID='his001'
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2016-12-29             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_member_R]
(
	@UserID nvarchar(100)
	--,@UserPwd varchar(50)
)
AS
BEGIN

	SELECT A.idx ,A.UserID ,A.UserPwd ,A.UserName ,A.UserGroup , B.CodeName AS UserGroupName
		,A.UserHP ,A.UserRegIP ,A.regdate ,A.regid ,A.regip
		,A.moddate ,A.modid ,A.modip
	FROM Tbl_member A WITH(NOLOCK)
	inner join Tbl_Code B WITH(NOLOCK) on B.CodeGroup='UserGroup' AND A.UserGroup = B.code
	WHERE UserID = @UserID

END
GO
/****** Object:  StoredProcedure [dbo].[SPM_Web_COMMON_Tbl_member_S]    Script Date: 2017-01-23 오후 12:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 회원가입 저장
*          인수		:
*          Output    :
* exec SPM_Web_COMMON_Tbl_member_S @UserID='his001',@UserPwd='545434',@UserName='박희수'
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2017-01-02             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_member_S]
(
	@UserID nvarchar(100)
	,@UserPwd varchar(50)
	,@UserName nvarchar(50)
)
AS
BEGIN TRAN

	INSERT INTO Tbl_member (UserID ,UserPwd ,UserName)
	SELECT @UserID, @UserPwd, @UserName

IF(@@ERROR<>0)
	BEGIN
		ROLLBACK TRAN
		SELECT '' AS RESULT
	END
ELSE
	BEGIN
		COMMIT TRAN
		SELECT 'OK' AS RESULT
	END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'G 정상 Y 미열 R 고열' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tbl_BokYongHooKi', @level2type=N'COLUMN',@level2name=N'Feber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'해열제 사용 O ,X' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tbl_BokYongHooKi', @level2type=N'COLUMN',@level2name=N'HaeYeolJe'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'처방 일분' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tbl_BokYongHooKi', @level2type=N'COLUMN',@level2name=N'Yak_iLbun'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'C 냉장 , S 실온' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tbl_BokYongHooKi', @level2type=N'COLUMN',@level2name=N'HangSaengJeBokan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A 식전 B 식간 C 식후' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tbl_BokYongHooKi', @level2type=N'COLUMN',@level2name=N'HangSaengJeEat'
GO
