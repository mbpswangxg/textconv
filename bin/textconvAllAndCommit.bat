::cd C:\workspace\textconv\bin

REM set P1=C:\resin\resin-pro-4.0.64\webapps\imart\WEB-INF\jssp\src\jae\0020_conmnfequip\equipinspect\
set P1=C:\workspace\jae\jae\src\main\jssp\src\jae\0012_consalesplan\publish\
set gitworkpath=C:\workspace\jae
set toolpath=C:\workspace\textconv\bin

call:RunAndCommit INSTR "INSTR対応"

call:RunAndCommit TO_DATE "TO_DATE変換"
call:RunAndCommit TO_CHAR "TO_CHAR変換"

call:RunAndCommit TO_NUMBER "TO_NUMBER変換"
call:RunAndCommit ADD_MONTH "ADD_MONTH変換"
call:RunAndCommit LPAD "LPAD変換"
call:RunAndCommit SUBSTRING "SUBSTRING変換"
call:RunAndCommit LENGTH "LENGTH変換"
call:RunAndCommit NVL "NVL変換"
call:RunAndCommit ROWID "ROWID変換"
call:RunAndCommit NULLIF "NULLIF変換"

call:RunAndCommit SYSDATE "SYSDATE変換"
call:RunAndCommit ORA_OR "OR条件変換"

::call:RunAndCommit UCASE_TAB "テーブル名大文字"
::call:RunAndCommit UCASE_COL "COL名大文字"
call:RunAndCommit DECODE "DECODE変換"
call:RunAndCommit LEFTJOIN "LEFTJOIN変換"

call:RunAndCommit UserTransaction "UserTransaction→Transaction"
call:RunAndCommit usertabcol "ユーザテーブル修正"
call:RunAndCommit insertscript "migrationスクリプト追加"
call:RunAndCommit showtitle "タイトル対応"

call:RunAndCommit DATENULL "DATENULL対応"
call:RunAndCommit delcoimart "コメントアウトされたIMARTタグ削除"
call:RunAndCommit PORTAL "メール中のURLアドレス修正"
call:RunAndCommit clearlineaddreturn "JS_clearlineメソッドにreturn false追加"

call:RunAndCommit VWRITECHANGE "HTML_縦表示文字背景色修正"

call:RunAndCommit BtnNoDisplay "HTML_非表示ボタンによる空白削除"

call:RunAndCommit HTMLescapeXml "escapeXML追加"

pause


:: %1:command　　%2:git comment
:RunAndCommit

echo %1 ########実行開始########
cd %toolpath%
textconv -c %1 -d %P1%

echo Git Commit Start!!!
cd %gitworkpath%
git commit -am %2

echo %1 ########実行完了########
goto:eof

