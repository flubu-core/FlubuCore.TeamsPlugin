using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FlubuCore.Context;
using FlubuCore.Tasks;

namespace FlubuCore.TeamsPlugin
{
    public class TeamsSendMessageTask : TaskBase<int, TeamsSendMessageTask>
    {
        private static JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        private readonly string _webhookUrl;

        private readonly TeamsMessage _message;
      
        public TeamsSendMessageTask(string webhookUrl, TeamsMessage message)
        {
            _webhookUrl = webhookUrl;
            _message = message;
        }

        protected override async Task<int> DoExecuteAsync(ITaskContextInternal context)
        {
            var json = JsonSerializer.Serialize(_message, serializeOptions);
            using (var client = new HttpClient())
            {
               var result = await client.PostAsync(_webhookUrl, new StringContent(json, Encoding.UTF8, "application/json"));
               if (result.IsSuccessStatusCode)
               {
                   return 0;
               }

               throw new TaskExecutionException(await result.Content.ReadAsStringAsync(), 20);
            }            
        }

        protected override int DoExecute(ITaskContextInternal context)
        {
            return DoExecuteAsync(context).Result;
        }

        protected override string Description { get; set; }
    }
}
