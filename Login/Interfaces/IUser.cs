using System.Security.Principal;

namespace Login
{
    public interface IUser
    {
        string Username { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string EmailAddress { get; set; }
        string Address { get; set; }
        string Password {get; set;}
        System.DateTime DateOfBirth { get; set; }


    }

    public enum UserStatusEnum
    {

    }
}