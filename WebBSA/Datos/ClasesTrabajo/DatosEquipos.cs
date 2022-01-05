using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades; 

namespace Datos
{
   public class DatosEquipos:IdatosEquipos 
    {
        private static DatosEquipos _instancia = null;
        private DatosEquipos() { }
        public static DatosEquipos GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosEquipos();
            return _instancia;
        }
        public Equipos BuscarEquipo(int idEquipo,string ncompetidor)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("BuscarEquipo", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@ide", idEquipo);
            ocom.Parameters.AddWithValue("@com", ncompetidor);

            Equipos equipo = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();

                    equipo = new Equipos((int)sqldr["Id_Equipo"],(string)sqldr["nombre"], (string)sqldr["nomcompleto"], (string)sqldr["ciudad"],
                                         (DateTime)sqldr["fundado"], (string)sqldr["pais"], DatosCompetidores.GetInstancia().CompetidorBuscar((string)sqldr["Competidor"]),
                                         (bool)sqldr["activo"],
                                         (string)sqldr["estadio"], (int)sqldr["capacidad"], (bool)sqldr["seleccion"]);
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
            return equipo;

        }
        public Equipos BuscarEquipoActivo(int idEquipo)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("BuscarEquipoActivo", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@ide", idEquipo);

            Equipos equipo = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();

                    equipo = new Equipos((int)sqldr["Id_Equipo"], (string)sqldr["nombre"], (string)sqldr["nomcompleto"], (string)sqldr["ciudad"],
                                          (DateTime)sqldr["fundado"], (string)sqldr["pais"], DatosCompetidores.GetInstancia().CompetidorBuscar((string)sqldr["Competidor"]),
                                          (bool)sqldr["activo"],
                                          (string)sqldr["estadio"], (int)sqldr["capacidad"], (bool)sqldr["seleccion"]);
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
            return equipo;

        }
        public List<Equipos> BuscarEquipos(string nombre)
        {           
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            SqlCommand _comando = new SqlCommand("BuscarEquipos", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", nombre);

            List<Equipos> _lista = new List<Equipos>();
            try
            {
                _cnn.Open();
                SqlDataReader sqldr = _comando.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read())
                    {                    
                        _lista.Add(new Equipos((int)sqldr["Id_Equipo"], (string)sqldr["nombre"], (string)sqldr["nomcompleto"], (string)sqldr["ciudad"],
                                          (DateTime)sqldr["fundado"], (string)sqldr["pais"], DatosCompetidores.GetInstancia().CompetidorBuscar((string)sqldr["Competidor"]),
                                          (bool)sqldr["activo"],
                                          (string)sqldr["estadio"], (int)sqldr["capacidad"], (bool)sqldr["seleccion"]));
                    }
                }
                sqldr.Close();
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
        } // sin hacer sp
        public void EquipoAgregar (Equipos eqp,UserSuperAdmin usp)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usp)); 
            SqlCommand _comando = new SqlCommand("AgregarEquipo", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", eqp.Nombre);
            _comando.Parameters.AddWithValue("@nco", eqp.NombreCompleto);
            _comando.Parameters.AddWithValue("@ciu", eqp.Ciudad);
            _comando.Parameters.AddWithValue("@pai", eqp.Pais);
            _comando.Parameters.AddWithValue("@est", eqp.Estadio);
            _comando.Parameters.AddWithValue("@cap", eqp.Capacidad);
            _comando.Parameters.AddWithValue("@fun", eqp.Fundado);
            _comando.Parameters.AddWithValue("@sel", eqp.Seleccion);


            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
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
        public void EquipoModificar(Equipos eqp, UserSuperAdmin usa) 
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa)); 
            SqlCommand _comando = new SqlCommand("ModificarEquipo", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@ide", eqp.ID);
            _comando.Parameters.AddWithValue("@nom", eqp.Nombre);
            _comando.Parameters.AddWithValue("@nco", eqp.NombreCompleto);
            _comando.Parameters.AddWithValue("@ciu", eqp.Ciudad);
            _comando.Parameters.AddWithValue("@pai", eqp.Pais);
            _comando.Parameters.AddWithValue("@est", eqp.Estadio);
            _comando.Parameters.AddWithValue("@cap", eqp.Capacidad);
            _comando.Parameters.AddWithValue("@fun", eqp.Fundado);
            _comando.Parameters.AddWithValue("@sel", eqp.Seleccion);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("Error, no existe Equipo con el ID especificado");
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
        public void EquipoEliminar(Equipos eqp,UserSuperAdmin usp)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usp));
            SqlCommand _comando = new SqlCommand("EliminarEquipo", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@ide", eqp.ID);


            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("Error, no se puede eliminar equipo Activo o con Participaciones anteriores");
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
        public void AltaEquipoyCompetidor(Equipos eqp,User usr)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usr));
            SqlCommand _comando = new SqlCommand("AltaRelacionEquipoyCompetidor", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@ide", eqp.ID);
            _comando.Parameters.AddWithValue("@com", eqp.Competidor.Nombre);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("Error, el equipo esta activo");
                else if ((int)_retorno.Value == -2)                
                    throw new Exception("Error en 'DB' al Actualizar");
                else if ((int)_retorno.Value == -3)
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
        public void DescensoIndividual(Equipos eqp, User usu)
        {
            DescensoInactivo(null, eqp, usu);
        }
        //cuidado transacion y conexion ver ahí, probar
        internal void DescensoInactivo(SqlTransaction sqlt, Equipos eqp, User usu)
        {
            SqlConnection sqlc;

            if (sqlt != null)
                sqlc = sqlt.Connection;
            else
                sqlc = new SqlConnection(Conexion.ConexionUsuario(usu)); 

            SqlCommand _comando = new SqlCommand("BajaRelacionEquipoyCompetidor", sqlc);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@ID", eqp.ID);
            _comando.Parameters.AddWithValue("@com", eqp.Competidor.Nombre);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                if (sqlt != null)
                    _comando.Transaction = sqlt;
                else
                    sqlc.Open();

                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("Error en 'DB', no se pudo completar la acción");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if(sqlt == null)
                sqlc.Close(); 
            }
        }

        
    
    }
}
