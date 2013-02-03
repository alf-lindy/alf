using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class MailService
    {
        public static void SendSignupEmail()
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

        public static void SendRegistrationConfirmed(string userid, string mail)
        {
            var message = string.Format(
@"Your personal registration for the competition can be accessed here: alf.apphb.com/Competition/SignUp/{0}

Payment information:
To pay for your attendace at ALF 2013 we will accept payments through www.deltaker.no here https://www.deltager.no/participant/arrangement.aspx?id=66098

We are looking forward to seeing you at ALF 2013 this April.

Regards
Stig Johnsen
ALF 2013
", userid);
            
            
            var client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator = new HttpBasicAuthenticator("api", "key-95iv2azanw509luzmo3a7eepdkfy3uu8");
            
            var request = new RestRequest();
            request.AddParameter("domain", "app13455.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Another Lindyhop Festival <another.lindy.fest@gmail.com>");
            request.AddParameter("to", mail);
            request.AddParameter("subject", "Payment and competitions for ALF");
            request.AddParameter("text", message);
            request.Method = Method.POST;
            var response = client.Execute(request);    
        
        }
    }
}
