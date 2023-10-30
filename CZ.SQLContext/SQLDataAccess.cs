using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Data.SqlTypes;

namespace CZ.SQLContext
{
    public class SQLDataAccess : IDisposable
    {
        string connectionString = @"Data Source=./;Initial Catalog=CZ.BallestLane;Integrated Security=True";

        public SqlConnection Cnn { get; set; }

        public SQLDataAccess()
        {
            Cnn = new SqlConnection(connectionString);
        }

        private void OpenConnection()
        {
            if (Cnn.State == ConnectionState.Closed)
                Cnn.Open();
        }

        private void CloseConnection()
        {
            Cnn.Close();
            Cnn.Dispose();
        }

        public void Dispose()
        {
            Cnn.Close();
            Cnn.Dispose();
        }

        public DataTable Read(string query, ArrayList parameters = null)
        {
            var Dt = new DataTable();
            var Da = default(SqlDataAdapter);
            var Cmd = new SqlCommand();

            try
            {
                Da = new SqlDataAdapter(query, Cnn);

                if ((parameters != null))
                {
                    foreach (SqlParameter data in parameters)
                    {
                        Da.SelectCommand.Parameters.Add(data);
                    }
                }

                Da.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Dt;
        }

        public SqlTransaction BeginTransaction()
        {
            this.OpenConnection();
            return this.Cnn.BeginTransaction();
        }

        public void Commit(SqlTransaction transaction)
        {
            transaction.Commit();
            transaction.Dispose();
        }

        public void RollBack(SqlTransaction transaction)
        {
            transaction.Rollback();
            transaction.Dispose();
        }

        public void ExecuteTransactionQuery(string query, ArrayList parameters, SqlTransaction tranx)
        {
            try
            {
                var cmd = new SqlCommand(query, Cnn, tranx);
                ExecuteCommand(cmd, query, parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ExecuteQuery(string query, ArrayList parameters)
        {
            OpenConnection();

            using (var cmd = new SqlCommand(query, Cnn))
            {
                try
                {
                    ExecuteCommand(cmd, query, parameters);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        private void ExecuteCommand(SqlCommand cmd, string query, ArrayList parameters)
        {
            cmd.Connection = Cnn;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            if ((parameters != null))
            {
                foreach (SqlParameter data in parameters)
                {
                    cmd.Parameters.Add(data);
                }
            }

            var result = cmd.ExecuteScalar();
        }

        public SqlCommand CreateCommand(string query, ArrayList parameters)
        {
            try
            {
                var command = new SqlCommand(query);

                if ((parameters != null))
                {
                    foreach (SqlParameter data in parameters)
                    {
                        command.Parameters.Add(data);
                    }
                }

                return command;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertAllCommands(IList<SqlCommand> sqlCommands)
        {
            SqlTransaction transaction = null;

            try
            {
                OpenConnection();
                transaction = Cnn.BeginTransaction();

                foreach (var cmd in sqlCommands)
                {
                    cmd.Connection = Cnn;
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
