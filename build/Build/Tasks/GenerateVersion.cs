using System.IO;

using Build.Common.Services;
using Build.Common.Services.Impl;

using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.MSBuild;
using Cake.Common.Xml;
using Cake.Frosting;

namespace Build
{
    public sealed class GenerateVersion : FrostingTask<Context>
    {
        private Context _context;

        public override void Run(Context context)
        {

            var buildPropertiesPath = context.Environment.WorkingDirectory.FullPath + "/Directory.Build.Props";
            string assemblyVersion = "0.0.0";
            assemblyVersion = context.XmlPeek(buildPropertiesPath, "//AssemblyVersion");
            context.Information($"Found {assemblyVersion} as assembly version in Build properties file.");
            _context = context;
            IArtifactVersionService versionService = new ArtifactVersionService(new BranchService());
            context.General.ArtifactVersion = versionService.GetArtifactVersion(
                context.General.IsLocal,
                context.General.CurrentBranch,
                context.General.CurrentBranchName,
                context.AzurePipelines()?.Environment.Build.Number,
                LogInformation,
                assemblyVersion);

            context.Information($"Artifact Version: {context.General.ArtifactVersion}");
        }

        private void LogInformation(string informationToLog) => _context.Information(informationToLog);
    }
}
