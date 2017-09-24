namespace Login
{
    public interface IUser
    {
        string EmailAddress { get; set; }
        string Password {get; set;}
    }
}