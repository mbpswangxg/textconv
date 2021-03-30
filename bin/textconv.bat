set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0010_conbsnplan\pricechg
set P1="C:\tool\textconv\bin\webyml\1800_Adv_Pur_Req.yml"
set P1=C:\tool\textconv\bin\webyml\1800_Customer.yml
cd C:\tool\textconv\bin
REM textconv -c BTN02W45 -d %P1%
REM textconv -x xpath -d %P1%
textconv -web -f %P1%
