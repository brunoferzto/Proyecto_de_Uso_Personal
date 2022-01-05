using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;


namespace Datos
{
   public class DatosUserAdmin: IdatosAdmin
    {
        private static DatosUserAdmin _instancia = null;
        private DatosUserAdmin() { }
        public static DatosUserAdmin GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosUserAdmin();
            return _instancia;
        }
        public void AgregarAdministrador (UserAdministrador usAdm, UserSuperAdmin usa)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("AgregarAdministrador", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nic", usAdm.Nick);
            _comando.Parameters.AddWithValue("@cont", usAdm.Contraseña);
            _comando.Parameters.AddWithValue("@nto", usAdm.FechaNTO);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("Error en 'Usuario.Nick' ya existe Usuario");
                else if ((int)_retorno.Value == -2)
                {
                    throw new Exception("Error en 'DB' al Agregar");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }

        public void ModificarAdministrador (UserAdministrador usAdm, User user)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(user));
            SqlCommand _comando = new SqlCommand("ModificarAdministrador", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", usAdm.Nick); // Solo su propio usuario o SuperAdmin. 
            _comando.Parameters.AddWithValue("@con", usAdm.Contraseña);
            _comando.Parameters.AddWithValue("@nto", usAdm.FechaNTO);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("Error en 'Usuario.Nick' no existe Usuario");
                else if ((int)_retorno.Value == -2)              
                    throw new Exception("Error en 'DB' al Modificar");
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }

        public void EliminarAdministrador(UserAdministrador usAdm, UserSuperAdmin usa)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("EliminarAdministrador", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nic", usAdm.Nick);
            _comando.Parameters.AddWithValue("@pas", usAdm.Contraseña);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("Error en 'Usuario.Nick' no existe Usuario");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("El Usuario está asociado a un Competidor");
                else if ((int)_retorno.Value == -3)               
                    throw new Exception("Error en 'DB' al Eliminar");
                else if ((int)_retorno.Value == -4)
                    throw new Exception("Error al Eliminar Usuario DB");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }

        public UserAdministrador Logueo (UserAdministrador usAdm)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("AdministradorLogueo", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@nic", usAdm.Nick);
            ocom.Parameters.AddWithValue("@con", usAdm.Contraseña);

            UserAdministrador usAd = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();
                    usAd = new UserAdministrador((DateTime)sqldr["FechaNTO"], (string)sqldr["NickName"], "n/d");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                ocox.Close();
            }

            return usAd;
        }

        //No muestra constraseña. // si no se utilizara, eliminar. 
        public UserAdministrador Buscar (string nick)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("BuscarAdministrador", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@nic", nick);

            UserAdministrador usAd = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();

                    usAd = new UserAdministrador((DateTime)sqldr["nacimiento"], (string)sqldr["nick"], "En tu Cora xD", (bool)sqldr["Activo"]);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                ocox.Close();
            }

            return usAd;
        }

        // ANOTACIONES 
        public void AgregarAnotacion(Anotaciones nota, UserAdministrador usAm)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usAm));
            SqlCommand _comando = new SqlCommand("AgregarAnotacion", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@men", nota.Anotacion);
            _comando.Parameters.AddWithValue("@usu", nota.Usuario.Nick);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("Error, No existe Usuario Administrador");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Error en 'DB' al Agregar ");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }

        //solo el admin creador
        public void ModificarAnotacion(Anotaciones nota, UserAdministrador usAm)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usAm));
            SqlCommand _comando = new SqlCommand("ModificarAnotacion", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@id", nota.ID);
            _comando.Parameters.AddWithValue("@men", nota.Anotacion);
            _comando.Parameters.AddWithValue("@fec", nota.Fecha);
            _comando.Parameters.AddWithValue("@usu", usAm.Nick); // No se cambia usu, se verifica.

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("Error, la anotación solo puede modificarla su creador, o no existe Anotación");
                else if ((int)_retorno.Value == -2)
                {
                    throw new Exception("Error en 'DB' al Modificar");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }
       
        public void EliminarAnotacion(Anotaciones nota, UserAdministrador usAm)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usAm));
            SqlCommand _comando = new SqlCommand("EliminarAnotacion", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@id", nota.ID);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("Error, la anotación solo puede eliminarla su creador, o no existe Anotación");
                else if ((int)_retorno.Value == -2)
                {
                    throw new Exception("Error en 'DB' al Eliminar");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }

        public List<Anotaciones> ListarAnotaciones (UserAdministrador usAm)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("ListarAnotaciones", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@usu", usAm);

            List<Anotaciones> lista = null;
            Anotaciones nota;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();

                    nota = new Anotaciones((int)sqldr["Id_Nota"],(string)sqldr["Anotacion"], usAm,(DateTime)sqldr["Fecha"]); 
      
                    lista.Add(nota);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ocox.Close();
            }
            return lista;
        }


    }
}
