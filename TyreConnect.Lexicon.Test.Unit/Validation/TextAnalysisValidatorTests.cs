// <copyright file="TextAnalysisValidatorTests.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using FluentAssertions;
using TyreConnect.Lexicon.ApplicationCore.Validation;
using TyreConnect.Lexicon.Contracts.Model;
using Xunit;

namespace TyreConnect.Lexicon.Test.Unit.Validation
{
    public class TextAnalysisValidatorTests
    {
        private TextAnalysisValidator _textAnalysisValidator;

        public TextAnalysisValidatorTests()
        {
            _textAnalysisValidator = new TextAnalysisValidator();
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
            var validationResult = await _textAnalysisValidator.ValidateAsync(textAnalysisInput);

            // Assert
            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
        }

        /// <summary>
        /// Input instance is an invalid combination of text and subtext
        /// Validation fails.
        /// </summary>
        /// <param name="text">Invalid Text.</param>
        /// <param name="subtext">Invalid Subtext.</param>
        [Theory]
        [InlineData("", "aba")] // invalid combination of text and subtext
        [InlineData("a", "")] // another invalid combination of text and subtext
        [InlineData(" ", "aba")] // another invalid combination of text and subtext
        [InlineData("a", "  ")] // another invalid combination of text and subtext
        [InlineData("", "")] // another invalid combination of text and subtext
        [InlineData(" ", "  ")] // another invalid combination of text and subtext
        public async void ValidateTextAnalysisInputAsync_InputIsNullOrEmpty_ValidationFails(string text, string subtext)
        {
            // Arrange
            var textAnalysisInput = new TextAnalysisInput()
            {
                Text = text,
                Subtext = subtext
            };

            // Act
            var validationResult = await _textAnalysisValidator.ValidateAsync(textAnalysisInput);

            // Assert
            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
        }

        /// <summary>
        /// Subtext is longer than Text.
        /// Validation fails.
        /// </summary>
        /// <param name="text">Valid Text.</param>
        /// <param name="subtext">Valid Subtext that is longer than Text.</param>
        [Theory]
        [InlineData("aba", "ababababa")] // invalid combination of text and subtext
        public async void ValidateTextAnalysisInputAsync_SubtextLongerThanText_ValidationFails(string text, string subtext)
        {
            // Arrange
            var textAnalysisInput = new TextAnalysisInput()
            {
                Text = text,
                Subtext = subtext
            };

            // Act
            var validationResult = await _textAnalysisValidator.ValidateAsync(textAnalysisInput);

            // Assert
            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
        }
    }
}
