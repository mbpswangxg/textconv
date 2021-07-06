# 各exeの使用説明：
* 1.textconv.exe : テキストファイルの置換用コマンド. [詳細](#textconv)

* 2.text.web.exe : IE,Chromeを起動し、webdriverより画面動作確認する. [詳細](#text.web)

# <a id="textconv">1.textconv.exe</a>
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
  
# <a id="text.web">2.text.web.exe</a>
### IE,Chromeを起動し、webdriverより画面動作確認する.
1. IE/Chromeのブラウザ選択はtext.web.configに設定変更する
2. テスト用ScriptはYMLで記載されています。
3. ymlファイルはymlexporter.xlsmより作成されます。
4. ymlexport.xlsmからactionを設定後、直接Run WebDriverよりScriptの実行が可能です。 
![image](https://user-images.githubusercontent.com/80798706/124542354-399fd400-de5e-11eb-9745-d461328ed83a.png)
![image](https://user-images.githubusercontent.com/80798706/124542795-0447b600-de5f-11eb-87e7-d165eb9a4e22.png)
