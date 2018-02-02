# TextGrunt
Windows productivity tool for managing short text snippets.
Requires .NET 4.6.1 or higher.

TextGrunt lives in your traybar and can start automatically (default setting). 
Use it for storage and quick access of commonly used text strings.

Features
---
To add/edit texts open the main view by double clicking the TextGrunt traybar icon. Changes are automatically saved. 
To copy text to clipboard either use main view or right click textgrunt traybar icon and then click on the text to copy.

Textgrunt also keeps track of text being added to the Windows clipboard from other applications in the "Clipboard history" tab. 
These texts NOT saved to disk, and will be removed when exiting the application.
There is a maximum of 100 texts in this tab. Once 100 is reached it will start removing old entries.

User data is stored in %appdata%\TextGrunt (Usually: C:\Users\<MyUserName>\AppData\Roaming\TextGrunt)
Note that user data is stored in a fairly readable format (json), so dont save sensitive data.


Textgrunt uses the MIT license.
