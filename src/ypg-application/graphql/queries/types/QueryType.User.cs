using HotChocolate.Types;
using YourPocketGym.Application.DTOs;
using YourPocketGym.Application.GraphQL.Queries;
using YourPocketGym.Application.GraphQL.Types;

namespace YourPocketGym.Application.GraphQL.Queries.Types
{
    public partial class QueryType : ObjectType<Query>
    {
        private void ConfigureUser(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor
                .Field(x => x.GetUserById(default!))
                .Name("user")
                .Type<UserType>();
        }
    }
}