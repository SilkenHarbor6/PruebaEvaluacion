CREATE TABLE Area
(
    IdArea int primary key identity(1,1),
    Nombre varchar(50),
    Descripcion varchar(250)
);
CREATE TABLE Empleado
(
    IdEmpleado int primary key identity(1,1),
    NombreCompleto varchar(300),
    Cedula varchar(15),
    Correo varchar(150),
    FechaNacimiento datetime,
    FechaIngreso datetime,
    IdJefe int FOREIGN KEY REFERENCES Empleado(IdEmpleado),
    IdArea int FOREIGN KEY REFERENCES Area(IdArea),
    Foto varchar(50)
);
CREATE TABLE Empleado_Habilidad
(
    IdHabilidad int primary key identity(1,1),
    IdEmpleado int FOREIGN KEY REFERENCES Empleado(IdEmpleado),
    NombreHabilidad varchar(50)
);