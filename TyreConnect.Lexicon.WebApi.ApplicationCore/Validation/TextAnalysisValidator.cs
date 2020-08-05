// <copyright file="TextAnalysisValidator.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using FluentValidation;
using TyreConnect.Lexicon.Contracts.Model;

namespace TyreConnect.Lexicon.ApplicationCore.Validation
{
    public class TextAnalysisValidator : AbstractValidator<TextAnalysisInput>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextAnalysisValidator"/> class.
        /// Validates whether given Text Analysis Input is valid.
        /// </summary>
        public TextAnalysisValidator()
        {
            // Text should NOT be NULL
            RuleFor(textAnalysisInput => textAnalysisInput.Text)
                .NotNull()
                .WithErrorCode("VE_TextAnalysisInput_Text_NotNull_001")
                .WithMessage("Text should NOT be NULL");

            // Text should NOT be EMPTY
            RuleFor(textAnalysisInput => textAnalysisInput.Text)
                .NotEmpty()
                .WithErrorCode("VE_TextAnalysisInput_Text_NotEmpty_001")
                .WithMessage("Text should NOT be Empty");

            // Subtext should NOT be NULL
            RuleFor(textAnalysisInput => textAnalysisInput.Subtext)
                .NotNull()
                .WithErrorCode("VE_TextAnalysisInput_Subtext_NotNull_001")
                .WithMessage("Subtext should NOT be NULL");

            // Subtext should NOT be EMPTY
            RuleFor(textAnalysisInput => textAnalysisInput.Subtext)
                .NotEmpty()
                .WithErrorCode("VE_TextAnalysisInput_Subtext_NotEmpty_001")
                .WithMessage("Subtext should NOT be Empty");


            // XRPL Address should be in a specific format
            // Source: https://xrpl.org/xrp-api-data-types-address.html#address
            RuleFor(textAnalysisInput => textAnalysisInput)
                .Must(BeTextLengthLargerThanSubtextLength)
                .WithErrorCode("VE_TextAnalysisInput_SubtextLengthLargerThanTextLength_NotValid_001")
                .WithMessage("Subtext Length is larger than Text Length");
        }

        /// <summary>
        /// Custom Validation for the following rule: Text Length Larger Than Subtext Length.
        /// </summary>
        /// <param name="textAnalysisInput">Text Analysis Input</param>
        /// <returns>True if Text Length is larger than Subtext Length, otherwise returns false.</returns>
        private bool BeTextLengthLargerThanSubtextLength(TextAnalysisInput textAnalysisInput)
        {
            return textAnalysisInput.Text.Length >= textAnalysisInput.Subtext.Length;
        }
    }
}
