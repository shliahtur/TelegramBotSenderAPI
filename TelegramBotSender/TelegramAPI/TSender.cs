using System;
using System.Net;
using Telegram.SimpleJSON;
using System.Collections.Specialized;

class Method
{
    string _token;
    string LINK = "https://api.telegram.org/bot";
    public Method(string Token)
    {
        _token = Token;
    }
    public void SendMessage(string message, int ChatID)
    {
        using (WebClient webclient = new WebClient())
        {
            NameValueCollection pars = new NameValueCollection();
            pars.Add("chat_id", ChatID.ToString());
            pars.Add("text", message);
            webclient.UploadValues(LINK + _token + "/sendMessage", pars);

        }
    }
}

public delegate void QResponse(object sender, ParameterResponse e);

public class ParameterResponse : EventArgs
{
    public string name;
    public string message;
    public string ChatID;
}

public class TelegramRequest
{
    public string _token;
    public TelegramRequest(string Token)
    {
        _token = Token;
    }
    int LastUpdateID = 0;
    public event QResponse ResponseRecieved;
    ParameterResponse e = new ParameterResponse();

    public void GetUpdates()
    {
        while (true)
        {
            using (WebClient webClient = new WebClient())
            {
                string response = webClient.DownloadString("https://api.telegram.org/bot" + _token + "/getupdates?offset=" + (LastUpdateID + 1));
                if (response.Length <= 23)
                    continue;
                var N = JSON.Parse(response);
                foreach (JSONNode r in N["result"].AsArray)
                {
                    LastUpdateID = r["update_id"].AsInt;
                    e.name = r["message"]["from"]["username"];
                    e.message = r["message"]["text"];
                    e.ChatID = r["message"]["chat"]["id"];
                }
            }
            ResponseRecieved(this, e);
        }
    }

}