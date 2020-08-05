// <copyright file="IServiceCollectionExtension.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TyreConnect.Lexicon.ApplicationCore.Services;
using TyreConnect.Lexicon.ApplicationCore.Validation;
using TyreConnect.Lexicon.Contracts.Model;

namespace TyreConnect.Lexicon.ApplicationCore.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddLexiconApplicationCoreServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<AbstractValidator<TextAnalysisInput>, TextAnalysisValidator>();

            serviceCollection.AddScoped<IValidationService, ValidationService>();

            serviceCollection.AddScoped<ICharacterPositionsService, CharacterPositionsService>();

            return serviceCollection;
        }
    }
}
