using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Text;

namespace Downcastly
{

    [Generator]
    public class BuilderGenerator : IIncrementalGenerator
    {        
        public const string AttributeName = "Downcastly.DowncastAttribute";

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            //#if DEBUG
            //Debugger.Launch();
            //#endif
            Debug.WriteLine("BuilderGenerator initialized");

            // Select all class declarations with attributes
            var classDeclarations = context.SyntaxProvider
                .ForAttributeWithMetadataName(AttributeName,
                    predicate: static (syntaxNode, ctx) =>
                        // using only class or record declarations
                        syntaxNode is ClassDeclarationSyntax or RecordDeclarationSyntax,                    
                    transform: TypeInfoBuilder.BuildClassInfo
                ).Where(static m => m is not null);

            context.RegisterSourceOutput(classDeclarations, static (spc, classInfo) =>
            {
                var source = PartialClassRenderer.RenderToString(classInfo);
                spc.AddSource($"{classInfo.Name}_Downcastly.g.cs", SourceText.From(source, Encoding.UTF8));
            });
        }
    }
}
