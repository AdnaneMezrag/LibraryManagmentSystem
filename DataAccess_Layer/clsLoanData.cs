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
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace DataAccess_Layer{
    public class clsLoanData
    {

    public static int AddNewLoan(DateTime LoanDate, bool IsReturned, DateTime ReturnDate, int MemberID, int BookID){
        int ID = -1;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = @"INSERT INTO Loan ( 
                            LoanDate, IsReturned, ReturnDate, MemberID, BookID)
                            VALUES (@LoanDate, @IsReturned, @ReturnDate, @MemberID, @BookID);
                            SELECT SCOPE_IDENTITY();
update book set book.Quantity = book.Quantity - 1
where bookid = @bookID;";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@LoanDate", LoanDate);
        command.Parameters.AddWithValue("@IsReturned", IsReturned);
        if(ReturnDate == DateTime.MaxValue)
        {
            command.Parameters.AddWithValue("@ReturnDate", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("@ReturnDate", ReturnDate );
        }
        command.Parameters.AddWithValue("@MemberID", MemberID);
        command.Parameters.AddWithValue("@BookID", BookID);


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
    public static bool UpdateLoan(int LoanID, DateTime LoanDate, bool IsReturned, DateTime ReturnDate, int MemberID, int BookID){
        int rowsAffected = 0;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = @"Update Loan set 
                            
LoanDate = @LoanDate,
IsReturned = @IsReturned,
ReturnDate = @ReturnDate,
MemberID = @MemberID,
BookID = @BookID
                            where LoanID = @LoanID";
                            

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@LoanID", LoanID);
        command.Parameters.AddWithValue("@LoanDate", LoanDate);
        command.Parameters.AddWithValue("@IsReturned", IsReturned);
        if(ReturnDate == DateTime.MaxValue)
        {
            command.Parameters.AddWithValue("@ReturnDate", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("@ReturnDate", ReturnDate );
        }
        command.Parameters.AddWithValue("@MemberID", MemberID);
        command.Parameters.AddWithValue("@BookID", BookID);


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
    public static bool GetLoanInfoByLoanID(int LoanID, ref DateTime LoanDate, ref bool IsReturned, ref DateTime ReturnDate, ref int MemberID, ref int BookID){
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        string query = "SELECT * FROM Loan WHERE LoanID = @LoanID";
                            

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@LoanID", LoanID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;
                    
                    LoanDate = (DateTime)reader["LoanDate"];
                    IsReturned = (bool)reader["IsReturned"];
                    
                    if (reader["ReturnDate"] == DBNull.Value)
                        ReturnDate = DateTime.MaxValue;
                    else
                        ReturnDate = (DateTime)reader["ReturnDate"];

                    MemberID = (int)reader["MemberID"];
                    BookID = (int)reader["BookID"];

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
    public static bool DeleteLoan(int LoanID)
    {

        int rowsAffected = 0;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = @"Delete Loan 
                            where LoanID = @LoanID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@LoanID", LoanID);

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
    public static bool IsLoanExist(int LoanID)
    {
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = "SELECT Found=1 FROM Loan WHERE LoanID = @LoanID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@LoanID", LoanID);

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
    public static DataTable GetAllLoan()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Loan";

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
    public static DataTable GetBooksStatistics()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT 
    DATENAME(MONTH, LoanDate) AS Month,  -- Returns the month name (e.g., January)
    COUNT(*) AS [Number Of Borrowing]
FROM 
    Loan
GROUP BY 
    DATENAME(MONTH, LoanDate), MONTH(LoanDate)  -- Grouping by both to preserve order
ORDER BY 
    MONTH(LoanDate);  -- Ensures chronological order
";

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

    public static DataTable GetPopularBooks()
    {

        DataTable dt = new DataTable();
        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = @"select BookID as [Book ID] , count(*) as [Number Of Borrowing]
from loan
group by bookid order by [Number Of Borrowing] desc";

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
    public static bool IsBookBorrowedByMember(int MemberID , int BookID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Found = 1 from loan where bookid = @BookID 
and MemberID = @MemberID and IsReturned = 0;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MemberID", MemberID);
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
    public static bool ReturnBook(int MemberID , int BookID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update loan set isReturned = 1 , ReturnDate = GETDATE() 
where MemberID = @MemberID and bookid = @BookID and IsReturned = 0;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MemberID", MemberID);
            command.Parameters.AddWithValue("@BookID", BookID);

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

    }
}