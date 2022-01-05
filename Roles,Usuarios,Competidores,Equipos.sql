use bsadb
--ROLES
Create role rol_usa -- Super Admin
go

exec master..sp_addsrvrolemember [IIS APPPOOL\DefaultAppPool], 'SecurityAdmin'
go

Create role rol_adm --  Admin
go

exec master..sp_addsrvrolemember [IIS APPPOOL\DefaultAppPool], 'SecurityAdmin'
go

---- SUPER ADMINISTRADOR--- 
Create Procedure AgregarSuperAdministrador @nic varchar(15),@nom varchar (15),@pas varchar (15)
as
begin
if exists (Select * from Usuarios where Usuarios.NickName = @nic)
	return -1

Else
	begin transaction 
	Insert into Usuarios (NickName,Contraseña) Values (@nic,@pas)
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -2
	end
	
	Insert into SuperAdmin (Usuario,Nombre,Activo) values (@nic,@nom) 
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -2
	end

	Commit Transaction 
		exec sp_addsrvrolemember @nic, 'SecurityAdmin'
		exec sp_addrolemember @rolename = rol_usa, @membername = @nic
		exec sp_addrolemember @rolename = db_owner, @membername = @nic
		exec sp_addrolemember @rolename = db_accessadmin, @membername = @nic
		return 1
		End
go

Create Procedure ModificarSuperAdministrador @nic varchar(15),@nom varchar (15),@pas varchar (15), @est bit
as 
begin
if not exists (Select * from Usuarios where Usuarios.NickName = @nic)
	return -1
else
	Begin Transaction
	Update Usuarios set Usuarios.Contraseña = @pas where Usuarios.NickName = @nic
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -2
	end
	
	Update SuperAdmin set SuperAdmin.Activo = @est, SuperAdmin.Nombre = @nom where SuperAdmin.Usuario = @nic
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -2
	end

	Commit Transaction 
		return 1
		End
go

Create Procedure EliminarSuperAdministrador  @nic varchar(15),@pas varchar (15)
as
begin
if not exists (Select * from Usuarios where Usuarios.NickName = @nic)
	return -1

	Begin Transaction

	Delete SuperAdmin Where SuperAdmin.Usuario = @nic
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -2
	end

	Delete Usuarios Where Usuarios.NickName = @nic
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -2
	end

	Declare @sentencia varchar(200)
	Set @sentencia = 'Drop User [' + CAST(@nic as varchar (15)) + ']'
	Execute (@sentencia)
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -3
	end

	Declare @otrasen varchar (200)
	Set @otrasen = 'Drop Login [' + CAST(@nic as varchar (15)) + ']'
	Execute (@otrasen)
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -3
	end

		Commit Transaction 
		return 1
		end
go

Create Procedure SuperAdministradorLogueo @nic varchar(15),@pas varchar (15)
as
begin
	Select *  From Usuarios inner join SuperAdmin
	on Usuarios.NickName = SuperAdmin.Usuario
	Where SuperAdmin.Usuario = @nic and Usuarios.Contraseña = @pas and SuperAdmin.Activo = 1
	end
go

Create Procedure BuscarSuperAdministrador  @nic varchar(15)
as
begin
Select SuperAdmin.Activo, SuperAdmin.Nombre,SuperAdmin.Usuario From SuperAdmin Where SuperAdmin.Usuario = @nic
end
go

---- ADMINISTRADOR --
Create Procedure AgregarAdministrador @nic varchar(15),@nto date,@pas varchar (15)
as
begin
if exists (Select * from Usuarios where Usuarios.NickName = @nic)
	return -1

Else
	begin transaction 
	Insert into Usuarios (NickName,Contraseña) Values (@nic,@pas)
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -2
	end
	
	Insert Into Administrador (Usuario,FechaNTO) Values (@nic,@nto)
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -2
	end

	Commit Transaction 
		exec sp_addsrvrolemember @nic, 'SecurityAdmin'
		exec sp_addrolemember @rolename = rol_adm, @membername = @nic
		exec sp_addrolemember @rolename = db_owner, @membername = @nic
		exec sp_addrolemember @rolename = db_accessadmin, @membername = @nic
		return 1
end
go

Create Procedure ModificarAdministrador @nic varchar(15),@nto date,@pas varchar (15)
as
begin
if not exists (Select * from Usuarios where Usuarios.NickName = @nic)
		return -1
else
	Begin Transaction
	Update Usuarios set Usuarios.Contraseña = @pas where Usuarios.NickName = @nic
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -2
	end
	
	Update Administrador set FechaNTO = @nto where Administrador.Usuario = @nic
	if @@ERROR <> 0
	begin
	rollback transaction 

	return -2
	end

	Commit Transaction 
		return 1
		End
		
go

Create Procedure EliminarAdministrador  @nic varchar(15),@nto date,@pas varchar (15)
AS
Begin
if not exists (Select * from Usuarios where Usuarios.NickName = @nic)
		return -1
If exists(Select * From UsyCo Where UsyCo.Usuario = @nic)
		return -2

		Begin Transaction

	Delete Administrador Where Administrador.Usuario = @nic
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -3
	end

	Delete Usuarios Where Usuarios.NickName = @nic
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -3
	end

	Declare @sentencia varchar(200)

	Set @sentencia = 'Drop User [' + CAST(@nic as varchar (15)) + ']'
	Execute (@sentencia)
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -4
	end

	Set @sentencia = 'Drop Login [' + CAST(@nic as varchar (15)) + ']'
	Execute (@sentencia)
	if @@ERROR <> 0
	begin
	rollback transaction 
	return -4
	end

		Commit Transaction 
		return 1
		end
go

Create Procedure AdministradorLogueo  @nic varchar(15),@pas varchar (15)
as
Begin
Select * From Usuarios inner join Administrador
	on Usuarios.NickName = Administrador.Usuario
	Where Administrador.Usuario = @nic and Usuarios.Contraseña = @pas
	end
go

Create Procedure BuscarAdministrador @nic varchar (15)
as
Begin
Select * From Administrador Where Administrador.Usuario = @nic  
	end
go

-- ANOTACIONES DE USUARIO ADMINISTRADOR --

Create Procedure AgregarAnotacion @men nvarchar, @pub bit, @usu varchar (15)
as
begin
if not exists(Select * From Administrador Where Administrador.Usuario = @usu)
	return -1
else
INSERT INTO Anotaciones (Id_Nota,Anotacion,Usuario,Fecha) Values (@men,@usu)
if @@ERROR <> 0
begin
return -2
end
else
return 1
end
go

Create Procedure ModificarAnotacion @id int, @men nvarchar, @usu varchar (15), @fec date
as
begin
If not exists(Select * From Anotaciones Where Anotaciones.Id_Nota = @id and Anotaciones.Usuario = @usu)
	return -1

Else
Update Anotaciones set Anotaciones.Anotacion = @men, Anotaciones.Fecha = @fec Where Anotaciones.Id_Nota = @id
if @@ERROR <> 0
begin
return -2
end
else
return 1
end
go

Create Procedure EliminarAnotacion  @id int,@usu varchar (15)
as
begin
If not exists(Select * From Anotaciones Where Anotaciones.Id_Nota = @id and Anotaciones.Usuario = @usu)
	return -1

Delete From Anotaciones Where Anotaciones.Id_Nota = @id
if @@ERROR <> 0
begin
return -2
end
else
return 1
end
go

Create Procedure ListarAnotaciones @usu varchar (15)
as
begin
Select * From Anotaciones Where Anotaciones.Usuario = @usu
end
go
-- 

---- COMPETIDORES ---

Create Procedure BuscarCompetidor @nom varchar (20)
as
Begin
Select * From Competidores Where Competidores.Nombre = @nom
end
go

Create Procedure ListarCompetidores 
as
Begin
Select * from Competidores
end 
go

Create Procedure AgregarCompetidor @nom varchar (20), @usu varchar(15)
as
Begin 
If  not exists (Select * from Usuarios where Usuarios.NickName = @usu)
		return -1
If exists (Select * From UsyCo Where UsyCo.Usuario = @usu or UsyCo.Competidor = @nom)
	return -3

		Begin Transaction 
		Insert into Competidores (Nombre,Activo) values (@nom)
		if @@ERROR <> 0
		begin
		rollback transaction 
		return -2
		end

		Insert into UsyCo(Usuario,Competidor) values (@usu,@nom)
		if @@ERROR <> 0
		begin
		rollback transaction 
		return -2
		end

Commit Transaction 
		return 1
end
go

Create Procedure ModificarCompetidor @nom varchar(20), @act bit
as
Begin
	Update Competidores set Competidores.Activo = @act where Competidores.Nombre = @nom
	if @@ERROR <> 0
		begin
		return -1
		end
end
go

Create Procedure EliminarCompetidor @nom varchar(20)
as
begin
If exists (Select * From Utilizan Where Utilizan.Competidor = @nom)
	return -1

	Begin Transaction
		Delete From UsyCo Where UsyCo.Competidor = @nom
		if @@ERROR <> 0
		begin
		rollback transaction 
		return -2
		end
		
		Delete From Competidores Where Competidores.Nombre = @nom
		if @@ERROR <> 0
		begin
		rollback transaction 
		return -2
		end
Commit Transaction 
		return 1
end
go

Create Procedure CompetidorAsociado @usu varchar(15)
as 
begin
Select Competidores.Nombre, Competidores.Activo From Competidores INNER JOIN UsyCo on Competidores.Nombre = UsyCo.Competidor where UsyCo.Usuario = @usu
end    --- FALTANTE C#
go


---- EQUIPOS -----

Create Procedure AgregarEquipo @nom varchar (20), @nco varchar (60), @ciu varchar (25),@pai varchar (25), @est varchar (30), @cap int, @fun date, @sel bit
as
begin
Insert Into Equipos (NombreCompleto,Nombre,Ciudad,Nacionalidad,Fechafundado,Estadio,Capacidad,Seleccion) values (@nco,@nom,@ciu,@pai,@est,@cap,@fun,@sel)
if @@ERROR <> 0
		begin
		return -1
		end
else
return 1
end
go

Create Procedure ModificarEquipo @nom varchar (20), @nco varchar (60), @ciu varchar (25),@pai varchar (25), @est varchar (30), @cap int, @fun date, @sel bit
as 
begin
If not exists (Select * From Equipos Where Equipos.NombreCompleto = @nco)
	return -1
else
Update Equipos set Equipos.Nombre = @nom, Equipos.Ciudad = @ciu, Equipos.Nacionalidad = @pai, Equipos.Estadio = @est, Equipos.Capacidad = @cap,  Equipos.Fechafundado = @fun, Equipos.Seleccion = @sel Where Equipos.NombreCompleto = @nco
if @@ERROR <> 0
		begin
		return -2
		end
else
return 1
end
go

Create Procedure EliminarEquipo @nco varchar (60)
as
begin
If exists (Select * from Integrantes Where Integrantes.EquipoI = @nco)
	return -1
Else If exists (Select * From Utilizan Where Utilizan.Equipo = @nco)
	return -1
Else
	Delete  From Equipos Where Equipos.NombreCompleto = @nco
if @@ERROR <> 0
		begin
		return -2
		end
else
return 1
end
go

Create Procedure AltaRelacionEquipoyCompetidor @nco varchar (60), @com varchar (20)
as
begin
If exists (Select * From Utilizan Where Utilizan.Equipo = @nco and Utilizan.Estado = 1)
	return -1 
	

else If exists (Select * From Utilizan Where Utilizan.Equipo = @nco and Utilizan.Competidor = @com)
	begin
	Update Utilizan set Utilizan.Estado = 1 where  Utilizan.Equipo = @nco and Utilizan.Competidor = @com
	if @@ERROR <> 0		
		return -2
		
	end
Else
   begin
 Insert Into Utilizan (Competidor,Equipo,Estado) Values (@com,@nco)
   if @@ERROR <> 0
		return -3
		
	else
		return 1
	end
end
	
	go

Create Procedure BajaRelacionEquipoyCompetidor @nco varchar (60), @com varchar (20)
as
Begin
Update Utilizan set Utilizan.Estado = 0 where  Utilizan.Equipo = @nco and Utilizan.Competidor = @com
 if @@ERROR <> 0
		return -1
else
		return 1
	end
go

Create Procedure BuscarEquipo @nom varchar (60), @com varchar (20)
as
begin
Select Equipos.*, Utilizan.Competidor 
From Equipos inner join Utilizan on Equipos.NombreCompleto = Utilizan.Equipo
where Utilizan.Equipo = @nom and Utilizan.Competidor = @com
end
go


Create Procedure BuscarEquipoActivo @nom varchar (60), @com varchar (20)
as
begin
Select Equipos.*, Utilizan.Competidor 
From Equipos inner join Utilizan on Equipos.NombreCompleto = Utilizan.Equipo
where Utilizan.Equipo = @nom and Utilizan.Competidor = @com and Utilizan.Estado = 1
end
go
	
	

