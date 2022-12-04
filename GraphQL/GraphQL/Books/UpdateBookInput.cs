using HotChocolate;
using HotChocolate.Types.Relay;

namespace GraphQL.GraphQL.Books
{

    public record UpdateBookInput(int id, string Name, string Genre, int authorId);
}
