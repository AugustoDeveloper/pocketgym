using System;
using System.Threading.Tasks;
using System.Threading;
using YourPocketGym.Application.DTOs;
using HotChocolate;

namespace YourPocketGym.Application.GraphQL.Mutations
{
    public partial class Mutation
    {
        public async Task<Login> SignUpAsync(RegistrationUser user)
        {
            await Task.CompletedTask;
            if (user.Password != user.ConfirmationPassword)
            {
                throw new GraphQLException("The passwords not matched");
            }

            return new Login
            {
                AccessToken = Guid.NewGuid().ToString(),
                Username = user.Username
            };
        }

        public Task<Login> SignInAsync(string username, string password)
        {
            return Task.FromResult(new Login
            {
                Username = username,
                AccessToken = Guid.NewGuid().ToString()
            });
        }
    }
}