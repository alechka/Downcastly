using Microsoft.CodeAnalysis;

namespace Downcastly;

internal static class AccessabilityExtensions
{
    extension(Accessibility accessibility)
    {
        /// <summary>
        /// Converts type kind to c# syntax
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string ToSyntax() => accessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
            Accessibility.Private => "private",
            Accessibility.Protected => "protected",
            Accessibility.ProtectedAndInternal => "protected internal",
            Accessibility.ProtectedOrInternal => "private protected",
            _ => throw new ArgumentOutOfRangeException(nameof(accessibility), accessibility, null)
        };
    }
}