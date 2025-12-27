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
        private const string SetRequeredMembersAttributeSyntax = "[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]";
        public const string AttributeName = "Downcastly.DowncastAttribute";

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
//#if DEBUG
//            Debugger.Launch();
//#endif
            Debug.WriteLine("BuilderGenerator initialized");

            // Select all class declarations with attributes
            var classDeclarations = context.SyntaxProvider
                .ForAttributeWithMetadataName(AttributeName,
                    predicate: static (syntaxNode, ctx) =>
                        //using only class or record declarations with a parent (to avoid top-level)
                        syntaxNode is ClassDeclarationSyntax or RecordDeclarationSyntax,
                    transform: TypeInfoBuilder.BuildClassInfo
                ).Where(static m => m is not null);

            context.RegisterSourceOutput(classDeclarations, static (spc, classInfo) =>
            {
                var source = GenerateToString(classInfo);
                spc.AddSource($"{classInfo.Name}_Downcastly.g.cs", SourceText.From(source, Encoding.UTF8));
            });
        }

        private static string GenerateToString(TypeInfo info)
        {
            var ns = string.IsNullOrWhiteSpace(info.Namespace) ? "" : $"namespace {info.Namespace};";
            var copyPart = string.Join("\r\n", info.Properties.Select(p => $"         this.{p} = source.{p};"));
            var setRequiredMembers = info.SetsRequiredMembers ? SetRequeredMembersAttributeSyntax : "";

            var result = $@"
{ns}
{info.Accessibility.ToSyntax()} partial {info.Kind.ToSyntax()} {info.Name}
{{
    {setRequiredMembers}
    {info.ConstructorAccessibility} {info.Name}({info.BaseName} source)
    {{
{copyPart}
    }}
}}
";
            return result;
        }

    }
}
