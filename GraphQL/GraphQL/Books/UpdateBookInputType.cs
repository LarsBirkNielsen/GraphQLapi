using HotChocolate.Types;

namespace GraphQL.GraphQL.Books
{
    public class UpdateBookInputType : InputObjectType<UpdateBookInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdateBookInput> descriptor)
        {
            descriptor.Description("Represents the input to update for a book.");

            descriptor
                .Field(p => p.id)
                .Description("Represents the unique identifier for the book.");

            base.Configure(descriptor);
        }
    }
}
