USE [PruebaTecnica]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17/02/2024 7:05:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 17/02/2024 7:05:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolesClaim]    Script Date: 17/02/2024 7:05:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolesClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RolesClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 17/02/2024 7:05:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[UserLoginId] [nvarchar](450) NOT NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[NombreUsuario] [nvarchar](max) NULL,
	[Contrasenia] [nvarchar](max) NULL,
	[Token] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[UserLoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginClaim]    Script Date: 17/02/2024 7:05:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserLoginId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserLoginClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginLogin]    Script Date: 17/02/2024 7:05:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginLogin](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserLoginId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLoginLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginRole]    Script Date: 17/02/2024 7:05:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginRole](
	[UserLoginId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLoginRole] PRIMARY KEY CLUSTERED 
(
	[UserLoginId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginToken]    Script Date: 17/02/2024 7:05:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginToken](
	[UserLoginId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserLoginToken] PRIMARY KEY CLUSTERED 
(
	[UserLoginId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 17/02/2024 7:05:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[FechaNacimiento] [datetime2](7) NOT NULL,
	[Sexo] [nvarchar](1) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240217232758_Initialize', N'7.0.16')
GO
INSERT [dbo].[UserLogin] ([UserLoginId], [Discriminator], [NombreUsuario], [Contrasenia], [Token], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'bb4b82f8-0650-4063-ab7b-248edad88be9', N'UserLogin', N'hamilton', N'h@1234', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImJiNGI4MmY4LTA2NTAtNDA2My1hYjdiLTI0OGVkYWQ4OGJlOSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJoYW1pbHRvbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiNTk3OWM1NzAtNzQ0ZC00OGFkLWI2ZWYtMmE4ZDhiYmEyODA3IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9leHBpcmF0aW9uIjoiZmViLiBkb20uIDE4IDIwMjQgMjM6MzI6NTYgcC7CoG0uIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQnVzaW5lc3MgSGFtaWx0b24iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9oYXNoIjoiUW5WemMybHVaWE56U0dGdGFXeDBiMjQ9IiwibmJmIjoxNzA4MjEyNzc2LCJleHAiOjE3MDgzMTcxNzYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMjAiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MjIwIn0.28k132QRrTY6-tYcNgb5pJ400wcV7ATHPfjKNQ-oefU', N'hamilton', N'HAMILTON', NULL, NULL, 0, N'AQAAAAIAAYagAAAAELUChG4f+sy3pVjJwd5xyselqBclXgnhBR/d3zgXEN2Qzm1Haf/PYSF/FJRfsrtGwA==', N'ZM6RDBJNJS47CPVU3MI4UAJMIJCHXQMF', N'90fbd1d9-d958-490f-ab42-bfd7bf341ba2', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([UsuarioId], [Nombre], [FechaNacimiento], [Sexo]) VALUES (1, N'Jhon Hamilton', CAST(N'2024-02-14T00:00:00.0000000' AS DateTime2), N'H')
INSERT [dbo].[Usuarios] ([UsuarioId], [Nombre], [FechaNacimiento], [Sexo]) VALUES (2, N'Aida Elena', CAST(N'2024-02-17T00:00:00.0000000' AS DateTime2), N'M')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[RolesClaim]  WITH CHECK ADD  CONSTRAINT [FK_RolesClaim_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolesClaim] CHECK CONSTRAINT [FK_RolesClaim_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserLoginClaim]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginClaim_UserLogin_UserLoginId] FOREIGN KEY([UserLoginId])
REFERENCES [dbo].[UserLogin] ([UserLoginId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLoginClaim] CHECK CONSTRAINT [FK_UserLoginClaim_UserLogin_UserLoginId]
GO
ALTER TABLE [dbo].[UserLoginLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginLogin_UserLogin_UserLoginId] FOREIGN KEY([UserLoginId])
REFERENCES [dbo].[UserLogin] ([UserLoginId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLoginLogin] CHECK CONSTRAINT [FK_UserLoginLogin_UserLogin_UserLoginId]
GO
ALTER TABLE [dbo].[UserLoginRole]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginRole_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLoginRole] CHECK CONSTRAINT [FK_UserLoginRole_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserLoginRole]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginRole_UserLogin_UserLoginId] FOREIGN KEY([UserLoginId])
REFERENCES [dbo].[UserLogin] ([UserLoginId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLoginRole] CHECK CONSTRAINT [FK_UserLoginRole_UserLogin_UserLoginId]
GO
ALTER TABLE [dbo].[UserLoginToken]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginToken_UserLogin_UserLoginId] FOREIGN KEY([UserLoginId])
REFERENCES [dbo].[UserLogin] ([UserLoginId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLoginToken] CHECK CONSTRAINT [FK_UserLoginToken_UserLogin_UserLoginId]
GO
/****** Object:  StoredProcedure [dbo].[SP_CrudUser]    Script Date: 17/02/2024 7:05:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_CrudUser]
(
	@TypeOperation varchar(1),
	@idUsuario int,
	@Nombre varchar(100),
	@Fecha varchar(10),
	@Sexo char(1)
)
As Begin
	Declare @tbResult table (idUsuario int)
	if (UPPER(@TypeOperation) = 'I')  Begin
		Insert Into Usuarios(Nombre, FechaNacimiento, Sexo) output inserted.UsuarioId into @tbResult
		Values (@Nombre, CAST(@Fecha as smalldatetime), @Sexo)
		Set @idUsuario = (select idUsuario from @tbResult)
		Set @TypeOperation = 'S'
	End
	if (UPPER(@TypeOperation) = 'U')  Begin
		Update u set u.Nombre = @Nombre, u.Sexo = @Sexo, u.FechaNacimiento = CAST(@Fecha as smalldatetime) 
		from Usuarios u
		where u.UsuarioId = @idUsuario
		Set @TypeOperation = 'S'
	End
	if (UPPER(@TypeOperation) = 'D')  Begin
		Delete u from Usuarios u where u.UsuarioId = @idUsuario
		Set @idUsuario = 0
	End
	if (UPPER(@TypeOperation) = 'S')  Begin
		select * from Usuarios u where (@idUsuario = 0 or u.UsuarioId = @idUsuario)
	End
End
GO
