// <copyright file="CharacterPositionsController.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TyreConnect.Lexicon.ApplicationCore.Services;
using TyreConnect.Lexicon.Contracts.Model;

namespace TyreConnect.Lexicon.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterPositionsController : ControllerBase
    {
        private readonly ICharacterPositionsService _characterPositionsService;

        public CharacterPositionsController(ICharacterPositionsService characterPositionsService)
        {
            _characterPositionsService = characterPositionsService;
        }

        // GET: api/<CharacterPositionsController>
        [HttpGet]
        public string Get()
        {
            var result = _characterPositionsService.GetInfo();

            return result;
        }

        // POST api/<CharacterPositionsController>
        [HttpPost]
        public async Task<string> PostAsync([FromBody] TextAnalysisInput textAnalysisInput)
        {
            // Using Dependency Injected service
            var result = await _characterPositionsService.GetCharacterPositionsAsync(textAnalysisInput);

            string jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);

            return jsonResult;
        }
    }
}
