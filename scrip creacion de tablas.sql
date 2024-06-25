--creación de tablas:


--Tabla usuario
CREATE TABLE Usuario (
    CodTrabajador INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Telefono NVARCHAR(15),
    Puesto NVARCHAR(50),
    Rol NVARCHAR(20) CHECK (Rol IN ('Encargado', 'Vendedor', 'Delivery', 'Repartidor'))
);


-- tabla pedidos
CREATE TABLE Pedidos (
    NroPedido INT PRIMARY KEY IDENTITY(1,1),
    ListaProductos NVARCHAR(MAX) NOT NULL,
    FechaPedido DATETIME NOT NULL,
    FechaRecepcion DATETIME,
    FechaDespacho DATETIME,
    FechaEntrega DATETIME,
    VendedorSolicitante INT FOREIGN KEY REFERENCES Usuario(CodTrabajador),
    Repartidor INT FOREIGN KEY REFERENCES Usuario(CodTrabajador),
    Estado NVARCHAR(20) CHECK (Estado IN ('Por atender', 'En proceso', 'En delivery', 'Recibido'))
);


--tabla Productos
CREATE TABLE Productos (
    SKU INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Tipo NVARCHAR(50),
    Etiquetas NVARCHAR(MAX),
    Precio DECIMAL(10,2) NOT NULL,
    UnidadMedida NVARCHAR(20)
);

