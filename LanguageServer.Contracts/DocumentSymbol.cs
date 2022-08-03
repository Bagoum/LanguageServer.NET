using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LanguageServer.VsCode.Contracts {

/// <summary>
/// </summary>
/// <param name="Name">The name of this symbol. Will be displayed in the user interface and therefore must not be an empty string or a string only consisting of white spaces.</param>
/// <param name="Detail">More detail for this symbol, e.g the signature of a function.</param>
/// <param name="Kind">The kind of this symbol.</param>
/// <param name="Tags">Tags for this document symbol.</param>
/// <param name="Range">The range enclosing this symbol not including leading/trailing whitespace but everything else like comments. This information is typically used to determine if the clients cursor is inside the symbol to reveal in the symbol in the UI.</param>
/// <param name="SelectionRange">The range that should be selected and revealed when this symbol is being picked, e.g. the name of a function. Must be contained by the `range`.</param>
/// <param name="Children">Children of this symbol, e.g. properties of a class.</param>
[JsonObject(MemberSerialization.OptOut)]
public record DocumentSymbol(string Name, string? Detail, SymbolKind Kind, SymbolTag[] Tags, Range Range,
    Range SelectionRange, DocumentSymbol[]? Children) {

    public DocumentSymbol(string name, string? detail, SymbolKind kind, Range range,
        IEnumerable<DocumentSymbol>? children = null) : 
        this(name, detail, kind, Array.Empty<SymbolTag>(), range, range, children?.ToArray()) { }
}
}