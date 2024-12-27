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
    public class clsMemberData
    {

    public static int AddNewMember(string FirstName, string SecondName, DateTime DateOfBirth, string PhoneNumber, bool Sex, string Password, string Email, string Messages){
        int ID = -1;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = @"INSERT INTO Member ( 
                            FirstName, SecondName, DateOfBirth, PhoneNumber, Sex, Password, Email, Messages)
                            VALUES (@FirstName, @SecondName, @DateOfBirth, @PhoneNumber, @Sex, @Password, @Email, @Messages);
                            SELECT SCOPE_IDENTITY();";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@FirstName", FirstName);
        command.Parameters.AddWithValue("@SecondName", SecondName);
        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
        command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
        command.Parameters.AddWithValue("@Sex", Sex);
        command.Parameters.AddWithValue("@Password", Password);
        command.Parameters.AddWithValue("@Email", Email);
        if(Messages == "")
        {
            command.Parameters.AddWithValue("@Messages", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("@Messages", Messages );
        }


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
    public static bool UpdateMember(int MemberID, string FirstName, string SecondName, DateTime DateOfBirth, string PhoneNumber, bool Sex, string Password, string Email, string Messages){
        int rowsAffected = 0;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = @"Update Member set 
                            
FirstName = @FirstName,
SecondName = @SecondName,
DateOfBirth = @DateOfBirth,
PhoneNumber = @PhoneNumber,
Sex = @Sex,
Password = @Password,
Email = @Email,
Messages = @Messages
                            where MemberID = @MemberID";
                            

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@MemberID", MemberID);
        command.Parameters.AddWithValue("@FirstName", FirstName);
        command.Parameters.AddWithValue("@SecondName", SecondName);
        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
        command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
        command.Parameters.AddWithValue("@Sex", Sex);
        command.Parameters.AddWithValue("@Password", Password);
        command.Parameters.AddWithValue("@Email", Email);
        if(Messages == "")
        {
            command.Parameters.AddWithValue("@Messages", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("@Messages", Messages );
        }


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
    public static bool GetMemberInfoByMemberID(int MemberID, ref string FirstName, ref string SecondName, ref DateTime DateOfBirth, ref string PhoneNumber, ref bool Sex, ref string Password, ref string Email, ref string Messages){
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = "SELECT * FROM Member WHERE MemberID = @MemberID";
                            

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@MemberID", MemberID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;
                    
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    PhoneNumber = (string)reader["PhoneNumber"];
                    Sex = (bool)reader["Sex"];
                    Password = (string)reader["Password"];
                    Email = (string)reader["Email"];
                    
                    if (reader["Messages"] == DBNull.Value)
                        Messages = "";
                    else
                        Messages = (string)reader["Messages"];


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
    public static bool DeleteMember(int MemberID)
    {

        int rowsAffected = 0;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = @"Delete Member 
                            where MemberID = @MemberID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@MemberID", MemberID);

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
    public static bool IsMemberExist(int MemberID)
    {
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = "SELECT Found=1 FROM Member WHERE MemberID = @MemberID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@MemberID", MemberID);

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
    public static DataTable GetAllMember()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Member";

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


    public static bool GetMemberByMemberIDAndPassword(int MemberID, ref string FirstName, ref string SecondName, ref DateTime DateOfBirth, ref string PhoneNumber, ref bool Sex, string Password, ref string Email, ref string Messages)
    {
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = @"select *from Member
where MemberID = @MemberID and Password = @Password";


        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@MemberID", MemberID);
        command.Parameters.AddWithValue("@Password", Password);

            try
            {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                // The record was found
                isFound = true;

                FirstName = (string)reader["FirstName"];
                SecondName = (string)reader["SecondName"];
                DateOfBirth = (DateTime)reader["DateOfBirth"];
                PhoneNumber = (string)reader["PhoneNumber"];
                Sex = (bool)reader["Sex"];
                Email = (string)reader["Email"];

                if (reader["Messages"] == DBNull.Value)
                    Messages = "";
                else
                    Messages = (string)reader["Messages"];


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