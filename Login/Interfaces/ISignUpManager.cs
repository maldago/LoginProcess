﻿namespace Login
{
    public interface ISignUpManager
    {
        RegistrationStatus Register(IUser user);
        LoginStatus Login(IUser user);
    }
}