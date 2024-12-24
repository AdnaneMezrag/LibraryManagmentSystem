using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DataAccess_Layer;

namespace Business_Layer{
    public class clsMember
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int MemberID {set;get;}
        public string FirstName {set;get;}
        public string SecondName {set;get;}
        public DateTime DateOfBirth {set;get;}
        public string PhoneNumber {set;get;}
        public bool Sex {set;get;}
        public string Password {set;get;}
        public string Email {set;get;}
        public string Messages {set;get;}
    public clsMember (){
        this.MemberID = -1;
        this.FirstName = "";
        this.SecondName = "";
        this.DateOfBirth = DateTime.Now;
        this.PhoneNumber = "";
        this.Sex = false;
        this.Password = "";
        this.Email = "";
        this.Messages = "";


        this.Mode = enMode.AddNew;
}
    private clsMember (int MemberID, string FirstName, string SecondName, DateTime DateOfBirth, string PhoneNumber, bool Sex, string Password, string Email, string Messages){
        this.MemberID = MemberID;
        this.FirstName = FirstName;
        this.SecondName = SecondName;
        this.DateOfBirth = DateOfBirth;
        this.PhoneNumber = PhoneNumber;
        this.Sex = Sex;
        this.Password = Password;
        this.Email = Email;
        this.Messages = Messages;



        this.Mode = enMode.Update;
}
    private bool _AddNewMember(){
        this.MemberID = clsMemberData.AddNewMember(this.FirstName, this.SecondName, this.DateOfBirth, this.PhoneNumber, this.Sex, this.Password, this.Email, this.Messages);
        return (this.MemberID != -1);
    }
    private bool _UpdateMember(){
        return clsMemberData.UpdateMember(this.MemberID, this.FirstName, this.SecondName, this.DateOfBirth, this.PhoneNumber, this.Sex, this.Password, this.Email, this.Messages);
    }
    public static clsMember Find(int MemberID){
        string FirstName = "";
        string SecondName = "";
        DateTime DateOfBirth = DateTime.Now;
        string PhoneNumber = "";
        bool Sex = false;
        string Password = "";
        string Email = "";
        string Messages = "";

        bool IsFound = clsMemberData.GetMemberInfoByMemberID(
            MemberID, ref FirstName, ref SecondName, ref DateOfBirth, ref PhoneNumber, ref Sex, ref Password, ref Email, ref Messages);

        if (IsFound){
            return new clsMember(MemberID, FirstName, SecondName, DateOfBirth, PhoneNumber, Sex, Password, Email, Messages);}
        else{ return null;}
    }
    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                if (_AddNewMember())
                {

                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            case enMode.Update:

                return _UpdateMember();

        }

        return false;
    }
    public bool Delete()
    {
        return clsMemberData.DeleteMember(this.MemberID); 
    }
    public static bool IsMemberExist(int MemberID)
    {
        return clsMemberData.IsMemberExist(MemberID); 
    }
    public static DataTable GetAllMember()
    {
        return clsMemberData.GetAllMember();

    }

    }
}