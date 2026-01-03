using Microsoft.CodeAnalysis;

namespace Downcastly
{
    internal static class PartialClassRenderer
    {
        const string SetRequeredMembersAttributeSyntax = "[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]";

        /// <summary>
        /// Renders the partial class as a string
        /// </summary>
        internal static string RenderToString(TypeInfo info)
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