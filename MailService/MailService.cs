using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MailService
    {
        public void SendSignupEmail()
        {
            var client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               "key-95iv2azanw509luzmo3a7eepdkfy3uu8");
            var request = new RestRequest();
            request.AddParameter("domain",
                                 "app13455.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Another Lindyhop Festival <another.lindy.fest@gmail.com>");
            request.AddParameter("to", "sirjonas@gmail.com");
            request.AddParameter("subject", "Sign up for competitions at Alf!");
            request.AddParameter("text", "Go to this link to see where you're competing!");
            request.Method = Method.POST;
            var response = client.Execute(request);
        }
    }
}
