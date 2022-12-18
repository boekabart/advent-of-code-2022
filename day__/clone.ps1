if ($args.count -eq 0) { write-host "Pass number please"; return }
$X=$args[0]
Remove-Item "../day$X" -force -recurse
mkdir "../day$X"
Copy-Item *.cs* "../day$X"
Set-Location "../day$X"
fart -f "*.*" __ $X
fart "*.cs" __ $x
dotnet build
Set-Location "../day__"
