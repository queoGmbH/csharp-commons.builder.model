using System.Collections.Generic;

using Build.Common.Enums;

using Cake.Core;
using Cake.Frosting;

using Semver;

namespace Build;

public class Context : FrostingContext
{
    public Context(ICakeContext context)
        : base(context)
    {
    }

    public App App { get; } = new();

    public General General { get; } = new();

    public Tests Tests { get; } = new();
}

public class General
{
    public string ArtifactsDir => ".artifacts";

    public IList<string> ArtifactsToUploadToPipeline { get; } = new List<string>();

    public SemVersion ArtifactVersion { get; set; }

    public Branches CurrentBranch { get; set; }

    public string CurrentBranchName { get; set; }

    public bool IsLocal { get; set; }

    public string Name { get; } = "ModelBuilder";

    public string SolutionName { get; } = "NA";
}

public class App
{
    public const string PROJECT_NAME = "Commons.Builders.Model";
    public string BuildConfig { get; } = "Release";
    public string Framework { get; } = "netstandard2.0";
    public string Runtime { get; } = "win-x64";

    public string MainProject { get; } = @$"src\{PROJECT_NAME}\{PROJECT_NAME}.csproj";
}

public class Tests
{
    public const string TEST_NAME = "Commons.Builders.Model.Tests";
    public string TestProjectName { get; } = $"{TEST_NAME}";

    public string TestProject { get; } = @$"tests\{TEST_NAME}\{TEST_NAME}.csproj";
}
