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

# Create a temporary copy of the .nuspec file
Write-Host "Creating a temporary copy of the .nuspec file..."
$nupkgFile = "ToggleSteamVR.nuspec"
$tempNupkgFile = "ToggleSteamVR.temp.nuspec"
Copy-Item -Path $nupkgFile -Destination $tempNupkgFile

# Update the temporary .nuspec file with version and release notes
Write-Host "Updating the temporary .nuspec file..."
$nupkgContent = Get-Content $tempNupkgFile
$nupkgContent = $nupkgContent -replace '\$version\$', $version
$nupkgContent = $nupkgContent -replace '\$releaseNotes\$', $releaseNotes
Set-Content $tempNupkgFile $nupkgContent

# Pack the project into a NuGet package using the temporary .nuspec file
Write-Host "Packing the project into a NuGet package..."
nuget pack $tempNupkgFile

# Releasify the NuGet package using Squirrel
Write-Host "Releasifying the NuGet package..."
$nupkgFileName = "ToggleSteamVR.$version.nupkg"
Squirrel --releasify $nupkgFileName

# Clean up the temporary .nuspec file
Write-Host "Cleaning up the temporary .nuspec file..."
Remove-Item $tempNupkgFile

Write-Host "Releasification complete. Please push the changes to GitHub."
