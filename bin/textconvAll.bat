cd C:\tool\textconv\bin

set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0020_conmnfequip\equipinspect\

textconv -c INSTR -d %P1%
textconv -c TO_DATE -d %P1%
textconv -c TO_CHAR -d %P1%

textconv -c TO_NUMBER -d %P1%
textconv -c ADD_MONTH -d %P1%
textconv -c LPAD -d %P1%
textconv -c SUBSTRING -d %P1%
textconv -c LENGTH -d %P1%
textconv -c NVL -d %P1%
textconv -c ROWID -d %P1%
textconv -c NULLIF -d %P1%

textconv -c SYSDATE -d %P1%
textconv -c ORA_OR -d %P1%

textconv -c UCASE_TAB -d %P1%
textconv -c UCASE_COL -d %P1%
textconv -c DECODE -d %P1%
textconv -c LEFTJOIN -d %P1%

textconv -c UserTransaction -d %P1%
textconv -c usertabcol -d %P1%
textconv -c insertscript -d %P1%
textconv -c showtitle -d %P1%

textconv -c DATENULL -d %P1%
textconv -c delcomment -d %P1%

