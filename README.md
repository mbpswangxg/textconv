# textconv
source replacer witten by c#.net

### usage:  
   textconv -c patternName -d c:\source\  replace on rules
   textconv -x -d c:\source\              export test case text file.
   
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
- skipInput=([^\t]+): find pattern in file content.
- mustInput=([^\t]+): not find pattern in file content.
