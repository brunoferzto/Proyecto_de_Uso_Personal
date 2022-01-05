using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
 public class DatosIntegrantes:IdatosIntegrantes
    {
        private static DatosIntegrantes _instancia = null;
        private DatosIntegrantes() { }
        public static DatosIntegrantes GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosIntegrantes();
            return _instancia;
        }

        public List<Integrantes> IntegrantexSubfase (int idSF)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("IntegrantexSubfase", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@idS", idSF);

            List<Integrantes> lista = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr  = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read())
                    {              
                        lista.Add(new Integrantes((int)sqldr["id"], (int)sqldr["pos"],
                            DatosEquipos.GetInstancia().BuscarEquipo((int)sqldr["Equipo"], (string)sqldr["Competidor"]),
                                                  (int)sqldr["pts"], (int)sqldr["pg"],
                                                  (int)sqldr["pe"], (int)sqldr["pp"],
                                                  (int)sqldr["gf"], (int)sqldr["gc"],
                                                  (int)sqldr["dg"]));

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

        public Integrantes Buscar(int idI)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("BuscarIntegrante", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@idS", idI);

            Integrantes integra = null;
            Equipos eqpo = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read())
                    {


                        integra = new Integrantes((int)sqldr["id"], (int)sqldr["pos"],
                                DatosEquipos.GetInstancia().BuscarEquipo((int)sqldr["Equipo"], (string)sqldr["Competidor"]),
                                                  (int)sqldr["pts"], (int)sqldr["pg"],
                                                  (int)sqldr["pe"], (int)sqldr["pp"],
                                                  (int)sqldr["gf"], (int)sqldr["gc"],
                                                  (int)sqldr["dg"]);
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

            return integra;
        }
        public void Modificar(Integrantes imod, UserSuperAdmin usa)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("ModificarIntegrante", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
  
            _comando.Parameters.AddWithValue("@nom", imod.ID);
            _comando.Parameters.AddWithValue("@eqp", imod.Equipo); // VER SU USO.
            _comando.Parameters.AddWithValue("@pos", imod.POS);
            _comando.Parameters.AddWithValue("@pts", imod.PTS);
            _comando.Parameters.AddWithValue("@dg", imod.DG);
            _comando.Parameters.AddWithValue("@gf", imod.GF);
            _comando.Parameters.AddWithValue("@gc", imod.GC);
            _comando.Parameters.AddWithValue("@pg", imod.PG);
            _comando.Parameters.AddWithValue("@pe", imod.PE);
            _comando.Parameters.AddWithValue("@pp", imod.PP);
            _comando.Parameters.AddWithValue("@pj", imod.PJ);

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

        public void Agregar(Integrantes imod,int idSf,SqlTransaction sqlt)
        {
            SqlCommand _comando = new SqlCommand("NuevoIntegrante", sqlt.Connection);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", imod.ID);
            _comando.Parameters.AddWithValue("@idS", idSf);
            _comando.Parameters.AddWithValue("@eqp", imod.Equipo); // VER SU USO.
            _comando.Parameters.AddWithValue("@pos", imod.POS);
            _comando.Parameters.AddWithValue("@pts", imod.PTS);
            _comando.Parameters.AddWithValue("@dg", imod.DG);
            _comando.Parameters.AddWithValue("@gf", imod.GF);
            _comando.Parameters.AddWithValue("@gc", imod.GC);
            _comando.Parameters.AddWithValue("@pg", imod.PG);
            _comando.Parameters.AddWithValue("@pe", imod.PE);
            _comando.Parameters.AddWithValue("@pp", imod.PP);
            _comando.Parameters.AddWithValue("@pj", imod.PJ);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                sqlt.Connection.Open(); 
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
                sqlt.Connection.Close();
            }

        }

        public void Eliminar(Integrantes imod, User usr)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usr));
            SqlCommand _comando = new SqlCommand("BorrarIntegrante", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", imod.ID);

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
