using System.IO;

using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Frosting;

namespace Build
{
    public sealed class ZipArtifacts : FrostingTask<Context>
    {
        public override void Run(Context context)
        {
            string appZip = $"{context.General.Name}_App-{context.General.ArtifactVersion}.zip";
            string destination = Path.Combine(context.Environment.WorkingDirectory.FullPath,
                context.General.ArtifactsDir, appZip);

            context.Information($"Zip als {appZip}");

            context.Zip(
                Path.Combine(context.Environment.WorkingDirectory.FullPath, context.General.ArtifactsDir),
                destination);

            context.General.ArtifactsToUploadToPipeline.Add(destination);
        }
    }
}
