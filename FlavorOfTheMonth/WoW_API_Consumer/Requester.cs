using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ConsoleProgram
{
    public class DataObject
    {
        public string[] rows { get; set; }
    }

    public class DataObject2
    {
        public string ranking { get; set; }
        public string rating { get; set; }
        public string name { get; set; }
        public string realmId { get; set; }
        public string realmName { get; set; }
        public string realmSlug { get; set; }
        public string raceId { get; set; }
        public string classId { get; set; }
        public string specId { get; set; }
        public string factionId { get; set; }
        public string genderId { get; set; }
        public string seasonWins { get; set; }
        public string seasonLosses { get; set; }
        public string weeklyWins { get; set; }
        public string weeklyLosses { get; set; }
    }

    public class Requester
    {
        private const string URL = "https://us.api.battle.net/wow/leaderboard/3v3?locale=en_US&apikey=8e4txvdtgmpp5gbavwm9pysb9nuak8mf";

        public void Request()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            // List data response.
            HttpResponseMessage response = client.GetAsync(URL).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
                foreach (var d in dataObjects)
                {
                    Debug.WriteLine("{0}", d.rows);
                }
            }
            else
            {
                Debug.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public ResourceSet MakeRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ResourceSet));
                    object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    ResourceSet jsonResponse
                    = objResponse as ResourceSet;
                    return jsonResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }

    [DataContract]
    public class Response
    {
        [DataMember(Name = "ranking")]
        public string ranking { get; set; }
        [DataMember(Name = "rating")]
        public string rating { get; set; }
        [DataMember(Name = "name")]
        public string name { get; set; }
        [DataMember(Name = "realmId")]
        public string realmId { get; set; }
        [DataMember(Name = "realmName")]
        public string realmName { get; set; }
        [DataMember(Name = "realmSlug")]
        public string realmSlug { get; set; }
        [DataMember(Name = "raceId")]
        public string raceId { get; set; }
        [DataMember(Name = "classId")]
        public string classId { get; set; }
        [DataMember(Name = "specId")]
        public string specId { get; set; }
        [DataMember(Name = "factionId")]
        public string factionId { get; set; }
        [DataMember(Name = "genderId")]
        public string genderId { get; set; }
        [DataMember(Name = "seasonWins")]
        public string seasonWins { get; set; }
        [DataMember(Name = "seasonLosses")]
        public string seasonLosses { get; set; }
        [DataMember(Name = "weeklyWins")]
        public string weeklyWins { get; set; }
        [DataMember(Name = "weeklyLosses")]
        public string weeklyLosses { get; set; }
    }

    [DataContract]
    public class ResourceSet
    {
        [DataMember(Name = "rows")]
        public List<Response> rows { get; set; }
    }
}