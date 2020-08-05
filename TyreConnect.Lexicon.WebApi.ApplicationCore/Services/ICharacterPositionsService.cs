// <copyright file="ICharacterPositionsService.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using TyreConnect.Lexicon.Contracts.Model;

namespace TyreConnect.Lexicon.ApplicationCore.Services
{
    public interface ICharacterPositionsService
    {
        Task<List<CharacterPosition>> GetCharacterPositionsAsync(TextAnalysisInput textAnalysisInput);

        string GetInfo();
    }
}
