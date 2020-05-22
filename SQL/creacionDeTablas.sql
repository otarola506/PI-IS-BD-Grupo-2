USE BD_Grupo2;

CREATE TABLE Miembro(
	miembroIdPK INTEGER NOT NULL PRIMARY KEY IDENTITY (1,1),
	nombre VARCHAR(50) NOT NULL,
	pesoMiembro INTEGER,
	nombreUsuario VARCHAR(50)
);

CREATE TABLE Nucleo(
	miembroIdFK INTEGER NOT NULL PRIMARY KEY,
	tipo INTEGER NOT NULL,

	CONSTRAINT FK_Nucleo_Miembro
		FOREIGN KEY(miembroIdFK) REFERENCES Miembro(miembroIdPK)
			ON DELETE CASCADE
);

CREATE TABLE Perfil(
	perfilIdPK INTEGER NOT NULL IDENTITY (1,1),
	miembroIdFK INTEGER NOT NULL,
	informacionLaboral VARCHAR(MAX),
	informacionBiografica VARCHAR(MAX),
	telefono VARCHAR(20),
	correo VARCHAR(20),
	merito FLOAT

	CONSTRAINT PK_PerfilMiembro PRIMARY KEY(perfilIdPK,miembroIdFK)

	CONSTRAINT FK_Perfil_Miembro
		FOREIGN KEY(miembroIdFK) REFERENCES Miembro(miembroIdPK)
			ON DELETE CASCADE
);


CREATE TABLE Articulo(
	artIdPK INTEGER NOT NULL PRIMARY KEY IDENTITY (1,1),
	titulo VARCHAR(max) NOT NULL,
	resumen VARBINARY(max) NOT NULL,
	contenido VARBINARY(max) NOT NULL,
	puntuacion INTEGER,
	visitas INTEGER,
	estado INTEGER,
	tipoArt bit NOT NULL

); 

CREATE TABLE Miembro_Articulo(
	miembroIdFK INTEGER NOT NULL,
	artIdFK INTEGER NOT NULL, 

	CONSTRAINT PK_ArticuloMiembro PRIMARY KEY(miembroIdFK,artIdFK),
	CONSTRAINT FK_Escribe_Miembro
		FOREIGN KEY(miembroIdFK) REFERENCES Miembro(miembroIdPK)
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
	miembroIdFK INTEGER DEFAULT -1,
	pregunta VARCHAR(max) NOT NULL,
	respuesta VARCHAR(max) NOT NULL,

	CONSTRAINT FK_Pregunta_Miembro
		FOREIGN KEY(miembroIdFK) REFERENCES Miembro(miembroIdPK)
			ON DELETE SET DEFAULT
);

