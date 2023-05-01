# Project overview

It's just a small project for personal use, so it is useful for me 
to grab some video lessons locally, <b>NOT FOR BROADCAST</b>.

## Structure

Nothing special...
- VideoGrabber c# script that take in account a list of URLs refers 
to video/audio streams , process them with FFmpeg by merging audio video tracks.

- JSON file required by VideoGrabber for fetch all the HLS streams.

## Usage of script

- setup your machine with your favourite grabbing tools
- <p>When create the json be aware of fetch streams URLs from the right tools, (video grabbers plugins etc)</p>
- <p>Before running check and replace the ```projPath``` variable with your directory path.</p>
- <p>On runtime, when prompted choose the preferred choice.</p>

## Disclaimer
This is not a "plug and play" application so it require some checks before running it.
A basic knowledge of json and scripting would be useful.
## references

- https://restsharp.dev/
- https://json2video.com/