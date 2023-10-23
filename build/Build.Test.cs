using Nuke.Common;
using Nuke.Common.Tools.DotNet;

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
        .DependsOn(CleanTest)
        .DependsOn(CompileTest)
        .Executes(() =>
        {
            DotNetTasks.DotNetTest(s => s
                .SetProjectFile(Solution.GetProject(Globals.TestProjectName)!.Path)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
            );
        });
}