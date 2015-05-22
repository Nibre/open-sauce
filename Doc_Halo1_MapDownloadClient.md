# Map Download System : Client #

## Introduction ##
The map download system implemented in OpenSauce for Halo1 CE from version 3.1.0 allows players to download maps they do not have in-game with minimal interaction.

## How to use ##
To try and download a map you simply need to try and join a server running a map you do not have. If the server is serving the map the game will begin to download the map automatically from that server. If the server is not serving the map, it will see if it can be downloaded from one of a list of master servers kindly donated by people in the community.

The download will take a variable amount of time, depending on the size of the map and the speed of the client and servers connections. Once complete the map will be extracted into your maps directory and added to Halo's map list; after which the game will wait a few seconds before attempting to rejoin the server.

Should the server switch to a map you do not have, you will be disconnected from the server, the map will be downloaded and then the game will try to reconnect to the server. In this situation the server will treat this as you leaving the game and then reconnecting.

## Things to note ##
  * Due to the variable connection speeds people have it is quite possible that whilst downloading a map the server may switch to another map that you do not have. When this happens your current download will continue and complete as normal, the game will attempt to connect to the server, and a new download process will begin.
  * There is currently no way to know what version of a map the server is running. The map to download is selected using its name only. This means there will be occasions where a server is running a different version of a map than is available on the master servers. The game will still download the map but you will be unable to join the game as your map will not not match the one on the server. In this situation you will have to get the map the server is running manually.
  * If you try to download a map from a password protected server, the map may have been encrypted using the server password. If you try to download the map with an incorrect password your download will fail after downloading the first part. If this happens to you, check that you entered the password correctly when joining the server. If you don't know the correct password, then naughty naughty stop that you bad bad man. If you are certain your password is correct and the download is still failing, it may be that the server password was changed but the map was not re-encrypted to match it, in which case inform the server host to update their map server files.