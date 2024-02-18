USE [PruebaTecnica]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16/02/2024 7:55:42 p. m. ******/
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
/****** Object:  Table [dbo].[Usuarios]    Script Date: 16/02/2024 7:55:42 p. m. ******/
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
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240215200822_Initialize', N'7.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240215235414_UpdateDataBase', N'7.0.16')
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([UsuarioId], [Nombre], [FechaNacimiento], [Sexo]) VALUES (2, N'Jhon Hamilton Romaña Moreno', CAST(N'2024-02-15T00:00:00.0000000' AS DateTime2), N'H')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_CrudUser]    Script Date: 16/02/2024 7:55:42 p. m. ******/
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
