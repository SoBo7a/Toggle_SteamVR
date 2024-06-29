param (
    [string]$version,
    [string]$releaseNotes = ""
)

# Ensure all necessary tools are available
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Error "dotnet CLI is not installed or not in PATH."
    exit 1
}

if (-not (Get-Command nuget -ErrorAction SilentlyContinue)) {
    Write-Error "NuGet CLI is not installed or not in PATH."
    exit 1
}

if (-not (Get-Command Squirrel -ErrorAction SilentlyContinue)) {
    Write-Error "Squirrel CLI is not installed or not in PATH."
    exit 1
}

# Build the project
Write-Host "Building the project..."
dotnet build --configuration Release

# Update .nuspec file with version and release notes
Write-Host "Updating .nuspec file..."
$nupkgFile = "ToggleSteamVR.nuspec"
$nupkgContent = Get-Content $nupkgFile
$nupkgContent = $nupkgContent -replace '\$version\$', $version
$nupkgContent = $nupkgContent -replace '\$releaseNotes\$', $releaseNotes
Set-Content $nupkgFile $nupkgContent

# Pack the project into a NuGet package
Write-Host "Packing the project into a NuGet package..."
nuget pack $nupkgFile

# Releasify the NuGet package using Squirrel
Write-Host "Releasifying the NuGet package..."
$nupkgFileName = "ToggleSteamVR.$version.nupkg"
Squirrel --releasify $nupkgFileName

Write-Host "Releasification complete. Please push the changes to GitHub."
