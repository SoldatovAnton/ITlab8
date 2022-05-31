
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SID] [int] IDENTITY(1,1) NOT NULL,
	[SName] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Address1] [varchar](50) NOT NULL,
	[Address2] [varchar](50) NULL,
	[Suburb] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Postcode] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON
INSERT [dbo].[Suppliers] ([SID], [SName], [Phone], [Email], [Address1], [Address2], [Suburb], [State], [Postcode]) VALUES (2, N'LG', N'0425874123', N'LG@yahoo.com', N'50,St.Kilda road', NULL, N'SouthBank', N'VIC', N'3006')
INSERT [dbo].[Suppliers] ([SID], [SName], [Phone], [Email], [Address1], [Address2], [Suburb], [State], [Postcode]) VALUES (6, N'Breville', N'0425678915', N'Breville@yahoo.com', N'14', N'St.Kilda Road', N'Southbank', N'VIC', N'3006')
SET IDENTITY_INSERT [dbo].[Suppliers] OFF