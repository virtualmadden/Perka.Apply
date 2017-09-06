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
                var projects = GetGithubProjects();

                var resumeContents = GetEncryptedResume();

                var applicationRequest = MapToRequest(projects, resumeContents);

                var response = PostApplicationToPerka(applicationRequest);

                return response.Response.Equals("You did it!");
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

        private static PerkaApplicationRequest MapToRequest(IEnumerable<GithubProjectResponse> projects, string resumeContents)
        {
            return new PerkaApplicationRequest
            {
                FirstName = ApplicationSettingsAdapter.ApplicationSettings.FirstName,
                LastName = ApplicationSettingsAdapter.ApplicationSettings.LastName,
                Email = ApplicationSettingsAdapter.ApplicationSettings.Email,
                PositionId = ApplicationSettingsAdapter.ApplicationSettings.PositionId,
                Explanation = ApplicationSettingsAdapter.ApplicationSettings.Explanation,
                Projects = projects.Select(x => $"{x.Name} - {x.Uri}").ToList(),
                Source = ApplicationSettingsAdapter.ApplicationSettings.Source,
                Resume = resumeContents
            };
        }

        private PerkaApplicationResponse PostApplicationToPerka(PerkaApplicationRequest applicationRequest)
        {
            return _perkaActions.PostApplication(applicationRequest).Result;
        }
    }
}