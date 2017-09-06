using System;
using System.Collections.Generic;
using System.Linq;
using Perka.Apply.Client.Actions;
using Perka.Apply.Client.Adapters;
using Perka.Apply.Client.Models;

namespace Perka.Apply.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var result = GetGithubProjects();

                var resumeContents = GetEncryptedResume();

                var applicationRequest = new PerkaApplicationRequest
                {
                    FirstName = "Jonathan",
                    LastName = "Madden",
                    Email = "virtualmadden@gmail.com",
                    PositionId = "GENERALIST",
                    Explanation = "Custom C# Leveraging HTTPClient - https://github.com/virtualmadden/Perka.Apply.Client",
                    Projects = result.Select(x => $"{x.Name} - {x.Uri}").ToList(),
                    Source = "Perka.com",
                    Resume = resumeContents
                };

                var response = PostApplicationToPerka(applicationRequest);

                if (response.Response.Equals("You did it!"))
                    Console.WriteLine(response.Response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static IEnumerable<GithubProjectResponse> GetGithubProjects()
        {
            return new GithubActions().GetProjects().Result;
        }

        private static string GetEncryptedResume()
        {
            return new FileSystemAdapter().GetBase64EncodedResume();
        }

        private static PerkaApplicationResponse PostApplicationToPerka(PerkaApplicationRequest applicationRequest)
        {
            return new PerkaActions().PostApplication(applicationRequest).Result;
        }
    }
}