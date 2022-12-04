using HotChocolate.Types;

namespace GraphQL.GraphQL.Authors
{
    public class AddAuthorPayloadType : ObjectType<AddAuthorPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<AddAuthorPayload> descriptor)
        {
            descriptor.Description("Represents the payload to return for an added author.");

            descriptor
                .Field(author => author.author)
                .Description("Represents the added author.");

            base.Configure(descriptor);
        }
    }
}