USE [site]
GO
/****** Object:  Table [dbo].[blok]    Script Date: 30.06.2024 18:46:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[blok](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[siteId] [int] NOT NULL,
	[blokAdi] [varchar](100) NOT NULL,
 CONSTRAINT [PK_blok] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[daire]    Script Date: 30.06.2024 18:46:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[daire](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[blokId] [int] NOT NULL,
	[daireNo] [varchar](100) NOT NULL,
 CONSTRAINT [PK_daire] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[daireKisi]    Script Date: 30.06.2024 18:46:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[daireKisi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[daireId] [int] NOT NULL,
	[kisiId] [int] NOT NULL,
 CONSTRAINT [PK_daireKisi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[iller]    Script Date: 30.06.2024 18:46:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iller](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ilAdi] [varchar](100) NOT NULL,
	[plakaKodu] [int] NOT NULL,
 CONSTRAINT [PK_iller] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kisiler]    Script Date: 30.06.2024 18:46:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kisiler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[tcKimlikNo] [varchar](11) NOT NULL,
	[adi] [varchar](100) NOT NULL,
	[soyadi] [varchar](100) NOT NULL,
	[dogumTarihi] [date] NULL,
	[dogumYeriId] [int] NOT NULL,
	[medeniDurumId] [int] NOT NULL,
	[kiracimi] [bit] NOT NULL,
	[gsmNo] [varchar](11) NOT NULL,
 CONSTRAINT [PK_kisiler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kisiParemetre]    Script Date: 30.06.2024 18:46:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kisiParemetre](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[kisiId] [int] NOT NULL,
	[parametreId] [int] NOT NULL,
	[aciklama] [varchar](255) NOT NULL,
 CONSTRAINT [PK_kisiParemetre] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[parametreler]    Script Date: 30.06.2024 18:46:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[parametreler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[parentId] [int] NOT NULL,
	[aciklama] [varchar](100) NOT NULL,
 CONSTRAINT [PK_parametreler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[site]    Script Date: 30.06.2024 18:46:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[site](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[siteName] [varchar](100) NOT NULL,
	[adres] [varchar](255) NOT NULL,
	[ilId] [int] NOT NULL,
 CONSTRAINT [PK_siteler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[siteParametre]    Script Date: 30.06.2024 18:46:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[siteParametre](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[siteId] [int] NOT NULL,
	[parametreId] [int] NOT NULL,
	[kisiId] [int] NOT NULL,
 CONSTRAINT [PK_siteParametre] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[blok] ON 

INSERT [dbo].[blok] ([Id], [siteId], [blokAdi]) VALUES (1, 1, N'A Blok')
INSERT [dbo].[blok] ([Id], [siteId], [blokAdi]) VALUES (2, 1, N'B Blok')
SET IDENTITY_INSERT [dbo].[blok] OFF
GO
SET IDENTITY_INSERT [dbo].[daire] ON 

INSERT [dbo].[daire] ([Id], [blokId], [daireNo]) VALUES (1, 1, N'D1')
INSERT [dbo].[daire] ([Id], [blokId], [daireNo]) VALUES (2, 1, N'D2')
SET IDENTITY_INSERT [dbo].[daire] OFF
GO
SET IDENTITY_INSERT [dbo].[daireKisi] ON 

INSERT [dbo].[daireKisi] ([Id], [daireId], [kisiId]) VALUES (4, 1, 2)
INSERT [dbo].[daireKisi] ([Id], [daireId], [kisiId]) VALUES (5, 2, 1)
SET IDENTITY_INSERT [dbo].[daireKisi] OFF
GO
SET IDENTITY_INSERT [dbo].[iller] ON 

INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (1, N'Adana', 1)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (2, N'Adıyaman', 2)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (3, N'Afyonkarahisar', 3)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (4, N'Ağrı', 4)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (5, N'Amasya', 5)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (6, N'Ankara', 6)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (7, N'Antalya', 7)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (8, N'Artvin', 8)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (9, N'Aydın', 9)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (10, N'Balıkesir', 10)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (11, N'Bilecik', 11)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (12, N'Bingöl', 12)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (13, N'Bitlis', 13)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (14, N'Bolu', 14)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (15, N'Burdur', 15)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (16, N'Bursa', 16)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (17, N'Çanakkale', 17)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (18, N'Çankırı', 18)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (19, N'Çorum', 19)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (20, N'Denizli', 20)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (21, N'Diyarbakır', 21)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (22, N'Edirne', 22)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (23, N'Elazığ', 23)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (24, N'Erzincan', 24)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (25, N'Erzurum', 25)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (26, N'Eskişehir', 26)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (27, N'Gaziantep', 27)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (28, N'Giresun', 28)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (29, N'Gümüşhane', 29)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (30, N'Hakkâri', 30)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (31, N'Hatay', 31)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (32, N'Isparta', 32)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (33, N'Mersin', 33)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (34, N'İstanbul', 34)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (35, N'İzmir', 35)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (36, N'Kars', 36)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (37, N'Kastamonu', 37)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (38, N'Kayseri', 38)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (39, N'Kırklareli', 39)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (40, N'Kırşehir', 40)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (41, N'Kocaeli', 41)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (42, N'Konya', 42)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (43, N'Kütahya', 43)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (44, N'Malatya', 44)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (45, N'Manisa', 45)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (46, N'Kahramanmaraş', 46)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (47, N'Mardin', 47)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (48, N'Muğla', 48)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (49, N'Muş', 49)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (50, N'Nevşehir', 50)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (51, N'Niğde', 51)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (52, N'Ordu', 52)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (53, N'Rize', 53)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (54, N'Sakarya', 54)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (55, N'Samsun', 55)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (56, N'Siirt', 56)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (57, N'Sinop', 57)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (58, N'Sivas', 58)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (59, N'Tekirdağ', 59)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (60, N'Tokat', 60)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (61, N'Trabzon', 61)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (62, N'Tunceli', 62)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (63, N'Şanlıurfa', 63)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (64, N'Uşak', 64)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (65, N'Van', 65)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (66, N'Yozgat', 66)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (67, N'Zonguldak', 67)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (68, N'Aksaray', 68)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (69, N'Bayburt', 69)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (70, N'Karaman', 70)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (71, N'Kırıkkale', 71)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (72, N'Batman', 72)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (73, N'Şırnak', 73)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (74, N'Bartın', 74)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (75, N'Ardahan', 75)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (76, N'Iğdır', 76)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (77, N'Yalova', 77)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (78, N'Karabük', 78)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (79, N'Kilis', 79)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (80, N'Osmaniye', 80)
INSERT [dbo].[iller] ([Id], [ilAdi], [plakaKodu]) VALUES (81, N'Düzce', 81)
SET IDENTITY_INSERT [dbo].[iller] OFF
GO
SET IDENTITY_INSERT [dbo].[kisiler] ON 

INSERT [dbo].[kisiler] ([Id], [tcKimlikNo], [adi], [soyadi], [dogumTarihi], [dogumYeriId], [medeniDurumId], [kiracimi], [gsmNo]) VALUES (1, N'11111111111', N'ali', N'bekar', CAST(N'2000-06-29' AS Date), 61, 10, 0, N'11111111111')
INSERT [dbo].[kisiler] ([Id], [tcKimlikNo], [adi], [soyadi], [dogumTarihi], [dogumYeriId], [medeniDurumId], [kiracimi], [gsmNo]) VALUES (2, N'22222222222', N'cemil', N'kadı', CAST(N'2010-06-29' AS Date), 2, 9, 1, N'22222222222')
SET IDENTITY_INSERT [dbo].[kisiler] OFF
GO
SET IDENTITY_INSERT [dbo].[parametreler] ON 

INSERT [dbo].[parametreler] ([Id], [parentId], [aciklama]) VALUES (1, 0, N'Site Parametreleri')
INSERT [dbo].[parametreler] ([Id], [parentId], [aciklama]) VALUES (2, 1, N'Yönetici')
INSERT [dbo].[parametreler] ([Id], [parentId], [aciklama]) VALUES (8, 0, N'Medeni Durumu')
INSERT [dbo].[parametreler] ([Id], [parentId], [aciklama]) VALUES (9, 8, N'Evli')
INSERT [dbo].[parametreler] ([Id], [parentId], [aciklama]) VALUES (10, 8, N'Bekar')
SET IDENTITY_INSERT [dbo].[parametreler] OFF
GO
SET IDENTITY_INSERT [dbo].[site] ON 

INSERT [dbo].[site] ([Id], [siteName], [adres], [ilId]) VALUES (1, N'Deneme Site 1', N'Deneme Adres', 61)
SET IDENTITY_INSERT [dbo].[site] OFF
GO
SET IDENTITY_INSERT [dbo].[siteParametre] ON 

INSERT [dbo].[siteParametre] ([Id], [siteId], [parametreId], [kisiId]) VALUES (1, 1, 2, 1)
SET IDENTITY_INSERT [dbo].[siteParametre] OFF
GO
ALTER TABLE [dbo].[blok]  WITH CHECK ADD  CONSTRAINT [FK_blok_site] FOREIGN KEY([siteId])
REFERENCES [dbo].[site] ([Id])
GO
ALTER TABLE [dbo].[blok] CHECK CONSTRAINT [FK_blok_site]
GO
ALTER TABLE [dbo].[daire]  WITH CHECK ADD  CONSTRAINT [FK_daire_blok] FOREIGN KEY([blokId])
REFERENCES [dbo].[blok] ([Id])
GO
ALTER TABLE [dbo].[daire] CHECK CONSTRAINT [FK_daire_blok]
GO
ALTER TABLE [dbo].[daireKisi]  WITH CHECK ADD  CONSTRAINT [FK_daireKisi_daire] FOREIGN KEY([daireId])
REFERENCES [dbo].[daire] ([Id])
GO
ALTER TABLE [dbo].[daireKisi] CHECK CONSTRAINT [FK_daireKisi_daire]
GO
ALTER TABLE [dbo].[daireKisi]  WITH CHECK ADD  CONSTRAINT [FK_daireKisi_kisiler] FOREIGN KEY([kisiId])
REFERENCES [dbo].[kisiler] ([Id])
GO
ALTER TABLE [dbo].[daireKisi] CHECK CONSTRAINT [FK_daireKisi_kisiler]
GO
ALTER TABLE [dbo].[kisiler]  WITH CHECK ADD  CONSTRAINT [FK_kisiler_iller] FOREIGN KEY([dogumYeriId])
REFERENCES [dbo].[iller] ([Id])
GO
ALTER TABLE [dbo].[kisiler] CHECK CONSTRAINT [FK_kisiler_iller]
GO
ALTER TABLE [dbo].[kisiler]  WITH CHECK ADD  CONSTRAINT [FK_kisiler_parametreler] FOREIGN KEY([medeniDurumId])
REFERENCES [dbo].[parametreler] ([Id])
GO
ALTER TABLE [dbo].[kisiler] CHECK CONSTRAINT [FK_kisiler_parametreler]
GO
ALTER TABLE [dbo].[kisiParemetre]  WITH CHECK ADD  CONSTRAINT [FK_kisiParemetre_kisiler] FOREIGN KEY([kisiId])
REFERENCES [dbo].[kisiler] ([Id])
GO
ALTER TABLE [dbo].[kisiParemetre] CHECK CONSTRAINT [FK_kisiParemetre_kisiler]
GO
ALTER TABLE [dbo].[kisiParemetre]  WITH CHECK ADD  CONSTRAINT [FK_kisiParemetre_parametreler] FOREIGN KEY([parametreId])
REFERENCES [dbo].[parametreler] ([Id])
GO
ALTER TABLE [dbo].[kisiParemetre] CHECK CONSTRAINT [FK_kisiParemetre_parametreler]
GO
ALTER TABLE [dbo].[site]  WITH CHECK ADD  CONSTRAINT [FK_site_iller] FOREIGN KEY([ilId])
REFERENCES [dbo].[iller] ([Id])
GO
ALTER TABLE [dbo].[site] CHECK CONSTRAINT [FK_site_iller]
GO
ALTER TABLE [dbo].[siteParametre]  WITH CHECK ADD  CONSTRAINT [FK_siteParametre_kisiler] FOREIGN KEY([kisiId])
REFERENCES [dbo].[kisiler] ([Id])
GO
ALTER TABLE [dbo].[siteParametre] CHECK CONSTRAINT [FK_siteParametre_kisiler]
GO
ALTER TABLE [dbo].[siteParametre]  WITH CHECK ADD  CONSTRAINT [FK_siteParametre_parametreler] FOREIGN KEY([parametreId])
REFERENCES [dbo].[parametreler] ([Id])
GO
ALTER TABLE [dbo].[siteParametre] CHECK CONSTRAINT [FK_siteParametre_parametreler]
GO
ALTER TABLE [dbo].[siteParametre]  WITH CHECK ADD  CONSTRAINT [FK_siteParametre_site] FOREIGN KEY([siteId])
REFERENCES [dbo].[site] ([Id])
GO
ALTER TABLE [dbo].[siteParametre] CHECK CONSTRAINT [FK_siteParametre_site]
GO
