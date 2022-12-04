using HotChocolate.Types;

namespace GraphQL.GraphQL.Books
{
    public class AddBookInputType : InputObjectType<AddBookInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddBookInput> descriptor)
        {
            descriptor.Description("Represents the input to add for a book.");

            descriptor
                .Field(p => p.Name)
                .Description("Represents the name for the book.");

            base.Configure(descriptor);
        }
    }
}
