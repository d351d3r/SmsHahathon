using System;

using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Number_of_words
{
    class SmsSender
    {
        public static void Send(string Phonenumber,DateTime date,int railway, string coach) {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = "ACf09dd6e169b9f24e3015b81954b5b3d5";
            string authToken = "";
            
            // ONLY FOR TEST
            Phonenumber = "+79142167272";
            
            var to = new Twilio.Types.PhoneNumber(Phonenumber);
            /* string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
               string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");*/
            string body = "Здравствуйте, ваш поезд прибывает " + date + " поезд №" + coach + " отправляется с пути "+ railway;
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: body,
/*                body: "Здравствуйте, ваш поезд прибывает в %s часов на %s платформе",*/
                from: new Twilio.Types.PhoneNumber("+16467982915"),
                to: to
            );

            Console.WriteLine(message.Sid);
        } 
    }

}
