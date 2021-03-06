# Base Station
Contains the code that will run on the base station computer, mainly the user interface,
along with the server code that will communicate with rover to control it and receive 
information from it.

## Project Layout
 - EntryPoint: the "main method" of the base station, in the HuskyRobotics.BaseStation.StartBaseStation class. 
 - UserInterface: user interface, displaying map and other info. 
 - Utilities: methods for use by all base station projects.
 - Server: networking code for communicating with the rover.
 - PTZCamera: networking code for communicating with rover camera.

## UI Functions
 - Navigation
 - Diagnostics
 - Video Feed
 
## Status Info UI
 - Wheel spinning/stuck status
 - Direction of wheels
 - See the encoders, BE the encoders
 - Same with arm

## Orientation/Informational Map
 - Show current position and direction of rover
 - Click on map to select a point
 - Be able to type in coordinates and show them
 - Points are selectable
 - Unload map tiles that aren't in the viewport
 - Distance and direction between waypoints
 - List of waypoints
 - Named waypoints
 - Goal waypoint
 
## Video Feeds
 - Separate windows
 - Always on top option
 
## Required Libraries
 - GStreamer must be installed for video feeds to work (see UserInterface readme for required .dll files)
