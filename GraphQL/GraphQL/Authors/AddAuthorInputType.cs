using HotChocolate.Types;

namespace GraphQL.GraphQL.Authors
{
    public class AddAuthorInputType : InputObjectType<AddAuthorInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddAuthorInput> descriptor)
        {
            descriptor.Description("Represents the input to add for an author.");

            descriptor
                .Field(author => author.Name)
                .Description("Represents the name of the author.");
            descriptor
                .Field(author => author.Age)
                .Description("Represents the age of the author.");
            //descriptor
            //    .Field(author => author.BookId)
            //    .Description("Represents the unique ID of the book which the author belongs to.");

            base.Configure(descriptor);
        }
    }
}
