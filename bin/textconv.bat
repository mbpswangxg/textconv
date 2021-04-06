set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0010_conbsnplan\pricechg
set P1="C:\tool\textconv\bin\webyml\1800_PurReq_Sales&MktgMgmt_PurReq.yml"
REM set P1=C:\tool\textconv\bin\webyml
REM set P1=C:\tmp\wangx\_projects\jae\develop\jae\jae\src\main\jssp\src\jae\0000_common\proposal\
REM set P1=C:\tool\textconv\bin\webyml\1800.yml
cd C:\tool\textconv\bin
REM textconv -c BTN02W45 -d %P1%
REM textconv -x xpath -d %P1%
textconv -web -f %P1%
REM textconv -c ayml -d %P1%
REM textconv -c DATENULL -d %P1%

taskkill /im textconv.exe /F
taskkill /im IEDriverServer.exe /F
