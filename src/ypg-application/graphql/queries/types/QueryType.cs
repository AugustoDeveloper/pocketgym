using HotChocolate.Types;
using YourPocketGym.Application.DTOs;
using YourPocketGym.Application.GraphQL.Queries;

namespace YourPocketGym.Application.GraphQL.Queries.Types
{
    public partial class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            ConfigureUser(descriptor);
        }
    }
}