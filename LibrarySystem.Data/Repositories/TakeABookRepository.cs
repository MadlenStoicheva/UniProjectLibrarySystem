using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Repositories
{
    public class TakeABookRepository
    {
        private string connectionString;

        public TakeABookRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<TakeABook> GetAll()
        {
            List<TakeABook> result = new List<TakeABook>();
            IDbConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText =
@"SELECT * FROM TakeABooks";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        TakeABook takeABook = new TakeABook();
                        takeABook.Id = (int)reader["Id"];
                        takeABook.book = (Book)reader["Book"];
                        takeABook.user = (User)reader["User"];
                        takeABook.dateTaken = (DateTime)reader["Date Taken"];
                        takeABook.dateForReturn = (DateTime)reader["Date For Return"];
                        takeABook.dateReturn = (DateTime)reader["dateReturn"];

                        result.Add(takeABook);
                    }
                }
            }

            return result;
        }

        public void Insert(TakeABook takeABook)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"INSERT INTO TakeABooks (book, user, dateTaken, dateForReturn, dateReturn)
    VALUES (@book, @user, @dateTaken, @dateForReturn, @dateReturn)
";
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@book";
                parameter.Value = takeABook.book;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@user";
                parameter.Value = takeABook.user;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@dateTaken";
                parameter.Value = takeABook.dateTaken;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@dateForReturn";
                parameter.Value = takeABook.dateForReturn;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@dateReturn";
                parameter.Value = takeABook.dateReturn;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
        }

        private void Update(TakeABook takeABook)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE TakeABooks SET book=@book, user=@user,dateTaken=@dateTaken, dateForReturn=@dateForReturn, dateReturn=@dateReturn  WHERE Id=@Id";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@book";
            parameter.Value = takeABook.book;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@user";
            parameter.Value = takeABook.user;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@dateTaken";
            parameter.Value = takeABook.dateTaken;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@dateForReturn";
            parameter.Value = takeABook.dateForReturn;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@dateReturn";
            parameter.Value = takeABook.dateReturn;
            command.Parameters.Add(parameter);

        

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Save(TakeABook takeABook)
        {
            if (takeABook.Id > 0)
            {
                Update(takeABook);
            }
            else
            {
                Insert(takeABook);
            }
        }

        public void Delete(int id)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"DELETE FROM TakeABooks WHERE Id=@Id";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = id;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Delete(TakeABook takeABook)
        {
            Delete(takeABook.Id);
        }
    }
}
