set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0010_conbsnplan\pricechg
cd C:\tool\textconv\bin
REM textconv -c BTN02W45 -d %P1%
textconv -x xpath -d %P1%
