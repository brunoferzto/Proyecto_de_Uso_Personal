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

Use bsadb	
go
-----------------------------------------------------------------------------------
Create Table Usuarios
(
NickName varchar (15) primary key,
Contrase�a varchar (15) not null check ((len([Contrase�a])>(5) AND [Contrase�a] like '%[0-9]%' 
AND [Contrase�a] <> Lower([Contrase�a]) COLLATE Latin1_General_CS_AI)),
Activo bit not null Default (1)
)
--REGISTRO DE LOGUEO HISTORICO XML - NICKNAME,TIPO DE USUARIO,FECHA LOGUEO, acciones en db. 
go


Create Table Administrador -- puede ejecutar algunos sp. 
(
Usuario varchar (15) not null primary key foreign key references Usuarios (NickName),
FechaNTO date not null
)
go

Create Table SuperAdmin -- puede ejecutar todos los sp.
(
Usuario varchar (15) not null primary key foreign key references Usuarios (NickName),
Nombre varchar (20) not null
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
Usuario varchar (15) foreign key references Administrador (Usuario),
Competidor varchar (20)  foreign key references Competidores (Nombre)
primary key (Usuario,Competidor)
)

Create Table Anotaciones
(
Id_Nota int primary key identity,
Anotacion nvarchar,
Usuario varchar (15) foreign key references Administrador (Usuario),
Fecha date not null default(GETDATE())
)
go


Create Table Equipos  -- cambiar id 
(
Id_Equipo int primary key identity,
NombreCompleto varchar(60),
Nombre varchar (20) not null,
Ciudad varchar (25) not null,
Nacionalidad varchar (25), --en selecciones va null
Fechafundado date not null, 
Estadio varchar (30) not null,
Capacidad int not null check (Capacidad >= 0),
Seleccion bit not null
)
go

--NO ESTA EN C# 
Create Table Clasicos
(
Nombre varchar(40) not null primary key,
EquipoI  int not null foreign key references Equipos (Id_Equipo),
EquipoII  int not null foreign key references Equipos (Id_Equipo)
)
go

--NO ESTA EN C#
Create Table Utilizan -- UN EQUIPO ETERNO NO PUEDE QUEDAR ELIMINADO CON EL COMPPETIDOR (Estado SIEMPRE EN TRUE)
(
Competidor varchar (20)  foreign key references Competidores (Nombre),
Equipo  int  foreign key references Equipos (Id_Equipo),
Estado bit not null Default (1), -- ESTADO CON EL COMPETIDOR
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
Estado bit default (1) not null --no se le pueden agregar mas copas a una temporada ya finalizada Ni tener dos activa en paralelo
)
go

Create Table ComentariosTemporada -- string pk, pk modificable? no, mejorar.
(
Id_Comentario int primary key identity,
Comentario varchar (300),
Temporada int not null foreign key references Temporadas (Id_Temporada),
Usuario varchar (15) not null foreign key references Usuarios (NickName),
)
go

Create Table Competencias 
(
Id_Commpetencia int primary key identity, 
Temporada int not null foreign key references Temporadas (Id_Temporada),
TipodeTorneo varchar not null check (TipodeTorneo = 'BSA' or TipodeTorneo = 'Mundial' or TipodeTorneo = 'Copa' or TipodeTorneo = 'No Oficial' or TipodeTorneo = 'Oficial Menor Equipos' or  TipodeTorneo = 'Oficial Menor Selecciones' or  TipodeTorneo = 'Otro' ), --1 BSA, 2 Copa, 3 Mundial, 4 Trofeo Menor, 5 Otros Trofeos
Nombre varchar (30) not null, 
Numero int not null, --��?x algo  lo puse ver antes d orrar
Trofeo varchar (255),
Descripcion varchar (500), 
Estado bit default (1), --no se le pueden agregar mas fases a una competencia ya finalizada
)
go


Create Table Fases
(
Id_Fase int primary key IDENTITY,
Numero int not null, -- se puede ingresar a mano, aunque se hace automaticamente contando.  Puede haber dos fases con mismo num y mismo todo.menos id
Nombre varchar (30) not null,
Competencia INT not null foreign key references Competencias (Id_Commpetencia),
Cant_Equipos int not null, 
Cant_Subfases int not null, 
Tipo varchar check (Tipo = 'Eliminatoria' or Tipo = 'Liga'), 
DatosSimulados bit Default (0), -- ?
Activo bit not null -- no se le pueden agregar mas subfases a una fase finalizada
)
go

--Distriucion equitativa?


Create Table Subfases 
(
Id_Gru int primary key identity (1,1),
Fase  int not null foreign key references  Fases (Id_Fase), 
NombreSubfase varchar (20) not null, -- El usuario elige Eliminatoria o Fase de Grupos + el n�mero automatico O TAL Vez concatenar.
Tipo varchar check (Tipo = 'Eliminatoria' or Tipo = 'Grupo'), -- por si hay alguna que se agrega despues.
Numero int not null,
RespetaReglas bit Default (1)-- al agregar una subfase a mano recordarlo
)
go

Create Table ReglaxPosici�n -- PARA CADA FASE o SUBFASE SI NO RESPETA REGLAS
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
EquipoI int not null,
CompetidorI  varchar (20) not null,
POS int not null default (0), -- ordenacion
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
Equipo int not null,
Competidor varchar (20) not null,
Subfase int not null foreign key references Subfases (Id_Gru),
Pos int not null,
foreign key (Competidor,Equipo) references Utilizan (Competidor,Equipo)
)
go

Create Table TablaHistorica -- se hacen ambas tablas h. con esta tabla. 
(
Id_THist int primary key identity(1,1),
Equipo int not null, -- fk 
Competidor  varchar (20) not null,-- fk
PTS int not null Check (PTS > 0), --Se necesita la informacion completa, Subfase, Fase, Competencia.
NombreFase varchar (30) not null, 
Subfase int not null foreign key references Subfases (Id_Gru), -- en c# se debe poder llegar hasta temporadas escalando. 
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

EquipoLocal int not null,
CompetidorLocal  varchar (20) not null,
ResultadoLocal int not null,

EquipoVisita int not null,
CompetidorVisita  varchar (20) not null,
ResultadoVisitante int,

FechaJugado smalldatetime not null default (Getdate()),
Pertenecen int foreign key references Subfases (Id_Gru), -- ver esto, poner fase tmb . ACTUALIZACION: Se debe poner Subfase, y posteriormente 
Simulado bit not null,								-- crear la Fase hacia arriba (sin lista de Subfase)  y la copa sin lista de fase (MEJORAR ESTO)
Estadio varchar (30) not null , -- De donde sale estadio?  . EquipoLocal.Estadio 
Fecha varchar (15) not null,
foreign key (CompetidorLocal,EquipoLocal) references Utilizan (Competidor,Equipo),
foreign key (CompetidorVisita,EquipoVisita) references Utilizan (Competidor,Equipo)
)
go

Create Table Puntuacion-- Mejr asi menos join. // tipo = especiales o partidos? // ACTUALIZACION: AMBOS PG,PE,PP Y PUNTOS_
(
id_Pts int primary key identity,
Competidor varchar (20) not null  foreign key references Competidores (Nombre),
Competencia INT not null foreign key references Competencias (Id_Commpetencia),
PG int not null Default (0),
PE int not null Default (0),
PP int not null Default (0),
PTS int not null Default (0) -- <--- Partidos especiales
)
go
-- un competidor tiene un registro 'puntuacion' por competencia.

Create Table TandaPenales
(
Partido int primary key foreign key references Partidos (Id_Partido),
ResultadoLocal int not null,
ResultadoVisit int not null
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
--ver si es necesario el not null en los bit


Create Table ComentariosPartidos
(
Id_Comentario int primary key identity (1,1),
Partido int not null foreign key references Partidos (Id_Partido),
Competidor varchar (20)  foreign key references Competidores (Nombre),
Comentario varchar (100) not null,
Video varchar (300)
)
go

Create Table Asociacion -- un Jug puede asociarse a varios equipos.
(
Id_Aso int primary key identity(1,1),
Equipo  int not null,
Competidor  varchar (20) not null,
Jugador float not null foreign key references Jugadores (Id_Jug),
Competencia int foreign key references Competencias (Id_Commpetencia), -- si finalmente va, puede tener nulos. <-- Temporada es mejor
foreign key (Competidor,Equipo)  references  Utilizan (Competidor,Equipo),
Estado bit not null -- las asociaciones tienen los dos tipos de bajas. mas usado sera la baja logica. 
)
go

Create	Table	Datos 
(
Id_Dato int primary key identity,
Partido int not null foreign key references Partidos (Id_Partido),
Minuto int not null, 
Equipo int not null,
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
Equipo  int  not null,
Competidor  varchar (20) not null,
FechaLesion date not null, ---Representaran fechas ficticias. no getdate
FechaVuelta date not null,
DiasLesion int not null,
Curado bit not null Default (0),
foreign key (Competidor,Equipo)  references  Utilizan (Competidor,Equipo)

)
go

Create Table DatosGeneralesxJugador -- PARA ANTIGUOS REGISTROS, LOS DATOS LLENARAN LOS NUEVOS. 
(
Id_Gen int primary key identity (1,1),
Jugador float not null foreign key references Jugadores (Id_Jug),
Competencia INT not null foreign key references Competencias (Id_Commpetencia),
Equipo  int  not null,
Competidor  varchar (20) not null,
foreign key (Competidor,Equipo)  references  Utilizan (Competidor,Equipo),
Gol int not null Check (Gol >= 0),
Asi int not null Check (Asi >= 0),
Ama int not null Check (Ama >= 0) ,
Roj int not null Check (Roj >= 0),
Glo int not null Default (0)
)
go

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
Nombre varchar not null Check (Nombre = 'Campe�n' or Nombre = 'Sub-Campe�n'),
Equipo int not null,
Competidor varchar (20) not null,
foreign key (Competidor,Equipo) references Utilizan (Competidor,Equipo)
)
go



Create Table Premios
(
Id_Premio int primary key identity(1,1),
Competencia INT not null foreign key references Competencias (Id_Commpetencia),
Motivo varchar (70) not null, -- ej Campe�n TaLA de cruces temporada xx
Competidor varchar (20) foreign key references Competidores (Nombre),
Fecha date not null Default (GetDate()),
Activo bit not null Default (0),-- usado o no
)
go

Create Table Fichajes 
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Jugador float not null foreign key references Jugadores (Id_Jug),
Equipo  int foreign key references Equipos (Id_Equipo),
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
Equipo  int foreign key references Equipos (Id_Equipo),
)
go

Create Table ComodinInmunidad
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Equipo  int foreign key references Equipos (Id_Equipo),
Usado bit not null Default (0),
)
go

Create Table ComodinRetencion
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Equipo  int not null  foreign key references Equipos (Id_Equipo),
Jugador float not null foreign key references Jugadores (Id_Jug),
Usado bit not null Default (0)
)
go


Create Table Ediccion 
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Jugador float not null foreign key references Jugadores (Id_Jug),
PTS int not null Check (PTS > 0)
)
go

Create Table Eterno
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Equipo  int foreign key references Equipos (Id_Equipo)                                                                                                                                            
)
go                                                                                                                                                                                                                     

Create Table Otros 
(
Premio int not null  primary key foreign key references Premios (Id_Premio),
Info varchar (300),
)
go

create Table Cruces 
(
Id_Cruz int primary key identity(1,1),
Valor int not null Check (Valor >= 0) Default (0),
Nombre varchar (100) not null, 
Equipo int not null,
Competidor varchar (20) not null,
Competencia int  foreign key references Competencias (Id_Commpetencia),
Jugador float foreign key references Jugadores (Id_Jug),
foreign key (Competidor,Equipo) references Utilizan (Competidor,Equipo)
)
go

