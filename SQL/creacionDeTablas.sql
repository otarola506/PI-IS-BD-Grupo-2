USE BD_Grupo2;

CREATE TABLE Miembro(
	nombreUsuarioPK VARCHAR(50) NOT NULL PRIMARY KEY,
	nombre VARCHAR(50) NOT NULL,
	pesoMiembro INTEGER,	
);

CREATE TABLE Nucleo(
	nombreUsuarioFK VARCHAR(50) NOT NULL PRIMARY KEY,
	tipo VARCHAR(50) NOT NULL,

	CONSTRAINT FK_Nucleo_Miembro
		FOREIGN KEY(nombreUsuarioFK) REFERENCES Miembro(nombreUsuarioPK)
			ON DELETE CASCADE
);

CREATE TABLE Perfil(
	perfilIdPK INTEGER NOT NULL IDENTITY (1,1),
	nombreUsuarioFK VARCHAR(50) NOT NULL,
	informacionLaboral VARCHAR(MAX),
	informacionBiografica VARCHAR(MAX),
	telefono VARCHAR(20),
	correo VARCHAR(20),
	merito FLOAT

	CONSTRAINT PK_PerfilMiembro PRIMARY KEY(perfilIdPK,nombreUsuarioFK)

	CONSTRAINT FK_Perfil_Miembro
		FOREIGN KEY(nombreUsuarioFK) REFERENCES Miembro(nombreUsuarioPK)
			ON DELETE CASCADE
);


CREATE TABLE Articulo(
	artIdPK INTEGER NOT NULL PRIMARY KEY IDENTITY (1,1),
	titulo VARCHAR(max) NOT NULL,
	resumen VARBINARY(max) NOT NULL,
	contenido VARBINARY(max) NOT NULL,
	puntuacion INTEGER,
	visitas INTEGER,
	estado VARCHAR(20),
	tipoArt VARCHAR(10) NOT NULL,
	nombreArchivo VARCHAR(50) NULL

); 

CREATE TABLE Miembro_Articulo(
	nombreUsuarioFK VARCHAR(50) NOT NULL,
	artIdFK INTEGER NOT NULL, 

	CONSTRAINT PK_ArticuloMiembro PRIMARY KEY(nombreUsuarioFK,artIdFK),
	CONSTRAINT FK_Escribe_Miembro
		FOREIGN KEY(nombreUsuarioFK) REFERENCES Miembro(nombreUsuarioPK)
			ON DELETE CASCADE,
	CONSTRAINT FK_Escribe_Articulo
		FOREIGN KEY(artIdFK) REFERENCES Articulo(artIdPK)
			ON DELETE CASCADE
);

CREATE TABLE Categoria(
	categoriaIdPK INTEGER NOT NULL PRIMARY KEY IDENTITY (1,1),
	nombre VARCHAR(20) NOT NULL, 
);

CREATE TABLE Art_Categoria (
	categoriaIdFK INTEGER NOT NULL,
	artIdFK INTEGER NOT NULL
	
	CONSTRAINT PK_ArticuloCategoria PRIMARY KEY(categoriaIdFK,artIdFK)

	CONSTRAINT FK_Art_Categoria_Categoria
	FOREIGN KEY(categoriaIdFK) REFERENCES Categoria (categoriaIdPK)
		ON DELETE CASCADE,

	CONSTRAINT FK_Art_Categoria_Articulo
	FOREIGN KEY(artIdFK) REFERENCES Articulo (artIdPK)
		ON DELETE CASCADE
);

CREATE TABLE Pregunta_Frecuente(
	pregIdPK INTEGER NOT NULL PRIMARY KEY IDENTITY (1,1),
	nombreUsuarioFK VARCHAR(50) DEFAULT -1,
	pregunta VARCHAR(max) NOT NULL,
	respuesta VARCHAR(max) NOT NULL,

	CONSTRAINT FK_Pregunta_Miembro
		FOREIGN KEY(nombreUsuarioFK) REFERENCES Miembro(nombreUsuarioPK)
			ON DELETE SET DEFAULT
);

