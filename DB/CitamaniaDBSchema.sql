USE [Citamania]
GO
/****** Object:  Table [dbo].[Citas]    Script Date: 5/3/2023 11:52:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Citas](
	[CitaId] [int] IDENTITY(1,1) NOT NULL,
	[CodigoUnico] [uniqueidentifier] NOT NULL,
	[FechaDeCita] [datetime] NOT NULL,
	[ClienteId] [int] NOT NULL,
	[PrestadorDeServicioId] [int] NOT NULL,
	[SolicitanteId] [int] NOT NULL,
	[Estatus] [nvarchar](50) NOT NULL,
	[UsuarioAprobacionId] [int] NULL,
	[FechaAprobacion] [datetime] NULL,
	[FechaCancelacion] [datetime] NULL,
	[UsuarioCancelacionId] [int] NULL,
	[MotivoDeCancelacion] [nvarchar](400) NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[Notas] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[CitaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CitasDetalles]    Script Date: 5/3/2023 11:52:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CitasDetalles](
	[CitaId] [int] NOT NULL,
	[ServicioId] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Precio] [money] NOT NULL,
 CONSTRAINT [PK_CitasDetalles] PRIMARY KEY CLUSTERED 
(
	[CitaId] ASC,
	[ServicioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiciosPorUsuario]    Script Date: 5/3/2023 11:52:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiciosPorUsuario](
	[ServicioId] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[Servicio] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](400) NOT NULL,
	[Precio] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ServicioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 5/3/2023 11:52:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](250) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[HasPassword] [nvarchar](400) NOT NULL,
	[SaltStrig] [nvarchar](400) NOT NULL,
	[TokenDeRecuperacion] [nvarchar](100) NULL,
	[PrestaServicio] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Citas] ADD  DEFAULT (newid()) FOR [CodigoUnico]
GO
ALTER TABLE [dbo].[Citas] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[CitasDetalles] ADD  DEFAULT ((1)) FOR [Cantidad]
GO
ALTER TABLE [dbo].[Citas]  WITH CHECK ADD  CONSTRAINT [FK_Citas_Usuarios_Aprobador] FOREIGN KEY([UsuarioAprobacionId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
ALTER TABLE [dbo].[Citas] CHECK CONSTRAINT [FK_Citas_Usuarios_Aprobador]
GO
ALTER TABLE [dbo].[Citas]  WITH CHECK ADD  CONSTRAINT [FK_Citas_Usuarios_Cancelador] FOREIGN KEY([UsuarioCancelacionId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
ALTER TABLE [dbo].[Citas] CHECK CONSTRAINT [FK_Citas_Usuarios_Cancelador]
GO
ALTER TABLE [dbo].[Citas]  WITH CHECK ADD  CONSTRAINT [FK_Citas_Usuarios_Cliente] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
ALTER TABLE [dbo].[Citas] CHECK CONSTRAINT [FK_Citas_Usuarios_Cliente]
GO
ALTER TABLE [dbo].[Citas]  WITH CHECK ADD  CONSTRAINT [FK_Citas_Usuarios_PrestadorDeServicio] FOREIGN KEY([PrestadorDeServicioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
ALTER TABLE [dbo].[Citas] CHECK CONSTRAINT [FK_Citas_Usuarios_PrestadorDeServicio]
GO
ALTER TABLE [dbo].[Citas]  WITH CHECK ADD  CONSTRAINT [FK_Citas_Usuarios_Solicitante] FOREIGN KEY([SolicitanteId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
ALTER TABLE [dbo].[Citas] CHECK CONSTRAINT [FK_Citas_Usuarios_Solicitante]
GO
ALTER TABLE [dbo].[CitasDetalles]  WITH CHECK ADD  CONSTRAINT [FK_CitasDetalles_Citas] FOREIGN KEY([CitaId])
REFERENCES [dbo].[Citas] ([CitaId])
GO
ALTER TABLE [dbo].[CitasDetalles] CHECK CONSTRAINT [FK_CitasDetalles_Citas]
GO
ALTER TABLE [dbo].[CitasDetalles]  WITH CHECK ADD  CONSTRAINT [FK_CitasDetalles_ServiciosPorUsuario] FOREIGN KEY([ServicioId])
REFERENCES [dbo].[ServiciosPorUsuario] ([ServicioId])
GO
ALTER TABLE [dbo].[CitasDetalles] CHECK CONSTRAINT [FK_CitasDetalles_ServiciosPorUsuario]
GO
ALTER TABLE [dbo].[ServiciosPorUsuario]  WITH CHECK ADD  CONSTRAINT [FK_ServiciosPorUsuario_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
ALTER TABLE [dbo].[ServiciosPorUsuario] CHECK CONSTRAINT [FK_ServiciosPorUsuario_Usuarios]
GO
