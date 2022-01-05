using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    
    public class DatosCompetencias:IdatosCompetencia
    {
        private static DatosCompetencias _instancia = null;
        private DatosCompetencias() { }
        public static DatosCompetencias GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosCompetencias();
            return _instancia;
        }


        public void CompetenciaAgregar(Competencias copa, UserSuperAdmin usp)
        {         
                SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usp));
                SqlCommand _comando = new SqlCommand("AgregarCompetencia", _cnn);
                _comando.CommandType = System.Data.CommandType.StoredProcedure;
                _comando.Parameters.AddWithValue("@tem", copa.Tempo.ID);
                _comando.Parameters.AddWithValue("@tip", copa.Tipo);
                _comando.Parameters.AddWithValue("@nom", copa.Nombre);
                _comando.Parameters.AddWithValue("@tro", copa.Trofeo);
                _comando.Parameters.AddWithValue("@des", copa.Descripcion);

                SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
                _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
                _comando.Parameters.Add(_retorno);

                try
                {
                    _cnn.Open();
                    _comando.ExecuteNonQuery();

                    if ((int)_retorno.Value == -1)
                        throw new Exception("BD");
                    else if ((int)_retorno.Value == -2)
                    {
                        throw new Exception("BD:");
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
        public void CompetenciaModificar (Competencias copa, User usr)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usr));
            SqlCommand _comando = new SqlCommand("ModificarCompetencia", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@id", copa.Tempo.ID);
            _comando.Parameters.AddWithValue("@act", copa.Activo);
            _comando.Parameters.AddWithValue("@cop", copa.ID); // buscar
            _comando.Parameters.AddWithValue("@num", copa.Numero);
            _comando.Parameters.AddWithValue("@tip", copa.Tipo);
            _comando.Parameters.AddWithValue("@nom", copa.Nombre);
            _comando.Parameters.AddWithValue("@tro", copa.Trofeo);
            _comando.Parameters.AddWithValue("@des", copa.Descripcion);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {               
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("BD");
                else if ((int)_retorno.Value == -2)
                {
                    throw new Exception("BD:");
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
        public void CompetenciaEliminar(Competencias copa, UserSuperAdmin usa)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("EliminarCompetencia", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@tem", copa.ID);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("BD");
                else if ((int)_retorno.Value == -2)
                {
                    throw new Exception("BD:");
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
        public Competencias BuscarHaciaArriba (int id)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("Buscar", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@id", id);

            Competencias copa = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();
      

                     copa = new Competencias(DatosTemporadas.GetInstancia().BuscarHaciaArriba((int)sqldr["id"]), (int)sqldr["id"], (string)sqldr["nombre"], (string)sqldr["tipo"],
                        (string)sqldr["descripcion"], (string)sqldr["trofeo"], (bool)sqldr["activo"],
                        (int)sqldr["numero"]);

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

            return copa;
        }
        public List<Competencias> CompetenciaxTemporada(int idT)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("CopaxTemporada", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@id", idT);

            List<Competencias> lista = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read())
                    {

                        lista.Add(new Competencias((int)sqldr["id"], (string)sqldr["nombre"], (string)sqldr["tipo"],
                           (string)sqldr["descripcion"], (string)sqldr["trofeo"], (bool)sqldr["activo"],
                           (int)sqldr["numero"]));
                    }

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
