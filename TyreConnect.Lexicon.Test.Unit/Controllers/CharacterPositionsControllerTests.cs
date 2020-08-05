// <copyright file="CharacterPositionsControllerTests.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Moq;
using TyreConnect.Lexicon.ApplicationCore.Services;
using TyreConnect.Lexicon.Contracts.Model;
using TyreConnect.Lexicon.WebApi.Controllers;
using Xunit;

namespace TyreConnect.Lexicon.Test.Unit.Controllers
{
    public class CharacterPositionsControllerTests
    {
        private readonly Mock<IValidationService> _validationService;
        private readonly Mock<ICharacterPositionsService> _characterPositionsService;

        private readonly CharacterPositionsController _characterPositionsController;

        private readonly Fixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterPositionsControllerTests"/> class.
        /// </summary>
        public CharacterPositionsControllerTests()
        {
            _validationService = new Mock<IValidationService>(MockBehavior.Strict);
            _characterPositionsService = new Mock<ICharacterPositionsService>(MockBehavior.Strict);

            _characterPositionsController = new CharacterPositionsController(_characterPositionsService.Object);

            _fixture = new Fixture();
        }

        /// <summary>
        /// PostAsync when called returns OK result.
        /// </summary>
        [Fact]
        public async void PostAsync_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var characterPositionsResponse = _fixture.Build<List<CharacterPosition>>().Create();

            _characterPositionsService
                .Setup(svc => svc.GetCharacterPositionsAsync(It.IsAny<TextAnalysisInput>()))
                .ReturnsAsync(characterPositionsResponse);

            // Act
            var actionResult = await _characterPositionsController.PostAsync(It.IsAny<TextAnalysisInput>());

            // Assert

            // Asserts that the Controller calls the GetCharacterPositionsAsync method of the underlying CharacterPositionsService
            _characterPositionsService.Verify(svc => svc.GetCharacterPositionsAsync(It.IsAny<TextAnalysisInput>()), Times.Once);

            actionResult.Should().NotBeNullOrEmpty();
            actionResult.Should().BeOfType<string>();
        }

        /// <summary>
        /// PostAsync when called returns OK result.
        /// </summary>
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var getInfoResponse = _fixture.Build<string>().Create();

            _characterPositionsService
                .Setup(svc => svc.GetInfo())
                .Returns(getInfoResponse);

            // Act
            var actionResult = _characterPositionsController.Get();

            // Assert

            // Asserts that the Controller calls the GetInfo method of the underlying CharacterPositionsService
            _characterPositionsService.Verify(svc => svc.GetInfo(), Times.Once);

            actionResult.Should().NotBeNullOrEmpty();
            actionResult.Should().BeOfType<string>();
        }
    }
}
