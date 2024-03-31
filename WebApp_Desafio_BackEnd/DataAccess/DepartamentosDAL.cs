using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp_Desafio_BackEnd.Models;

namespace WebApp_Desafio_BackEnd.DataAccess
{
    public class DepartamentosDAL : BaseDAL
    {
        public IEnumerable<Departamento> ListarDepartamentos()
        {
            IList<Departamento> lstDepartamentos = new List<Departamento>();

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {

                    dbCommand.CommandText = "SELECT * FROM departamentos ";

                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        var departamento = Departamento.Empty;

                        while (dataReader.Read())
                        {
                            departamento = new Departamento();

                            if (!dataReader.IsDBNull(0))
                                departamento.ID = dataReader.GetInt32(0);
                            if (!dataReader.IsDBNull(1))
                                departamento.Descricao = dataReader.GetString(1);

                            lstDepartamentos.Add(departamento);
                        }
                        dataReader.Close();
                    }
                    dbConnection.Close();
                }

            }

            return lstDepartamentos;
        }
        public int AdicionarDepartamento(string descricao)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "INSERT INTO departamentos (Descricao) VALUES (@Descricao); SELECT last_insert_rowid();";
                    dbCommand.Parameters.AddWithValue("@Descricao", descricao);

                    dbConnection.Open();
                    int id = Convert.ToInt32(dbCommand.ExecuteScalar());
                    dbConnection.Close();

                    return id;
                }
            }
        }

        public void AtualizarDepartamento(int id, string descricao)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "UPDATE departamentos SET Descricao = @Descricao WHERE ID = @ID";
                    dbCommand.Parameters.AddWithValue("@Descricao", descricao);
                    dbCommand.Parameters.AddWithValue("@ID", id);

                    dbConnection.Open();
                    dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
        }

        public void ExcluirDepartamento(int id)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "DELETE FROM departamentos WHERE ID = @ID";
                    dbCommand.Parameters.AddWithValue("@ID", id);

                    dbConnection.Open();
                    dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
        }

        public Departamento ObterDepartamentoPorId(int id)
        {
            Departamento departamento = null;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "SELECT * FROM departamentos WHERE ID = @ID";
                    dbCommand.Parameters.AddWithValue("@ID", id);

                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            departamento = new Departamento();
                            departamento.ID = dataReader.GetInt32(0);
                            departamento.Descricao = dataReader.GetString(1);
                        }
                    }
                }
            }

            return departamento;
        }
    }
}
