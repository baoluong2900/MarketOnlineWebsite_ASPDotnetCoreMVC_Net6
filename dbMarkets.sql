USE [dbMarkets]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [varchar](12) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Salt] [nchar](10) NULL,
	[Active] [bit] NOT NULL,
	[Avatar] [nvarchar](255) NULL,
	[FullName] [nvarchar](150) NULL,
	[RoleID] [int] NULL,
	[LastLogin] [datetime] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Advertisements]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Advertisements](
	[AdvertisementsID] [int] IDENTITY(1,1) NOT NULL,
	[SubTitle] [nvarchar](150) NULL,
	[Title] [nvarchar](150) NULL,
	[ImageBG] [nvarchar](250) NULL,
	[ImageProduct] [nvarchar](250) NULL,
	[UrlLink] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_QuangCaos] PRIMARY KEY CLUSTERED 
(
	[AdvertisementsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attributes]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attributes](
	[AttributeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Attributes] PRIMARY KEY CLUSTERED 
(
	[AttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttributesPrices]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttributesPrices](
	[AttributesPriceID] [int] IDENTITY(1,1) NOT NULL,
	[AttributeID] [int] NULL,
	[ProductID] [int] NULL,
	[Price] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_AttributesPrices] PRIMARY KEY CLUSTERED 
(
	[AttributesPriceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CatID] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](250) NULL,
	[Description] [nvarchar](max) NULL,
	[ParentID] [int] NULL,
	[Levels] [int] NULL,
	[Ordering] [int] NULL,
	[Published] [bit] NOT NULL,
	[Thumb] [nvarchar](250) NULL,
	[Title] [nvarchar](250) NULL,
	[Alias] [nvarchar](250) NULL,
	[Cover] [nvarchar](255) NULL,
	[SchemaMarkup] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](255) NULL,
	[Birthday] [datetime] NULL,
	[Avatar] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Email] [nchar](150) NULL,
	[Phone] [varchar](12) NULL,
	[LocationID] [int] NULL,
	[District] [int] NULL,
	[Ward] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Password] [nvarchar](50) NULL,
	[Salt] [nchar](8) NULL,
	[LastLogin] [datetime] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[LocationID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Parent] [int] NULL,
	[Levels] [int] NULL,
	[Slug] [nvarchar](100) NULL,
	[NameWithType] [nvarchar](100) NULL,
	[Type] [nvarchar](10) NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[SContents] [nvarchar](255) NULL,
	[Contents] [nvarchar](max) NULL,
	[Thumb] [nvarchar](255) NULL,
	[Published] [bit] NOT NULL,
	[Alias] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[Author] [nvarchar](255) NULL,
	[AccountID] [int] NULL,
	[Tags] [nvarchar](max) NULL,
	[CatID] [int] NULL,
	[isHot] [bit] NOT NULL,
	[isNewfeed] [bit] NOT NULL,
	[Views] [int] NULL,
 CONSTRAINT [PK_tblTinTucs] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[OrderNumber] [int] NULL,
	[Amount] [int] NULL,
	[Discount] [int] NULL,
	[TotalMoney] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Price] [int] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[OrderDate] [datetime] NULL,
	[ShipDate] [datetime] NULL,
	[TransactStatusID] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Paid] [bit] NOT NULL,
	[PaymentDate] [datetime] NULL,
	[TotalMoney] [int] NOT NULL,
	[PaymentID] [int] NULL,
	[Note] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[LocationID] [int] NULL,
	[District] [int] NULL,
	[Ward] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[PageID] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [nvarchar](250) NULL,
	[Contents] [nvarchar](max) NULL,
	[Thumb] [nvarchar](250) NULL,
	[Published] [bit] NOT NULL,
	[Title] [nvarchar](250) NULL,
	[Alias] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[Ordering] [int] NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[PageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[ShortDesc] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[CatID] [int] NULL,
	[Price] [int] NULL,
	[Discount] [int] NULL,
	[Thumb] [nvarchar](255) NULL,
	[Video] [nvarchar](255) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[BestSellers] [bit] NOT NULL,
	[HomeFlag] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[Tags] [nvarchar](max) NULL,
	[Title] [nvarchar](255) NULL,
	[Alias] [nvarchar](255) NULL,
	[PushilberId] [nchar](10) NULL,
	[UnitsInStock] [int] NULL,
	[SupplierID] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shippers]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shippers](
	[ShipperID] [int] IDENTITY(1,1) NOT NULL,
	[ShipperName] [nvarchar](150) NULL,
	[Phone] [nchar](10) NULL,
	[Company] [nvarchar](150) NULL,
	[ShipDate] [datetime] NULL,
 CONSTRAINT [PK_Shippers] PRIMARY KEY CLUSTERED 
(
	[ShipperID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [int] NULL,
	[EmailContact] [nchar](150) NULL,
	[Companyname] [nvarchar](255) NULL,
	[Salt] [nchar](8) NULL,
	[ContactTitle] [nvarchar](255) NULL,
	[Addresss] [nvarchar](255) NULL,
	[PostalCode] [nchar](15) NULL,
	[Fax] [nchar](20) NULL,
	[PaymentMethods] [nvarchar](150) NULL,
	[LocationID] [int] NULL,
	[District] [int] NULL,
	[Ward] [int] NULL,
	[DiscountType] [nchar](10) NULL,
	[Notes] [nvarchar](max) NULL,
	[CurrentOrder] [nvarchar](50) NULL,
	[Logo] [nvarchar](255) NULL,
 CONSTRAINT [PK__Supplier__4BE66694AB596B6E] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactStatus]    Script Date: 5/16/2022 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactStatus](
	[TransactStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_TransactStatus] PRIMARY KEY CLUSTERED 
(
	[TransactStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([AccountID], [Phone], [Email], [Password], [Salt], [Active], [Avatar], [FullName], [RoleID], [LastLogin], [CreateDate]) VALUES (1, N'094-2344-233', N'admin@gmail.com', N'123456', N'1         ', 1, N'test.jpg', N'lương viết bảo', 1, CAST(N'2022-05-15T13:19:13.090' AS DateTime), NULL)
INSERT [dbo].[Accounts] ([AccountID], [Phone], [Email], [Password], [Salt], [Active], [Avatar], [FullName], [RoleID], [LastLogin], [CreateDate]) VALUES (3, N'094-2344-233', N'nhanvien', N'123', N'1         ', 0, NULL, N'test', 1, NULL, CAST(N'2022-03-26T23:13:40.510' AS DateTime))
INSERT [dbo].[Accounts] ([AccountID], [Phone], [Email], [Password], [Salt], [Active], [Avatar], [FullName], [RoleID], [LastLogin], [CreateDate]) VALUES (4, N'29192', N'0210210', N'12234', N'1         ', 1, NULL, N'test', 2, CAST(N'2022-01-01T00:00:00.000' AS DateTime), CAST(N'2022-03-26T23:14:38.163' AS DateTime))
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [Cover], [SchemaMarkup]) VALUES (3, N'Rau củ', N'Rau, củ', 1, 1, 1, 1, N'1', N'Danh mục rau củ', N'dn-rau-cu', N'1', N'1')
INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [Cover], [SchemaMarkup]) VALUES (4, N'Trái cây', N'Trái cây', 1, 1, 1, 1, N'1', N'Danh mục trái cây', N'trai-cay', N'1', N'1')
INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [Cover], [SchemaMarkup]) VALUES (5, N'Thịt', N'Thịt', 1, 1, 1, 1, N'1', N'Danh mục thịt', N'thit', N'1', N'1')
INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [Cover], [SchemaMarkup]) VALUES (1005, N'Sữa', N'Sữa', 1, 1, 1, 1, N'Sữa', N'Sữa', N'sua', N'Sữa', N'Sữa')
INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [Cover], [SchemaMarkup]) VALUES (1006, N'test', N'test', 1, 1, 1, 1, N'test', N'test', N'test', N'test', N'test')
INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [Cover], [SchemaMarkup]) VALUES (1007, N'Sữa', N'<p>Sữa<br></p>', NULL, NULL, 1, 1, N'default.jpg', N'Sữa', N'Sữa', NULL, N'Sữa')
INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [Cover], [SchemaMarkup]) VALUES (1008, N'Sữa', N'sữa', NULL, NULL, 1, 1, N'sua.jpg', N'Sữa', N'Sữa', NULL, N'Sữa')
INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [Cover], [SchemaMarkup]) VALUES (1009, N'Sữa', N'sữa', NULL, NULL, 1, 1, N'sua.jpg', N'Sữa', N'Sữa', NULL, N'Sữa')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (1, N'Dadad Dada', NULL, N'dadad-dada.jpg', N'adadad', N'bao@gmail.com                                                                                                                                         ', N'21212121', 7501, 7503, 7502, CAST(N'2022-04-04T01:50:04.703' AS DateTime), N'a263c1804ad58adb3eb1afb76ac1800f', N'0(n]2   ', CAST(N'2022-04-29T01:14:39.100' AS DateTime), 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (2, N'Dadad Dada', NULL, N'dadad-dada.jpg', N'test', N'test@gmail.com                                                                                                                                        ', N'21212121111', 7501, 7505, 7506, CAST(N'2022-04-04T02:15:14.547' AS DateTime), N'0e701e221fe795c28f55ffe7e5a06c93', N'8$d:{   ', CAST(N'2022-05-02T17:28:09.420' AS DateTime), 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (1002, N'test', NULL, NULL, NULL, N'test@gmail                                                                                                                                            ', N'123', NULL, NULL, NULL, CAST(N'2022-04-11T04:01:49.557' AS DateTime), N'dddb116e02ee9bba32fc494be84d2b5c', N'&^+ah   ', NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (1003, N'bac', NULL, NULL, NULL, N'123                                                                                                                                                   ', N'123', NULL, NULL, NULL, CAST(N'2022-04-11T20:19:30.330' AS DateTime), N'9e538196165b4aa50cf906521c3bce6d', N's]{)~   ', NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (2002, N'bacf', NULL, NULL, NULL, N'bao@gmail.com                                                                                                                                         ', N'1111111111', NULL, NULL, NULL, CAST(N'2022-04-29T08:55:29.620' AS DateTime), N'f0b73028f6453d30b143c40b87db3684', N'$1*cy   ', NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (2003, N'aaaa', NULL, NULL, NULL, N'aaaa@gmail.com                                                                                                                                        ', N'2121212111', NULL, NULL, NULL, CAST(N'2022-04-29T09:49:55.540' AS DateTime), N'9cc65bcfe2347f58fb18cdec8ef471cb', N'!t~ka   ', NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (2004, N'test123', NULL, NULL, NULL, N'test123@gmail.com                                                                                                                                     ', N'1111111111', NULL, NULL, NULL, CAST(N'2022-04-30T23:39:21.230' AS DateTime), N'b557959ea833b7687e2453cc3edea008', N'3o2s1   ', NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (2005, N'Aaaa', NULL, N'aaaa.png', N'test', N'bao1@gmail.com                                                                                                                                        ', N'1111111112', 7501, 7503, 7502, CAST(N'2022-05-01T14:22:51.783' AS DateTime), N'd01ef893a53dd7da8b1ba934e5dad4d6', N'yb$o}   ', CAST(N'2022-05-15T11:57:35.047' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
INSERT [dbo].[Locations] ([LocationID], [Name], [Parent], [Levels], [Slug], [NameWithType], [Type]) VALUES (603, N'Hà Nội', 1, 1, N'1', N'1', N'1')
INSERT [dbo].[Locations] ([LocationID], [Name], [Parent], [Levels], [Slug], [NameWithType], [Type]) VALUES (7501, N'Huế', 1, 1, N'1', N'Hue', N'Tỉnh')
INSERT [dbo].[Locations] ([LocationID], [Name], [Parent], [Levels], [Slug], [NameWithType], [Type]) VALUES (7502, N'Xuân Phú', 7503, 3, N'1', N'1', N'1')
INSERT [dbo].[Locations] ([LocationID], [Name], [Parent], [Levels], [Slug], [NameWithType], [Type]) VALUES (7503, N'Thành Phố Huê', 7501, 2, N'1', N'1', N'1')
INSERT [dbo].[Locations] ([LocationID], [Name], [Parent], [Levels], [Slug], [NameWithType], [Type]) VALUES (7504, N'Phường An Cựu', 7503, 3, N'1', N'1', N'1')
INSERT [dbo].[Locations] ([LocationID], [Name], [Parent], [Levels], [Slug], [NameWithType], [Type]) VALUES (7505, N'A Luowis', 7501, 2, N'1', N'1', N'1')
INSERT [dbo].[Locations] ([LocationID], [Name], [Parent], [Levels], [Slug], [NameWithType], [Type]) VALUES (7506, N'test', 7505, 3, N'1', N'1', N'1')
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (1, 4, 33, NULL, 3, NULL, 12000, CAST(N'2022-04-10T05:24:22.550' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (2, 4, 30, NULL, 1, NULL, 12000, CAST(N'2022-04-10T05:24:22.553' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (3, 5, 34, NULL, 1, NULL, 3000, CAST(N'2022-04-10T05:27:51.083' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (4, 6, 34, NULL, 1, NULL, 3000, CAST(N'2022-04-10T17:27:23.643' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (5, 7, 34, NULL, 1, NULL, 3000, CAST(N'2022-04-10T19:56:03.517' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (6, 8, 33, NULL, 1, NULL, 3000, CAST(N'2022-04-10T19:57:58.620' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (7, 9, 33, NULL, 1, NULL, 3000, CAST(N'2022-04-10T19:59:37.567' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (8, 10, 34, NULL, 1, NULL, 3000, CAST(N'2022-04-10T20:06:27.417' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (9, 11, 34, NULL, 1, NULL, 3000, CAST(N'2022-04-10T20:10:31.573' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (10, 12, 34, NULL, 1, NULL, 3000, CAST(N'2022-04-10T20:14:38.233' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (11, 13, 34, NULL, 1, NULL, 3000, CAST(N'2022-04-10T20:30:47.560' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (12, 14, 34, NULL, 1, NULL, 3000, CAST(N'2022-04-10T20:58:21.613' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (13, 15, 34, NULL, 1, NULL, 3000, CAST(N'2022-04-11T15:23:03.080' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (14, 16, 33, NULL, 1, NULL, 3000, CAST(N'2022-04-11T23:49:11.717' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (15, 17, 34, NULL, 2, NULL, 6000, CAST(N'2022-04-12T03:17:26.173' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (16, 18, 34, 1, 1, NULL, 3000, CAST(N'2022-04-12T03:44:58.730' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (1002, 1002, 34, 1, 1, NULL, 3000, CAST(N'2022-04-14T15:03:44.707' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (1003, 1003, 34, 1, 1, NULL, 3000, CAST(N'2022-04-14T15:05:05.440' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (1004, 1004, 34, 1, 1, NULL, 3000, CAST(N'2022-04-14T15:08:03.037' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (1005, 1005, 34, 1, 1, NULL, 3000, CAST(N'2022-04-14T15:09:31.363' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (1006, 1006, 33, 1, 1, NULL, 3000, CAST(N'2022-04-30T23:46:49.540' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (1007, 1007, 34, 1, 1, NULL, 3000, CAST(N'2022-05-01T11:58:42.543' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (1008, 1008, 34, 1, 1, NULL, 3000, CAST(N'2022-05-01T13:42:28.590' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (1009, 1009, 34, 1, 1, NULL, 3000, CAST(N'2022-05-01T13:45:51.740' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (1010, 1010, 34, 1, 1, NULL, 3000, CAST(N'2022-05-11T01:28:57.320' AS DateTime), 3000)
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Amount], [Discount], [TotalMoney], [CreateDate], [Price]) VALUES (1011, 1011, 41, 1, 1, NULL, 50000, CAST(N'2022-05-15T11:58:46.327' AS DateTime), 50000)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (4, 1, CAST(N'2022-04-10T05:24:06.633' AS DateTime), CAST(N'2022-05-02T01:44:12.150' AS DateTime), 15, 0, 0, NULL, 12000, 1, NULL, N'NULL121', 0, 0, 0)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (5, 1, CAST(N'2022-04-10T05:27:50.997' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 2, NULL, N'2121', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (6, 1, CAST(N'2022-04-10T17:27:23.577' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 1, NULL, N'21212', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (7, 1, CAST(N'2022-04-10T19:56:03.437' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 1, NULL, N'212121', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (8, 1, CAST(N'2022-04-10T19:57:58.533' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 1, NULL, N'2121', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (9, 1, CAST(N'2022-04-10T19:59:37.470' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 3, NULL, N'1212', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (10, 1, CAST(N'2022-04-10T20:06:27.337' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 2, NULL, N'21212', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (11, 1, CAST(N'2022-04-10T20:10:31.493' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 1, NULL, N'21212', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (12, 1, CAST(N'2022-04-10T20:14:38.150' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 1, NULL, N'21212', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (13, 1, CAST(N'2022-04-10T20:30:47.467' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 1, NULL, N'212121', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (14, 1, CAST(N'2022-04-10T20:58:18.737' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 2, NULL, N'212121', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (15, 1, CAST(N'2022-04-11T15:23:02.990' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 2, NULL, N'1111', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (16, 1, CAST(N'2022-04-11T23:49:03.987' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 1, NULL, N'21212121212', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (17, 1, CAST(N'2022-04-12T03:17:26.090' AS DateTime), NULL, 13, 0, 0, NULL, 6000, 3, NULL, N'106/212121', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (18, 1, CAST(N'2022-04-12T03:44:58.643' AS DateTime), NULL, 13, 0, 0, NULL, 3000, 1, N'bnmn,,', N'1234444', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (1002, 1, CAST(N'2022-04-14T15:03:44.617' AS DateTime), NULL, 13, 0, 0, NULL, 3000, 1, N'csđsfsdfsdfsd', N'aa', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (1003, 1, CAST(N'2022-04-14T15:05:05.437' AS DateTime), NULL, 13, 0, 0, NULL, 3000, 3, N'11111', N'aa', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (1004, 1, CAST(N'2022-04-14T15:08:02.950' AS DateTime), NULL, 13, 0, 0, NULL, 3000, 1, N'addada', N'aa', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (1005, 1, CAST(N'2022-04-14T15:09:31.277' AS DateTime), NULL, 13, 0, 0, NULL, 3000, 1, N'dâd', N'adadad', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (1006, 2, CAST(N'2022-04-30T23:46:49.460' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 1, NULL, N'test', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (1007, 2, CAST(N'2022-05-01T11:58:42.433' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 1, NULL, N'test', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (1008, 2, CAST(N'2022-05-01T13:42:28.500' AS DateTime), NULL, 14, 1, 0, NULL, 3000, 1, NULL, N'test', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (1009, 2, CAST(N'2022-05-01T13:45:51.737' AS DateTime), NULL, 14, 1, 0, NULL, 3000, 1, NULL, N'test', 7501, 7505, 7506)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (1010, 2005, CAST(N'2022-05-11T01:28:57.157' AS DateTime), NULL, 14, 0, 0, NULL, 3000, 2, NULL, N'test', 7501, 7503, 7502)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [TotalMoney], [PaymentID], [Note], [Address], [LocationID], [District], [Ward]) VALUES (1011, 2005, CAST(N'2022-05-15T11:58:46.270' AS DateTime), NULL, 14, 0, 0, NULL, 50000, 1, NULL, N'test', 7501, 7503, 7502)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Pages] ON 

INSERT [dbo].[Pages] ([PageID], [PageName], [Contents], [Thumb], [Published], [Title], [Alias], [CreatedDate], [Ordering]) VALUES (1, N'Hướng dẫn mua hàng', N'abc', N'huong-dan-mua-hang.jpg', 1, N'abc1111', N'abc', NULL, 1)
INSERT [dbo].[Pages] ([PageID], [PageName], [Contents], [Thumb], [Published], [Title], [Alias], [CreatedDate], [Ordering]) VALUES (2, N'test', N'<p>aaaaa</p>', N'default.jpg', 1, N'a', N'a', CAST(N'2022-04-01T00:07:35.760' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Pages] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (3, N'Xà lách', N'1', N'1', 3, 1, 1, N'traicay.jpg', N'1', CAST(N'2022-03-25T00:00:00.000' AS DateTime), CAST(N'2022-03-25T00:00:00.000' AS DateTime), 1, 1, 1, N'1', N'1', N'1', N'1         ', 50, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (5, N'Chanh dây', N'Chanh dây', N'Chanh dây', 3, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:42:06.990' AS DateTime), CAST(N'2022-03-27T15:42:06.990' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (6, N'Táo', N'Chanh dây', N'Chanh dây', 3, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:23.070' AS DateTime), CAST(N'2022-03-27T15:43:23.070' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (7, N'Cam', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:24.053' AS DateTime), CAST(N'2022-03-27T15:43:24.053' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (8, N'Thịt bò', N'Chanh dây', N'Chanh dây', 5, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:24.477' AS DateTime), CAST(N'2022-03-27T15:43:24.477' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (9, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:24.817' AS DateTime), CAST(N'2022-03-27T15:43:24.817' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (11, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:25.507' AS DateTime), CAST(N'2022-03-27T15:43:25.507' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (12, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:25.707' AS DateTime), CAST(N'2022-03-27T15:43:25.707' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (13, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:26.070' AS DateTime), CAST(N'2022-03-27T15:43:26.070' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (14, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:26.460' AS DateTime), CAST(N'2022-03-27T15:43:26.460' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (15, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:26.777' AS DateTime), CAST(N'2022-03-27T15:43:26.777' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (16, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:27.147' AS DateTime), CAST(N'2022-03-27T15:43:27.147' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (17, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:27.520' AS DateTime), CAST(N'2022-03-27T15:43:27.520' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (18, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:27.910' AS DateTime), CAST(N'2022-03-27T15:43:27.910' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (19, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:43:28.270' AS DateTime), CAST(N'2022-03-27T15:43:28.270' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (20, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:36.307' AS DateTime), CAST(N'2022-03-27T15:45:36.307' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (21, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:36.727' AS DateTime), CAST(N'2022-03-27T15:45:36.727' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (22, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:37.067' AS DateTime), CAST(N'2022-03-27T15:45:37.067' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (23, N'Chanh dây', N'Chanh dây', N'Chanh dây', 3, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:37.397' AS DateTime), CAST(N'2022-03-27T15:45:37.397' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (24, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:37.747' AS DateTime), CAST(N'2022-03-27T15:45:37.747' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (25, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:38.090' AS DateTime), CAST(N'2022-03-27T15:45:38.090' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (27, N'Chanh dây', N'Chanh dây', N'Chanh dây', 3, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:38.830' AS DateTime), CAST(N'2022-03-27T15:45:38.830' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (29, N'Chanh dây', N'Chanh dây', N'Chanh dây', 3, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:39.437' AS DateTime), CAST(N'2022-03-27T15:45:39.437' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (30, N'Chanh dây', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:39.790' AS DateTime), CAST(N'2022-03-27T15:45:39.790' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (31, N'Chanh dây', N'Chanh dây', N'Chanh dây', 3, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:40.177' AS DateTime), CAST(N'2022-03-27T15:45:40.177' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (32, N'Bưởi', N'Chanh dây', N'Chanh dây', 4, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:40.520' AS DateTime), CAST(N'2022-03-27T15:45:40.520' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (33, N'Cam', N'Chanh dây', N'Chanh dây', 5, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:40.887' AS DateTime), CAST(N'2022-03-27T15:45:40.887' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (34, N'Táo', N'Chanh dây', N'Chanh dây', 3, 3000, 1, N'traicay.jpg', N'NULL', CAST(N'2022-03-27T15:45:41.240' AS DateTime), CAST(N'2022-03-27T15:45:41.240' AS DateTime), 1, 1, 1, N'Chanh Dây', N'Chanh dây', N'chanh-day', N'show      ', 30, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (35, N'Thanh Long1', N'Thanh long', N'Thanh long', 4, 10000, 10, N'traicay.jpg', NULL, NULL, CAST(N'2022-03-31T23:25:53.387' AS DateTime), 0, 0, 1, N'Thanh long', N'Thanh long1', N'thanh-long1', NULL, 100, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (36, N'Thịt Bò', N'Thịt bò hảo hạn', NULL, 5, 15000, NULL, N'thit-bo.jpg', NULL, CAST(N'2022-05-15T11:35:02.497' AS DateTime), CAST(N'2022-05-15T11:35:02.387' AS DateTime), 0, 1, 1, NULL, NULL, N'thit-bo', NULL, 50, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (37, N'Chuối', N'Chuối', NULL, 4, 30000, NULL, N'chuoi.jpg', NULL, CAST(N'2022-05-15T11:36:32.477' AS DateTime), CAST(N'2022-05-15T11:36:32.470' AS DateTime), 0, 1, 1, NULL, NULL, N'chuoi', NULL, 50, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (38, N'Sữa Bò', N'Sữa bò', NULL, 1005, 20000, NULL, N'sua-bo.jpg', NULL, CAST(N'2022-05-15T11:36:56.577' AS DateTime), CAST(N'2022-05-15T11:36:56.567' AS DateTime), 0, 1, 1, NULL, NULL, N'sua-bo', NULL, 50, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (39, N'Tôm', N'Tôm', NULL, 5, 70000, NULL, N'tom.jpg', NULL, CAST(N'2022-05-15T11:37:31.987' AS DateTime), CAST(N'2022-05-15T11:37:31.980' AS DateTime), 0, 1, 1, NULL, NULL, N'tom', NULL, 50, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (40, N'Bơ', N'Bơ', NULL, 4, 50000, NULL, N'bo.jpg', NULL, CAST(N'2022-05-15T11:38:22.853' AS DateTime), CAST(N'2022-05-15T11:38:22.847' AS DateTime), 0, 1, 1, NULL, NULL, N'bo', NULL, 50, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSellers], [HomeFlag], [Active], [Tags], [Title], [Alias], [PushilberId], [UnitsInStock], [SupplierID]) VALUES (41, N'Bơ', N'Bơ', NULL, 4, 50000, NULL, N'bo.jpg', NULL, CAST(N'2022-05-15T11:38:36.157' AS DateTime), CAST(N'2022-05-15T11:38:36.153' AS DateTime), 0, 1, 0, NULL, NULL, N'bo', NULL, 50, NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (1, N'Admin', N'Admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (2, N'Staff', N'Nhân viên')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[TransactStatus] ON 

INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (13, N'Chờ lấy hàng', N'Đã xác nhận và chuẩn bị hàng')
INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (14, N'Chờ xác nhận', N'Đang được người bán xác nhận với người mua')
INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (15, N'Đang giao', N'Đơn hàng được giao tới người mua')
INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (16, N'Đã giao thành công', N'Đơn hàng đã được giao thành công tới người mua')
INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (17, N'Đã hủy', N'Đơn hàng đã được hủy thành công')
INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (18, N'Trả hàng', N'Đơn hàng đã được trả hàng thành công')
SET IDENTITY_INSERT [dbo].[TransactStatus] OFF
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Advertisements] ADD  CONSTRAINT [DF_QuangCaos_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_TinDangs_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  CONSTRAINT [DF_OrderDetails_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Pages] ADD  CONSTRAINT [DF_Pages_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Video]  DEFAULT (NULL) FOR [Video]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Roles]
GO
ALTER TABLE [dbo].[AttributesPrices]  WITH CHECK ADD  CONSTRAINT [FK_AttributesPrices_Attributes] FOREIGN KEY([AttributeID])
REFERENCES [dbo].[Attributes] ([AttributeID])
GO
ALTER TABLE [dbo].[AttributesPrices] CHECK CONSTRAINT [FK_AttributesPrices_Attributes]
GO
ALTER TABLE [dbo].[AttributesPrices]  WITH CHECK ADD  CONSTRAINT [FK_AttributesPrices_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[AttributesPrices] CHECK CONSTRAINT [FK_AttributesPrices_Products]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Locations] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Locations]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers1] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers1]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_TransactStatus] FOREIGN KEY([TransactStatusID])
REFERENCES [dbo].[TransactStatus] ([TransactStatusID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_TransactStatus]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CatID])
REFERENCES [dbo].[Categories] ([CatID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Suppliers]
GO
ALTER TABLE [dbo].[Suppliers]  WITH CHECK ADD  CONSTRAINT [FK_Suppliers_Accounts] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Suppliers] CHECK CONSTRAINT [FK_Suppliers_Accounts]
GO
ALTER TABLE [dbo].[Suppliers]  WITH CHECK ADD  CONSTRAINT [FK_Suppliers_Locations] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO
ALTER TABLE [dbo].[Suppliers] CHECK CONSTRAINT [FK_Suppliers_Locations]
GO
