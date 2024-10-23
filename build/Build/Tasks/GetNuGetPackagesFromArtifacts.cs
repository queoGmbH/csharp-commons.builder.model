using System.IO;

using Cake.Common.Diagnostics;
using Cake.Frosting;

namespace Build;

public class GetNuGetPackagesFromArtifacts : FrostingTask<Context>
{
    /// <summary>Runs the task using the specified context.</summary>
    /// <param name="context">The context.</param>
    public override void Run(Context context)
    {
        context.Information("Found the following package files:");
        foreach (string file in Directory.GetFiles(".artifacts", "*.nupkg"))
        {
            context.Information(file);
        }
    }
}
