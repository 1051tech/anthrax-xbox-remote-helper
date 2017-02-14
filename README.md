# anthrax-xbox-remote-helper
Project Anthrax (using names alphabetically, don't worry) is something I put together that combines ASP.NET API as well as Xamarin (Android/PCL). The Android app makes JSON requests to the API to get status about the Xbox and allows remote powering.

# Project Info
A little bit of boredom, and a determination to be as productive as possible during my lunch breaks. For a while now I've wished I could entertain myself better on my lunches. The idea dawned on me that I could stream my Xbox to my tablet at work. At first I was just going to use a VPN, but a little port-forwarding quickly changed that idea. Then I realized I needed to be able to grab my IP address while I'm at work. Yeah, I could just ping my DDNS address, but why settle for simple?

That's where this project came in. I wanted to try out Xamarin development anywhere and figured I could make it into something actually useful as well. At first my idea was to use the Java.Net implementation of sockets but could simplify the project by using familiar code. I then added the ASP.NET Web API project to allow me to interact with my home computer. This required writing some code for AJAX using WebRequest. Thus, Anthrax.Lib was born. Learning I could not only grab my IP Address, but also toggle my Xbox power was pretty crazy. I found a project that someone made using python to send power-on packets (located in the thanks section) and ported it over to C#.

# Special Thanks
I just wanted to give a special thanks to the following people for allowing me to make this project what it is...
First up we have Schamper who uploaded the python script the Xbox code took form from. Here is his GitHub: https://github.com/Schamper

Second, we have Bailey Miller on StackOverflow who had an updated payload packet header that made this work.
