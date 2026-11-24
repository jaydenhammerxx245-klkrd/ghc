CREATE TABLE [Build] (
    [IdBuild] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [CantidadPisos] int NOT NULL,
    CONSTRAINT [PK_Build] PRIMARY KEY ([IdBuild])
);
GO


CREATE TABLE [Cliente] (
    [IdCliente] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    [Apellido] nvarchar(max) NOT NULL,
    [Cedula] nvarchar(max) NOT NULL,
    [Telefono] nvarchar(max) NOT NULL,
    [Correo] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY ([IdCliente])
);
GO


CREATE TABLE [Empleado] (
    [IdEmpleado] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    [Apellido] nvarchar(max) NOT NULL,
    [Cedula] nvarchar(max) NOT NULL,
    [Telefono] nvarchar(max) NOT NULL,
    [Cargo] nvarchar(max) NOT NULL,
    [Salario] decimal(18,2) NOT NULL,
    [Correo] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Empleado] PRIMARY KEY ([IdEmpleado])
);
GO


CREATE TABLE [Habitaciones] (
    [IdHabitacion] int NOT NULL IDENTITY,
    [Numero] nvarchar(max) NOT NULL,
    [Capacidad] int NOT NULL,
    [PrecioNoche] decimal(18,2) NOT NULL,
    [Disponible] bit NOT NULL,
    [IdPiso] int NOT NULL,
    [Precio] decimal(18,2) NOT NULL,
    [Tipo] nvarchar(max) NOT NULL,
    [Estado] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Habitaciones] PRIMARY KEY ([IdHabitacion])
);
GO


CREATE TABLE [Piso] (
    [IdPiso] int NOT NULL IDENTITY,
    [NumeroPiso] int NOT NULL,
    [IdBuild] int NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Piso] PRIMARY KEY ([IdPiso])
);
GO


CREATE TABLE [Reservas] (
    [IdReserva] int NOT NULL IDENTITY,
    [IdCliente] int NOT NULL,
    [IdHabitacion] int NOT NULL,
    [FechaEntrada] datetime2 NOT NULL,
    [FechaSalida] datetime2 NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Reservas] PRIMARY KEY ([IdReserva])
);
GO


CREATE TABLE [Usuario] (
    [IdUsuario] int NOT NULL IDENTITY,
    [NombreUsuario] nvarchar(max) NOT NULL,
    [Clave] nvarchar(max) NOT NULL,
    [Rol] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([IdUsuario])
);
GO
