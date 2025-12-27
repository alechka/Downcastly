namespace Downcastly;

internal static class TypeKindExtensions
{
    extension(TypeKind kind)
    {
        /// <summary>
        /// Converts type kind to c# syntax
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string ToSyntax() => kind switch
        {
            TypeKind.Class => "class",
            TypeKind.Record => "record",
            _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
        };
    }
}