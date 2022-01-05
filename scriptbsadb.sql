 Use master
go

If exists(Select * From SysDataBases Where name='bsadb')
Begin
	Drop Database bsadb
End
go

Create Database bsadb
On
(
	Name='bsadb',
	Filename='C:\db\bsadb.mdf'
)
go

Use master	
go

-- EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
-- GO
--EXEC sp_MSforeachtable @command1 = "DROP TABLE ?"
---- Ahora volver a habilitar la integridad referencial
-- EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
-- GO
-----------------------------------------------------------------------------------
Create Table Usuarios
(
NickName varchar (15) primary key,
Contraseña varchar (15) not null check ((len([Contraseña])>(5) AND [Contraseña] like '%[0-9]%' 
AND [Contraseña] <> Lower([Contraseña]) COLLATE Latin1_General_CS_AI)),
)
--REGISTRO DE LOGUEO HISTORICO XML - NICKNAME,TIPO DE USUARIO,FECHA LOGUEO, acciones en db.
go


Create Table Administrador -- puede ejecutar algunos sp. 
(
Usuario varchar (15) not null primary key foreign key references Usuarios (NickName),
FechaNTO date not null
)
go

Create Table SuperAdmin -- puede ejecutar todos los sp (MENOS MODIFICAR EQUIPO).
(
Usuario varchar (15) not null primary key foreign key references Usuarios (NickName),
Nombre varchar (20) not null,
Activo bit not null Default (1)-- otros SA pueden quitar la actividad. 
)
go

Create Table Competidores
(
Nombre varchar (20) primary key,
Activo bit not null Default (1)
)
go

Create Table UsyCo-- no existe clase.
(
Usuario varchar (15) foreign key references Usuarios (NickName),
Competidor varchar (20)  foreign key references Competidores (Nombre)
primary key (Usuario,Competidor)
)

Create Table Anotaciones
(
Anotacion varchar (250) primary key,
Publico bit not null default (0),
Usuario varchar (15) not null foreign key references Usuarios (NickName)
)
go


Create Table Equipos --recordar selecciones, estadios.
(
NombreCompleto varchar(60) primary key,
Nombre varchar (20) not null,
Ciudad varchar (20) not null,
Nacionalidad varchar (20) not null,
Fechafundado date not null, 
Estadio varchar (30) not null,
Capacidad int not null check (Capacidad >= 0),
Seleccion bit not null
)
go

Create Table Clasicos
(
Nombre varchar(40) not null primary key,
EquipoI  varchar(60) not null foreign key references Equipos (NombreCompleto),
EquipoII  varchar(60) not null foreign key references Equipos (NombreCompleto)
)
go

--NO ESTA EN C#
Create Table Utilizan -- UN EQUIPO ETERNO NO PUEDE QUEDAR ELIMINADO CON EL COMPPETIDOR (Estado SIEMPRE EN TRUE)
(
Competidor varchar (20)  foreign key references Competidores (Nombre),
Equipo  varchar(60)  foreign key references Equipos (NombreCompleto),
Estado bit not null, -- ESTADO CON EL COMPETIDOR
primary key (Competidor,Equipo)
)
go
-------------------------------------------------------------------------------
Create Table Temporadas 
(
Id_Temporada int  primary key identity,
Nombre varchar (30) not null, 
Numero int not null UNIQUE,
FechaComienzo date not null default(GETDATE()), 
FechaFinal date default (null),
Estado bit default (1) not null --no se le pueden agregar mas copas a una competencia ya finalizada Ni tener dos activa en paralelo
)
go

Create Table ComentariosTemporada
(
Comentario varchar (300) primary key,
Temporada int not null foreign key references Temporadas (Id_Temporada),
Usuario varchar (15) not null foreign key references Usuarios (NickName),

)
go

Create Table Competencias 
(
Id_Commpetencia int primary key identity, 
Temporada int not null foreign key references Temporadas (Id_Temporada),
TipodeTorneo varchar not null check (TipodeTorneo = 'BSA' or TipodeTorneo = 'Mundial' or TipodeTorneo = 'Copa' or TipodeTorneo = 'No Oficial' or TipodeTorneo = 'Oficial Menor Equipos' or  TipodeTorneo = 'Oficial Menor Selecciones' ), --1 BSA, 2 Copa, 3 Mundial, 4 Trofeo Menor, 5 Otros Trofeos
Nombre varchar (30) not null, 
Numero int not null,
Trofeo varchar (255),
Descripcion varchar (500), 
Estado bit default (1), --no se le pueden agregar mas fases a una competencia ya finalizada
)
go


Create Table Fases
(
Id_Fase int primary key IDENTITY,
Numero int not null, 
Nombre varchar (30) not null,
Competencia INT not null foreign key references Competencias (Id_Commpetencia),
Cant_Equipos int not null, 
Cant_Subfases int not null, 
Tipo varchar check (Tipo = 'Eliminatoria' or Tipo = 'Grupo' or Tipo = 'Ambos'),
Simula bit, --
DatosSimulados bit,
Activo bit not null -- no se le pueden agregar mas subfases a una fase finalizada
)
go


Create Table Subfases 
(
Id_Gru int primary key identity (1,1),
Fase  int not null foreign key references  Fases (Id_Fase), 
NombreSubfase varchar (20) not null, -- El usuario elige Eliminatoria o Fase de Grupos + el número automatico O TAL Vez concatenar.
Tipo varchar check (Tipo = 'Eliminatoria' or Tipo = 'Grupo'), -- por si hay alguna que se agrega despues.
Numero int not null,
RespetaReglas bit Default (1)-- al agregar una subfase a mano recordarlo
)
go

Create Table ReglaxPosición -- PARA CADA FASE 
(
Id_RegxPos int primary key identity,
Fase int  foreign key references Fases (Id_Fase),--ambas nulleable
Subfase int  foreign key references Subfases (Id_Gru),
Pos int not null, --EJ POS 1 DE FASE DE GRUPO OBTIENE 20 PTS T.HISTORICA, CLASIFICA Y NO QUEDA ELIMINADO,--EJ POS 1 OCTAVOS NO OBTIENE PUNTOS, CLASIFICA Y NO QUEDA ELIMINADO.
PtsHis int not null,
Clasifica bit not null, -- sOLO para presentacion. 
Eliminado bit not null, --  
Desciende bit not null, -- uno de estos dos atributos sera obligatoriamentre true y el otro false. 
)
go

Create Table Integrantes
(
Id_Integrante int primary key identity (1,1),
Subfase int not null foreign key references Subfases (Id_Gru),
EquipoI varchar (60) not null,
CompetidorI  varchar (20) not null,
POS int not null default (0), 
PTS As PG * 3 + PE,
PJ AS PG + PE + PP,
PG int default (0), 
PE int default (0), 
PP int default (0), 
GF int default (0),
GR int default (0), 
DG  AS GF - GR,
foreign key (CompetidorI,EquipoI) references Utilizan (Competidor,Equipo)
)
go

Create Table Eliminaciones
(
Id_Eliminacion int primary key identity (1,1),
Equipo varchar (60) not null,
Competidor varchar (20) not null,
--Competencia int not null foreign key references Competencias (Id_Commpetencia), -- tal vez no es un campo
Fase  int not null foreign key references  Fases (Id_Fase), 
Pos int not null,
foreign key (Competidor,Equipo) references Utilizan (Competidor,Equipo)
)
go
--------------------------------------------------------------------------------------------------------------------
Create Table Jugadores
(
Id_Jug float primary key,  -- ver si se puede cambiar a int -- al ingresarlos desde excel no es identity, luego si.
NombreCompleto varchar (50),
FechaNac smalldatetime not null,
--Foto varchar (255), -- el id sera la foto. 
Posicion varchar (20), 
Pais varchar (30), 
Pierna varchar (10)
)
go


Create Table Partidos 
(
Id_Partido int primary key identity(1,1),
EquipoLocal varchar (60) not null,
CompetidorLocal  varchar (20) not null,
ResultadoLocal int not null,
EquipoVisita varchar (60) not null,
CompetidorVisita  varchar (20) not null,
ResultadoVisitante int,
FechaJugado smalldatetime not null default (Getdate()),
Pertenecen int foreign key references Subfases (Id_Gru),
Simulado bit not null,
Estadio varchar (30) not null ,
Fecha varchar (15) not null ,
foreign key (CompetidorLocal,EquipoLocal) references Utilizan (Competidor,Equipo),
foreign key (CompetidorVisita,EquipoVisita) references Utilizan (Competidor,Equipo)
)
go

go
Create Table Puntuacion-- Mejr asi menos join. 
(
id_Pts int primary key identity,
Competidor varchar (20) not null  foreign key references Competidores (Nombre),
Competencia INT not null foreign key references Competencias (Id_Commpetencia),
PG int not null Default (0),
PE int not null Default (0),
PP int not null Default (0),
PTS int not null Default (0)
)
go

Create Table TandaPenales
(
Partido int primary key foreign key references Partidos (Id_Partido),
ResultadoLocal int not null,
ResultadoVisit int null,
)
go

Create Table DatodeTanda
(
Id_datoT int primary key identity,
Tipo varchar not null check (Tipo = 'Gol' or  Tipo = 'Atajado' or Tipo = 'Errado'),
Jugador float not null foreign key references Jugadores (Id_Jug),
Partido int  foreign key references TandaPenales (Partido)
)
go


Create Table ParEspeciales
(
Partido int primary key foreign key references Partidos (Id_Partido),
Valor int,  -- el valor de los especiales se calcula al insertar partido (normal).
Goleada bit not null,
PartidoEstrella bit not null, 
Derby bit not null,
PartidoalAzar bit not null,
NoPresenta bit not null, 
Retira bit not null, 
FinExpulsados bit not null
)
go



Create Table ComentariosPartidos
(
Id_Comentario int primary key identity (1,1),
Partido int not null foreign key references Partidos (Id_Partido),
Competidor varchar (20) not null  foreign key references Competidores (Nombre),-- usuario registrado.
Comentario varchar (100) not null,
Video varchar (300)
)
go

Create Table Asociacion -- un Jug puede asociarse a varios equipos.
(
Id_Aso int primary key identity(1,1),
Equipo  varchar (60) not null,
Competidor  varchar (20) not null,
Jugador float not null foreign key references Jugadores (Id_Jug),
Competencia int foreign key references Competencias (Id_Commpetencia), -- si finalmente va, puede tener nulos. 
foreign key (Competidor,Equipo)  references  Utilizan (Competidor,Equipo),
Estado bit not null -- las asociaciones tienen los dos tipos de bajas. mas usado sera la baja logica. 
)
go

Create	Table	Datos 
(
Id_Dato int primary key identity,
Partido int not null foreign key references Partidos (Id_Partido),
Minuto int not null, 
Equipo varchar (60) not null,
Competidor  varchar (20) not null,
Jugador float not null foreign key references Jugadores (Id_Jug),
foreign key (Competidor,Equipo)  references  Utilizan (Competidor,Equipo), -- gol y gol de penal = gol. 
TipodeDato varchar (30) check (TipodeDato = 'Gol' or TipodeDato = 'Asistencia' or TipodeDato = 'Amarilla' or
                               TipodeDato = 'Roja' or TipodeDato = 'Doble Amarilla' or TipodeDato = 'Porteria Imbatida'
							   or TipodeDato = 'Gol de Penal' or TipodeDato = 'Penal Atajado' or TipodeDato = 'Penal Errado'
							   or TipodeDato = 'Gol Propia Porteria') -- penales en un partido. foul o mano 
)
go

Create Table Sustituciones 
(
Id_Susti int primary key identity,
minuto int not null,
Partido int not null foreign key references Partidos (Id_Partido),
JugSale float not null foreign key references Jugadores (Id_Jug),
JugIngr float not null foreign key references Jugadores (Id_Jug)
)
GO

Create Table Lesiones
(
Id_Lesion int primary key identity,
Temporada int not null   foreign key references Temporadas (Id_Temporada),
Jugador float not null foreign key references Jugadores (Id_Jug),
Equipo  varchar(60)  not null,
Competidor  varchar (20) not null,
FechaLesion date not null, ---Representaran fechas ficticias. no getdate
FechaVuelta date not null,
DiasLesion int not null,
foreign key (Competidor,Equipo)  references  Utilizan (Competidor,Equipo),
Curado bit not null Default (0)
)
go

Create Table DatosGenerales -- PARA ANTIGUOS REGISTROS, LOS DATOS LLENARAN LOS NUEVOS. 
(
Id_Gen int primary key identity (1,1),
Jugador float not null foreign key references Jugadores (Id_Jug),
Competencia INT not null foreign key references Competencias (Id_Commpetencia),
Equipo  varchar(60)  not null,
Competidor  varchar (20) not null,
foreign key (Competidor,Equipo)  references  Utilizan (Competidor,Equipo),
Gol int not null Check (Gol >= 0),
Asi int not null Check (Asi >= 0),
Ama int not null Check (Ama >= 0) ,
Roj int not null Check (Roj >= 0),
Glo int not null Default (0)
)


-----------------------------------------------------------------------------------------------------------------------------+
Create Table Leyes
(
Id_Ley int primary key identity,
Ley varchar(500) not null,
Tipo varchar (30) not null check (Tipo = 'Generales' or Tipo = 'Premios' or Tipo = 'BSA'),
Fecha date not null Default (GetDate())
)
go

Create Table Palmares
(
Id_Pal int not null primary key identity,
Competencia INT not null foreign key references Competencias (Id_Commpetencia), -- NO se puede a una no finalizada.
Nombre varchar not null Check (Nombre = 'Campeón' or Nombre = 'Sub-Campeón'),
Equipo varchar (60) not null,
Competidor varchar (20) not null,-- fk
foreign key (Competidor,Equipo) references Utilizan (Competidor,Equipo)
)
go

Create Table TablaHistorica -- se hacen ambas tablas h. con esta tabla. 
(
Id_THist int primary key identity(1,1),
Equipo varchar (60) not null, -- fk 
Competidor  varchar (20) not null,-- fk
PTS int not null,
Subfase int not null foreign key references Subfases (Id_Gru), -- en c# se debe poder llegar hasta temporadas escalando. 
foreign key (Competidor,Equipo) references Utilizan (Competidor,Equipo)
)
go

Create Table Premios
(
Id_Premio int primary key identity(1,1),
Competencia INT not null foreign key references Competencias (Id_Commpetencia),
Motivo varchar (70) not null,
Competidor varchar (20) foreign key references Competidores (Nombre),
Fecha date not null Default (GetDate())
)
go

Create Table Fichajes 
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Jugador float not null foreign key references Jugadores (Id_Jug),
Equipo  varchar (60) foreign key references Equipos (NombreCompleto),
EquipoSale varchar (30) not null
)
go

Create Table Creacion
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
JugGlobal int not null,
FechaNac date not null,
Jugador float not null foreign key references Jugadores (Id_Jug),  --DEBE EXISTIR ANTES.
)
go

Create Table CesionesyLeyGK 
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Tipo varchar not null Check (Tipo = 'Cesion' or Tipo = 'Ley de Porteros'),
Jugador float not null foreign key references Jugadores (Id_Jug),
Equipo  varchar (60) foreign key references Equipos (NombreCompleto),
)
go

Create Table ComodinInmunidad
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Equipo  varchar (60) foreign key references Equipos (NombreCompleto),
Usado bit not null Default (0),
)
go

Create Table ComodinRetencion
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Equipo  varchar (60) not null  foreign key references Equipos (NombreCompleto),
Jugador float not null foreign key references Jugadores (Id_Jug),
Usado bit not null Default (0)
)
go


Create Table Ediccion 
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Jugador float not null foreign key references Jugadores (Id_Jug),
PTS int not null Check (PTS > 0),
)
go

Create Table Eterno
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Equipo  varchar (60) foreign key references Equipos (NombreCompleto)                                                                                                                                            
)
go                                                                                                                                                                                                                     

Create Table Otros 
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Info varchar (300),
)

Create Table Cruces 
(
Id_Cruz int primary key identity(1,1),
Valor int not null Check (Valor >= 0) Default (0),
Nombre varchar (100) not null, 
Equipo varchar (60) not null,
Competidor varchar (20) not null,
Competencia int  foreign key references Competencias (Id_Commpetencia),
Jugador float foreign key references Jugadores (Id_Jug),
foreign key (Competidor,Equipo) references Utilizan (Competidor,Equipo)
)
go
