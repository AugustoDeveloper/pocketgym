using HotChocolate.Types;
using YourPocketGym.Application.DTOs;
using YourPocketGym.Application.GraphQL.Queries;

namespace YourPocketGym.Application.GraphQL.Types
{
    public class LoginType : ObjectType<Login>
    {
        protected override void Configure(IObjectTypeDescriptor<Login> descriptor)
        {
            descriptor
                .Field(u => u.Username)
                .Type<StringType>();
            
            descriptor
                .Field(u => u.AccessToken)
                .Type<StringType>();
        }
    }
}