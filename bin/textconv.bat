REM set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0010_conbsnplan\pricechg
set P1="C:\tool\textconv\bin\webyml\1800_TrngPurReq_Acctg.yml"
set P1=C:\tmp\wangx\_projects\wang2\src\src122
REM set P1=C:\tmp\wangx\_projects\jae\develop\jae\jae\src\main\jssp\src\jae\0000_common\proposal\
cd C:\tool\textconv\bin
REM textconv -x xpath -d %P1%
textconv -c comment -d %P1%
REM textconv -web -f %P1%

taskkill /im textconv.exe /F
taskkill /im IEDriverServer.exe /F
