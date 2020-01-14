using Newtonsoft.Json;
using Notifications.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Notifications.Services
{
    public interface IMessageStoreClient
    {
        Task<List<Message>> GetDailySummary(string uid);
        Task<List<Message>> GetMentionsSummary(string uid);
        Task<List<Message>> GetRepliesSummary(string uid);

    }

    public class MessageStoreClient : IMessageStoreClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public MessageStoreClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
       
        public async Task<List<Message>> GetDailySummary(string uid)
        {
            List<Message> messages = null;

            var client = _clientFactory.CreateClient("messageStoreClient");
            HttpResponseMessage response = await client.GetAsync("/MessageStore/api/messages/daily/" + uid);
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;

                messages = JsonConvert.DeserializeObject<List<Message>>(responseString);

            }
            else
            {
                Debug.WriteLine($"Error accessing the daily summary resource for user {uid}");
            }

            return messages;
        }

        public async Task<List<Message>> GetMentionsSummary(string uid)
        {
            List<Message> messages = null;

            var client = _clientFactory.CreateClient("messageStoreClient");
            HttpResponseMessage response = await client.GetAsync("/MessageStore/api/messages/mentions/" + uid);
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;

                messages = JsonConvert.DeserializeObject<List<Message>>(responseString);

            }
            else
            {
                Debug.WriteLine($"Error accessing the mentions summary resource for user {uid}");
            }

            return messages;
        }

        public async Task<List<Message>> GetRepliesSummary(string uid)
        {
            List<Message> messages = null;

            var client = _clientFactory.CreateClient("messageStoreClient");
            HttpResponseMessage response = await client.GetAsync("/MessageStore/api/messages/replies/" + uid);
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;

                messages = JsonConvert.DeserializeObject<List<Message>>(responseString);

            }
            else
            {
                Debug.WriteLine($"Error accessing the replies summary resource for user {uid}");
            }

            return messages;
        }
    }
}


