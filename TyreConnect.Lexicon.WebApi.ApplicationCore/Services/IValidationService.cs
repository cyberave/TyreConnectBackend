// <copyright file="IValidationService.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using TyreConnect.Lexicon.ApplicationCore.Validation;
using TyreConnect.Lexicon.Contracts.Model;

namespace TyreConnect.Lexicon.ApplicationCore.Services
{
    public interface IValidationService
    {
        Task<ValidationResult> ValidateTextAnalysisInputAsync(TextAnalysisInput textAnalysisInput);
    }
}
