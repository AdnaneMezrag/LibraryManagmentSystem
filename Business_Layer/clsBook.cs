using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DataAccess_Layer;

namespace Business_Layer{
    public class clsBook
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int BookID {set;get;}
        public string BookName {set;get;}
        public string Author {set;get;}
        public string Genre {set;get;}
        public DateTime PublicationDate {set;get;}
        public int Quantity {set;get;}
    public clsBook (){
        this.BookID = -1;
        this.BookName = "";
        this.Author = "";
        this.Genre = "";
        this.PublicationDate = DateTime.Now;
        this.Quantity = -1;


        this.Mode = enMode.AddNew;
}
    private clsBook (int BookID, string BookName, string Author, string Genre, DateTime PublicationDate, int Quantity){
        this.BookID = BookID;
        this.BookName = BookName;
        this.Author = Author;
        this.Genre = Genre;
        this.PublicationDate = PublicationDate;
        this.Quantity = Quantity;



        this.Mode = enMode.Update;
}
    private bool _AddNewBook(){
        this.BookID = clsBookData.AddNewBook(this.BookName, this.Author, this.Genre, this.PublicationDate, this.Quantity);
        return (this.BookID != -1);
    }
    private bool _UpdateBook(){
        return clsBookData.UpdateBook(this.BookID, this.BookName, this.Author, this.Genre, this.PublicationDate, this.Quantity);
    }
    public static clsBook Find(int BookID){
        string BookName = "";
        string Author = "";
        string Genre = "";
        DateTime PublicationDate = DateTime.Now;
        int Quantity = -1;

        bool IsFound = clsBookData.GetBookInfoByBookID(
            BookID, ref BookName, ref Author, ref Genre, ref PublicationDate, ref Quantity);

        if (IsFound){
            return new clsBook(BookID, BookName, Author, Genre, PublicationDate, Quantity);}
        else{ return null;}
    }
    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                if (_AddNewBook())
                {

                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            case enMode.Update:

                return _UpdateBook();

        }

        return false;
    }
    public bool Delete()
    {
        return clsBookData.DeleteBook(this.BookID); 
    }
    public static bool IsBookExist(int BookID)
    {
        return clsBookData.IsBookExist(BookID); 
    }
    public static DataTable GetAllBook()
    {
        return clsBookData.GetAllBook();

    }


    }
}