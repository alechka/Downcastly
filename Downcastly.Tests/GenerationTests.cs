using Shouldly;

namespace Downcastly.Tests
{
    public class GenerationTests
    {
        [Fact]
        public void TestClasses_Inheritance()
        {
            var parent = new ParentClassEntity() { Id = 10 };
            var child = new ChildClassEntity(parent);
            child.ShouldNotBeSameAs(parent);
            child.Id.ShouldBe(parent.Id);
            child.Name.ShouldBeNull();
        }

        [Fact]
        public void TestClasses_Inheritance_WithMultipleProperties()
        {
            var parent = new MultiplePropertiesClassEntity { Id = 3, Age = 27, ParentClass = new() { Id = 2 }, Description = "description", Tags = [13, 30] };
            var child = new MultiplePropertiesDeriviedClassEntity(parent);
            child.ShouldNotBeSameAs(parent);
            child.Id.ShouldBe(parent.Id);
            child.Age.ShouldBe(parent.Age);
            child.ParentClass.ShouldBe(parent.ParentClass);
            child.Description.ShouldBe(parent.Description);
            child.Tags.ShouldBeEquivalentTo(parent.Tags);
        }

        [Fact]
        public void TestClasses_Inheritance_WithoutProperties()
        {
            var parent = new EmptyParent();
            var child = new EmptyChild(parent);
            child.ShouldNotBeSameAs(parent);
        }

        [Fact]
        public void TestClasses_InternalInheritance()
        {
            var parent = new InternalParentClassEntity() { Id = 10 };
            var child = new InternalChildClassEntity(parent);
            child.ShouldNotBeSameAs(parent);
            child.Id.ShouldBe(parent.Id);
            child.Name.ShouldBeNull();
        }

        [Fact]
        public void TestClasses_InitProperty()
        {
            var parent = new ParentWithInitProperty() { Id = 10 };
            var child = new ChildWithInitProperty(parent);
            child.ShouldNotBeSameAs(parent);
            child.Id.ShouldBe(parent.Id);
        }

        [Fact]
        public void TestClasses_GetOnly()
        {
            var parent = new ParentWithGetOnlyProperty();
            var child = new ChildWithGetOnlyProperty(parent);
            child.ShouldNotBeSameAs(parent);
            child.Id.ShouldBe(parent.Id);
        }

        [Fact]
        public void TestRecord_WithInit() 
        {
            var parent = new ParentRecordInitProperty() { Id = 10 };
            var child = new ChildRecordInitProperty(parent) { Status = "hey!"};
            child.ShouldNotBeSameAs(parent);
            child.Id.ShouldBe(parent.Id);
        }
    }
}
