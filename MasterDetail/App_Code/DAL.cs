using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// All the Data Access Configurations should be done here.
/// </summary>
/// 

namespace MasterDetail
{
    public class DAL
    {
     // public static string connetionstring = "Data Source=CTSINTBMVNVDB2;Initial Catalog=MetLifeDD;User ID=metlifedd;Password=metlifedd";
      public static string connetionstring = "Data Source=DESKTOP-KS262BJ\\SQLEXPRESS;Initial Catalog=METDASH;Integrated Security=True";
        SqlConnection con = new SqlConnection(connetionstring);


        public string insertintoDB(string smt)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(smt, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Success";
            }
            catch (Exception e)
            {
                con.Close();
                return e.Message;
            }

        }

        public int GetCountFromDB(string smt)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(smt, con);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count;
            }
            catch 
            {
                con.Close();
                return -1;
            }

        }
        public string UpdateDB(string smt)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(smt, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Success";
            }
            catch (Exception e)
            {
                con.Close();
                return e.Message;
            }

        }
        public int SelectMax(string smt)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(smt, con);
                con.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return i;
            }
            catch 
            {
                con.Close();
                return 0;
            }

        }
        public DataTable SelectDetails(string smt)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(smt,con);
            try
            {
                SqlCommand cmd = new SqlCommand(smt, con);
                da.Fill(dt);
                return dt;
            }
            catch(Exception e) 
            {
                con.Close();
                return null;
            }

        }


    }
}