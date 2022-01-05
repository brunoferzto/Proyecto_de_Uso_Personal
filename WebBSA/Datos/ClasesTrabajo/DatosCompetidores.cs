using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace Datos
{
   public class DatosCompetidores:IdatosCompetidores
    {
        private static DatosCompetidores _instancia = null;
        private DatosCompetidores() { }
        public static DatosCompetidores GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosCompetidores();
            return _instancia;
        }

        public Competidores CompetidorBuscar(string nom)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("BuscarCompetidor", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@nom", nom);

            Competidores competidor = null;  
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();
                    competidor = new Competidores((string)sqldr["nombre"], (bool)sqldr["Activo"]); 
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

            return competidor;           
        }

        public List<Competidores> CompetidorListar()
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            SqlCommand _comando = new SqlCommand("ListarCompetidores", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            List<Competidores> _lista = new List<Competidores>();

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        _lista.Add(new Competidores((string)_lector["Nombre"], (bool)_lector["Activo"]));
                    }
                }
                _lector.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
            return _lista;
        }

        public void CompetidorAgregar (Competidores com,User usr,UserSuperAdmin usa)
        {
            SqlConnection _cnn = new SqlConnection();
            SqlCommand _comando = new SqlCommand("AgregarCompetidor", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", com.Nombre);
            _comando.Parameters.AddWithValue("@usu", usr.Nick);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe Usuario, Debe asociar el Competidor a un Usuario existe");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Error en 'DB' al Agregar");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Error, El 'Usuario' o 'Competidor' ya esta Asociado");
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
        
        //solo su competidor o superadmin
        public void CompetidorModificar(Competidores com, User user)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(user));
            SqlCommand _comando = new SqlCommand("ModificarCompetidor", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", com.Nombre);
            _comando.Parameters.AddWithValue("@act", com.Activo);
            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
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

        public void CompetidorEliminar(Competidores com, UserSuperAdmin usa)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("EliminarCompetidor", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", com.Nombre);
            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("El Competidor tiene equipos ascociados");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Error en 'DB' al Eliminar");

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

    }
}
