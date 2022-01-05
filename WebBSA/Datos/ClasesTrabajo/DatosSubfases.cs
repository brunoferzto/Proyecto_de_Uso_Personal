using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class DatosSubfases:IdatosSubFase
    {
        private static DatosSubfases _instancia = null;
        private DatosSubfases() { }
        public static DatosSubfases GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosSubfases();
            return _instancia;
        }
        public void Agregar(SubFases sub, SqlTransaction sqlt)
        {
            SqlCommand _comando = new SqlCommand("AgregarSubfase", sqlt.Connection);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@fas", sub.Fase.ID);
            _comando.Parameters.AddWithValue("@fas", sub.Nombre);
            _comando.Parameters.AddWithValue("@fas", sub.Numero);
            _comando.Parameters.AddWithValue("@fas", sub.RespetaReglas);
            _comando.Parameters.AddWithValue("@fas", sub.Tipo);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {

                _comando.Transaction = sqlt;
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("BD");

                else if ((int)_retorno.Value == -2)
                {
                    throw new Exception("BD:");
                }


                foreach (Integrantes ninte in sub.Lista)
                    DatosIntegrantes.GetInstancia().Agregar(ninte, sub.ID_Gru, sqlt);


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

        public void AgregarIndependiente(SubFases sub, UserSuperAdmin usa)
        {         
                SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa));
                SqlCommand _comando = new SqlCommand("AgregarEquipo", _cnn);
                _comando.CommandType = System.Data.CommandType.StoredProcedure;
                _comando.Parameters.AddWithValue("@fas", sub.Fase.ID);
                _comando.Parameters.AddWithValue("@fas", sub.Nombre);
                _comando.Parameters.AddWithValue("@fas", sub.Numero);
                _comando.Parameters.AddWithValue("@fas", sub.RespetaReglas);
                _comando.Parameters.AddWithValue("@fas", sub.Tipo);


                SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
                _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
                _comando.Parameters.Add(_retorno);

                SqlTransaction sqlt = null;

                try
                {
                    _cnn.Open();
                    sqlt = _cnn.BeginTransaction();
                    _comando.Transaction = sqlt;
                    _comando.ExecuteNonQuery();



                    if ((int)_retorno.Value == -1)
                        throw new Exception("BD");
                    else if ((int)_retorno.Value == -2)
                    {
                        throw new Exception("BD:");
                    }

                    foreach (Integrantes ninte in sub.Lista)
                        DatosIntegrantes.GetInstancia().Agregar(ninte, sub.ID_Gru, sqlt);


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

        public void Modificar(SubFases sub, UserSuperAdmin usa)

        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("ModificarSubfase", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@id", sub.ID_Gru);
            _comando.Parameters.AddWithValue("@nom", sub.Nombre);
            _comando.Parameters.AddWithValue("@num", sub.Numero);
            _comando.Parameters.AddWithValue("@tip", sub.Tipo);
            _comando.Parameters.AddWithValue("@reg", sub.RespetaReglas);

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

        public void Eliminar(SubFases sub, UserSuperAdmin usa)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("ModificarSubfase", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@id", sub.ID_Gru);

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

        public List<SubFases> SubFasexFase(int idFA)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("SubFasexFase", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@id", idFA);

            List<SubFases> listasf = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read())
                    {
                        listasf.Add(new SubFases((int)sqldr["id"], (int)sqldr["numero"], (string)sqldr["nombre"], (string)sqldr["tipo"], (bool)sqldr["respetareglas"],
                            DatosIntegrantes.GetInstancia().IntegrantexSubfase((int)sqldr["id"])));
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
            return listasf;

        }

        public SubFases BuscarHaciaArriba(bool completo, int idsf)

        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("BuscarSubFase", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@id", idsf);

            Fases fase = null;
            SubFases subfa = null;

            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read())
                    {
                        fase = DatosFases.GetInstancia().BuscarHaciaArriba((int)sqldr["idFa"]);

                        if (completo == true) {                  
                            subfa = new SubFases((int)sqldr["id"], (int)sqldr["numero"], (string)sqldr["nombre"], (string)sqldr["tipo"], (bool)sqldr["respetareglas"],
                                DatosIntegrantes.GetInstancia().IntegrantexSubfase((int)sqldr["id"]), fase);

                        }
                        else
                        subfa = new SubFases((int)sqldr["id"], (int)sqldr["numero"], (string)sqldr["nombre"], (string)sqldr["tipo"], (bool)sqldr["respetareglas"], fase);

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
            return subfa;

        }

    }
   }

