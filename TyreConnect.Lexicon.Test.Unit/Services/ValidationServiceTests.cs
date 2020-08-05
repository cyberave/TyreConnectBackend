// <copyright file="ValidationServiceTests.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using AutoFixture;
using FluentAssertions;
using TyreConnect.Lexicon.ApplicationCore.Services;
using TyreConnect.Lexicon.ApplicationCore.Validation;
using TyreConnect.Lexicon.Contracts.Model;
using Xunit;

namespace TyreConnect.Lexicon.Test.Unit.Services
{
    public class ValidationServiceTests
    {
        // Cannot use Mock for Validator in this instance
        private readonly TextAnalysisValidator _textAnalysisValidator;

        private readonly ValidationService _validationService;

        private readonly Fixture _fixture;

        public ValidationServiceTests()
        {
            // Cannot use Mock for Validator in this instance
            _textAnalysisValidator = new TextAnalysisValidator();
            _validationService = new ValidationService(_textAnalysisValidator);

            _fixture = new Fixture();
        }

        /// <summary>
        /// Input instance is a valid TextAnalysisInput
        /// Validation succeeds.
        /// </summary>
        /// <param name="text">Valid Text.</param>
        /// <param name="subtext">Valid Subtext.</param>
        [Theory]
        [InlineData("ababababa", "aba")] // valid text and subtext
        [InlineData("a", "b")] // another valid text and subtext
        [InlineData("asdf", "df")] // yet another valid text and subtext
        public async void ValidateTextAnalysisInputAsync_InputIsValid_ValidationSucceeds(string text, string subtext)
        {
            // Arrange
            var textAnalysisInput = new TextAnalysisInput()
            {
                Text = text,
                Subtext = subtext
            };

            // Act
            var validationResult = await _validationService.ValidateTextAnalysisInputAsync(textAnalysisInput);

            // Assert
            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
        }

        /// <summary>
        /// Input instance is an invalid combination of TextAnalysisInput
        /// Validation fails.
        /// </summary>
        /// <param name="text">Valid or invalid Text.</param>
        /// <param name="subtext">Valid or invalid Subtext.</param>
        [Theory]
        [InlineData(null, "aba")] // valid text and subtext
        [InlineData("a", null)] // another valid text and subtext
        [InlineData(null, null)] // yet another valid text and subtext
        public async void ValidateTextAnalysisInputAsync_InputIsInValid_ValidationFails(string text, string subtext)
        {
            // Arrange
            var textAnalysisInput = new TextAnalysisInput()
            {
                Text = text,
                Subtext = subtext
            };

            // Act
            var validationResult = await _validationService.ValidateTextAnalysisInputAsync(textAnalysisInput);

            // Assert
            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
        }
    }
}
