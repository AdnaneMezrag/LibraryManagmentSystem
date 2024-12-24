using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DataAccess_Layer;

namespace Business_Layer{
    public class clsUser
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID {set;get;}
        public string UserName {set;get;}
        public string Password {set;get;}
        public bool IsActive {set;get;}
        public string FirstName {set;get;}
        public string SecondName {set;get;}
    public clsUser (){
        this.UserID = -1;
        this.UserName = "";
        this.Password = "";
        this.IsActive = false;
        this.FirstName = "";
        this.SecondName = "";


        this.Mode = enMode.AddNew;
}
    private clsUser (int UserID, string UserName, string Password, bool IsActive, string FirstName, string SecondName){
        this.UserID = UserID;
        this.UserName = UserName;
        this.Password = Password;
        this.IsActive = IsActive;
        this.FirstName = FirstName;
        this.SecondName = SecondName;



        this.Mode = enMode.Update;
}
    private bool _AddNewUser(){
        this.UserID = clsUserData.AddNewUser(this.UserName, this.Password, this.IsActive, this.FirstName, this.SecondName);
        return (this.UserID != -1);
    }
    private bool _UpdateUser(){
        return clsUserData.UpdateUser(this.UserID, this.UserName, this.Password, this.IsActive, this.FirstName, this.SecondName);
    }
    public static clsUser Find(int UserID){
        string UserName = "";
        string Password = "";
        bool IsActive = false;
        string FirstName = "";
        string SecondName = "";

        bool IsFound = clsUserData.GetUserInfoByUserID(
            UserID, ref UserName, ref Password, ref IsActive, ref FirstName, ref SecondName);

        if (IsFound){
            return new clsUser(UserID, UserName, Password, IsActive, FirstName, SecondName);}
        else{ return null;}
    }
    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                if (_AddNewUser())
                {

                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            case enMode.Update:

                return _UpdateUser();

        }

        return false;
    }
    public bool Delete()
    {
        return clsUserData.DeleteUser(this.UserID); 
    }
    public static bool IsUserExist(int UserID)
    {
        return clsUserData.IsUserExist(UserID); 
    }
    public static DataTable GetAllUser()
    {
        return clsUserData.GetAllUser();

    }

    }
}