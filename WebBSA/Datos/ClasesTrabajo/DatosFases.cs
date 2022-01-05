using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades; 

namespace Datos
{
  public class DatosFases:IdatosFases
    {
        private static DatosFases _instancia = null;
        private DatosFases() { }
        public static DatosFases GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosFases();
            return _instancia;
        }

     

        public void Agregar (Fases fa, User usr)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usr));
            SqlCommand _comando = new SqlCommand("AgregarEquipo", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@cop", fa.Competencia.ID);
            _comando.Parameters.AddWithValue("@nom", fa.Nombre);
            _comando.Parameters.AddWithValue("@num", fa.Numero);
            _comando.Parameters.AddWithValue("@sim", fa.Simula);
            _comando.Parameters.AddWithValue("@tip", fa.Tipo);
            _comando.Parameters.AddWithValue("@dat", fa.DatosSimula);
            _comando.Parameters.AddWithValue("@sfa", fa.CantSubfases);
            _comando.Parameters.AddWithValue("@eqp", fa.CantEquipos);
            _comando.Parameters.AddWithValue("@act", fa.Activo);


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

        public void Eliminar (Fases fa, UserSuperAdmin usa)

        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("eliminarFase", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@id", fa.ID);
       
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

        public void Modificar (Fases fa, User usr)

        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usr));
            SqlCommand _comando = new SqlCommand("ModificarFase", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@cop", fa.Competencia.ID);
            _comando.Parameters.AddWithValue("@id", fa.ID);
            _comando.Parameters.AddWithValue("@nom", fa.Nombre);
            _comando.Parameters.AddWithValue("@num", fa.Numero);
            _comando.Parameters.AddWithValue("@sim", fa.Simula);
            _comando.Parameters.AddWithValue("@tip", fa.Tipo);
            _comando.Parameters.AddWithValue("@dat", fa.DatosSimula);
            _comando.Parameters.AddWithValue("@sfa", fa.CantSubfases);
            _comando.Parameters.AddWithValue("@eqp", fa.CantEquipos);
            _comando.Parameters.AddWithValue("@act", fa.Activo);


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

        public Fases BuscarHaciaAbajo(int idFa)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("BuscarFaseHaciaAbajo", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@id", idFa);

            Fases fase = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    if (sqldr.Read())
                    {


                        fase = new Fases(idFa, (string)sqldr["nombre"], (int)sqldr["numero"],
                                         (int)sqldr["cantEquipos"], (int)sqldr["cantSubFase"],
                                         (string)sqldr["tipo"], (bool)sqldr["Simula"],
                                         (bool)sqldr["DatosSimula"], (bool)sqldr["Activo"],
                                         DatosSubfases.GetInstancia().SubFasexFase(idFa));
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
            return fase;
        }

        public Fases BuscarHaciaArriba(int idFa)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("BuscarFaseHaciaArriba", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@id", idFa);

            Fases fase = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read())
                    {                   
                        fase = new Fases(idFa, (string)sqldr["nombre"], (int)sqldr["numero"],
                                         (int)sqldr["cantEquipos"], (int)sqldr["cantSubFase"],
                                         (string)sqldr["tipo"], (bool)sqldr["Simula"],
                                         (bool)sqldr["DatosSimula"], (bool)sqldr["Activo"],
                                         DatosCompetencias.GetInstancia().BuscarHaciaArriba((int)sqldr["Competencia"]);
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
            return fase;
        }

        public List<Fases> ListarFasesdeCopa(int idCopa)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("FasexCopa", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@id", idCopa);

            List<Fases> lista = null; 
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    if (sqldr.Read())
                    {
                        lista.Add(new Fases((int)sqldr["idFa"], (string)sqldr["nombre"], (int)sqldr["numero"],
                                         (int)sqldr["cantEquipos"], (int)sqldr["cantSubFase"],
                                         (string)sqldr["tipo"], (bool)sqldr["Simula"],
                                         (bool)sqldr["DatosSimula"], (bool)sqldr["Activo"]));
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
