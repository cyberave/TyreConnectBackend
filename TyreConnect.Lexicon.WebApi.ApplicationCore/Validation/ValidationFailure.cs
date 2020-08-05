// <copyright file="ValidationFailure.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

namespace TyreConnect.Lexicon.ApplicationCore.Validation
{
    public class ValidationFailure
    {
        public ValidationFailure(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
