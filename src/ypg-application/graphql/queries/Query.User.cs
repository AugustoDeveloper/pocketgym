using YourPocketGym.Application.DTOs;

namespace YourPocketGym.Application.GraphQL.Queries
{
    public partial class Query
    {
        public User GetUserById(string id) => new User { Username = "user", Name = "User" };
    }
}