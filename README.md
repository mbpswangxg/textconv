# textconv
source replacer witten by c#.net

## 1. Parameters  
 -c paramName.  
 -d destination folder.  
### usage:  
   textconv -c patternName -d c:\source\  
   patternName=\w+  repCmdKey=ABC  
   
## 2.Option Parameters about Regex.
- if repCmdKey== "repfile" then replacement= readAllText(repfile);
- IgnoreCase=(true|false): RegexOptions.IgnoreCase
- Multiline=(true|false): RegexOptions.Multiline

## 3.Replace Option About Match In Range.
- rangeFrom=([^\t]+) : Range of start pattern.
- rangeTo=([^\t]+)   : Range of end pattern.
- rangeSkip=(true|false): required. rangeFrom and rangeTo is not empty. 
if true, skip replace the match, else do replace the match.

## 4.Replace Option About filefilter.
- filefilter=([^\t]+) : filepath pattern. 
- fileSkip=(true|false)  : required.filefilter is not empty. 
 if true, skip replace the match, else do replace the match.
 
## 5.Replace or skip file on check file contents.
- iffindstr=([^\t]+): find pattern in file content.
- ifnotfindstr=([^\t]+): not find pattern in file content.
- iffindand=(true|false): iffindstr and ifnotfindstr can be duplicated. 
true as and, false as or between duplicated findstr match.  

## 6.Replace on repCmdKey=LCASE_GROUP | UCASE_GROUP. 
   ex. UCASE_GROUP=1,3 : UCASE group[1] and group[3] of captures.
- excludewords=([^\t]+): skip on contained in excluedwords.  
- excludefile=([^\t]+): skip on contained in excluedwords read from this file. 

## 7.Replace or skip about replaceIndexes and skipMatchIndex.
- replaceIndexes=([^\t]+): like replaceIndexes=1,2. if matchcollection index=1 or 2, do replace.
- skipMatchIndex=(true|false): if true, skip the match index contained in replaceIndexes.

