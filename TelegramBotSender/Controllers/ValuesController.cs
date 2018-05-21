using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TelegramBotSender.Models;

namespace TelegramBotSender.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpPost]
        public void PersonPost([FromBody]Person person)
        {
  
            string Msg = String.Format("Новый клиент! \n Имя: {0} \n Номер: {1}", person.Name, person.Phone);
            string Token = "310637143:AAE9Qjmrxg6JlgpgxFdA5aC0QIBYF21wuRQ";
            Method m = new Method(Token);
            m.SendMessage(Msg, 232076268);
        }
    }
}
