namespace Downcastly.Tests
{
    public class ParentClassEntity
    {
        public int Id { get; set; }
    }

    [Downcast]
    public partial class ChildClassEntity : ParentClassEntity
    {
        public string? Name { get; set; }
    }

    internal class InternalParentClassEntity
    {
        public int Id { get; set; }
    }

    [Downcast]
    internal partial class InternalChildClassEntity : InternalParentClassEntity
    {
        public string? Name { get; set; }
    }

    public class MultiplePropertiesClassEntity
    {
        public int Id { get; set; }

        public int Age { get; set; }

        public string? Description { get; set; }

        public ParentClassEntity? ParentClass { get; set; }

        public List<int> Tags { get; set; } = [1,2,3 ];
    }

    [Downcast]
    public  partial class MultiplePropertiesDeriviedClassEntity : MultiplePropertiesClassEntity
    {
    }

    public class EmptyParent
    {

    }

    [Downcast]
    public partial class EmptyChild : EmptyParent
    {
    }

    // todo not supported yet
    //public class ContainerClass
    //{
    //    public class NestedParentClass
    //    {
    //        public int Id { get; set; }
    //    }
    //}

    //[Downcast]
    //public partial class NestedChildClass : NestedParentClass
    //{
    //    public string Name { get; set; }
    //}

    public class ParentWithInitProperty
    {
        public int Id { get; init; }
    }

    [Downcast]
    public partial class ChildWithInitProperty : ParentWithInitProperty
    {
    }

    public class ParentWithGetOnlyProperty
    {
#pragma warning disable CA1822 // Mark members as static
        public int Id => 42;
#pragma warning restore CA1822 // Mark members as static
    }

    [Downcast]
    public partial class ChildWithGetOnlyProperty : ParentWithGetOnlyProperty
    {
    }

    public class ParentWithPrivateProperty
    {
        public int Id { get; init; }

        private string? Status { get; init; }
    }

    [Downcast]
    public partial class ChildWithPrivateProperty : ParentWithPrivateProperty
    {
    }

    public record ParentRecordInitProperty
    {
        public int Id { get; init; }
    }

    [Downcast]
    public partial record ChildRecordInitProperty : ParentRecordInitProperty
    {
        public required string Status { get; init; }
    }
}