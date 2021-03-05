cd C:\workspace\textconv\bin

set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0020_conmnfequip\equipinspect\
set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0010_conbsnplan\equipchk
set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0002_jaecs\elearning

REM TextConv.exe -c delcoimart -d %P1%
textconv -c td2 -d %P1%
