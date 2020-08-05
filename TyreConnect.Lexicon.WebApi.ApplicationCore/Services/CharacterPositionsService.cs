// <copyright file="CharacterPositionsService.cs" company="TyreConnect">
// Copyright (c) TyreConnect. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TyreConnect.Lexicon.ApplicationCore.Extensions;
using TyreConnect.Lexicon.Contracts.Model;

namespace TyreConnect.Lexicon.ApplicationCore.Services
{
    public class CharacterPositionsService : ICharacterPositionsService
    {
        private readonly IValidationService _validationService;

        public CharacterPositionsService(IValidationService validationService)
        {
            _validationService = validationService;
        }

        public string GetInfo()
        {
            return "[" +
                "{\"key\":\"Version\", \"value\":\"1.0.12\"}," +
                "{\"key\":\"Product\", \"value\":\"TyreConnect Lexicon\"}" +
            "]";
        }

        /// <summary>
        /// Main entry point for the Controller to get Character Positions.
        /// </summary>
        /// <param name="textAnalysisInput">Input for Text Analysis.</param>
        /// <returns>Character Position List or Error List.</returns>
        public async Task<List<CharacterPosition>> GetCharacterPositionsAsync(TextAnalysisInput textAnalysisInput)
        {
            var validationResult = await _validationService.ValidateTextAnalysisInputAsync(textAnalysisInput);

            if (!validationResult.IsValid)
            {
                return validationResult.MapToCharacterPositionListModel();
            }

            var positionsIntCollection = GetMatchedSubtextPositions(textAnalysisInput.Text, textAnalysisInput.Subtext);

            positionsIntCollection.Sort();

            return positionsIntCollection.MapToCharacterPositionListModel();
        }

        /// <summary>
        /// Method to perform the pattern match. Looks for the occurences of the <paramref name="subText"/>
        /// in the <paramref name="inputText"/> and returns a comma separated string with the <paramref name="inputText"/>
        /// string 1-based indexes.
        /// </summary>
        /// <param name="inputText">The Input string which will be used to search for the <paramref name="subText"/></param>
        /// <param name="subText">The pattern string whose occurances will be search for in the <paramref name="inputText"/>.</param>
        /// <returns>A comma separated string with the occurences of the <paramref name="subText"/> in the <paramref name="inputText"/></returns>
        private List<int> GetMatchedSubtextPositions(string inputText, string subText)
        {
            // Check null occurences, result is an exception
            if (inputText == null)
            {
                throw new ArgumentNullException("inputText");
            }

            if (subText == null)
            {
                throw new ArgumentNullException("subText");
            }

            // Check empty string occurences, result is an empty string
            if (inputText == string.Empty)
            {
                return null;
            }

            if (subText == string.Empty)
            {
                return null;
            }

            var result = GetMatchesByKnuthMorrisPratt(inputText, subText);

            return result;
        }

        /// <summary>
        /// Perform character comparison
        /// </summary>
        /// <param name="x">First character will be compared with <paramref name="y"/>.</param>
        /// <param name="y">Second character will be compared with <paramref name="x"/>.</param>
        /// <param name="caseSensitive">If false then perform case insensitive comparison otherwise perform case sensitive comparison.</param>
        /// <returns>True if <paramref name="x"/> matches <paramref name="y"/>, otherwise False.</returns>
        private bool MatchCharacters(char x, char y, bool caseSensitive = false)
        {
            if (!caseSensitive)
            {
                return char.ToLower(x) == char.ToLower(y);
            }
            else
            {
                return x == y;
            }
        }

        /// <summary>
        /// Perform the Knuth Morris Pratt string search algorithm.
        /// </summary>
        /// <remarks>
        /// <a href = "https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm" >Knuth Morris Pratt</a>
        /// </remarks>
        /// <param name="text">The Input string which will be used to search for the <paramref name="subtext"/></param>
        /// <param name="subtext">The pattern string whose occurances will be search for in the <paramref name="text"/>.</param>
        /// <returns>List of integers marking positions of substring in the input text</returns>
        private List<int> GetMatchesByKnuthMorrisPratt(string text, string subtext)
        {
            var result = new List<int>();

            // Note the lengths of the two strings, so we know when we are done
            // NB. C# strings are zero based arrays so the Length will be one more that the last array index
            var inputTextSize = text.Length;
            var subTextSize = subtext.Length;

            // Mark the current Index we are on for both of the strings
            var subTextMarker = 0;
            var inputTextMarker = 0;

            // loop until we are told to stop
            var endOfTextNotReached = true;

            while (endOfTextNotReached)
            {
                // we reached the end of the text, we are done
                if (inputTextMarker == inputTextSize)
                {
                    // the following assignment is redundant but is left here for demonstration purposes 'readability'
                    endOfTextNotReached = false;
                    break;
                }

                // match characters from text and Subtext
                if (MatchCharacters(text[inputTextMarker], subtext[subTextMarker]))
                {
                    subTextMarker++; // move to the next subText char
                    inputTextMarker++; // move to the next inputText char

                    // if we have reached the end of the subText
                    if (subTextMarker == subTextSize)
                    {
                        // match found, add to the resulting collection
                        // NB. need to take the inputTextMarker back to the beginning index
                        // i.e. the length of the subText
                        // then we need to add 1 because of 1 based positions not 0 based
                        result.Add((inputTextMarker - subTextSize) + 1);

                        // reset subText marker, so we can keep looking
                        subTextMarker = 0;

                        // Moving inputTextMarker back completely and increasing position by 1 to cover for scenario e.g. abababa: aba
                        inputTextMarker = inputTextMarker - subTextSize + 1;
                    }
                }

                // we did not match the characters but there was partial match so we will reset the subTextMarker
                else if (subTextMarker > 0)
                {
                    subTextMarker = 0;
                }

                // we did not match the characters and there was NO partial match so we will reset the inputTextMarker
                else
                {
                    inputTextMarker++;
                }
            }

            return result;
        }
    }
}
