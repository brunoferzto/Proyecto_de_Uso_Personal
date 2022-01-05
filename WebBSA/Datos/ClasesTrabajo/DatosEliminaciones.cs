using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
   public class DatosEliminaciones:IdatosEliminaciones
    {
        private static DatosEliminaciones _instancia = null;
        private DatosEliminaciones() { }
        public static DatosEliminaciones GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosEliminaciones();
            return _instancia;
        }


        public void AgregarIndividual (Eliminaciones eli, User usr)
        {
            AgregarEliminaciones(eli, null, usr);
        }
        // AGREGAR INDIVIDUAL A TRAVEZ DE SUPERADMIN.

        internal void AgregarEliminaciones (Eliminaciones eli , SqlTransaction sqlt, User usr)
        {
            SqlConnection sqlc;

            if (sqlt != null)
                sqlc = sqlt.Connection;
            else
                sqlc = new SqlConnection(Conexion.ConexionUsuario(usr));

            SqlCommand _comando = new SqlCommand("NuevaEliminacion", sqlc);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@idS",eli.SubFase.ID_Gru );
            _comando.Parameters.AddWithValue("@pos",eli.POS);
            _comando.Parameters.AddWithValue("@equ", eli.Equipo.NombreCompleto);
            _comando.Parameters.AddWithValue("@com", eli.Equipo.Competidor.Nombre);

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

        public void Modificar (Eliminaciones eli, UserSuperAdmin usa)
        {
            SqlConnection sqlc = sqlc = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("ModificarEliminacion", sqlc);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@id", eli.ID);
            _comando.Parameters.AddWithValue("@idF", eli.SubFase.ID_Gru);
            _comando.Parameters.AddWithValue("@pos", eli.POS);
            _comando.Parameters.AddWithValue("@equ", eli.Equipo.NombreCompleto); 
            _comando.Parameters.AddWithValue("@com", eli.Equipo.Competidor.Nombre);

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

        public void Eliminar (Eliminaciones eli, UserSuperAdmin usa)
        {
            SqlConnection sqlc = sqlc = new SqlConnection(Conexion.ConexionUsuario(usa));
            SqlCommand _comando = new SqlCommand("EliminarEliminacion", sqlc);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@id", eli.ID);
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

        public Eliminaciones Buscar(int idEl)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("EquipoActivo", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@nom", idEl);

            Eliminaciones eli = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    sqldr.Read();

                    eli = new Eliminaciones((int)sqldr["id"], (int)sqldr["pos"], DatosEquipos.GetInstancia().BuscarEquipo((int)sqldr["idSF"], (string)sqldr["competidor"]),
                        DatosSubfases.GetInstancia().BuscarHaciaArriba(false, ((int)sqldr["idSF"]))); 
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
            return eli;
        } // VER SU USO

        public List<Eliminaciones> EliminacionxEquipo (int idEquipo)

        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("EliminacionxEquipo", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@eqp", idEquipo);

            List<Eliminaciones> lista = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read())
                    {
                        lista.Add(new Eliminaciones((int)sqldr["id"], (int)sqldr["pos"], DatosEquipos.GetInstancia().BuscarEquipo((int)sqldr["Equipo"], (string)sqldr["competidor"]),
                            DatosSubfases.GetInstancia().BuscarHaciaArriba(true, (int)sqldr["Subfase"])));
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

        public List<Eliminaciones> EliminacionxCompetencia(string idCopa)
        {
            SqlConnection ocox = new SqlConnection(Conexion.Cnn);
            SqlCommand ocom = new SqlCommand("EliminacionxTemporada", ocox);
            ocom.CommandType = System.Data.CommandType.StoredProcedure;
            ocom.Parameters.AddWithValue("@eqp", idCopa);

            List<Eliminaciones> lista = null;
            try
            {
                ocox.Open();
                SqlDataReader sqldr = ocom.ExecuteReader();
                if (sqldr.HasRows)
                {
                    while (sqldr.Read()) {

                        lista.Add(new Eliminaciones((int)sqldr["id"], (int)sqldr["pos"],
                            DatosEquipos.GetInstancia().BuscarEquipo((int)sqldr["equipo"], (string)sqldr["competidor"]),
                            DatosSubfases.GetInstancia().BuscarHaciaArriba(false, (int)sqldr["Subfase"])));
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
