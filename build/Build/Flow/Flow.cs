using Cake.Frosting;

namespace Build {
    // [IsDependentOn(typeof(FormatCheck))]
    [IsDependentOn(typeof(NugetRestore))]
    [IsDependentOn(typeof(GenerateVersion))]
    [IsDependentOn(typeof(Build))]
    [IsDependentOn(typeof(RunTestsAndPublishResults))]
    [IsDependentOn(typeof(PublishApp))]
    [IsDependentOn(typeof(ZipArtifacts))]
    [IsDependentOn(typeof(UploadArtifactsToPipeline))]
    public partial class Default { }
}
