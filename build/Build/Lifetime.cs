using System;

using Build.Common.Extensions;
using Build.Common.Services.Impl;

using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.GitVersion;
using Cake.Core;
using Cake.Frosting;

namespace Build;

public sealed class Lifetime : FrostingLifetime<Context>
{
    public override void Setup(Context context, ISetupContext info)
    {
        context.Information("Setting things up...");

        context.CleanDirectory(context.General.ArtifactsDir.AsDirectoryPath());
        context.General.IsLocal = context.BuildSystem().IsLocalBuild;

        try
        {
            SetBranchInContext(context);
        }
        catch (Exception ex)
        {
            context.Error(ex);
            // running dev pipeline by default if we can't determine otherwise
            context.General.CurrentBranchName = "develop";
            context.General.CurrentBranch = new BranchService().GetBranch(context.General.CurrentBranchName);
        }

        context.Information($"Running pipeline for branch: {context.General.CurrentBranchName}");
        context.Information(string.Join(Environment.NewLine, context.Environment.GetEnvironmentVariables()));
    }

    public override void Teardown(Context context, ITeardownContext info) =>
        context.Information("Tearing things down...");

    private void SetBranchInContext(Context context)
    {
        // this only works if the fetch-depth on checkout is set to 0 (see main.yml)
        // else this tool version will throw an error
        GitVersion gitVersion = context.GitVersion(new GitVersionSettings { NoFetch = true });
        string branchName = gitVersion.BranchName.ToLower();
        context.General.CurrentBranchName = branchName;
        context.General.CurrentBranch = new BranchService().GetBranch(branchName);
    }
}
