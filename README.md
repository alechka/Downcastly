# Downcastly - A C# Library for Safe Downcasting with Compile-Time Type Checking 

Downcastly is a C# library that provides a safe and efficient way to downcast objects with compile-time type checking. It automatically generates downcasting 
constructors for your classes to copy all the properties from the base class to the derived class, ensuring type safety and reducing boilerplate code.


## usage example
```csharp
    public record ParentRecord
    {
        public int Id { get; init; }

        public string Name { get; init; }
    }

    [Downcast]
    public partial record ChildRecord : ParentRecord
    {
        public string Status { get; init; }
    }

    public class Program
    {
        public static void Main()
        {
            ParentRecord parent = new()
            {
                Id = 1,
                Name = "Parent"
            };

            // Safe downcasting using Downcastly
            ChildRecord child = new ChildRecord(parent) { Status = "active" };
            Console.WriteLine($"Id: {child.Id}, Name: {child.Name}, Status: {child.Status}");
        }
    }
```

## installation
```
dotnet add package Downcastly
```

## usefull tips

Add following line to your PropertyGroup of your .csproj file so you can see generated files in your obj folder:


```xml
<CompilerGeneratedFilesOutputPath>$(BaseIntermediateOutputPath)\Generated</CompilerGeneratedFilesOutputPath>
```