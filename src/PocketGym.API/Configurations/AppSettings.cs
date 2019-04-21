using System;
namespace PocketGym.API.Configurations
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public JwtSettings JWT { get; set; }
        public string AdministratorPasswordHash { get; set; }
        public string AdministratorPasswordSalt { get; set; }
        public bool UpdateAdministratorPassword { get; set; }
    }

    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}