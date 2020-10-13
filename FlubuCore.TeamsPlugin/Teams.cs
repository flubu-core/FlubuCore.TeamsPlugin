using System;
using System.Collections.Generic;
using System.Text;

namespace FlubuCore.TeamsPlugin
{
    public class Teams
    {
        /// <summary>
        /// Send's message to microsoft teams via webhook.
        /// </summary>
        /// <returns></returns>
        public TeamsSendMessageTask SendMessage(string incomingWebHookUrl, TeamsMessage message)
        {
            return new TeamsSendMessageTask(incomingWebHookUrl, message);
        }
    }
}
