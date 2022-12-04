using HotChocolate.Types;

namespace GraphQL.GraphQL.Authors
{
    public class UpdateAuthorInputType : InputObjectType<UpdateAuthorInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdateAuthorInput> descriptor)
        {
            descriptor.Description("Represents the input to add for an author.");


            descriptor
                .Field(author => author.id)
                .Description("Represents the id of the author.");

            //descriptor
            //    .Field(author => author.BookId)
            //    .Description("Represents the unique ID of the book which the author belongs to.");

            base.Configure(descriptor);
        }
    }
}
