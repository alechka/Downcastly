using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace Downcastly
{
    internal static class TypeInfoBuilder
    {
        internal static TypeInfo BuildClassInfo(GeneratorAttributeSyntaxContext ctx, CancellationToken ct)
        {
            var classSymbol = (INamedTypeSymbol)ctx.TargetSymbol;
            if (classSymbol == null)
            {
                return null!;
            }

            var baseType = classSymbol.BaseType;
            if (baseType is null)
            {
                return null!;
            }

            var displayName = classSymbol.ContainingNamespace.IsGlobalNamespace ?
                string.Empty
                : classSymbol.ContainingNamespace.ToDisplayString();
            bool setsRequiredMembers = GetSetRequiredMembersAttributeValue(ctx);

            return new()
            {
                Name = classSymbol.Name,
                Kind = GetKind(ctx.TargetNode),
                Namespace = displayName,
                Properties = GetPropertiesNames(baseType),
                BaseName = baseType.Name,
                Accessibility = classSymbol.DeclaredAccessibility,
                SetsRequiredMembers = setsRequiredMembers,
                ConstructorAccessibility = GetAccessabilityAttributeValue(ctx)
            };
        }

        /// <summary>
        /// Gets the value of the SetsRequiredMembers attribute
        /// </summary>
        private static bool GetSetRequiredMembersAttributeValue(GeneratorAttributeSyntaxContext ctx)
        {
            var arguments = ctx.Attributes.FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == BuilderGenerator.AttributeName)?
                            .NamedArguments
                            .ToDictionary(a => a.Key, a => a.Value);

            Debug.Assert(arguments != null, "attribute != null");
            var setsRequiredMembers = false;
            if (arguments!.TryGetValue("SetsRequeiredMembers", out var memberFound))
            {
                setsRequiredMembers = memberFound.Value as bool? == true;
            }

            return setsRequiredMembers;
        }

        /// <summary>
        /// Gets the value of the SetsRequiredMembers attribute
        /// </summary>
        private static string GetAccessabilityAttributeValue(GeneratorAttributeSyntaxContext ctx)
        {
            var arguments = ctx.Attributes.FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == BuilderGenerator.AttributeName)?
                            .NamedArguments
                            .ToDictionary(a => a.Key, a => a.Value);

            Debug.Assert(arguments != null, "attribute != null");
            
            var attributeValue = "public";

            if (arguments!.TryGetValue("ConstructorAccessibility", out var memberFound))
            {
                attributeValue = memberFound.Value as string ?? attributeValue;
            }

            return attributeValue;
        }

        private static TypeKind GetKind(SyntaxNode targetNode)
        {
            return targetNode switch
            {
                RecordDeclarationSyntax => TypeKind.Record,
                ClassDeclarationSyntax => TypeKind.Class,
                _ => throw new ArgumentOutOfRangeException($"Unsupported type kind {targetNode.GetType().FullName}")
            };
        }

        private static List<string> GetPropertiesNames(INamedTypeSymbol baseType)
        {
            var properties = baseType?.GetMembers().OfType<IPropertySymbol>()
                            // selecting only instance, non-private, read-write properties
                            .Where(p => !p.IsStatic && p.DeclaredAccessibility != Accessibility.Private
                            && p.GetMethod != null && p.SetMethod != null)
                            .ToList() ?? [];

            var propertyNames = properties.Select(p => p.Name).ToList();
            return propertyNames;
        }
    }
}