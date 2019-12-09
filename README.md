# Localization

Create language file from ```Create>Localization>Create Language```

Add words and assign them ids as you see in the screenshot below.

![image](https://i.imgur.com/UIcz602.png)


Make sure to add the created languages in the ```LanguageCollection```.

Also assign default language.

Now in the Unity UI ```Text``` or ```TextmeshPro``` Text component add the script ```LocalizedTextScript```.
In the ID try searching for the word defined in your language file you can search with ID or text content.
Select the word and that's it.
![image](https://i.imgur.com/gR0weYw.gif)

You can change language by calling the ```ChangeLanguage``` method in the ```LocalizationCollection``` scriptable object file.
You can also translate runtime with the id.

There is an example scene in the package check that out first.
