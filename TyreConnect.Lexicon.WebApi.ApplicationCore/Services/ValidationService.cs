// <copyright file="ValidationService.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using FluentValidation;
using TyreConnect.Lexicon.ApplicationCore.Extensions;
using TyreConnect.Lexicon.ApplicationCore.Validation;
using TyreConnect.Lexicon.Contracts.Model;

namespace TyreConnect.Lexicon.ApplicationCore.Services
{
    public class ValidationService: IValidationService
    {
        private readonly AbstractValidator<TextAnalysisInput> _textAnalysisInputValidator;

        public ValidationService(AbstractValidator<TextAnalysisInput> textAnalysisInputValidator)
        {
            _textAnalysisInputValidator = textAnalysisInputValidator;
        }

        public async Task<ValidationResult> ValidateTextAnalysisInputAsync(TextAnalysisInput textAnalysisInput)
        {
            // Null objects are not handled by Fluent Validation so it has to be handled first
            if (textAnalysisInput == null)
            {
                return new ValidationResult("VE_TextAnalysisInput_NotNull_001", "TextAnalysisInput should NOT be NULL");
            }

            if (textAnalysisInput.Text == null)
            {
                return new ValidationResult("VE_TextAnalysisInput_Text_NotNull_001", "Text should NOT be NULL");
            }

            if (textAnalysisInput.Subtext == null)
            {
                return new ValidationResult("VE_TextAnalysisInput_Subtext_NotNull_001", "Subtext should NOT be NULL");
            }

            // Fluent Validation
            var fluentValidationResult = await _textAnalysisInputValidator.ValidateAsync(textAnalysisInput);

            // Mapping through extension methods
            var validationResult = fluentValidationResult.MapToValidationResultModel();

            return validationResult;
        }
    }
}
