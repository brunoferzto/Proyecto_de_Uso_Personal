using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
   public class DatosReglas:IdatosReglas
    {
        private static DatosReglas _instancia = null;
        private DatosReglas() { }
        public static DatosReglas GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosReglas();
            return _instancia;
        }

        public void AgregarReglas(ReglasxPosicion reg, User usr)
        {
            SqlConnection  sqlc = new SqlConnection(Conexion.ConexionUsuario(usr));
            SqlCommand _comando = new SqlCommand("NuevoIntegrante", sqlc);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@pos", reg.Posicion);
            _comando.Parameters.AddWithValue("@pts",reg.PtstablaH);
            _comando.Parameters.AddWithValue("@eli", reg.Eliminado);
            _comando.Parameters.AddWithValue("@cla", reg.Clasifica);
            _comando.Parameters.AddWithValue("@des", reg.Desciende);



            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                sqlc.Open();
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
                sqlc.Close();
            }
        }

        public void EliminarRegla (ReglasxPosicion reg, UserSuperAdmin usa)
        {
            SqlConnection sqlc = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("NuevoIntegrante", sqlc);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@idF", reg.ID);


            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                sqlc.Open();
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
                sqlc.Close();
            }
        }

        public void ModificarRegla(ReglasxPosicion reg, UserSuperAdmin usa)

        {
            SqlConnection sqlc = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("ModificarRegla", sqlc);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@idF", reg.ID);
            _comando.Parameters.AddWithValue("@idF", reg.Posicion);
            _comando.Parameters.AddWithValue("@idF", reg.PtstablaH);
            _comando.Parameters.AddWithValue("@idF", reg.Eliminado);
            _comando.Parameters.AddWithValue("@idF", reg.Desciende);
            _comando.Parameters.AddWithValue("@idF", reg.Clasifica);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                sqlc.Open();
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
                sqlc.Close();
            }
        }

        public List<ReglasxPosicion> ReglasxFase (int faid)
       {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("ReglaxFase", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@fase", faid);

            List<ReglasxPosicion> lista = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read())
                    {
                        lista.Add(new ReglasxPosicion((int)sqldr["id"], (int)sqldr["posicion"], (int)sqldr["ptsth"],
                                                    (bool)sqldr["clasifica"], (bool)sqldr["eliminado"], (bool)sqldr["desciende"]));
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

        public List<ReglasxPosicion> ReglasxSubFase(int faid)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("ReglaxSubFase", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@fase", faid);

            ReglasxPosicion regla;
            List<ReglasxPosicion> lista = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read())
                    {
                        lista.Add(new ReglasxPosicion((int)sqldr["id"], (int)sqldr["posicion"], (int)sqldr["ptsth"],
                                                 (bool)sqldr["clasifica"], (bool)sqldr["eliminado"], (bool)sqldr["desciende"]));
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
