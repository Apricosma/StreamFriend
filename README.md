## A work-in-progress streamer companion AI written with C#

Goals:
- Periodically read twitch chat at certain intervals and pass to generative-text AI
- Assume a defined, consistent personality 
- Currently using ChatGPT, plans to change models later on
- Plans to pass the generation results to Elevenlabs API to generate a specialized voice
  for your companion. Elevenlabs allows easy training and swapping of voices, which is
  perfect for this project
- Utilize a live2d model which animates and lip syncs to the voice output by Elevenlabs

I am fairly inexperienced when it comes to programming, I will be learning alongside
the creation of this project and hopefully improve over time. The initial commits may be
very messy as I figure out the basics. 

## Dependancies
- TwitchLib

Install via NuGet Package Manager

`Install-Package TwitchLib`