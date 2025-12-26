-- =========================================
-- MODULO: GESTIÓN DE ALUMNOS
-- =========================================

CREATE TABLE Alumno (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombres VARCHAR(50) NOT NULL,
    Apellidos VARCHAR(50) NOT NULL,
    FechaNacimiento DATE,
    CodigoAlumno VARCHAR(20) NOT NULL UNIQUE,
    Estado BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE Encargado (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombres VARCHAR(50) NOT NULL,
    Apellidos VARCHAR(50) NOT NULL,
    Documento VARCHAR(20),
    Telefono VARCHAR(15),
    Email VARCHAR(100),
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE AlumnoEncargado (
    IdAlumno INT NOT NULL,
    IdEncargado INT NOT NULL,
    Parentesco VARCHAR(20) NOT NULL,
    PRIMARY KEY (IdAlumno, IdEncargado),
    FOREIGN KEY (IdAlumno) REFERENCES Alumno(Id),
    FOREIGN KEY (IdEncargado) REFERENCES Encargado(Id)
);
GO

CREATE TABLE Grado (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(20) NOT NULL,
    Nivel VARCHAR(30) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Turno (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(30) NOT NULL UNIQUE,
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Grupo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdGrado INT NOT NULL,
    IdTurno INT NOT NULL,
    Nombre VARCHAR(10) NOT NULL,
    AnioEscolar INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Grupo_Grado FOREIGN KEY (IdGrado) REFERENCES Grado(Id),
    CONSTRAINT FK_Grupo_Turno FOREIGN KEY (IdTurno) REFERENCES Turno(Id),
    CONSTRAINT UQ_Grupo UNIQUE (IdGrado, IdTurno, Nombre, AnioEscolar)
);
GO

CREATE TABLE Maestro (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombres VARCHAR(50) NOT NULL,
    Apellidos VARCHAR(50) NOT NULL,
    Especialidad VARCHAR(50),
    Telefono VARCHAR(15),
    Email VARCHAR(100),
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE AlumnoGrupo (
    IdAlumno INT NOT NULL,
    IdGrupo INT NOT NULL,
    FechaAsignacion DATETIME NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY (IdAlumno, IdGrupo),
    FOREIGN KEY (IdAlumno) REFERENCES Alumno(Id),
    FOREIGN KEY (IdGrupo) REFERENCES Grupo(Id)
);
GO

CREATE TABLE GrupoMaestro (
    IdGrupo INT NOT NULL,
    IdMaestro INT NOT NULL,
    EsTitular BIT NOT NULL DEFAULT 0,
    PRIMARY KEY (IdGrupo, IdMaestro),
    FOREIGN KEY (IdGrupo) REFERENCES Grupo(Id),
    FOREIGN KEY (IdMaestro) REFERENCES Maestro(Id)
);
GO
