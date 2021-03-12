# textconv
source replacer witten by c#.net

## 1. Parameters
 - -p regex pattern.
 - -r regex replacement.
 - -i input string.

 - -c cmdkey: the name of a replace rule.  ***optional***. if it's empty, all rules in the rulefile are used.
 - -d srcfolder: a folder for the destination files of replacement.
 - -f srcfile: a file for the destination of replacement.

 
### usage:  
   textconv -p \d{,3}.\d{,3}.\d{,3}.(\d{,3}) -r 192.168.0.$1 -input 10.0.0.1-10.0.0.255    
   textconv -p \d{,3}.\d{,3}.\d{,3}.(\d{,3}) -r 192.168.0.$1 -d destDirectory  
   textconv -p \d{,3}.\d{,3}.\d{,3}.(\d{,3}) -r 192.168.0.$1 -f destFilePath  
   textconv -c cmdKey -d destDirectory
   textconv -c cmdKey -f destFilePath
   textconv -x -d srcfolder		: create casefile.txt