using HotChocolate.Types;
using YourPocketGym.Application.DTOs;
using YourPocketGym.Application.GraphQL.Queries;

namespace YourPocketGym.Application.GraphQL.Types
{
    public class ExerciseType : ObjectType<Exercise>
    {
        protected override void Configure(IObjectTypeDescriptor<Exercise> descriptor)
        {
            descriptor
                .Field(u => u.Name)
                .Type<StringType>();
            
        }
    }
}