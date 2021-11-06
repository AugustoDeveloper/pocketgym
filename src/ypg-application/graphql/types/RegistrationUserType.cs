using HotChocolate.Types;
using YourPocketGym.Application.DTOs;
using YourPocketGym.Application.GraphQL.Queries;

namespace YourPocketGym.Application.GraphQL.Types
{
    public class RegistrationUserType : ObjectType<RegistrationUser>
    {
        protected override void Configure(IObjectTypeDescriptor<RegistrationUser> descriptor)
        {
            descriptor
                .Field(u => u.Name)
                .Type<StringType>();
            
            descriptor
                .Field(u => u.Username)
                .Type<StringType>();

            descriptor
                .Field(u => u.Password)
                .Type<StringType>();

            descriptor
                .Field(u => u.ConfirmationPassword)
                .Type<StringType>();
        }
    }
}