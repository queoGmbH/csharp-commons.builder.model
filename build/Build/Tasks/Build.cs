using System.IO;

using Build.Common.Enums;

using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Frosting;

namespace Build
{
    [TaskName("Build")]
    public sealed class Build : FrostingTask<Context>
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

            context.DotNetBuild(
                Path.Combine(context.Environment.WorkingDirectory.FullPath, context.App.MainProject),
                new DotNetBuildSettings { VersionSuffix = versionSuffix, Configuration = context.App.BuildConfig });
        }
    }
}
