using FEP.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FEP.Data
{
    public class ADOHelper
    {
        #region design pattern
        private static ADOHelper instance;
        public static ADOHelper Instance
        {
            get { if (instance == null) instance = new ADOHelper(); return instance; }
        }
        private ADOHelper() { }
        #endregion

        #region Attribute
        string _connectionString = @"Data Source=.;Initial Catalog=SneakerManagement;Integrated Security=True";
        #endregion

        #region Make Query
        public void AddParameters(ref SqlCommand cmd, object[] obj)
        {
            int lenPara = obj.Length;
            for (int i = 0; i < lenPara; i++)
                cmd.Parameters.AddWithValue(@"@para_" + i.ToString(), obj[i]);
        }
        public List<T> ExecuteReader<T>(string serverName, string databaseName, string query, params object[] obj) where T : class, new()
        {
            List<T> list = new List<T>();
            using (SqlConnection connection = new SqlConnection(DataHelper.Instance.getConnectionString(serverName, databaseName)))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (obj != null)
                    AddParameters(ref cmd, obj);
                SqlDataReader reader = cmd.ExecuteReader();
                int lenField = reader.FieldCount;
                while (reader.Read())
                {
                    T item = new T();
                    for (int i = 0; i < lenField; i++)
                    {
                        PropertyInfo propertyInfo = typeof(T).GetProperty(reader.GetName(i));
                        if (propertyInfo != null)
                            propertyInfo.SetValue(item, reader.GetValue(i));
                    }
                    list.Add(item);
                }
            }
            return list;
        }
        public int ExecuteNonQuery(string serverName, string databaseName, string query, params object[] obj)
        {
            int rowEffect = 0;
            using (SqlConnection connection = new SqlConnection(DataHelper.Instance.getConnectionString(serverName, databaseName)))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (obj != null)
                    AddParameters(ref cmd, obj);
                rowEffect = cmd.ExecuteNonQuery();
            }
            return rowEffect;
        }
        public int ExecuteScalar(string serverName, string databaseName, string query, params object[] obj)
        {
            int rowEffect = 0;
            using (SqlConnection connection = new SqlConnection(DataHelper.Instance.getConnectionString(serverName, databaseName)))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (obj != null)
                    AddParameters(ref cmd, obj);
                rowEffect = (int)cmd.ExecuteScalar();
            }
            return rowEffect;
        }


        public List<T> ExecuteReader<T>(string query, params object[] obj) where T : class, new()
        {
            List<T> list = new List<T>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (obj != null)
                    AddParameters(ref cmd, obj);
                SqlDataReader reader = cmd.ExecuteReader();
                int lenField = reader.FieldCount;
                while (reader.Read())
                {
                    T item = new T();
                    for (int i = 0; i < lenField; i++)
                    {
                        PropertyInfo propertyInfo = typeof(T).GetProperty(reader.GetName(i));
                        if (propertyInfo != null)
                            propertyInfo.SetValue(item, reader.GetValue(i));
                    }
                    list.Add(item);
                }
            }
            return list;
        }
        public int ExecuteNonQuery(string query, params object[] obj)
        {
            int rowEffect = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (obj != null)
                    AddParameters(ref cmd, obj);
                rowEffect = cmd.ExecuteNonQuery();
            }
            return rowEffect;
        }
        public int ExecuteScalar(string query, params object[] obj)
        {
            int rowEffect = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (obj != null)
                    AddParameters(ref cmd, obj);
                rowEffect = (int)cmd.ExecuteScalar();
            }
            return rowEffect;
        }
        #endregion
    }
}