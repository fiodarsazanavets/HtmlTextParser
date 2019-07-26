# HtmlTextParser

A simple console app that would allow you to convert a text into HTML for easy insertion into blogs.

The input format of the text would be easily to type in, even on a small hand-held device, such as mobile phone.

Once the text is typed, it can be saved into a text file. The console app will do the following modifications to the text:

* The string would be HTML-encoded.
* Every paragraph will be enclsed in <p> tag.
* Every text enclosed in =1= will be enclosed in <h1> tag.
* Every text enclosed in =2= will be enclosed in <h2> tag.
* Every text enclosed in =3= will be enclosed in <h3> tag.
* A line with <br/> tag will be added before any header tag.
* Any text enclosed in == tags will be enclosed in <a> tag with a placeholder for the URL.
