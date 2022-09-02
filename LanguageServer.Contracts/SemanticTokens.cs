namespace LanguageServer.Contracts {

//We don't have a SemanticTokens class since they're sent as uint[]

//Note: you don't have to use SemanticTokenTypes/SemanticTokenModifiers,
// you can declare your own strings, just make sure they correspond with your legend
public static class DefaultSemanticTokenTypes {
    public const string Namespace = "namespace";
    /// <summary>
    /// Represents a generic type. Acts as a fallback for types which can't be mapped to a specific type like class or enum.
    /// </summary>
    public const string Type = "type";
    public const string Class = "class";
    public const string Enum = "enum";
    public const string Interface = "interface";
    public const string Struct = "struct";
    public const string TypeParameter = "typeParameter";
    public const string Parameter = "parameter";
    public const string Variable = "variable";
    public const string Property = "property";
    public const string EnumMember = "enumMember";
    public const string Event = "event";
    public const string Function = "function";
    public const string Method = "method";
    public const string Macro = "macro";
    public const string Keyword = "keyword";
    public const string Modifier = "modifier";
    public const string Comment = "comment";
    public const string String = "string";
    public const string Number = "number";
    public const string Regexp = "regexp";
    public const string Operator = "operator";
    public const string Decorator = "decorator";

    public static readonly string[] Values = {
        Namespace,
        Type,
        Class,
        Enum,
        Interface,
        Struct,
        TypeParameter,
        Parameter,
        Variable,
        Property,
        EnumMember,
        Event,
        Function,
        Method,
        Macro,
        Keyword,
        Modifier,
        Comment,
        String,
        Number,
        Regexp,
        Operator,
        Decorator
    };
}

public static class DefaultSemanticTokenModifiers {
    public const string Declaration = "declaration";
    public const string Definition = "definition";
    public const string Readonly = "readonly";
    public const string Static = "static";
    public const string Deprecated = "deprecated";
    public const string Abstract = "abstract";
    public const string Async = "async";
    public const string Modification = "modification";
    public const string Documentation = "documentation";
    public const string DefaultLibrary = "defaultLibrary";

    public static readonly string[] Values = {
        Declaration, Definition, Readonly, Static,
        Deprecated, Abstract, Async, Modification, Documentation,
        DefaultLibrary
    };
}

/// <summary>
/// Response struct for textDocument/semanticTokens/full
/// </summary>
public record SemanticTokensResponse(uint[] Data, string? ResultId = null);
}