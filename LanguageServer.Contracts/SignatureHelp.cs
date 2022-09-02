using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LanguageServer.VsCode.Contracts {
/// <summary>
/// Signature help represents the signature of something
/// callable.There can be multiple signature but only one
/// active and only one active parameter.
/// </summary>
/// <param name="Signatures">
/// The active signature. If omitted or the value lies outside the
/// range of `signatures` the value defaults to zero or is ignored if
/// `signatures.length === 0`. Whenever possible implementors should 
/// make an active decision about the active signature and shouldn't 
/// rely on a default value.
/// In future version of the protocol this property might become
/// mandatory to better express this.</param>
/// <param name="ActiveSignature">
/// The active parameter of the active signature. If omitted or the value
/// lies outside the range of `signatures[activeSignature].parameters` 
/// defaults to 0 if the active signature has parameters. If 
/// the active signature has no parameters it is ignored. 
/// In future version of the protocol this property might become
/// mandatory to better express the active parameter if the
/// active signature does have any.</param>
/// <param name="ActiveParameter">
/// The active parameter of the active signature. If omitted or the value
/// lies outside the range of `signatures[activeSignature].parameters` 
/// defaults to 0 if the active signature has parameters. If 
/// the active signature has no parameters it is ignored. 
/// In future version of the protocol this property might become
/// mandatory to better express the active parameter if the
/// active signature does have any.</param>
public record SignatureHelp(IList<SignatureInformation> Signatures, int ActiveSignature = 0,
    int? ActiveParameter = null);

/// <summary>
/// Represents the signature of something callable. A signature
/// can have a label, like a function-name, a doc-comment, and
/// a set of parameters.
/// </summary>
/// <param name="Label">
/// The label of this signature. Will be shown in
/// the UI.</param>
/// <param name="Documentation">
/// The human-readable doc-comment of this signature. Will be shown
/// in the UI but can be omitted.</param>
/// <param name="Parameters">
/// The parameters of this signature.</param>
/// <param name="ActiveParameter">
/// The index of the active parameter.
/// If provided, this is used in place of `SignatureHelp.activeParameter`.</param>
public record SignatureInformation(string Label, MarkupContent? Documentation = null,
    IList<ParameterInformation>? Parameters = null, int? ActiveParameter = null);

/// <summary>
/// Represents a parameter of a callable-signature.A parameter can
/// have a label and a doc-comment.
/// </summary>
/// <param name="Label">
/// The label of this parameter. Will be shown in
/// the UI.</param>
/// <param name="Documentation">
/// The human-readable doc-comment of this parameter. Will be shown
/// in the UI but can be omitted.</param>
public record ParameterInformation(string Label, MarkupContent? Documentation = null);

/// <summary>
/// Describe options to be used when registered for signature help events.
/// </summary>
public class SignatureHelpRegistrationOptions : TextDocumentRegistrationOptions {
    [JsonConstructor]
    public SignatureHelpRegistrationOptions()
        : this(null, null) { }

    public SignatureHelpRegistrationOptions(IEnumerable<char> triggerCharacters)
        : this(triggerCharacters, null) { }


    public SignatureHelpRegistrationOptions(IEnumerable<char> triggerCharacters,
        IEnumerable<DocumentFilter> documentSelector)
        : base(documentSelector) {
        TriggerCharacters = triggerCharacters;
    }

    /// <summary>
    /// The characters that trigger signature help automatically.
    /// </summary>
    [JsonProperty]
    public IEnumerable<char> TriggerCharacters { get; set; }
}
}