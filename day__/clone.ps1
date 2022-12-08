if ($args.count -eq 0) { write-host "Pass number please"; return }
$X=$args[0]
rd "..\day$X" -force -recurse
mkdir "..\day$X"
copy *.cs* "..\day$X"
cd "..\day$X"
fart -f *.* __ $X
fart *.cs __ $x
fart *.sln day__ "day$X"
dotnet build
cd ..\day__
