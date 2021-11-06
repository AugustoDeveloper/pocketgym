using HotChocolate.Types;
using YourPocketGym.Application.DTOs;
using YourPocketGym.Application.GraphQL.Mutations;
using YourPocketGym.Application.GraphQL.Queries;

namespace YourPocketGym.Application.GraphQL.Mutations.Types
{
    public partial class MutationType : ObjectType<Mutation>
    {
        
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            ConfigureUser(descriptor);
        }
    }
}