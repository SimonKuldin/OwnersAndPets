using Newtonsoft.Json;
using OwnersAndPets.Constants;
using OwnersAndPets.Interface;
using OwnersAndPets.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;

namespace OwnersAndPets.Implementation
{
    public class OwnerAccessor : IOwnerAccessor
    {
        public const string NO_AZURE_RESPONSE = "No response found from Azure.";

        public List<OwnerModel> GetCatOwners()
        {
            string jsonResponse = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                jsonResponse = client.GetStringAsync(ConfigurationManager.AppSettings[SettingsConstants.PEOPLE_SERVICE_URL]).Result;
            }

            if (jsonResponse == null)
            {
                throw new HttpRequestException(NO_AZURE_RESPONSE);
            }

            List<OwnerModel> owners = ConvertJsonResponse(jsonResponse);

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