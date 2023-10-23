using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Tools.DotNet;

[GitHubActions("test", GitHubActionsImage.UbuntuLatest, On = new[] { GitHubActionsTrigger.PullRequest }, InvokedTargets = new[] { nameof(Test) })]
partial class Build
{
    Target CleanTest => _ => _
        .Before(CompileTest)
        .Executes(() =>
        {
            DotNetTasks.DotNetClean(s => s
                .SetProject(Solution.GetProject(Globals.TestProjectName))
                .SetConfiguration(Configuration)
            );
        });

    Target CompileTest => _ => _
        .Executes(() =>
        {
            DotNetTasks.DotNetBuild(s => s
                .SetProjectFile(Solution.GetProject(Globals.TestProjectName)!.Path)
                .SetConfiguration(Configuration)
            );
        });

    Target Test => _ => _
        .DependsOn(Clean)
        .DependsOn(CleanTest)
        .DependsOn(CompileTest)
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetTest(s => s
                .SetProjectFile(Solution.GetProject(Globals.TestProjectName)!.Path)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
            );
        });
}