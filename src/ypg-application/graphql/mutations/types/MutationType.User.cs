using HotChocolate.Types;
using YourPocketGym.Application.DTOs;
using YourPocketGym.Application.GraphQL.Mutations;
using YourPocketGym.Application.GraphQL.Queries;
using YourPocketGym.Application.GraphQL.Types;

namespace YourPocketGym.Application.GraphQL.Mutations.Types
{
    public partial class MutationType : ObjectType<Mutation>
    {
        
        private void ConfigureUser(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor
                .Field(l => l.SignInAsync(default!, default!))
                .Name("signin")
                .Type<LoginType>();

            descriptor
                .Field(l => l.SignUpAsync(default!))
                .Name("signup")
                .Type<LoginType>();
        }
    }
}