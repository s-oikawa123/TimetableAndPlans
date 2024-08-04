using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Classroom.v1;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using UnityEngine;

public class GmailAPI
{
    static string[] Scopes = { GmailService.Scope.GmailReadonly, ClassroomService.Scope.ClassroomCoursesReadonly };
    static string ApplicationName = "Gmail API .NET Quickstart";
    public static List<Plan> FetchPlans(DateTime lastCheckedDate)
    {
        List<Plan> result = new List<Plan>();
        UserCredential credential;

        using (var stream = new FileStream(Path.Combine(UnityEngine.Application.dataPath, "test.json"), FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
        }

        var service = new GmailService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        string userId = "me";

        UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List(userId);
        request.LabelIds = "INBOX";
        request.IncludeSpamTrash = false;
        request.MaxResults = 1000;
        request.Q = $"from:nihon-u.ac.jp after:{lastCheckedDate:yyyy/MM/dd}";

        IList<Message> messages = request.Execute().Messages;
        Console.WriteLine("Messages:");

        if (messages != null && messages.Count > 0)
        {
            foreach (var messageItem in messages)
            {
                var message = service.Users.Messages.Get(userId, messageItem.Id).Execute();

                var fromHeader = message.Payload.Headers.FirstOrDefault(header => header.Name == "From");
                string from = fromHeader != null ? fromHeader.Value : "Unknown";

                var emailRegex = new Regex(@"[a-zA-Z0-9._%+-]+@nihon-u\.ac\.jp");

                string emailBody = GetMessageBody(message);

                var dateHeader = message.Payload.Headers.FirstOrDefault(header => header.Name == "Date");
                string date = dateHeader != null ? dateHeader.Value : "Unknown";
                DateTime parsedDate;
                DateTime.TryParseExact(date, "ddd, dd MMM yyyy HH:mm:ss K", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

                var match = emailRegex.Match(from);
                string emailAddress = match.Success ? match.Value : "Unknown";

                var nameMatch = Regex.Match(from, @"^(.*?)(?=\s*<)");
                string name = nameMatch.Success ? nameMatch.Value.Trim() : from;

                var subjectHeader = message.Payload.Headers.FirstOrDefault(header => header.Name == "Subject");
                string subject = subjectHeader != null ? subjectHeader.Value : "No Subject";

                if (match.Success)
                {
                    result.Add(new Plan(message.Id, name, subject, emailBody, parsedDate));
                }
            }
        }

        return result;  // Return a list of Plan objects
    }

    static string DecodeBase64Url(string base64Url)
    {
        string base64 = base64Url.Replace('-', '+').Replace('_', '/');
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        byte[] bytes = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(bytes);
    }

    static string GetMessageBody(Message message)
    {
        if (message.Payload.Parts == null && message.Payload.Body != null)
        {
            return DecodeBase64Url(message.Payload.Body.Data);
        }

        StringBuilder body = new StringBuilder();
        foreach (var part in message.Payload.Parts)
        {
            if (part.MimeType == "text/plain")
            {
                body.Append(DecodeBase64Url(part.Body.Data));
            }
            else if (part.MimeType == "text/html")
            {
                // If you prefer HTML content over plain text, you can switch to this part
                // body.Append(DecodeBase64Url(part.Body.Data));
            }
        }

        return body.ToString();
    }
}
