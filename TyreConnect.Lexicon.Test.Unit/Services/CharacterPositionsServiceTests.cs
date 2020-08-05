// <copyright file="CharacterPositionsServiceTests.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using AutoFixture;
using FluentAssertions;
using Moq;
using TyreConnect.Lexicon.ApplicationCore.Services;
using TyreConnect.Lexicon.ApplicationCore.Validation;
using TyreConnect.Lexicon.Contracts.Model;
using Xunit;

namespace TyreConnect.Lexicon.Test.Unit.Services
{
    public class CharacterPositionsServiceTests
    {
        private readonly Mock<IValidationService> _validationService;

        private readonly ICharacterPositionsService _characterPositionsService;

        private readonly Fixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterPositionsServiceTests"/> class.
        /// </summary>
        public CharacterPositionsServiceTests()
        {
            _validationService = new Mock<IValidationService>(MockBehavior.Strict);
            _characterPositionsService = new CharacterPositionsService(_validationService.Object);

            _fixture = new Fixture();
        }

        /// <summary>
        /// Input instance is a valid TextAnalysisInput
        /// </summary>
        /// <param name="text">Valid Text.</param>
        /// <param name="subtext">Valid Subtext.</param>
        [Theory]
        [InlineData("ababababa", "aba")] // valid text and subtext
        [InlineData("a", "a")] // another valid text and subtext
        [InlineData("asdf", "df")] // yet another valid text and subtext
        public async void GetCharacterPositionsAsync_InputIsValid_OkResult(string text, string subtext)
        {
            // Arrange
            var textAnalysisInput = new TextAnalysisInput()
            {
                Text = text,
                Subtext = subtext
            };

            var validationResult = _fixture.Build<ValidationResult>().Create();
            validationResult.IsValid = true;

            _validationService
                .Setup(svc => svc.ValidateTextAnalysisInputAsync(It.IsAny<TextAnalysisInput>()))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _characterPositionsService.GetCharacterPositionsAsync(textAnalysisInput);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
        }

        /// <summary>
        /// Input instance is a valid TextAnalysisInput
        /// </summary>
        /// <param name="text">Valid Text.</param>
        /// <param name="subtext">Valid Subtext.</param>
        [Theory]
        [InlineData("Polly put the kettle on, polly put the kettle on, polly put the kettle on we'll all have tea", "Polly")] // valid text and subtext
        [InlineData("Polly put the kettle on, polly put the kettle on, polly put the kettle on we'll all have tea", "polly")] // another valid text and subtext
        [InlineData("Polly put the kettle on, polly put the kettle on, poLLy put the kettle on we'll all have tea", "pOlLy")] // another valid text and subtext
        public async void GetCharacterPositionsAsync_InputIsValid_Polly_OkResult(string text, string subtext)
        {
            // Arrange
            var textAnalysisInput = new TextAnalysisInput()
            {
                Text = text,
                Subtext = subtext
            };

            var validationResult = _fixture.Build<ValidationResult>().Create();
            validationResult.IsValid = true;

            _validationService
                .Setup(svc => svc.ValidateTextAnalysisInputAsync(It.IsAny<TextAnalysisInput>()))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _characterPositionsService.GetCharacterPositionsAsync(textAnalysisInput);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(3);

            // 1, 26, 51

            var characterPosition1 = result.Find(x => x.Mark.Equals("1"));
            characterPosition1.Should().NotBeNull();

            var characterPosition26 = result.Find(x => x.Mark.Equals("26"));
            characterPosition26.Should().NotBeNull();

            var characterPosition51 = result.Find(x => x.Mark.Equals("51"));
            characterPosition51.Should().NotBeNull();

        }

        /// <summary>
        /// Input instance is a valid TextAnalysisInput        
        /// </summary>
        /// <param name="text">Valid Text.</param>
        /// <param name="subtext">Valid Subtext.</param>
        [Theory]
        [InlineData("Polly put the kettle on, polly put the kettle on, polly put the kettle on we'll all have tea", "LL")] // valid text and subtext
        [InlineData("Polly put the kettle on, polly put the kettle on, polly put the kettle on we'll all have tea", "ll")] // another valid text and subtext
        [InlineData("Polly put the kettle on, polly put the kettle on, poLLy put the kettle on we'll all have tea", "Ll")] // another valid text and subtext
        [InlineData("Polly put the kettle on, polly put the kettle on, poLLy put the kettle on we'll all have tea", "lL")] // another valid text and subtext
        public async void GetCharacterPositionsAsync_InputIsValid_LL_OkResult(string text, string subtext)
        {
            // Arrange
            var textAnalysisInput = new TextAnalysisInput()
            {
                Text = text,
                Subtext = subtext
            };

            var validationResult = _fixture.Build<ValidationResult>().Create();
            validationResult.IsValid = true;

            _validationService
                .Setup(svc => svc.ValidateTextAnalysisInputAsync(It.IsAny<TextAnalysisInput>()))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _characterPositionsService.GetCharacterPositionsAsync(textAnalysisInput);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(5);

            // 3, 28, 53, 78, 82
            var characterPosition3 = result.Find(x => x.Mark.Equals("3"));
            characterPosition3.Should().NotBeNull();

            var characterPosition28 = result.Find(x => x.Mark.Equals("28"));
            characterPosition28.Should().NotBeNull();

            var characterPosition53 = result.Find(x => x.Mark.Equals("53"));
            characterPosition53.Should().NotBeNull();

            var characterPosition78 = result.Find(x => x.Mark.Equals("78"));
            characterPosition78.Should().NotBeNull();

            var characterPosition82 = result.Find(x => x.Mark.Equals("82"));
            characterPosition82.Should().NotBeNull();
        }

        /// <summary>
        /// Input instance is an valid TextAnalysisInput.
        /// No occurence => N/A result.
        /// </summary>
        /// <param name="text">Valid Text.</param>
        /// <param name="subtext">Not to be found Subtext.</param>
        [Theory]
        [InlineData("Polly put the kettle on, polly put the kettle on, polly put the kettle on we'll all have tea", "Xx")] // invalid text and subtext
        [InlineData("Polly put the kettle on, polly put the kettle on, polly put the kettle on we'll all have tea", "xX")] // another invalid text and subtext
        [InlineData("Polly put the kettle on, polly put the kettle on, poLLy put the kettle on we'll all have tea", "xx")] // another invalid text and subtext
        [InlineData("Polly put the kettle on, polly put the kettle on, poLLy put the kettle on we'll all have tea", "XX")] // another invalid text and subtext
        public async void GetCharacterPositionsAsync_InputIsValid_XX_NaResult(string text, string subtext)
        {
            // Arrange
            var textAnalysisInput = new TextAnalysisInput()
            {
                Text = text,
                Subtext = subtext
            };

            var validationResult = _fixture.Build<ValidationResult>().Create();
            validationResult.IsValid = true;

            _validationService
                .Setup(svc => svc.ValidateTextAnalysisInputAsync(It.IsAny<TextAnalysisInput>()))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _characterPositionsService.GetCharacterPositionsAsync(textAnalysisInput);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(1);

            var characterPositionNA = result.Find(x => x.Mark.Equals("N/A"));
            characterPositionNA.Should().NotBeNull();
        }

        /// <summary>
        /// Input instance is an invalid TextAnalysisInput.
        /// This test is for overlapping subtexts.
        /// </summary>
        /// <param name="text">Valid Text.</param>
        /// <param name="subtext">Not to be found Subtext.</param>
        [Theory]
        [InlineData("ababababa", "aba")] // valid text and subtext (overlapping)
        [InlineData("ababababa", "aBa")] // valid text and subtext (overlapping)
        [InlineData("ababababa", "abA")] // valid text and subtext (overlapping)
        [InlineData("ababababa", "ABa")] // valid text and subtext (overlapping)
        public async void GetCharacterPositionsAsync_InputIsValid_SubtextOverlap_NaResult(string text, string subtext)
        {
            // Arrange
            var textAnalysisInput = new TextAnalysisInput()
            {
                Text = text,
                Subtext = subtext
            };

            var validationResult = _fixture.Build<ValidationResult>().Create();
            validationResult.IsValid = true;

            _validationService
                .Setup(svc => svc.ValidateTextAnalysisInputAsync(It.IsAny<TextAnalysisInput>()))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _characterPositionsService.GetCharacterPositionsAsync(textAnalysisInput);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(4);

            // 1, 3, 5, 7
            var characterPosition1 = result.Find(x => x.Mark.Equals("1"));
            characterPosition1.Should().NotBeNull();

            var characterPosition3 = result.Find(x => x.Mark.Equals("3"));
            characterPosition3.Should().NotBeNull();

            var characterPosition5 = result.Find(x => x.Mark.Equals("5"));
            characterPosition5.Should().NotBeNull();

            var characterPosition7 = result.Find(x => x.Mark.Equals("7"));
            characterPosition7.Should().NotBeNull();
        }
    }
}
