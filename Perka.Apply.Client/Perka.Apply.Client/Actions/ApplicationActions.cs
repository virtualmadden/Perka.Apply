using System;
using System.Collections.Generic;
using System.Linq;
using Perka.Apply.Client.Adapters;
using Perka.Apply.Client.Models;

namespace Perka.Apply.Client.Actions
{
    public interface IApplicationActions
    {
        bool Apply();
    }

    public class ApplicationActions : IApplicationActions
    {
        private readonly IFileSystemAdapter _fileSystemAdapter;
        private readonly IGithubActions _githubActions;
        private readonly IPerkaActions _perkaActions;

        public ApplicationActions(IGithubActions githubActions = null, IFileSystemAdapter fileSystemAdapter = null, IPerkaActions perkaActions = null)
        {
            _githubActions = githubActions ?? new GithubActions();
            _fileSystemAdapter = fileSystemAdapter ?? new FileSystemAdapter();
            _perkaActions = perkaActions ?? new PerkaActions();
        }

        public bool Apply()
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
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private IEnumerable<GithubProjectResponse> GetGithubProjects()
        {
            return _githubActions.GetProjects().Result;
        }

        private string GetEncryptedResume()
        {
            return _fileSystemAdapter.GetBase64EncodedResume();
        }

        private PerkaApplicationResponse PostApplicationToPerka(PerkaApplicationRequest applicationRequest)
        {
            return _perkaActions.PostApplication(applicationRequest).Result;
        }
    }
}