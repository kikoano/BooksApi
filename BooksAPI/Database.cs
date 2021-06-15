using BooksAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BooksAPI
{
    public interface IDatabase<T>
    {
        DataTable GetAll();
        DataTable Get(int id);
        int Create(T t);
        int Update(T t, int id);
        int Delete(int id);
    }
    public class Database<T> : IDatabase<T>
    {
        private readonly IConfiguration configuration;
        private string connectionString;
        public Database(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public DataTable GetAll()
        {
            return get($"SELECT * FROM {typeof(T).Name}S");
        }
        public DataTable Get(int id)
        {
            return get($"SELECT * FROM {typeof(T).Name}S WHERE ID = ${id}");
        }
        public int Create(T t)
        {
            //Dictionary<string, object> fields = new Dictionary<string, object>();
            List<IDataParameter> parameters = new List<IDataParameter>();
            string f = "";
            string v = "";
            foreach (PropertyInfo prop in t.GetType().GetProperties())
            {
                if (prop.Name != "Id")
                {
                    f += $"{prop.Name},";
                    v += $"@{prop.Name},";
                    parameters.Add(new SqlParameter($"@{prop.Name}", prop.GetValue(t).ToString()));
                }
            }
            // remove last ,
            f = f.Remove(f.Length - 1);
            v = v.Remove(v.Length - 1);
            Debug.WriteLine(f, v);
            return executeData($"INSERT INTO {typeof(T).Name}S ({f}) values ({v});", parameters);
        }

        public int Update(T t, int id)
        {
            //Dictionary<string, object> fields = new Dictionary<string, object>();
            List<IDataParameter> parameters = new List<IDataParameter>();
            string f = "";
            foreach (PropertyInfo prop in t.GetType().GetProperties())
            {
                if (prop.Name != "Id")
                {
                    f += $"{prop.Name} = '{prop.GetValue(t).ToString()}',";
                    parameters.Add(new SqlParameter($"@{prop.Name}", prop.GetValue(t).ToString()));
                }
            }
            // remove last ,
            f = f.Remove(f.Length - 1);
            parameters.Add(new SqlParameter("Id", id));
            return executeData($"UPDATE {typeof(T).Name}S SET {f} WHERE Id = @Id;", parameters);
        }

        public int Delete(int id)
        {
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("Id", id));
            return executeData($"DELETE FROM {typeof(T).Name}S WHERE Id = @Id", parameters);
        }
        private DataTable get(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader myReader;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        myReader = cmd.ExecuteReader();
                        dt.Load(myReader);

                        myReader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR", ex.Message);
            }
            return dt;
        }

        private int executeData(string str, List<IDataParameter> sqlParams)
        {
            int rows = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(str, conn))
                    {
                        if (sqlParams != null)
                        {
                            foreach (IDataParameter para in sqlParams)
                            {
                                cmd.Parameters.Add(para);
                            }
                            rows = cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR", ex.Message);
            }
            return rows;

        }
    }
}
