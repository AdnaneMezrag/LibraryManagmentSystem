using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Policy;
using System.ComponentModel;

namespace DataAccess_Layer{
    public class clsUserData
    {

    public static int AddNewUser(string UserName, string Password, bool IsActive, string FirstName, string SecondName){
        int ID = -1;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = @"INSERT INTO User ( 
                            UserName, Password, IsActive, FirstName, SecondName)
                            VALUES (@UserName, @Password, @IsActive, @FirstName, @SecondName);
                            SELECT SCOPE_IDENTITY();";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@UserName", UserName);
        command.Parameters.AddWithValue("@Password", Password);
        command.Parameters.AddWithValue("@IsActive", IsActive);
        command.Parameters.AddWithValue("@FirstName", FirstName);
        command.Parameters.AddWithValue("@SecondName", SecondName);


        try
        {
            connection.Open();

            object result = command.ExecuteScalar();

            if (result != null && int.TryParse(result.ToString(), out int insertedID))
            {
                ID = insertedID;
            }
        }

        catch (Exception ex)
        {
            //Console.WriteLine("Error: " + ex.Message);

        }

        finally
        {
            connection.Close();
        }


        return ID;
    }
    public static bool UpdateUser(int UserID, string UserName, string Password, bool IsActive, string FirstName, string SecondName){
        int rowsAffected = 0;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = @"Update User set 
                            
UserName = @UserName,
Password = @Password,
IsActive = @IsActive,
FirstName = @FirstName,
SecondName = @SecondName
                            where UserID = @UserID";
                            

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@UserID", UserID);
        command.Parameters.AddWithValue("@UserName", UserName);
        command.Parameters.AddWithValue("@Password", Password);
        command.Parameters.AddWithValue("@IsActive", IsActive);
        command.Parameters.AddWithValue("@FirstName", FirstName);
        command.Parameters.AddWithValue("@SecondName", SecondName);


        try
        {
            connection.Open();
            rowsAffected = command.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            //Console.WriteLine("Error: " + ex.Message);
            return false;
        }

        finally
        {
            connection.Close();
        }

        return (rowsAffected > 0);
    }
    public static bool GetUserInfoByUserID(int UserID, ref string UserName, ref string Password, ref bool IsActive, ref string FirstName, ref string SecondName){
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = "SELECT * FROM User WHERE UserID = @UserID";
                            

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;
                    
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    public static bool DeleteUser(int UserID)
    {

        int rowsAffected = 0;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = @"Delete User 
                            where UserID = @UserID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@UserID", UserID);

        try
        {
            connection.Open();

            rowsAffected = command.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            // Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {

            connection.Close();

        }

        return (rowsAffected > 0);

    }
    public static bool IsUserExist(int UserID)
    {
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = "SELECT Found=1 FROM User WHERE UserID = @UserID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@UserID", UserID);

        try
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            isFound = reader.HasRows;

            reader.Close();
        }
        catch (Exception ex)
        {
            //Console.WriteLine("Error: " + ex.Message);
            isFound = false;
        }
        finally
        {
            connection.Close();
        }

        return isFound;
    }
    public static DataTable GetAllUser()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from User";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }


    public static bool GetUserByUserNameAndPassword(ref int UserID, string UserName, string Password, ref bool IsActive, ref string FirstName, ref string SecondName)
    {
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = @"select *from [user]
where UserName = @UserName and Password = @Password
";


        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@UserName", UserName);
        command.Parameters.AddWithValue("@Password", Password);

            try
            {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                // The record was found
                isFound = true;

                UserID = (int)reader["UserID"];
                IsActive = (bool)reader["IsActive"];
                FirstName = (string)reader["FirstName"];
                SecondName = (string)reader["SecondName"];

            }
            else
            {
                // The record was not found
                isFound = false;
            }

            reader.Close();


        }
        catch (Exception ex)
        {
            //Console.WriteLine("Error: " + ex.Message);
            isFound = false;
        }
        finally
        {
            connection.Close();
        }

        return isFound;
    }

    }
}