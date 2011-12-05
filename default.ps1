$framework = '4.0x86'
$version = '1.0.0'

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


task default -depends compile, dist
task local -depends compile, test
task full -depends local, dist
task ci -depends clean, release, commonAssemblyInfo, local, dist

task clean {
	delete_directory "$build_dir"
	delete_directory "$dist_dir"
}

task release {
    $global:config = "release"
}

task compile -depends clean { 
    exec { msbuild /t:Clean /t:Build /p:Configuration=$config /p:OutDir=$build_dir /v:q /nologo $source_dir\Nextbus.NET.sln }
}

task dist {
	create_directory $dist_dir\lib\net40
	copy-item "$build_dir\NextbusNET.dll" "$dist_dir\lib\net40"
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