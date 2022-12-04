using HotChocolate.Types;

namespace GraphQL.GraphQL.Authors
{
    public class UpdateAuthorPayloadType : ObjectType<UpdateAuthorPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<UpdateAuthorPayload> descriptor)
        {
            descriptor.Description("Represents the payload to return for an added author.");

            descriptor
                .Field(author => author.author)
                .Description("Represents the unique identifier of the updated author.");

            base.Configure(descriptor);
        }
    }
}