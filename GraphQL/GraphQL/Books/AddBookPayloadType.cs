using HotChocolate.Types;

namespace GraphQL.GraphQL.Books
{
    public class AddBookPayloadType : ObjectType<AddBookPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<AddBookPayload> descriptor)
        {
            descriptor.Description("Represents the payload to return for an added book.");

            descriptor
                .Field(c => c.book)
                .Description("Represents the added book.");

            base.Configure(descriptor);
        }
    }
}