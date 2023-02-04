using System.Runtime.Versioning;

using Build.Common.Enums;

using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Publish;
// using Cake.Common.Tools.DotNetCore.Publish;
using Cake.Frosting;

namespace Build
{
    public class PublishApp : FrostingTask<Context>
    {
        /// <summary>Runs the task using the specified context.</summary>
        /// <param name="context">The context.</param>
        public override void Run(Context context)
        {
            string versionSuffix;

            if (!context.General.IsLocal && context.General.CurrentBranch == Branches.Main)
            {
                versionSuffix = string.Empty;
            }
            else
            {
                versionSuffix = $"{context.General.ArtifactVersion.Prerelease}-{context.General.ArtifactVersion.Build}";
            }

            DotNetPublishSettings dotNetCorePublishSettings = new()
            {
                Configuration = "release",
                PublishSingleFile = false,
                SelfContained = false,
                //PublishTrimmed = true,
                PublishReadyToRun = false,
                Runtime = "win-x64",
                OutputDirectory = context.General.ArtifactsDir,
                VersionSuffix = versionSuffix,
                //TODO: adjust for multiple outputs
                Framework = "net7.0"
            };

            context.DotNetPublish(context.App.MainProject, dotNetCorePublishSettings);
        }
    }
}
