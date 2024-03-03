DROP TABLE DepositHistory;
DROP TABLE OrderDetail;
DROP TABLE MenuItem;
DROP TABLE Orders;
DROP TABLE MEMBER;
DROP TABLE FAMILY;


USE [MCOS]
GO

/****** Object:  Table [dbo].[Family]    Script Date: 8/31/2015 8:17:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Family](
	[FAMILY_ID] [int] NOT NULL,
	[PUBLISH] [char](1) NULL,
	[NAME] [nvarchar](80) NULL,
	[ADDRESS] [nvarchar](80) NULL,
	[CITY] [nvarchar](80) NULL,
	[STATE] [nvarchar](2) NULL,
	[ZIP] [nvarchar](10) NULL,
	[HOME_PHONE] [varchar](50) NULL,
	[EMAIL] [varchar](80) NULL,
	[FamilyPicture] [image] NULL,
	[PUBLISH_DATE] [datetime] NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_DATE] [datetime] NULL,
 CONSTRAINT [PK_Family] PRIMARY KEY CLUSTERED 
(
	[FAMILY_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [MCOS]
GO

/****** Object:  Table [dbo].[Member]    Script Date: 8/31/2015 8:30:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Member](
	[MEMBER_ID] [int] NOT NULL,
	[MemberCode] [nvarchar](10) NOT NULL,
	[FAMILY_ID] [int] NULL,
	[USERID] [int] NULL,
	[CHINESE_NAME] [nvarchar](20) NULL,
	[TITLE] [nvarchar](20) NULL,
	[LAST_NAME] [nvarchar](80) NULL,
	[FIRST_NAME] [nvarchar](80) NULL,
	[MIDDLE_NAME] [nvarchar](20) NULL,
	[LEGAL_NAME] [nvarchar](80) NULL,
	[MAIDEN_NAME] [nvarchar](80) NULL,
	[DISPLAY_NAME] [nvarchar](80) NULL,
	[BIRTH_DATE] [varchar](10) NULL,
	[RELATION] [char](1) NULL,
	[GENDER] [char](1) NULL,
	[MEMBERSHIP] [char](1) NULL,
	[CONGREGATION] [char](2) NULL,
	[CELL_PHONE] [varchar](50) NULL,
	[WORK_PHONE] [varchar](50) NULL,
	[EMAIL] [varchar](80) NULL,
	[ALIAS_EMAIL] [varchar](80) NULL,
	[FORWARD_ADDRESS] [varchar](80) NULL,
	[PUBLISH] [char](1) NULL,
	[LAST_VOTE_DATE] [datetime] NULL,
	[BAPTIZED_DATE] [datetime] NULL,
	[TRANSFER_DATE] [datetime] NULL,
	[BULLETIN] [char](1) NULL,
	[ROCKNEWS] [char](1) NULL,
	[CROSSTALK] [char](1) NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_DATE] [datetime] NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[MEMBER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_Family] FOREIGN KEY([FAMILY_ID])
REFERENCES [dbo].[Family] ([FAMILY_ID])
GO

ALTER TABLE [dbo].[Member] CHECK CONSTRAINT [FK_Member_Family]
GO


USE [MCOS]
GO

/****** Object:  Table [dbo].[Orders]    Script Date: 8/31/2015 8:30:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[Member_ID] [int] NULL,
	[OrderDate] [date] NULL,
	[OrderAmount] [decimal](8, 2) NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_DATE] [datetime] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Member] FOREIGN KEY([Member_ID])
REFERENCES [dbo].[Member] ([MEMBER_ID])
GO

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Member]
GO


USE [MCOS]
GO

/****** Object:  Table [dbo].[MenuItem]    Script Date: 8/31/2015 8:23:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MenuItem](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[ItemStartDate] [date] NULL,
	[ItemEndDate] [date] NULL,
	[ItemPrice] [decimal](8, 2) NULL,
	[Itemcode] [nvarchar](50) NULL,
	[ItemDescription] [nvarchar](50) NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_DATE] [datetime] NULL,
 CONSTRAINT [PK_MenuItem] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO





USE [MCOS]
GO

/****** Object:  Table [dbo].[OrderDetail]    Script Date: 8/31/2015 8:32:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ItemID] [int] NOT NULL,
	[ItemQuanity] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_DATE] [datetime] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_MenuItem] FOREIGN KEY([ItemID])
REFERENCES [dbo].[MenuItem] ([ItemID])
GO

ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_MenuItem]
GO

ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO

ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Orders]
GO








USE [MCOS]
GO

/****** Object:  Table [dbo].[DepositHistory]    Script Date: 8/31/2015 8:21:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DepositHistory](
	[DepositID] [int] IDENTITY(1,1) NOT NULL,
	[DepositDate] [date] NULL,
	[DepositAmount] [decimal](8, 2) NULL,
	[DepositType] [nvarchar](10) NULL,
	[Balance] [decimal](8, 2) NULL,
	[Family_ID] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_DATE] [datetime] NULL,
 CONSTRAINT [PK_DepositHistory] PRIMARY KEY CLUSTERED 
(
	[DepositID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DepositHistory]  WITH CHECK ADD  CONSTRAINT [FK_DepositHistory_Family] FOREIGN KEY([Family_ID])
REFERENCES [dbo].[Family] ([FAMILY_ID])
GO

ALTER TABLE [dbo].[DepositHistory] CHECK CONSTRAINT [FK_DepositHistory_Family]
GO


