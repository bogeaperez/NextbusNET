$framework = '4.0'
$version = '1.1.0'

properties {
	$base_dir = resolve-path .
	$build_dir = "$base_dir\build\"
	$dist_dir = "$base_dir\release"
	$source_dir = "$base_dir\src"
	$tools_dir = "$base_dir\tools"
	$test_dir = "$build_dir\test"
	$result_dir = "$build_dir\results"
	$lib_dir = "$base_dir\lib"
	$buildNumber = if ($env:build_number -ne $NULL) { $version + '.' + $env:build_number } else { $version + '.0' }
	$global:config = "debug"
	$framework_dir = Get-FrameworkDirectory
}


task default -depends compile
task full -depends release, update-assemblyInfo, local, package

task clean {
	delete_directory "$build_dir"
	delete_directory "$dist_dir"
}

task release {
    $global:config = "release"
}

task compile -depends clean { 
    exec { msbuild /t:Clean /t:Build /p:Configuration=$config /p:OutDir=$build_dir\net40 /v:q /nologo $source_dir\NextbusNET\NextbusNET.csproj }
    exec { msbuild /t:Clean /t:Build /p:Configuration=$config /p:OutDir=$build_dir\tests /v:q /nologo $source_dir\NextbusNET.Tests\NextbusNET.Tests.csproj }
    exec { msbuild /t:Clean /t:Build /p:Configuration=$config /p:OutDir=$build_dir\net45 /v:q /nologo $source_dir\NextbusNET4.5\NextbusNET4.5.csproj }
}

task update-nuspec {
	$filename = "$base_dir\NextbusNET.nuspec"
	$content = [xml] (Get-Content $filename)
	$content.package.metadata.version = $version
	$content.Save($filename)
}

task update-assemblyInfo {
	$filename = "$source_dir\CommonAssemblyInfo.cs"
	$assemblyVersionPattern = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $fileVersionPattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $assemblyVersion = 'AssemblyVersion("' + $version.Substring(0,3) + '.0.0")';
    $fileVersion = 'AssemblyFileVersion("' + $version + '.0")';

	$content = Get-Content $filename
	$content = $content -replace $assemblyVersionPattern, $assemblyVersion
	$content = $content -replace $fileVersionPattern, $fileVersion
	$content | Set-Content $filename
}

task package -depends update-nuspec {
	create_directory $dist_dir\lib\net40
	create_directory $dist_dir\lib\net45
	copy-item "$build_dir\net40\NextbusNET.dll" "$dist_dir\lib\net40"
	copy-item "$build_dir\net40\NextbusNET.XML" "$dist_dir\lib\net40"
	copy-item "$build_dir\net40\NextbusNET.XML" "$dist_dir\lib\net45"
	copy-item "$build_dir\net45\NextbusNET.dll" "$dist_dir\lib\net45"
	copy-item "$base_dir\NextbusNET.nuspec" "$dist_dir"

    exec { & $tools_dir\NuGet.exe pack $dist_dir\NextbusNET.nuspec -Symbols }
}

# -------------------------------------------------------------------------------------------------------------
# generalized functions 
# --------------------------------------------------------------------------------------------------------------
function Get-FrameworkDirectory()
{
    $([System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory().Replace("v2.0.50727", "v4.0.30319"))
}

function global:delete_directory($directory_name)
{
  rd $directory_name -recurse -force  -ErrorAction SilentlyContinue | out-null
}

function global:delete_file($file)
{
    if($file) {
        remove-item $file  -force  -ErrorAction SilentlyContinue | out-null} 
}

function global:create_directory($directory_name)
{
  mkdir $directory_name  -ErrorAction SilentlyContinue  | out-null
}

function global:copy_files($source, $destination, $exclude = @()) {
    create_directory $destination
    Get-ChildItem $source -Recurse -Exclude $exclude | Copy-Item -Destination {Join-Path $destination $_.FullName.Substring($source.length)} 
}