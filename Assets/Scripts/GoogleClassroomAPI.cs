using Google.Apis.Auth.OAuth2;
using Google.Apis.Classroom.v1;
using Google.Apis.Classroom.v1.Data;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

public class GoogleClassroomAPI
{
    static string[] Scopes = { GmailService.Scope.GmailReadonly, ClassroomService.Scope.ClassroomCoursesReadonly };
    static string ApplicationName = "Gmail API .NET Quickstart";
    public static List<Class> FetchTimetable()
    {
        List<Class> result = new List<Class>();
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

        var service = new ClassroomService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        var request = service.Courses.List();
        ListCoursesResponse response = request.Execute();
        IList<Course> courses = response.Courses;

        if (courses != null && courses.Count > 0)
        {
            string term;
            string year;
            if (DateTime.Now >= DateTime.Parse($"{DateTime.Now.Year}/04/01") && DateTime.Now < DateTime.Parse($"{DateTime.Now.Year}/09/01"))
            {
                term = "‘OŠú";
                year = $"{DateTime.Now.Year}";
            }
            else if (DateTime.Now >= DateTime.Parse($"{DateTime.Now.Year}/09/01") && DateTime.Now < DateTime.Parse($"{DateTime.Now.Year + 1}/01/01"))
            {
                term = "ŒãŠú";
                year = $"{DateTime.Now.Year}";
            }
            else
            {
                term = "ŒãŠú";
                year = $"{DateTime.Now.Year - 1}";
            }

            foreach (var course in courses)
            {
                string[] param = course.Section.Split(' ');

                if (year == param[0] && (term == param[3] || term == "”NŠÔ"))
                {
                    result.Add(new Class(param[1], course.Name, param[2].Substring(0, 1), param[2].Substring(1, 1)));
                }
            }
        }

        // Logic to fetch timetable from Google Classroom
        return result;  // Return a list of Class objects
    }
}
