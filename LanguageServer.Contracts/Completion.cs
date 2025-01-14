﻿using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LanguageServer.VsCode.Contracts {
/// <summary>
/// The kind of a completion entry.
/// </summary>
public enum CompletionItemKind {
    Text = 1,
    Method = 2,
    Function = 3,
    Constructor = 4,
    Field = 5,
    Variable = 6,
    Class = 7,
    Interface = 8,
    Module = 9,
    Property = 10,
    Unit = 11,
    Value = 12,
    Enum = 13,
    Keyword = 14,
    Snippet = 15,
    Color = 16,
    File = 17,
    Reference = 18,
}

/// <summary>
/// Defines whether the insert text in a completion item should be interpreted as
/// plain text or a snippet.
/// </summary>
public enum InsertTextFormat {
    /// <summary>
    /// The primary text to be inserted is treated as a plain string.
    /// </summary>
    PlainText = 1,
    /// <summary>
    ///  The primary text to be inserted is treated as a snippet.
    /// </summary>
    /// <remarks>
    /// <para>A snippet can define tab stops and placeholders with <c>$1</c>, <c>$2</c>
    /// and <c>${3:foo}</c>. <c>$0</c> defines the final tab stop, it defaults to
    /// the end of the snippet.Placeholders with equal identifiers are linked,
    /// that is typing in one will update others too.</para>
    /// <para>
    /// See also: https://github.com/Microsoft/vscode/blob/master/src/vs/editor/contrib/snippet/common/snippet.md
    /// </para>
    /// </remarks>
    Snippet = 2,
}

/// <summary>
/// Represents a collection of <see cref="CompletionItem"/> to be presented in the editor。
/// </summary>
/// <param name="Items">
/// The completion items.</param>
/// <param name="IsIncomplete">
/// This list it not complete. Further typing should result in recomputing this list.</param>
public record CompletionList(IEnumerable<CompletionItem> Items, bool IsIncomplete = false);

/// <summary>
/// A completion item.
/// </summary>
/// <param name="Label">
/// The label of this completion item. By default
/// also the text that is inserted when selecting
/// this completion.</param>
/// <param name="Kind">
/// The kind of this completion item. Based of the kind
/// an icon is chosen by the editor.</param>
/// <param name="Detail">
/// A human-readable (short) string with additional information
/// about this item, like type or symbol information.</param>
/// <param name="Documentation">
/// A human-readable string that represents a doc-comment.</param>
[PublicAPI]
public record CompletionItem(string Label, CompletionItemKind Kind = CompletionItemKind.Text, string? Detail = null, MarkupContent? Documentation = null) {
    /// <summary>
    /// A string that should be used when comparing this item
    /// with other items. When `falsy` the label is used.
    /// </summary>
    public string? SortText { get; set; }

    /// <summary>
    /// A string that should be used when filtering a set of
    /// completion items. When `falsy` the label is used.
    /// </summary>
    public string? FilterText { get; set; }

    /// <summary>
    /// A string that should be inserted a document when selecting
    /// this completion. When `falsy` the label is used.
    /// </summary>
    public string? InsertText { get; set; }

    /// <summary>
    /// The format of the insert text. The format applies to both the `insertText` property
    /// and the `newText` property of a provided `textEdit`.
    /// </summary>
    public InsertTextFormat InsertTextFormat { get; set; } = InsertTextFormat.PlainText;

    /// <summary>
    /// An edit which is applied to a document when selecting this completion. When an edit is provided the value of
    /// `insertText` is ignored.
    ///
    /// *Note:* The range of the edit must be a single line range and it must contain the position at which completion
    /// has been requested.
    /// </summary>
    public TextEdit? TextEdit { get; set; }

    /// <summary>
    /// An optional array of additional text edits that are applied when
    /// selecting this completion. Edits must not overlap with the main edit
    /// nor with themselves.
    /// </summary>
    public IEnumerable<TextEdit>? AdditionalTextEdits { get; set; }

    /// <summary>
    /// An optional set of characters that when pressed while this completion is active will accept it first and
    /// then type that character.
    /// </summary>
    public IEnumerable<char>? CommitCharacters { get; set; }

    /// <summary>
    /// An optional command that is executed *after* inserting this completion. *Note* that
    /// additional modifications to the current document should be described with the
    /// <see cref="AdditionalTextEdits"/> property.
    /// </summary>
    public EditorCommand? Command { get; set; }
    
    /// <summary>
    /// An data entry field that is preserved on a completion item between
    /// a completion and a completion resolve request.
    /// </summary>
    public JToken? Data { get; set; }
}

/// <summary>
/// Descibe options to be used when registered for code completion events.
/// </summary>
public class CompletionRegistrationOptions : TextDocumentRegistrationOptions {
    [JsonConstructor]
    public CompletionRegistrationOptions()
        : this(false, null, null) { }

    public CompletionRegistrationOptions(bool resolveProvider)
        : this(resolveProvider, null, null) { }


    public CompletionRegistrationOptions(bool resolveProvider, IEnumerable<char> triggerCharacters)
        : this(resolveProvider, triggerCharacters, null) { }


    public CompletionRegistrationOptions(bool resolveProvider, IEnumerable<char> triggerCharacters,
        IEnumerable<DocumentFilter> documentSelector)
        : base(documentSelector) {
        ResolveProvider = resolveProvider;
        TriggerCharacters = triggerCharacters;
    }

    /// <summary>
    /// The server provides support to resolve additional
    /// information for a completion item. (i.e. supports <c>completionItem/resolve</c>.)
    /// </summary>
    [JsonProperty]
    public bool ResolveProvider { get; set; }

    /// <summary>
    /// The characters that trigger completion automatically.
    /// </summary>
    [JsonProperty]
    public IEnumerable<char> TriggerCharacters { get; set; }
}
}