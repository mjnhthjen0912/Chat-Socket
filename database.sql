USE [ServerChatApp]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 08/09/2017 19:55:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[ID] [varchar](20) NOT NULL,
	[PASSWORD] [varchar](50) NULL,
	[NAME] [nvarchar](50) NULL,
	[IMAGE] [varchar](50) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[admin4_akiyoshi]    Script Date: 08/09/2017 19:55:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[admin4_akiyoshi](
	[Stt] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[IdChat] [varchar](20) NULL,
	[LogTime] [varchar](20) NULL,
	[TypeMess] [smallint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[akiyoshi_admin1]    Script Date: 08/09/2017 19:55:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[akiyoshi_admin1](
	[Stt] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[IdChat] [varchar](20) NULL,
	[LogTime] [varchar](20) NULL,
	[TypeMess] [smallint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[akiyoshi_minhthien0912]    Script Date: 08/09/2017 19:55:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[akiyoshi_minhthien0912](
	[Stt] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[IdChat] [varchar](20) NULL,
	[LogTime] [varchar](20) NULL,
	[TypeMess] [smallint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FRIENDS]    Script Date: 08/09/2017 19:55:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FRIENDS](
	[ID1] [varchar](20) NOT NULL,
	[ID2] [varchar](20) NOT NULL,
	[LASTMESS] [nvarchar](max) NULL,
	[TYPEMESS] [smallint] NULL,
	[STATUS] [smallint] NULL,
 CONSTRAINT [PK_FRIENDS] PRIMARY KEY CLUSTERED 
(
	[ID1] ASC,
	[ID2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'account001', N'123456', N'account001', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'account01', N'123456', N'account01', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'account1', N'7610226', N'account1', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin1', N'7610226', N'admin1', N'60838637_p11_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin10', N'7610226', N'admin10', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin11', N'7610226', N'admin11', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin12', N'7610226', N'admin12', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin13', N'7610226', N'admin13', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin14', N'7610226', N'admin14', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin15', N'7610226', N'admin15', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin16', N'7610226', N'admin16', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin17', N'7610226', N'admin17', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin18', N'7610226', N'admin18', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin19', N'7610226', N'admin19', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin2', N'7610226', N'admin2', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin20', N'7610226', N'admin20', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin21', N'7610226', N'admin21', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin3', N'7610226', N'admin3', N'60838637_p11_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin4', N'7610226', N'admin4', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin5', N'7610226', N'admin5', N'60838637_p11_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin6', N'7610226', N'admin6', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin7', N'7610226', N'admin7', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin8', N'7610226', N'admin8', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'admin9', N'7610226', N'admin9', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'administrator', N'123456', N'administrator', N'60838637_p8_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'akiyoshi', N'7610226', N'Aki001', N'60838637_p11_master1200.jpg')
INSERT [dbo].[Account] ([ID], [PASSWORD], [NAME], [IMAGE]) VALUES (N'minhthien0912', N'7610226', N'Minh Thien', N'60838637_p8_master1200.jpg')
SET IDENTITY_INSERT [dbo].[akiyoshi_admin1] ON 

INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (1, N'alo', N'akiyoshi', N'18/08/17 7:33', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (2, N'ola', N'admin1', N'18/08/17 7:33', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (3, N'alo', N'admin1', N'18/08/17 7:33', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (4, N'moshi', N'admin1', N'18/08/17 7:33', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (5, N'moshi', N'admin1', N'18/08/17 7:33', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (6, N'1234', N'admin1', N'18/08/17 7:33', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (8, N'loploloolo', N'akiyoshi', N'18/08/17 7:33', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (10, N'hihihi', N'admin1', N'18/08/17 7:34', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (14, N'dfsdf', N'akiyoshi', N'02/09/17 2:48', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (7, N'alalal', N'admin1', N'18/08/17 7:33', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (9, N'das', N'akiyoshi', N'18/08/17 7:33', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (11, N'mamama', N'admin1', N'18/08/17 7:34', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (13, N'mimimimi', N'admin1', N'18/08/17 7:34', 0)
INSERT [dbo].[akiyoshi_admin1] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (12, N'momomom', N'admin1', N'18/08/17 7:34', 0)
SET IDENTITY_INSERT [dbo].[akiyoshi_admin1] OFF
SET IDENTITY_INSERT [dbo].[akiyoshi_minhthien0912] ON 

INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (1, N'alo', N'minhthien0912', N'18/08/17 4:26', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (2, N'ola', N'akiyoshi', N'18/08/17 4:26', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (3, N'moshi moshi', N'minhthien0912', N'18/08/17 4:26', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (97, N'alo', N'akiyoshi', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (98, N'1', N'akiyoshi', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (99, N'2', N'akiyoshi', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (100, N'3', N'akiyoshi', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (103, N'f', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (105, N'sd', N'akiyoshi', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (107, N'b', N'akiyoshi', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (110, N'poi', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (112, N'123', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (114, N'789', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (116, N'epoi', N'akiyoshi', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (117, N'dkla', N'akiyoshi', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (118, N'czxczxc', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (121, N'5454', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (124, N'ola', N'minhthien0912', N'18/08/17 6:53', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (126, N'moshi moshi', N'minhthien0912', N'18/08/17 7:23', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (129, N'la', N'minhthien0912', N'18/08/17 7:23', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (131, N'ola', N'akiyoshi', N'18/08/17 7:25', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (132, N'ola', N'akiyoshi', N'18/08/17 7:26', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (134, N'ola', N'akiyoshi', N'18/08/17 7:26', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (137, N'ila', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (139, N'ola', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (140, N'moshi', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (142, N'2', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (144, N'4', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (146, N'6', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (149, N'9', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (150, N'ahihi', N'akiyoshi', N'18/08/17 7:37', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (153, N'wthhhhhhhhhhhhhhhh', N'minhthien0912', N'18/08/17 7:37', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (154, N'wtffffffffffffffffffffff', N'minhthien0912', N'18/08/17 7:37', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (156, N'-_______________________________-', N'akiyoshi', N'18/08/17 7:37', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (158, N'....', N'akiyoshi', N'18/08/17 7:38', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (159, N'alo', N'akiyoshi', N'22/08/17 18:02', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (161, N'loel', N'minhthien0912', N'22/08/17 18:03', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (162, N'alala', N'minhthien0912', N'01/09/17 9:18', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (164, N'123', N'akiyoshi', N'01/09/17 10:32', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (165, N'gfdgdfg', N'akiyoshi', N'01/09/17 10:35', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (166, N'alo', N'akiyoshi', N'02/09/17 2:08', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (168, N'123', N'minhthien0912', N'02/09/17 2:09', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (169, N'alo', N'akiyoshi', N'02/09/17 2:11', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (172, N'456', N'minhthien0912', N'02/09/17 2:12', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (175, N'alo', N'minhthien0912', N'02/09/17 2:42', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (177, N'erw', N'minhthien0912', N'02/09/17 2:42', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (179, N'alo', N'minhthien0912', N'08/09/17 1:55', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (180, N'asdasd', N'akiyoshi', N'08/09/17 1:55', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (182, N'ddf', N'minhthien0912', N'08/09/17 1:58', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (183, N'123', N'minhthien0912', N'08/09/17 1:58', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (184, N'123', N'akiyoshi', N'08/09/17 1:59', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (186, N'asd', N'minhthien0912', N'08/09/17 2:00', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (187, N'444', N'akiyoshi', N'08/09/17 2:02', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (192, N'asd', N'minhthien0912', N'08/09/17 2:11', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (194, N'zxc', N'akiyoshi', N'08/09/17 2:12', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (195, N'dsf', N'minhthien0912', N'08/09/17 4:09', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (196, N'sdf', N'akiyoshi', N'08/09/17 15:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (197, N'ads', N'akiyoshi', N'08/09/17 15:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (198, N'alo', N'akiyoshi', N'08/09/17 16:53', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (200, N'qwe', N'akiyoshi', N'08/09/17 16:53', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (202, N'alo', N'minhthien0912', N'08/09/17 16:53', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (203, N'1234', N'akiyoshi', N'08/09/17 16:53', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (204, N'chattest', N'minhthien0912', N'08/09/17 16:53', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (206, N'5678', N'akiyoshi', N'08/09/17 16:54', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (207, N'hello', N'akiyoshi', N'08/09/17 16:54', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (101, N'a', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (102, N'd', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (104, N'z', N'akiyoshi', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (106, N'f', N'akiyoshi', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (108, N'w', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (111, N'lol', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (113, N'456', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (115, N'wpo', N'akiyoshi', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (119, N'asda', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (120, N'1212', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (122, N'alo', N'minhthien0912', N'18/08/17 6:51', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (123, N'ola', N'akiyoshi', N'18/08/17 6:53', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (125, N'alo', N'minhthien0912', N'18/08/17 6:53', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (127, N'alo', N'akiyoshi', N'18/08/17 7:23', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (128, N'ola', N'minhthien0912', N'18/08/17 7:23', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (130, N'ola', N'akiyoshi', N'18/08/17 7:23', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (133, N'ola', N'akiyoshi', N'18/08/17 7:26', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (135, N'alo', N'akiyoshi', N'18/08/17 7:26', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (136, N'ali', N'akiyoshi', N'18/08/17 7:27', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (138, N'zxc', N'minhthien0912', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (141, N'1', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (143, N'32', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (145, N'5', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (147, N'7', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (148, N'8', N'akiyoshi', N'18/08/17 7:28', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (151, N'có gì hem', N'akiyoshi', N'18/08/17 7:37', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (152, N'...', N'minhthien0912', N'18/08/17 7:37', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (155, N'lololololololo', N'minhthien0912', N'18/08/17 7:37', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (157, N'wthhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh', N'akiyoshi', N'18/08/17 7:38', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (109, N'er', N'minhthien0912', N'18/08/17 6:45', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (160, N'lzlzlz', N'minhthien0912', N'22/08/17 18:03', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (163, N'ola', N'akiyoshi', N'01/09/17 9:39', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (167, N'ola', N'minhthien0912', N'02/09/17 2:09', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (170, N'alo', N'minhthien0912', N'02/09/17 2:11', 0)
GO
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (174, N'123', N'akiyoshi', N'02/09/17 2:33', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (181, N'sddf', N'minhthien0912', N'08/09/17 1:56', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (188, N'455', N'minhthien0912', N'08/09/17 2:02', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (190, N'asd', N'akiyoshi', N'08/09/17 2:09', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (193, N'asd', N'minhthien0912', N'08/09/17 2:12', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (171, N'123', N'akiyoshi', N'02/09/17 2:12', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (173, N'456', N'akiyoshi', N'02/09/17 2:12', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (176, N'asd', N'akiyoshi', N'02/09/17 2:42', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (178, N'cxvx', N'minhthien0912', N'02/09/17 2:42', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (199, N'zxc', N'akiyoshi', N'08/09/17 16:53', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (201, N'fsd', N'akiyoshi', N'08/09/17 16:53', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (205, N'chatchat', N'minhthien0912', N'08/09/17 16:53', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (185, N'asda', N'minhthien0912', N'08/09/17 2:00', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (189, N'asd', N'akiyoshi', N'08/09/17 2:08', 0)
INSERT [dbo].[akiyoshi_minhthien0912] ([Stt], [Content], [IdChat], [LogTime], [TypeMess]) VALUES (191, N'sdf', N'akiyoshi', N'08/09/17 2:10', 0)
SET IDENTITY_INSERT [dbo].[akiyoshi_minhthien0912] OFF
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'admin1', N'minhthien0912', N'', 0, 1)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'admin13', N'admin1', N'', 0, 1)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'admin13', N'admin10', N'', 0, 1)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'admin13', N'admin11', N'', 0, 1)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'admin13', N'admin12', N'', 0, 1)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'admin13', N'admin14', N'', 0, 1)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'admin4', N'akiyoshi', N'', 0, 2)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'akiyoshi', N'admin1', N'', 0, 2)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'akiyoshi', N'admin12', N'', 0, 1)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'akiyoshi', N'admin2', N'', 0, 1)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'akiyoshi', N'admin3', N'', 0, 1)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'akiyoshi', N'admin5', N'', 0, 1)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'akiyoshi', N'admin6', N'', 0, 1)
INSERT [dbo].[FRIENDS] ([ID1], [ID2], [LASTMESS], [TYPEMESS], [STATUS]) VALUES (N'akiyoshi', N'minhthien0912', N'', 0, 2)
