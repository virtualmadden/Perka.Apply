//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var projDir = Directory("./Perka.Apply.Client/Perka.Apply.Client/bin/netcoreapp2.0");
var testDir = Directory("./Perka.Apply.Client/UnitTests/Perka.Apply.Client.Tests/bin/netcoreapp2.0");

var solution = "./Perka.Apply.Client/Perka.Apply.Client.sln";

var buildSettings = new DotNetCoreBuildSettings {
        Framework = "netcoreapp2.0",
        Configuration = configuration
    };

var testSettings = new DotNetCoreTestSettings {
        Framework = "netcoreapp2.0",
        Configuration = configuration
    };

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("Clean")
    .Description("Cleaning the build directory.")
    .Does(() =>
{
    CleanDirectory(projDir);
    CleanDirectory(testDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Description("Restoring NuGet packages.")
    .Does(() =>
{
    NuGetRestore(solution);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Description("Building the solution.")
    .Does(() =>
{
    DotNetCoreBuild(solution, buildSettings);
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Description("Runs Unit tests.")
    .Does(() =>
{
    var unitTests = GetFiles("./Perka.Apply.Client/UnitTests/**/*.csproj");

    foreach(var test in unitTests)
     {
         DotNetCoreTest(test.FullPath, testSettings);
     }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);