C:\tool\PortableGit\git-cmd.exe

cd C:\tool\textconv\bin

set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0020_conmnfequip\equipinspect\
set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0010_conbsnplan\equipchk
set P1=C:\tmp\wangx\_projects\jae\develop\jae\jae\src\main\jssp\src\jae\0010_conbsnplan\equipchk\
REM TextConv.exe -c delcoimart -d %P1%

textconv -c DECODE -d %P1%
textconv -c LEFTJOIN -d %P1%
git commit -am "decodeURL -> htmlDecode"
