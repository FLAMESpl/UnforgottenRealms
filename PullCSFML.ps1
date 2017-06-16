Add-Type -AssemblyName System.IO.Compression.FileSystem

$url = "https://www.sfml-dev.org/files/CSFML-2.4-windows-32-bit.zip"
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
$bin = $zip.Entries | where {[System.IO.Path]::GetDirectoryName($_.FullName) -eq "CSFML\bin"}

foreach ($entry in $bin | where {$_.FullName -ne "CSFML/bin/"}) {
    [System.IO.Compression.ZipFileExtensions]::ExtractToFile($entry, $csfmlRoot + "\" + $entry.Name)
}

$zip.Dispose()
Remove-Item $output


