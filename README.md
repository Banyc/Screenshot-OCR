# Screenshot OCR

Apply **OCR** or **Reverse Image Search** to Real-Time Screenshot in a Single Key Pressing and a Mouse Dragging.

## Warnings

Make sure to make yourself informed the warnings below before kicking off.

- Screenshots within the red rectangle are sent to a third-party website to do OCR processing. No privacy guarantee.
- BUGs may occasionally occur, which might mistaken the range of the red rectangle leading to an extra screenshot sending. No privacy guarantee for your computer's full-size screenshot.
- Your IP address may be exposed to the screenshots receiving websites (servers).
- Your screenshot and your IP address might be used in analysis use to the receiving websites (servers).
- Chances are, though little, files downloaded from the download link might be swapped. No file integrity guarantee.
- some POST links are using unsafe HTTP Communications Protocol, rather than HTTPs.

## Introduction to Different APIs

- A9T9 can identify as many as 25 languages;
- Sogou OCR API has a better recognition for English and Chinese but with size restriction for uploaded image; sizing problem solved;
- SauceNAO includes uploaded files storage and redirection to other image reverse search engine. This source is not stable.

## Prerequisite

- **Windows** system
- [Install **Microsoft .NET Framework 4.5**](https://www.microsoft.com/en-us/download/details.aspx?id=30653) OR(maybe) higher version
- (Optional) [Register an **API key**](https://us11.list-manage.com/subscribe?u=ce17e59f5b68a2fd3542801fd&id=252aee70a1)

## How to use

### For Screenshot OCR Now

1. Run executive file;
2. Press `F4` when you need to convert letters on the screen to editable with OCR;
3. Press `F4` again or right click to undo;
4. Drag a rectangle to select the letters-contained region while pressing left key;
5. Wait for a yellow message box to pop up. The recognized letters shown in the box are in your clipboard;
6. Double click the message box to copy them again, right click to close it;
7. Go to step *2*.

Hint: In screenshot mode, press `Ctrl` while left click to re-capture the previous region.

### For Image Reverse Search

- Goto settings and choose SauceNAO mode.

### For Uploading Images

1. Right click the tray icon;
2. Left click "Upload" item;
3. Choose an image to upload;
4. Wait for a message box to pop up after clicking "OK". Clipboard updates at the same time;
5. Click "OK" to copy them again, "Cancel" to close the message box.

**Hint**: API that is used is based on the mode you chose at settings.

### For Settings

1. Right click tray icon to list out menu and click "Settings"; or simply left click the icon;
2. All settings save after the option "Exit" in the menu is clicked.

## Note

### WPF Window Overview

[Window_Lifetime_Events](https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ms748948(v=vs.100)#Window_Lifetime_Events)

### ActualWidth and ActualHeight Updating Lag of TextBlock

Reasons

- [The process cycle](https://www.codeproject.com/Questions/181118/WPF-TextBlock-Width-and-Height)
- [Calculation of Double](https://stackoverflow.com/questions/9008525/why-actualsize-is-not-updating-its-value-on-wpf)

[Solution](https://stackoverflow.com/questions/10556019/how-to-calculate-the-textbock-height-and-width-in-on-load-if-i-create-textblock)

### Data Binding

[Model-View Binding](https://rachel53461.wordpress.com/2012/07/14/what-is-this-datacontext-you-speak-of/)

## Appreciation

- [天若OCR文字识别工具](https://www.52pojie.cn/thread-692917-1-1.html) - Source of Inspiration;
- [Free-OCR-API](https://github.com/A9T9/Free-OCR-API-CSharp) - Free OCR API;
- [Sogou API for OCR](https://ocr.shouji.sogou.com/v2/ocr/json) - High Quality OCR API without Documentation;
- [SauceNAO](https://saucenao.com/) - an image reverse search site and also an image host;
- [icon8](https://icons8.com/) - Free Icons site;
- [ConvertICO](https://convertico.com/) - site for Converting PNG to ICO;
- [Kelly](https://github.com/guo40020) - a pro giving me technical advice;
- and authors whose original code is at the website whose links were written between my codes.

## TODO

- [X] Add [Sogou API for OCR](http://ai.sogou.com/ai-docs/api/ocr)(link here with documentation is different from the former one)
- [X] Add a Search-by-Image feature. Redirect from SauceNAO
- [X] Allow hotkey customization.
- [X] Fix BUG: pressing Alt + F4 kills the program though it does not activated.
- [X] Break limitation for minimum image size by Sogou API
- [X] Perfect UI
- [X] fix resolution issue in some desktops
- [ ] Auto fixs the grammar mistakes on OCR results
  - https://textgears.com/api/
  - https://www.afterthedeadline.com/api.slp
  - https://docs.microsoft.com/en-us/windows/desktop/intl/about-the-spell-checker-api
  - https://stackoverflow.com/a/38128912/9920172
- [ ] Add feature: Notes Display
- [X] Adapt MVC pattern
- [X] Replace the draw board from Winform to WPF
- [X] Reduce startup flickering
