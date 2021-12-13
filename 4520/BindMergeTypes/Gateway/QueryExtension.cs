using Common;
using HotChocolate.Language;

namespace Gateway
{
    [ExtendObjectType("Query")]
    public class QueryExtension
    {
        public string Goodbye(string name) => $"Goodbye {name}";
    }

    public record FileUploadPath(string Url);

    [ExtendObjectType("Mutation")]
    public class MutationExtension
    {
        [GraphQLType(typeof(CreateFileUploadPathResultType))]
        public object CreateFileUploadPath(string path)
        {
            if (path is { Length: < 1 })
            {
                return new ValidationErrors(new[] { new StringLengthValidationError("URL013") });
            }
            else
            {
                return new FileUploadPath("https://s3.aws.com");
            }
        }
    }

    public class CreateFileUploadPathResultType : UnionType
    {
        protected override void Configure(IUnionTypeDescriptor descriptor)
        {
            descriptor.Name("CreateFileUploadPathResult")
                .Type<ObjectType<FileUploadPath>>()
                .Type(new NamedTypeNode(nameof(ValidationErrors)));
        }
    }
}
