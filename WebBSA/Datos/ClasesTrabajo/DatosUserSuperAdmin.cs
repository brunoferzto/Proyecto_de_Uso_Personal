using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace Datos
{
    class DatosUserSuperAdmin:IdatosSuperAdmin
    {
        private static DatosUserSuperAdmin _instancia = null;
        private DatosUserSuperAdmin() { }
        public static DatosUserSuperAdmin GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosUserSuperAdmin();
            return _instancia;
        }

        public void AgregarSuperAdministrador(UserSuperAdmin nuevousa, UserSuperAdmin usactual)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usactual));
            SqlCommand _comando = new SqlCommand("AgregarSuperAdministrador", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nic", nuevousa.Nick);
            _comando.Parameters.AddWithValue("@nom", nuevousa.Nombre);
            _comando.Parameters.AddWithValue("@pas", nuevousa.Contraseña);

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
                    throw new Exception("Error en 'DB' al Agregar");


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
        public void ModificarSuperAdministrador(UserSuperAdmin nuevousa, UserSuperAdmin usactual)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usactual));
            SqlCommand _comando = new SqlCommand("ModificarSuperAdministrador", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nic", nuevousa.Nick);
            _comando.Parameters.AddWithValue("@nom", nuevousa.Nombre);
            _comando.Parameters.AddWithValue("@pas", nuevousa.Contraseña);
            _comando.Parameters.AddWithValue("@est", nuevousa.Activo);

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
        public void EliminarSuperAdministrador(UserSuperAdmin nuevousa, UserSuperAdmin usactual)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usactual));
            SqlCommand _comando = new SqlCommand("EliminarSuperAdministrador", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nic", nuevousa.Nick);
            _comando.Parameters.AddWithValue("@pas", nuevousa.Contraseña);

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
                    throw new Exception("Error en 'DB' al eliminar");
                else if ((int)_retorno.Value == -3)
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
        public UserSuperAdmin LogueoSuperAdministrador(UserAdministrador usAdm)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("SuperAdministradorLogueo", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@nic", usAdm.Nick);
            ocom.Parameters.AddWithValue("@con", usAdm.Contraseña);

            UserSuperAdmin usu = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();

                    usu = new UserSuperAdmin((bool)sqldr["Activo"], (string)sqldr["Nombre"], (string)sqldr["NickName"], "n/a");
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

            return usu;
        }

        //No muestra constraseña. // si no se utilizara, eliminar. 
        public UserSuperAdmin BuscarSuperAdmin(string nick)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("BuscarSuperAdministrador", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@nic", nick);

            UserSuperAdmin usa = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();

                    usa = new UserSuperAdmin((bool)sqldr["Activo"], (string)sqldr["Nombre"], (string)sqldr["NickName"], "n/a");
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

            return usa;
        }
    }
}
