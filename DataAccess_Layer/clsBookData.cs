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
    public class clsBookData
    {

    public static int AddNewBook(string BookName, string Author, string Genre, DateTime PublicationDate, int Quantity){
        int ID = -1;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = @"INSERT INTO Book ( 
                            BookName, Author, Genre, PublicationDate, Quantity)
                            VALUES (@BookName, @Author, @Genre, @PublicationDate, @Quantity);
                            SELECT SCOPE_IDENTITY();";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@BookName", BookName);
        if(Author == "")
        {
            command.Parameters.AddWithValue("@Author", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("@Author", Author );
        }
        command.Parameters.AddWithValue("@Genre", Genre);
        command.Parameters.AddWithValue("@PublicationDate", PublicationDate);
        command.Parameters.AddWithValue("@Quantity", Quantity);


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
    public static bool UpdateBook(int BookID, string BookName, string Author, string Genre, DateTime PublicationDate, int Quantity){
        int rowsAffected = 0;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = @"Update Book set 
                            
BookName = @BookName,
Author = @Author,
Genre = @Genre,
PublicationDate = @PublicationDate,
Quantity = @Quantity
                            where BookID = @BookID";
                            

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@BookID", BookID);
        command.Parameters.AddWithValue("@BookName", BookName);
        if(Author == "")
        {
            command.Parameters.AddWithValue("@Author", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("@Author", Author );
        }
        command.Parameters.AddWithValue("@Genre", Genre);
        command.Parameters.AddWithValue("@PublicationDate", PublicationDate);
        command.Parameters.AddWithValue("@Quantity", Quantity);


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
    public static bool GetBookInfoByBookID(int BookID, ref string BookName, ref string Author, ref string Genre, ref DateTime PublicationDate, ref int Quantity){
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = "SELECT * FROM Book WHERE BookID = @BookID";
                            

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@BookID", BookID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;
                    
                    BookName = (string)reader["BookName"];
                    
                    if (reader["Author"] == DBNull.Value)
                        Author = "";
                    else
                        Author = (string)reader["Author"];

                    Genre = (string)reader["Genre"];
                    PublicationDate = (DateTime)reader["PublicationDate"];
                    Quantity = (int)reader["Quantity"];

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
    public static bool DeleteBook(int BookID)
    {

        int rowsAffected = 0;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = @"Delete Book 
                            where BookID = @BookID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@BookID", BookID);

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
    public static bool IsBookExist(int BookID)
    {
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = "SELECT Found=1 FROM Book WHERE BookID = @BookID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@BookID", BookID);

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
    public static DataTable GetAllBook()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Book";

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

    }
}