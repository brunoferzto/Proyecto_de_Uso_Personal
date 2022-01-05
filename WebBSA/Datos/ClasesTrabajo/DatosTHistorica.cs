using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace Datos
{
   public  class DatosTHistorica:IdatosTHistorica
    {
        private static DatosTHistorica _instancia = null;
        private DatosTHistorica() { }
        public static DatosTHistorica GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosTHistorica();
            return _instancia;
        }

        public void AgregarIndividual (THistorica tablaH, User usr) // XQ NO LLAMA DIRECTO SIN USAR ESTA
        {
            Agregar(tablaH, usr,null);
        }

        internal void Agregar (THistorica tablaH, User usr, SqlTransaction sqlT)
        {
            SqlConnection sqlc;

            if (sqlT != null)
                sqlc = sqlT.Connection;
            else // user null?
                sqlc = new SqlConnection(Conexion.ConexionUsuario(usr));
            {
                
                SqlCommand _comando = new SqlCommand("AgregarCompetencia", sqlc);
                _comando.CommandType = System.Data.CommandType.StoredProcedure;
                _comando.Parameters.AddWithValue("@tem", tablaH.Equipo.Nombre);
                _comando.Parameters.AddWithValue("@com", tablaH.Equipo.Competidor.Nombre);
                _comando.Parameters.AddWithValue("@pts", tablaH.PTS);
                _comando.Parameters.AddWithValue("@nom", tablaH.NombreFase);
                _comando.Parameters.AddWithValue("@cop", tablaH.Subfase.ID_Gru);



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
        }     
        public void Modificar (THistorica tablaH, UserSuperAdmin usa)
        {
                SqlConnection sqlc = new SqlConnection(Conexion.ConexionUsuario(usa));
                SqlCommand _comando = new SqlCommand("AgregarCompetencia", sqlc);
                _comando.CommandType = System.Data.CommandType.StoredProcedure;
                _comando.Parameters.AddWithValue("@id", tablaH.ID);
                _comando.Parameters.AddWithValue("@pts", tablaH.PTS);
                _comando.Parameters.AddWithValue("@nom", tablaH.NombreFase);
                _comando.Parameters.AddWithValue("@cop", tablaH.Subfase.ID_Gru);



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
        public void Eliminar(THistorica tablaH, UserSuperAdmin usa)
        {
            SqlConnection sqlc = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("AgregarCompetencia", sqlc);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@id", tablaH.ID);

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
        public List<THistorica> Listar ()
        {
                SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
                SqlCommand _comando = new SqlCommand("ListarTablaHistorica", _cnn);
                _comando.CommandType = System.Data.CommandType.StoredProcedure;

                List<THistorica> _lista = new List<THistorica>();
                THistorica tablaH = null;

                try
                {
                    _cnn.Open();
                    SqlDataReader sqldr = _comando.ExecuteReader();
                    if (sqldr.HasRows)
                    {
                        while (sqldr.Read())
                        {

                        tablaH = new THistorica((int)sqldr["Id_THist"], DatosEquipos.GetInstancia().BuscarEquipo((string)sqldr["Equipo"],
                                                    (string)sqldr["Competidor"]), (int)sqldr["PTS"],
                                                    DatosSubfases.GetInstancia().BuscarHaciaArriba(false, (int)sqldr["Subfase"]), (string)sqldr["NombreFase"]);

                        _lista.Add(tablaH);
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
            }
        public List<THistorica> ListarxEquipo(string nomEquipo)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            SqlCommand _comando = new SqlCommand("TablaHistoricaxEquipo", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@nom", nomEquipo);

            List<THistorica> _lista = new List<THistorica>();
            THistorica tablaH = null;


            try
            {
                _cnn.Open();
                SqlDataReader sqldr = _comando.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read())
                    {                                           
                        tablaH = new THistorica((int)sqldr["Id_THist"], DatosEquipos.GetInstancia().BuscarEquipo((string)sqldr["Equipo"],
                            (string)sqldr["Competidor"]), (int)sqldr["PTS"], DatosSubfases.GetInstancia().BuscarHaciaArriba(false, (int)sqldr["Subfase"]), (string)sqldr["NombreFase"]);


                        _lista.Add(tablaH);
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
        }
        



    }
}
