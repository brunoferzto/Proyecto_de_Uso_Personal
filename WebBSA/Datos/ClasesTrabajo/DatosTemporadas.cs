using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades; 

namespace Datos
{
  public class DatosTemporadas:IdatosTemporada
    {
        private static DatosTemporadas _instancia = null;
        private DatosTemporadas() { }
        public static DatosTemporadas GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosTemporadas();
            return _instancia;
        }

        public void TemporadaAgregar (Temporadas tempo, UserSuperAdmin usa )
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("AgregarTemporada", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", tempo.Nombre);
            _comando.Parameters.AddWithValue("@fco", tempo.FechaComienzo);

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
        public void TemporadaModificar(Temporadas tempo, User usr) 
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usr));
            SqlCommand _comando = new SqlCommand("ModificarTemporada", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", tempo.ID);
            _comando.Parameters.AddWithValue("@nom", tempo.Nombre);
            _comando.Parameters.AddWithValue("@num", tempo.Numero);
            _comando.Parameters.AddWithValue("@fco", tempo.FechaComienzo);
            _comando.Parameters.AddWithValue("@ffi", tempo.FechaFinal);
            _comando.Parameters.AddWithValue("@est", tempo.Estado);

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
        public void TemporadaEliminar(Temporadas tempo, UserSuperAdmin usa)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("EliminarTemporada", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@id", tempo.ID);

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
        public Temporadas BuscarHaciaArriba (int id)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("BuscarTemporada", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@id", id);

            Temporadas tempo = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();

                    tempo = new Temporadas((int)sqldr["id"], (string)sqldr["nombre"], (DateTime)sqldr["fco"],
                                (DateTime)sqldr["ffin"], (bool)sqldr["est"], (int)sqldr["num"]);
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
            return tempo;
        }
        public List<Temporadas> TemporadasListar()
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("ListarTemporadas", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;

            List<Temporadas> lista = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                  while(sqldr.Read())
                    {
                        lista.Add(new Temporadas((int)sqldr["id"], (string)sqldr["nombre"], (DateTime)sqldr["fco"],
                                (DateTime)sqldr["ffin"], (bool)sqldr["est"], (int)sqldr["num"], 
                                DatosCompetencias.GetInstancia().CompetenciaxTemporada((int)sqldr["id"])));
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

        //------------------------------------//
        public List<ComentariosTemporada> ComentariosdeTemporada (int idT)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("BuscarComentarios", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@id", idT);

            List<ComentariosTemporada> lista = null; 
            ComentariosTemporada comen = null;
            UserAdministrador usadm = null; 
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();

                    usadm = DatosUserAdmin.GetInstancia().Buscar((string)sqldr["usu"]);

                    comen = new ComentariosTemporada((int)sqldr["Id_Comentario"],usadm, (string)sqldr["comentario"]);

                    lista.Add(comen);
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

        public void AgregarComentario(string come,Temporadas tempo, UserAdministrador usAm)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usAm));
            SqlCommand _comando = new SqlCommand("AgregarComentario", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@com", come);
            _comando.Parameters.AddWithValue("@idt", tempo.ID);
            _comando.Parameters.AddWithValue("@nic", usAm.Nick);

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

        //solo modifica el string
        public void ModificarComentario(ComentariosTemporada come, UserAdministrador usAm)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usAm));
            SqlCommand _comando = new SqlCommand("AgregarComenario", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@ide", come.ID);
            _comando.Parameters.AddWithValue("@nom", come.Comentario);
            _comando.Parameters.AddWithValue("@nic", usAm.Nick); // No se cambia usu, se verifica. 


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

        //el admin creador o cualquier superademin. 
        public void EliminarComentario(ComentariosTemporada come, User usr)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usr));
            SqlCommand _comando = new SqlCommand("EliminarComentario", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", come.Comentario);
            _comando.Parameters.AddWithValue("@usu", come.Usuario.Nick);

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
    }
}
