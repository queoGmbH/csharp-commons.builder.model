using Build.Common.Enums;
using Cake.Common.Tools.DotNet;
using Cake.Frosting;
using System.IO;

namespace Build;

public sealed class BuildNuGetPackage : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        string versionPrefix, versionSuffix;
        var version = context.General.ArtifactVersion;

        if (!context.General.IsLocal && context.General.CurrentBranch == Branches.Main)
        {
            versionPrefix = $"{version.Major}.{version.Minor}.{version.Patch}";
            versionSuffix = string.Empty;
        }
        else
        {
            versionPrefix = $"{version.Major}.{version.Minor}.{version.Patch}";
            versionSuffix = $"{version.Prerelease}-{version.Build}";
        }
        context.DotNetPack(Path.Combine(context.Environment.WorkingDirectory.FullPath, context.App.MainProject),
        new Cake.Common.Tools.DotNet.Pack.DotNetPackSettings()
        {
            Configuration = context.App.BuildConfig,
            OutputDirectory = "./.artifacts",
            VersionSuffix = versionSuffix
        });
    }
}
