#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Runtime.CompilerServices
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    //workaround for C# 9 init only setters in projects targeting netstandard2.0
    internal static class IsExternalInit { }
}