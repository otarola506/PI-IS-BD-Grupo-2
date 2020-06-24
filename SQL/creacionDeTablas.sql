CREATE TABLE Miembro(
	nombreUsuarioPK VARCHAR(50) NOT NULL PRIMARY KEY,
	nombre VARCHAR(50) NOT NULL,
	apellido VARCHAR(50),
	pesoMiembro INTEGER,	
	telefono VARCHAR(20),
	informacionBiografica VARCHAR(MAX),
	pais VARCHAR(50),
	habilidades VARCHAR(MAX),
	idiomas VARCHAR(MAX),
	hobbies VARCHAR(MAX),
	informacionLaboral VARCHAR(MAX),
	correo VARCHAR(20),
	merito FLOAT
);

CREATE TABLE Nucleo(
	nombreUsuarioFK VARCHAR(50) NOT NULL PRIMARY KEY,
	tipo VARCHAR(50) NOT NULL,

	CONSTRAINT FK_Nucleo_Miembro
		FOREIGN KEY(nombreUsuarioFK) REFERENCES Miembro(nombreUsuarioPK)
			ON DELETE CASCADE
);



CREATE TABLE Articulo(
	artIdPK INTEGER NOT NULL PRIMARY KEY IDENTITY (1,1),
	titulo VARCHAR(max) NOT NULL,
	resumen VARBINARY(max) NOT NULL,
	contenido VARBINARY(max) NOT NULL,
	puntuacionInicial INTEGER,
	visitas INTEGER,
	estado VARCHAR(20),
	tipoArt VARCHAR(10) NOT NULL,
	nombreArchivo VARCHAR(50) NULL,
	fecha DATE,
	noMeGusta INTEGER,
	meGusta INTEGER
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

CREATE TABLE Art_Topico (
	topicoIdFK INTEGER NOT NULL,
	artIdFK INTEGER NOT NULL
	
	CONSTRAINT PK_ArticuloTopico PRIMARY KEY(topicoIdFK,artIdFK)

	CONSTRAINT FK_Art_Topico_Topico
	FOREIGN KEY(topicoIdFK) REFERENCES Topico(topicoIdPK)
		ON DELETE CASCADE,

	CONSTRAINT FK_Art_Topico_Articulo
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

CREATE TABLE Topico(
	topicoIdPK INTEGER NOT NULL PRIMARY KEY IDENTITY(1,1),
	categoriaIdFK INTEGER NOT NULL, 

	CONSTRAINT FK_Topico_Categoria
	FOREIGN KEY(categoriaIdFK) REFERENCES Categoria(categoriaIdPK)
		ON DELETE CASCADE

);

CREATE TABLE Nucleo_Solicita_Articulo(
	nombreUsuarioFK VARCHAR(50) NOT NULL,
	artIdFK INTEGER NOT NULL,
	estadoSolicitud VARCHAR(20)

	CONSTRAINT PK_Nucleo_Solicita_Articulo PRIMARY KEY(nombreUsuarioFK,artIdFK)

	CONSTRAINT FK_Nucleo_Solicita_Articulo_Miembro
	FOREIGN KEY(nombreUsuarioFK) REFERENCES Miembro(nombreUsuarioPK)
		ON DELETE CASCADE,

	CONSTRAINT FK_Nucleo_Solicita_Articulo_Articulo
	FOREIGN KEY(artIdFK) REFERENCES Articulo(artIdPK)
		ON DELETE CASCADE

)

CREATE TABLE Nucleo_Revisa_Articulo(
	nombreUsuarioFK VARCHAR(50) NOT NULL,
	artIdFK INTEGER NOT NULL,
	estadoRevision VARCHAR(20),
	puntuacion float,
	comentarios VARCHAR(MAX)

	CONSTRAINT PK_Nucleo_Revisa_Articulo PRIMARY KEY(nombreUsuarioFK,artIdFK)

	CONSTRAINT FK_Nucleo_Revisa_Articulo_Miembro
	FOREIGN KEY(nombreUsuarioFK) REFERENCES Miembro(nombreUsuarioPK)
		ON DELETE CASCADE,

	CONSTRAINT FK_Nucleo_Revisa_Articulo_Articulo
	FOREIGN KEY(artIdFK) REFERENCES Articulo(artIdPK)
		ON DELETE CASCADE
)

