using HotChocolate.Types;

namespace GraphQL.GraphQL.Books
{
    public class UpdateBookPayloadType : ObjectType<UpdateBookPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<UpdateBookPayload> descriptor)
        {
            descriptor.Description("Represents the payload to return for the updated book.");

            descriptor
                .Field(c => c.book)
                .Description("Represents the updated book.");

            base.Configure(descriptor);
        }
    }
}