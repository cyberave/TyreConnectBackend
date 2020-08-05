// <copyright file="ValidationResult.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace TyreConnect.Lexicon.ApplicationCore.Validation
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<ValidationFailure>();
        }

        public ValidationResult(string errorCode, string errorMessage)
        {
            Errors = new List<ValidationFailure>();

            Errors.Add(new ValidationFailure(errorCode, errorMessage));
        }

        /// <summary>Gets or sets a value indicating whether whether validation succeeded.</summary>
        /// <remarks>Default value is set to false.</remarks>
        public bool IsValid { get; set; } = false;

        public List<ValidationFailure> Errors { get; set; }
    }
}
