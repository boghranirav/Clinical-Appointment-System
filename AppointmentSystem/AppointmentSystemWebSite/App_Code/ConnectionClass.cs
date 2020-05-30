using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Reflection;
using System.Data.Common;

/// <summary>
/// Summary description for ConnectionClass
/// </summary>
public class ConnectionClass
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString);

    public string s_p_Name1 = "";

    public ConnectionClass()
    { }
    
	public ConnectionClass(string pro_name)
	{
        this.s_p_Name1 = pro_name;
	}

    SqlTransaction sqlT;
    public bool SaveData(List<SqlParameter> str)
    {
        con.Open();
        sqlT = con.BeginTransaction();
        try
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = sqlT;
            cmd.CommandText = s_p_Name1;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            str.ToArray();
            for (int i = 0; i < str.Count; i += 1)
            {
                cmd.Parameters.Add(str[i]);
            }
            cmd.ExecuteNonQuery();
            sqlT.Commit();
            con.Close();
            return true;
        }
        catch (Exception )
        {
            sqlT.Rollback();
            return false;
        }
    }

    public int GetGlobalId()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "GetGlobalMaxId";
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        string s = cmd.ExecuteScalar().ToString();
        con.Close();
        if (s == null || s == "")
        {
            return 1;
        }
        else
        {
            return Convert.ToInt32(s) + 1;
        }
    }

    public int GetMaxTableCode(string TableName,string ColumnName)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "GetMaxCodeTable";
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = TableName;
        cmd.Parameters.Add("@TableFieldName", SqlDbType.VarChar).Value = ColumnName;
        string s = cmd.ExecuteScalar().ToString();
        
        con.Close();
        if (s == null || s == "")
        {
            return 1;
        }
        else
        {
            return Convert.ToInt32(s) + 1;
        }
    }

    public DataSet DisplayData(string tablename)
    {
        SqlCommand command = new SqlCommand();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
        con.Open();
        command.Connection = con;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = s_p_Name1;
        command.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tablename;
        adapter = new SqlDataAdapter(command);
        adapter.Fill(ds);
        con.Close();
        return ds;
    }

    public bool UserLogin(string userid)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "AdminLogin_SP";
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = userid;
       
        if (Convert.ToInt32(cmd.ExecuteScalar())>0)
        {
            con.Close();
            return true;
        }
        else
        {
            con.Close();
            return false;
        }
        
    }

    public bool UserLoginPassword(string userid, string password)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "AdminLoingPass_SP";
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = userid;
        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
        {
            // Session["admin_id"] = admin_id.Text;
            // Response.Redirect("index2.aspx");
            con.Close();
            return true;
        }
        else
        {
            con.Close();
            return false;
        }

    }

    public string AdminUserIdCheck(string uid)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "AdminIdExist";
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = uid;
        string s = cmd.ExecuteScalar().ToString();

        con.Close();
        if (s == null || s == "" || Convert.ToInt32(s) == 0)
        {
            return uid;
        }
        else
        {
            return uid+Convert.ToInt32(s);
        }
    }

    public string DoctorUserIdCreate(string uid,int companyId)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = s_p_Name1;
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = uid;
        cmd.Parameters.Add("@CompanyId", SqlDbType.BigInt).Value = companyId;
        string s = cmd.ExecuteScalar().ToString();

        con.Close();
        if (s == null || s == "" || Convert.ToInt32(s) == 0)
        {
            return uid;
        }
        else
        {
            return uid + Convert.ToInt32(s);
        }
    }

    public void AdminLogout(string uid)
    {
        SqlCommand cmd = new SqlCommand("AdminLogOut", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", uid);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();  
    }

    public void SetAdminOnline(string uid)
    {
        SqlCommand cmd = new SqlCommand("AdminSetOnLine", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", uid);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    public DataSet GetAdminDetail(string uid)
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
        con.Open();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetAdminDetail";
        cmd.Parameters.AddWithValue("@UserId", uid);
        adapter = new SqlDataAdapter(cmd);
        adapter.Fill(ds);
        con.Close();
        return ds;  
    }

    public bool DeleteAdminData(List<SqlParameter> str)
    {
        try{
        con.Open();
        SqlCommand cmd = new SqlCommand(s_p_Name1, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        str.ToArray();
        for (int i = 0; i < str.Count; i += 1)
        {
            cmd.Parameters.Add(str[i]);
        }
        cmd.ExecuteNonQuery();
        con.Close();
        return true;
        }catch(Exception ){
            return false;
        }
    }

    public DataSet GetAdminDetailById(List<SqlParameter> str)
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(s_p_Name1, con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();

            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter p1 = new SqlParameter();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            str.ToArray();
            for (int i = 0; i < str.Count; i += 1)
            {
                cmd.Parameters.Add(str[i]);
            }
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            con.Close();
            return ds;  
        }
        catch (Exception )
        {
            return null;
        }
    }

    public DataSet DisplayDataById(int id,string fieldName)
    {
        SqlCommand command = new SqlCommand();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
        con.Open();
        command.Connection = con;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = s_p_Name1;
        command.Parameters.Add(fieldName, SqlDbType.BigInt).Value = id;
        adapter = new SqlDataAdapter(command);
        adapter.Fill(ds);
        con.Close();
        return ds;
    }

    public string checkNoDoctor(string companyId)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "AdminDoctorMaxCheck";
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add("@CompanyId", SqlDbType.BigInt).Value = companyId;
        SqlDataReader dataU = cmd.ExecuteReader();
        string s="";
        while (dataU.Read())
        {
            s= dataU[0].ToString();
        }
        con.Close();
        return s;
    }

    
    //For Company Use
    public string checkUserLogin(List<SqlParameter> str)
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "CheckUserLogin";
            SqlParameter p1 = new SqlParameter();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            str.ToArray();
            for (int i = 0; i < str.Count; i += 1)
            {
                cmd.Parameters.Add(str[i]);
            }
            SqlDataReader dataU = cmd.ExecuteReader();
            string s = "";
            while (dataU.Read())
            {
                s = dataU[0].ToString();
                break;
            }
            con.Close();
            return s;
        }catch(Exception){
            con.Close();
            return "E";
        }
    }

    public DataSet DisplayUserData(List<SqlParameter> str)
    {
        try
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            con.Open();
            command.Connection = con;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = s_p_Name1;
            SqlParameter p1 = new SqlParameter();
            str.ToArray();
            for (int i = 0; i < str.Count; i += 1)
            {
                command.Parameters.Add(str[i]);
            }
            adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            con.Close();
            return ds;
        }
        catch (Exception)
        {
            con.Close();
            return null;
        }
    }

    public int CountRecords(List<SqlParameter> str)
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = s_p_Name1;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter p1 = new SqlParameter();
            str.ToArray();
            for (int i = 0; i < str.Count; i += 1)
            {
                cmd.Parameters.Add(str[i]);
            }
            string s = cmd.ExecuteScalar().ToString();

            con.Close();
            return Convert.ToInt32(s);
        }
        catch (Exception)
        {
            con.Close();
            return 0;
        }
    }
}