Add-Type -AssemblyName System.IO.Compression.FileSystem

$url = "https://www.sfml-dev.org/files/CSFML-2.1-windows-32bits.zip"
$output = $PSScriptRoot + "/packages/csfml.zip"
$csfmlRoot = $PSScriptRoot + "/csfml"

If ([System.IO.File]::Exists($output) -eq $False) {
    $wc = New-Object System.Net.WebClient
    $wc.DownloadFile($url, $output)
}

If ([System.IO.Directory]::Exists($csfmlRoot) -eq $True) {
    RmDir $csfmlRoot -Recurse
}

MkDir $csfmlRoot

$zip = [System.IO.Compression.ZipFile]::OpenRead($output)
$bin = $zip.Entries | where {[System.IO.Path]::GetDirectoryName($_.FullName) -eq "CSFML-2.1\bin"}

foreach ($entry in $bin | where {$_.FullName -ne "CSFML-2.1/bin/"}) {
    [System.IO.Compression.ZipFileExtensions]::ExtractToFile($entry, $csfmlRoot + "\" + $entry.Name)
}

$zip.Dispose()
Remove-Item $output


