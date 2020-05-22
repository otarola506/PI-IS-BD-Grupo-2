USE BD_Grupo2;
CREATE TABLE Miembro(
	miembroIdPK INTEGER NOT NULL PRIMARY KEY IDENTITY (-1,1),
	nombre VARCHAR(50) NOT NULL,
	pesoMiembro INTEGER,
	correo VARCHAR(50)
);
CREATE TABLE Articulo(
	artIdPK INTEGER NOT NULL PRIMARY KEY IDENTITY (1,1),
	titulo VARCHAR(max) NOT NULL,
	resumen VARBINARY(max) NOT NULL,
	contenido VARBINARY(max) NOT NULL,
	tipoArt bit NOT NULL,
	extn VARCHAR(15),
	estado INTEGER,
	puntuacion INTEGER
); 

CREATE TABLE Categoria(
	categoriaIdPK INTEGER NOT NULL PRIMARY KEY IDENTITY (-1,1),
	nombre VARCHAR(20) NOT NULL, 
);

CREATE TABLE Art_Categoria (
	categoriaIdFK INTEGER NOT NULL DEFAULT -1,
	artIdFK INTEGER NOT NULL
	
	CONSTRAINT PK_ArticuloCategoria PRIMARY KEY(categoriaIdFK,artIdFK)

	CONSTRAINT FK_Art_Categoria_Categoria
	FOREIGN KEY(categoriaIdFK) REFERENCES Categoria (categoriaIdPK)
		ON DELETE SET DEFAULT,

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
CREATE TABLE Miembro_Nucleo(
	miembroIdFK INTEGER  NOT NULL DEFAULT -1,
	
	CONSTRAINT FK_Nucleo_Miembro
		FOREIGN KEY(miembroIdFK) REFERENCES Miembro(miembroIdPK)
			ON DELETE SET DEFAULT
);

CREATE TABLE Miembro_Articulo(
	miembroIdFK INTEGER NOT NULL DEFAULT -1,
	artIdFK INTEGER NOT NULL, 

	CONSTRAINT PK_ArticuloMiembro PRIMARY KEY(miembroIdFK,artIdFK),
	CONSTRAINT FK_Escribe_Miembro
		FOREIGN KEY(miembroIdFK) REFERENCES Miembro(miembroIdPK)
			ON DELETE SET DEFAULT,
	CONSTRAINT FK_Escribe_Articulo
		FOREIGN KEY(artIdFK) REFERENCES Articulo(artIdPK)
			ON DELETE CASCADE
);




INSERT INTO Miembro
VALUES('Anonimo')

INSERT INTO Categoria
VALUES('No especificada')
