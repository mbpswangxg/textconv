# textconv
A command tool written by c#.net.  

## Main Functions
1. regex match and replace txt files.  
   eg. textconv -c patternName -f c:/src/java/test.java  
       textconv -c patternName -d c:/src/java/  

2. xpath analyze for html, export matched elements  
   eg. textconv -x -d c:/src/xpath-rules/

3. web driver for web page auto test. using webdriver. Currently IE and Chrome are supported.
   eg. textconv -web -f c:/src/rules/test01.yml

## 1. Regex Replacer.
 - -c cmdkey: the name of a replace rule.  ***optional***. if it's empty, all rules in the rulefile are used.
 - -d destfolder: a folder for the destination files. ***-f or -d required***. pre-set in textconv.config. 
 - -f srcfile: a file for the destination. ***-f or -d required***.
 * replace rules are saved in yml file under [TextConv/replaceYml/#example_format.yml](TextConv/replaceYml/#example_format.yml) 
 * replace rules saved path can be redirected on change [textconv.config](./textconv.config).
   
## 2. Xpath analyzer
 - -x : define Xpath analyzing. ***required***.
 - -d destfolder: a folder for the destination files. ***-f or -d required***. pre-set in textconv.config. 
 - -f srcfile: a file for the destination. ***-f or -d required***.

 * export element(attribute) info for ut-case.  
   eg. textconv -x -d c:/src/Directory/path/

 * replace rules are saved in yml file under [TextConv/yml/#example_format.yml](TextConv/yml/#example_format.yml) 
 * replace rules saved path can be redirected on change [textconv.config](./textconv.config).
   
## 3.Web Driver 
 - -web : define web driver working. ***required***.
 - -d destfolder: a folder for the destination files. ***-f or -d required***. pre-set in textconv.config. 
 - -f srcfile: a file for the destination. ***-f or -d required***.

 * export element(attribute) info for ut-case.  
   eg. textconv -web -f rulefile.yml
 * replace rules are saved in yml file under [TextConv/webyml/#example_format.yml](TextConv/webyml/#example_format.yml) 
 * replace rules saved path can be redirected on change [textconv.config](./textconv.config).
 * The tool of [*YmlExporter.xlsm*](./textconv/bin/YmlExporter.xlsm) is written by excel macro, which helps for making webdriver.yml
 * if the element can't be found, please using conver.exe(a UI Tool) to test XPath. 

#### Web Driver Mode parameters expression
 | Gobal Parameters |  |  | 
 | ---- | ---- | ---- |
 | interval | 500 | # 間隔時間: 0.5秒 |
 | shotcmd | [open, popup, click] | screenshotの監視command |
 | shotfromstep | 15 | screenshotの開始STEP位置 |

 
 | command | 概要説明➡例： | command | target | value | 
 | ---- | ---- | ---- | ---- | ----  | 
 | var | 変数宣言 | var | routeX | 1 | 
 | open | 特定URLを開く,URLが必須 | open | www.google.com |  | 
 | wait | valueの指定ms時間を待つ | wait |  | 1000 | 
 | resize | windowサイズ設定 | resize | window | 1000x900 | 
 | overwrite | input-textにvalueを上書く | overwrite | id=userid | tester | 
 | click | targetで特定elementをクリック | click | css=.imui-btn-login |  | 
 | label | 特定処理位置宣言 | label | draft01 |  | 
 | switchTo | 特定Windowへ切替 | switchTo | window | root | 
 | switchTo | Default画面へ切替 | switchTo | DefaultContent |  | 
 | switchTo | 特定Frameへ切替 | switchTo | frame | IM_MAIN | 
 | switchTo | alert・confirmへ切替 | switchTo | alert |  | 
 | ifvar | 変数値を判定して；後ろの処理を行う | ifvar | routeX=2;goto:draft01 |  | 
 | ifind | 特定elementが存在する場合；後ろを実行 | ifind | XPathFormat=//*[contains(text(),"{0}")]/parent::tr/td[1]/a;goto:draft02 | param1 | 
 | ifnot | 特定elementが存在しない場合；後ろを実行 | ifnot | XPathFormat=//*[contains(text(),"{0}")]/parent::tr/td[1]/a; goto:im_bpw_consented | param1 | 
 | goto | 特定処理位置へ移動 | goto | draft01 |  | 
 | #ref:data | 別worksheet[data]に記載されたデータを参照 | #ref:data |  |  | 
 | type | input-textにvalueを追記入力 | type | name=process_name | test002 | 
 | popup | 特定elementをクリックして、POPUP画面を起動 | popup | XPathFormat=//td[contains(text(),"{0}")]/parent::tr/td/a/img[contains(@src, "flow")] | newwind | 
 | sendkeys | 特定fileuploadにvalueを送信 | sendkeys | name=content | C:\test.xml | 
 | sendkeys | キー送信。EnterKeyで確定等 | sendkeys |  | {enter} | 
 | close | windowを閉じる | close |  |  | 


 | target | 概要説明➡例： | command | target | value | 
 | ---- | ---- | ---- | ---- | ----  | 
 | id=testid | idでelementを取得 | overwrite | id=userid | tester | 
 | css=.classname | cssでelementを取得 | click | css=tr:nth-child(9) > .list_data_bg > input |  | 
 | linktext=検索 | 表示文字でelementを取得 | click | linktext=検索 |  | 
 | XPath=//img[contains(@src,"next")] | xpathでelementを取得 | click | XPath=//img[contains(@src,"next")] |  | 
 | XPathFormat=//*[contains(text(),"{0}")]/ | xpathでelementを取得。必要なパラメータ1がvalueから取得 | click | XPathFormat=//*[contains(text(),"{0}")]/parent::tr/td[1]/a | test | 
 | routeX=1;goto:route1 | ifの判定内容、；以降は独立コマンドで実行 | ifvar | routeX=2;goto:draft01 |  | 
 | hasroute=1;math|routeX+1 | ；以降の算数計算式、変数＝変数+-*/ decimalで計算 | ifvar  | hasroute=1;math|routeX+1 |  | 

 | value | 概要説明➡例： | command | target | value | 
 | ---- | ---- | ---- | ---- | ----  | 
 | F1-F12,Enter… | sendkeyの送信内容. {KeyName} | sendkeys |  | {enter} | 
 | ctr,shift,alt | ctr^, shift+,ALT%. | 参考： | https://lqwangxg.blogspot.com/2021/03/c-sendkeys.html |  | 
 | A-Z | 直接入力, | sendkeys | id=userid | ABCD | 
 | shotflag | 概要説明➡例： | command | target | value | 
 | 1 | 強制ScreenShot送信sendkeys("%{PRTSC}") | sendkeys |  | %{PRTSC} | 
 | -1 | 強制ScreenShot送信しない |  |  |  | 

