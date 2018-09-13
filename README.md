# Screenshot OCR

Apply OCR to Real-Time Screenshot in Single Key Pressing and a Mouse Dragging.

## Warnings

Make sure to make yourself informed the warnings below before kicking off.

- Screenshots within the red rectangle are sent to third-party website to do OCR processing. No privacy guarantee.
- BUGs may occasionally occur, which might mistaken the range of the red rectangle leading to extra screenshot sending. No privacy guarantee for your computer's full-size screenshot.
- Your IP address may be exposed to the screenshots receiving websites.
- Your screenshot and your IP address might be used in analyse use to the receiving websites.
- Changes are, though little, files downloaded from the download link might be swapped. No file integrity guarantee.

## Introduction to Different APIs

- A9T9 can identify as many as 25 languages.
- Sogou OCR API has a better recognition for English and Chinese but with size restriction for uploaded image;

## Prerequisite

- **Windows** system
- [Install **Microsoft .NET Framework 4.5**](https://www.microsoft.com/en-us/download/details.aspx?id=30653) OR(maybe) higher version
- (Optional) [Register an **API key**](https://us11.list-manage.com/subscribe?u=ce17e59f5b68a2fd3542801fd&id=252aee70a1)

## How to use

### For Screenshot OCR Now

1. Run executive file;
2. Press F4 when you need to convert letters on the screen to editable with OCR;
3. Press F4 again to undo;
4. Drag a rectangle to select the letters-contained region while pressing left key;
5. Wait for a message box to pop up. The recognized letters shown in the box are in your clipboard;
6. Click "OK" to copy them again, "Cancel" to close the message box;
7. Go to step 2.

### For Uploading Images

1. Right click the tray icon;
2. Left click "Upload" item;
3. Choose an image to upload;
4. Wait for a message box to pop up after clicking "OK". Clipboard updates at the same time;
5. Click "OK" to copy them again, "Cancel" to close the message box.

### For Settings

1. Right click tray icon to list out menu and click "Settings"; or simply left click the icon;
2. All settings save when option "Exit" in the menu clicked.

## Appreciation

- [天若OCR文字识别工具](https://www.52pojie.cn/thread-692917-1-1.html) - Source of Inspiration;
- [Free-OCR-API](https://github.com/A9T9/Free-OCR-API-CSharp) - Free OCR API;
- [Sogou API for OCR](http://ocr.shouji.sogou.com/v2/ocr/json) - High Quality OCR API without Documentation;
- [icon8](https://icons8.com/) - Free Icons site;
- [ConvertICO](https://convertico.com/) - site for Converting PNG to ICO;
- [Kelly](https://github.com/guo40020) - a pro giving me technical advises;
- and authors whose original code is at the website whose links were written between my codes.

## TODO

- [X] Add [Sogou API for OCR](http://ai.sogou.com/ai-docs/api/ocr)(link here with documentation is different from the former one)
- [ ] ~~Add a Search-by-Image feature~~ - No free reverse image search API available.
- [ ] Allow hotkey customization.
- [ ] Fix BUG: pressing Alt + F4 kills the program though it does not activated.
