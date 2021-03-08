cd C:\workspace\textconv\bin

set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0020_conmnfequip\equipinspect\
set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0010_conbsnplan\equipchk
set P1=C:\tmp\wangx\_projects\jae\develop\jae\jae\src\main\jssp\src\jae\0010_conbsnplan\equipchk\
set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0012_consalesplan\publish\


TextConv.exe -c HTMLescapeXml -d %P1%

pause
REM textconv -x %P1%        