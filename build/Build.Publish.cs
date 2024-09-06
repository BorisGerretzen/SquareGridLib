using System.Linq;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;

[GitHubActions(
    "publish",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.WorkflowDispatch },
    InvokedTargets = new[] { nameof(Pack), nameof(Push) },
    ImportSecrets = new[] { nameof(NugetApiKey) },
    FetchDepth = 0
)]
partial class Build
{
    Target Pack => _ => _
        .DependsOn(Clean)
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetPack(s => s
                .SetProject(LibProject)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .SetDescription("Blazor dashboard layout component that allows for placing panels of any size on a 2d grid.")
                .SetPackageTags("blazor dashboard dashboard-layout grid grid-layout")
                .SetNoDependencies(true)
                .SetOutputDirectory(PackageDirectory)
                .SetAuthors("Boris Gerretzen")
                .SetPackageProjectUrl(Globals.RepositoryUrl)
                .SetRepositoryUrl(Globals.RepositoryUrl)
                .SetVersion(GitVersion.NuGetVersionV2)
            );
        });

    Target Push => _ => _
        .DependsOn(Pack)
        .Requires(() => NugetApiUrl)
        .Requires(() => NugetApiKey)
        .Executes(() =>
        {
            PackageDirectory.GlobFiles("*.nupkg")
                .Where(x => !x.Name.EndsWith("symbols.nupkg"))
                .ForEach(x =>
                {
                    DotNetTasks.DotNetNuGetPush(s => s
                        .SetTargetPath(x)
                        .SetSource(NugetApiUrl)
                        .SetApiKey(NugetApiKey)
                        .EnableSkipDuplicate()
                    );
                });
        });
}