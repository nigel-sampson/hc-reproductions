using Common;

namespace Service
{
    public record Person(int Id, string Name);

    public class Query
    {
        public string Hello(string name) => $"Hello {name}";
    }

    public class Mutation
    {
        [GraphQLType(typeof(CreatePersonResultType))]
        public object CreatePerson(string name)
        {
            if (name is { Length: < 1})
            {
                return new ValidationErrors(new []{ new StringLengthValidationError("LN001") });
            }
            else
            {
                return new Person(1, name);
            }
        }
    }

    public class CreatePersonResultType : UnionType
    {
        protected override void Configure(IUnionTypeDescriptor descriptor)
        {
            descriptor.Name("CreatePersonResult")
                .Type<ObjectType<Person>>()
                .Type<ObjectType<ValidationErrors>>();
        }
    }
}
