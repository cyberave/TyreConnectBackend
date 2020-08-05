// <copyright file="CharacterPosition.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace TyreConnect.Lexicon.Contracts.Model
{
    public class CharacterPosition
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("character_position")]
        public string Mark { get; set; }

        [JsonProperty("character_position_details")]
        public string Details { get; set; }
    }
}
