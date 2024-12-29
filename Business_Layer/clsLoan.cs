using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DataAccess_Layer;

namespace Business_Layer{
    public class clsLoan
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LoanID {set;get;}
        public DateTime LoanDate {set;get;}
        public bool IsReturned {set;get;}
        public DateTime ReturnDate {set;get;}
        public int MemberID {set;get;}
        public int BookID {set;get;}
        public clsMember Member {set;get;}
        public clsBook Book {set;get;}
    public clsLoan (){
        this.LoanID = -1;
        this.LoanDate = DateTime.Now;
        this.IsReturned = false;
        this.ReturnDate = DateTime.Now;
        this.MemberID = -1;
        this.BookID = -1;


        this.Mode = enMode.AddNew;
}
    private clsLoan (int LoanID, DateTime LoanDate, bool IsReturned, DateTime ReturnDate, int MemberID, int BookID){
        this.LoanID = LoanID;
        this.LoanDate = LoanDate;
        this.IsReturned = IsReturned;
        this.ReturnDate = ReturnDate;
        this.MemberID = MemberID;
        this.BookID = BookID;

        this.Member = clsMember.Find(MemberID);
        this.Book = clsBook.Find(BookID);


        this.Mode = enMode.Update;
}
    private bool _AddNewLoan(){
        this.LoanID = clsLoanData.AddNewLoan(this.LoanDate, this.IsReturned, this.ReturnDate, this.MemberID, this.BookID);
        return (this.LoanID != -1);
    }
    private bool _UpdateLoan(){
        return clsLoanData.UpdateLoan(this.LoanID, this.LoanDate, this.IsReturned, this.ReturnDate, this.MemberID, this.BookID);
    }
    public static clsLoan Find(int LoanID){
        DateTime LoanDate = DateTime.Now;
        bool IsReturned = false;
        DateTime ReturnDate = DateTime.Now;
        int MemberID = -1;
        int BookID = -1;

        bool IsFound = clsLoanData.GetLoanInfoByLoanID(
            LoanID, ref LoanDate, ref IsReturned, ref ReturnDate, ref MemberID, ref BookID);

        if (IsFound){
            return new clsLoan(LoanID, LoanDate, IsReturned, ReturnDate, MemberID, BookID);}
        else{ return null;}
    }
    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                if (_AddNewLoan())
                {

                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            case enMode.Update:

                return _UpdateLoan();

        }

        return false;
    }
    public bool Delete()
    {
        return clsLoanData.DeleteLoan(this.LoanID); 
    }
    public static bool IsLoanExist(int LoanID)
    {
        return clsLoanData.IsLoanExist(LoanID); 
    }
    public static DataTable GetAllLoan()
    {
        return clsLoanData.GetAllLoan();

    }
    public static DataTable GetBooksStatistics()
        {
            return clsLoanData.GetBooksStatistics();
        }
    }
}