using System.Runtime.Versioning;

using Build.Common.Enums;

using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Publish;
using Cake.Frosting;

namespace Build
{
    public class PublishApp : FrostingTask<Context>
    {
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
                Configuration = context.App.BuildConfig,
                PublishSingleFile = false,
                SelfContained = false,
                PublishReadyToRun = false,
                Runtime = context.App.Runtime,
                OutputDirectory = context.General.ArtifactsDir,
                VersionSuffix = versionSuffix,
                Framework = context.App.Framework
            };

            context.DotNetPublish(context.App.MainProject, dotNetCorePublishSettings);
        }
    }
}
