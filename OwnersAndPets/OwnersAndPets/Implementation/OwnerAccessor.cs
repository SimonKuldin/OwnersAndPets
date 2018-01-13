using Newtonsoft.Json;
using OwnersAndPets.Constants;
using OwnersAndPets.Interface;
using OwnersAndPets.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;

namespace OwnersAndPets.Implementation
{
    public class OwnerAccessor : IOwnerAccessor
    {
        public const string NO_AZURE_RESPONSE = "No response found from Azure.";
        public const string INVALID_JSON_RESPONSE = "Invalid Json response found.";

        private HttpClientHandler _httpClientHandler;

        public OwnerAccessor(HttpClientHandler httpClientHandler)
        {
            _httpClientHandler = httpClientHandler;
        }

        public List<OwnerModel> GetCatOwners()
        {
            string jsonResponse = string.Empty;

            HttpClient httpClient = new HttpClient(_httpClientHandler);

            Uri uri = null;

            if (ConfigurationManager.AppSettings[SettingsConstants.PEOPLE_SERVICE_URL] != null)
            {
                uri = new Uri(ConfigurationManager.AppSettings[SettingsConstants.PEOPLE_SERVICE_URL]);
            }

            jsonResponse = httpClient.GetStringAsync(uri).Result;

            if (string.IsNullOrEmpty(jsonResponse))
            {
                throw new HttpRequestException(NO_AZURE_RESPONSE);
            }

            List<OwnerModel> owners = null;

            try
            {
                owners = ConvertJsonResponse(jsonResponse);
            }
            catch (Exception)
            {
                throw new HttpRequestException(INVALID_JSON_RESPONSE);
            }

            //Grab all owners that have pets that include a cat
            owners = owners.FindAll(p => p.Pets != null).FindAll(p => p.Pets.Any(pet => pet.Type == PetType.Cat));

            //Filter out all their pets that aren't cats
            owners.ForEach(o => o.Pets.RemoveAll(p => p.Type != PetType.Cat));

            return owners;
        }

        private List<OwnerModel> ConvertJsonResponse(string jsonResponse)
        {
            return JsonConvert.DeserializeObject<IEnumerable<OwnerModel>>(jsonResponse) as List<OwnerModel>;
        }
    }
}