using JetBrains.Annotations;
using LanguageServer.VsCode.Contracts;
using Newtonsoft.Json.Linq;

namespace LanguageServer.Contracts {

[PublicAPI]
public enum InlayHintKind {
    Type = 1,
    Parameter = 2,
}

/// <summary>
/// 
/// </summary>
/// <param name="Position">The position of this hint.</param>
/// <param name="Label">The label of this hint. Cannot be empty.</param>
/// <param name="Kind">The kind of this hint. Can be omitted in which case the client should fall back to a reasonable default.</param>
[PublicAPI]
public record InlayHint(Position Position, string Label, InlayHintKind? Kind) {
    /// <summary>
    /// Optional text edits that are performed when accepting this inlay hint.
    /// *Note* that edits are expected to change the document so that the inlay
    /// hint (or its nearest variant) is now part of the document and the inlay
    /// hint itself is now obsolete.
    /// Depending on the client capability `inlayHint.resolveSupport` clients
    /// might resolve this property late using the resolve request.
    /// </summary>
    public TextEdit[]? TextEdits { get; set; }
    /// <summary>
    /// The tooltip text when you hover over this item.
    /// Depending on the client capability `inlayHint.resolveSupport` clients
    /// might resolve this property late using the resolve request.
    /// </summary>
    public MarkupContent? Tooltip { get; set; }
    /// <summary>
    /// Render padding before the hint.
    /// <br/>Note: Padding should use the editor's background color, not the background color of the hint itself. That means padding can be used to visually align/separate an inlay hint.
    /// </summary>
    public bool? PaddingLeft { get; set; }
    /// <summary>
    /// Render padding after the hint.
    /// <br/>Note: Padding should use the editor's background color, not the background color of the hint itself. That means padding can be used to visually align/separate an inlay hint.
    /// </summary>
    public bool? PaddingRight { get; set; }
    /// <summary>
    /// A data entry field that is preserved on a inlay hint between a `textDocument/inlayHint` and a `inlayHint/resolve` request.
    /// </summary>
    public JToken? Data { get; set; }
}
}