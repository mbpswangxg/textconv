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
