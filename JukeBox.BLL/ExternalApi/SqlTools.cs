using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL.ExternalApi
{
    public class SqlTools
    {
        public static string ConnectionString(Type type)
        {
            //  Configuration configFile = PersistentObjectHelper.GetConfiguration(PersistentObjectHelper.GetConfigName(type));
            return null;
        }

        public static string ConvertToYankDate(DateTime date)
        {
            return date.Month + "-" + date.Day + "-" + date.Year;
        }

        public static string ConvertToYankDateTime(DateTime dateTime)
        {
            //Date Time Format
            //"3-19-2007 16:34:13.220"
            return dateTime.Month + "-" + dateTime.Day + "-" + dateTime.Year + " " +
                dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second + "." +

                ((dateTime.Millisecond.ToString().Length > 3) ?
                    dateTime.Millisecond.ToString().Substring(0, 3) :
                    dateTime.Millisecond.ToString().PadLeft(3, '0'));
        }

        public static string ConvertToYMD(DateTime date)
        {
            return date.Year + "-" + date.Month.ToString().PadLeft(2, '0') + "-" + date.Day.ToString().PadLeft(2, '0');

        }

        public static string ConvertToYMDTime(DateTime dateTime)
        {
            return dateTime.Year + "-" + dateTime.Month.ToString().PadLeft(2, '0') + "-" + dateTime.Day.ToString().PadLeft(2, '0') + " " + dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second + "." +
                ((dateTime.Millisecond.ToString().Length > 3) ?
                    dateTime.Millisecond.ToString().Substring(0, 3) :
                    dateTime.Millisecond.ToString().PadLeft(3, '0'));
        }

        public static DataTable GetDataTableSql(string sql, string connString)
        {
            return GetDataTableSql(sql, connString, 0);
        }

        public static DataTable GetDataTableOle(string sql, string connString)
        {
            return GetDataTableOle(sql, connString, 0);
        }

        public static DataTable GetDataTableSql(string sql, string connString, int timeout)
        {
            //DataTable dt = new DataTable();
            //SqlConnection con = new SqlConnection(connString);
            //try
            //{
            //    con.Open();
            //    SqlDataAdapter da = new SqlDataAdapter(sql, con);
            //    if (timeout > 0) da.SelectCommand.CommandTimeout = timeout;
            //    da.Fill(dt);
            //}
            //catch (Exception ex) { throw ex; }
            //finally
            //{
            //    if (con != null && con.State != ConnectionState.Closed) con.Close();
            //}
            ////con.Close();
            //return dt;

            return GetDataTableSql(sql, connString, timeout, null);
        }

        public static DataTable GetDataTableSql(string sql, string connString, int timeout, SqlConnection pConn)
        {
            DataTable dt = new DataTable();
            SqlConnection con = null;

            //is a connection passed through?
            if (pConn != null) con = pConn;
            //create new connection
            else con = new SqlConnection(connString);

            try
            {
                //open connection
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                if (timeout > 0) da.SelectCommand.CommandTimeout = timeout;
                da.Fill(dt);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                //don't close connection if it was passed through
                if (con != null && con.State != ConnectionState.Closed && pConn == null) con.Close();
            }

            return dt;
        }

        public static DataTable GetDataTableOle(string sql, string connString, int timeout)
        {
            DataTable dt = new DataTable();
            OleDbConnection con = new OleDbConnection(connString);
            try
            {
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(sql, con);

                if (timeout > 0) da.SelectCommand.CommandTimeout = timeout;

                da.Fill(dt);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed) con.Close();
            }
            return dt;
        }

        public static DataTable GetDataTableSP(string spName, List<SqlParameter> parameters, string connString)
        {
            SqlConnection con = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                if (parameters != null)
                    foreach (SqlParameter param in parameters)
                        cmd.Parameters.Add(param);

                con.Open();

                SqlDataAdapter adapt = new SqlDataAdapter(cmd);

                adapt.Fill(dt);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                if (con != null) con.Close();
            }
            return dt;
        }

        public static SqlDataReader GetSqlDataReader(string sql, string connString)
        {
            SqlConnection con = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            try
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                //CloseConnection parameter passed through ensures the connection is closed when the reader is closed
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                //con.Close();
            }

            return reader;
        }

        public static SqlDataReader StoredProcResult(string procName, List<SqlParameter> parameters, string connString)
        {
            SqlConnection con = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            try
            {
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                foreach (SqlParameter param in parameters)
                    cmd.Parameters.Add(param);

                con.Open();

                //CloseConnection parameter passed through ensures the connection is closed when the reader is closed
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                con.Close();
            }
            return reader;
        }

        public static int GetSqlScalarInt(string sql, string connString)
        {
            return GetSqlScalarInt(sql, connString, 120);
        }

        public static int GetSqlScalarInt(string sql, string connString, int timeout)
        {
            object result = null;
            result = GetSqlScalar(sql, connString, timeout);

            if (result is int) return (int)result;
            else { return int.MinValue; }
        }

        public static string GetSqlScalarString(string sql, string connString, int timeout)
        {
            object result = null;
            result = GetSqlScalar(sql, connString, timeout);

            if (result is string) return (string)result;
            else { return null; }
        }

        public static object GetSqlScalar(string sql, string connString)
        {
            return GetSqlScalar(sql, connString, 120);
        }

        public static object GetSqlScalar(string sql, string connString, int timeout)
        {
            object o = null;

            DataTable dt = GetDataTableSql(sql, connString, timeout);

            if (dt.Rows.Count != 1)
                throw new Exception("wrong amount of rows returned. exactly 1 expected.");
            else if (dt.Rows[0].ItemArray.Length != 1)
                throw new Exception("wrong amount of columns returned. exactly 1 expected.");

            o = dt.Rows[0][0];

            return o;
        }

        public static int RunCmd(string SQL, string connString)
        {
            return RunCmd(SQL, connString, null);
        }

        public static int RunCmd(string SQL, string connString, IDbTransaction pTransaction)
        {
            SqlConnection conn;

            if (pTransaction != null)
                conn = (SqlConnection)pTransaction.Connection;
            else
                conn = new SqlConnection(connString);

            try
            {
                if (pTransaction == null)
                    conn.Open();

                SqlCommand UpdateCommand = conn.CreateCommand();
                UpdateCommand.Transaction = (SqlTransaction)pTransaction;
                UpdateCommand.CommandTimeout = 360;
                UpdateCommand.CommandText = SQL;

                return UpdateCommand.ExecuteNonQuery();

            }
            catch (Exception ex) { throw ex; }

            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed && pTransaction == null) conn.Close();
            }
        }
    }

}
