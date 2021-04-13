# textconv
A command tool written by c#.net.  

## Main Functions
1. regex match and replace txt files.
   eg. textconv -c patternName -f txtFilePath  
       textconv -c patternName -d txtDirectoryPath

2. xpath analyze for html, export matched elements  
   eg. textconv -x -d %P1%

3. web driver for web page auto test. using webdriver. Currently IE and Chrome are supported.
   eg. textconv -web -f %P1% 

## 1. Regex Replacer Parameters Description.
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
   eg. textconv -x -d %P1%
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

