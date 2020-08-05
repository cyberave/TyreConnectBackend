// <copyright file="ModelMapper.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using System.Collections.Generic;
using TyreConnect.Lexicon.ApplicationCore.Validation;
using TyreConnect.Lexicon.Contracts.Model;

namespace TyreConnect.Lexicon.ApplicationCore.Extensions
{
    /// <summary>
    /// Main Model Mapper class for ApplicationCore.
    /// </summary>
    public static class ModelMapper
    {
        public static CharacterPosition MapToCharacterPositionModel(this int characterPositionInt, int id)
        {
            var characterPositionString = characterPositionInt.ToString();

            return new CharacterPosition()
            {
                Id = id.ToString(),
                Mark = characterPositionString,
                Details = $"Occurence #{id} of Subtext was found at position mark: {characterPositionString} of the main Text."
            };
        }

        public static List<CharacterPosition> MapToCharacterPositionListModel(this List<int> characterPositionIntList)
        {
            var characterPositions = new List<CharacterPosition>();

            if (characterPositionIntList.Count == 0)
            {
                characterPositions.Add(EmptyResult());
            }

            var i = 0;

            foreach (var characterPositionInt in characterPositionIntList)
            {
                i++;
                characterPositions.Add(characterPositionInt.MapToCharacterPositionModel(i));
            }

            return characterPositions;
        }

        public static ValidationFailure MapToValidationFailureModel(this FluentValidation.Results.ValidationFailure fluentValidationFailure)
        {
            return new ValidationFailure(fluentValidationFailure.ErrorCode, fluentValidationFailure.ErrorMessage);
        }

        public static List<ValidationFailure> MapToValidationFailureListModel(this IList<FluentValidation.Results.ValidationFailure> fluentValidationFailureList)
        {
            var validationFailureList = new List<ValidationFailure>();

            foreach (var fluentValidationFailure in fluentValidationFailureList)
            {
                validationFailureList.Add(fluentValidationFailure.MapToValidationFailureModel());
            }

            return validationFailureList;
        }

        public static ValidationResult MapToValidationResultModel(this FluentValidation.Results.ValidationResult fluentValidationResult)
        {
            return new ValidationResult()
            {
                IsValid = fluentValidationResult.IsValid,
                Errors = fluentValidationResult.Errors.MapToValidationFailureListModel()
            };
        }

        public static CharacterPosition MapToCharacterPositionModel(this ValidationFailure validationFailure, int id)
        {
            return new CharacterPosition()
            {
                Id = id.ToString(),
                Mark = validationFailure.ErrorCode,
                Details = $"ERROR: {validationFailure.ErrorMessage}"
            };
        }

        public static List<CharacterPosition> MapToCharacterPositionListModel(this ValidationResult validationResult)
        {
            var characterPositions = new List<CharacterPosition>();
            var i = 0;

            foreach (var validationFailure in validationResult.Errors)
            {
                i++;
                characterPositions.Add(validationFailure.MapToCharacterPositionModel(i));
            }

            return characterPositions;
        }

        private static CharacterPosition EmptyResult()
        {
            return new CharacterPosition()
            {
                Id = "N/A",
                Mark = "N/A",
                Details = $"No Occurences of Subtext were found in the main Text."
            };
        }
    }
}
