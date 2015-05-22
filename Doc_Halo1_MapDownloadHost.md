# Map Download System : Dedicated Server #

  * [Introduction](Doc_Halo1_MapDownloadHost#Introduction.md)
  * [Your options](Doc_Halo1_MapDownloadHost#Your_options.md)
    * [Your OS Dedicated server & 3rd party file host](Doc_Halo1_MapDownloadHost#OS_Dedicated_server_&_3rd_party_file_host.md)
    * [OS Dedicated server & dedi serves everything](Doc_Halo1_MapDownloadHost#OS_Dedicated_server_&_dedi_serves_everything.md)
    * [Stock server & master server spokes](Doc_Halo1_MapDownloadHost#Stock_server_&_master_server_spokes.md)
  * [OS Dedicated server setup](Doc_Halo1_MapDownloadHost#OS_Dedicated_server_setup.md)
    * [Preparing your maps](Doc_Halo1_MapDownloadHost#Preparing_your_maps.md)
    * [Putting files in the right place](Doc_Halo1_MapDownloadHost#Putting_files_in_the_right_place.md)
      * [Map Part Definitions](Doc_Halo1_MapDownloadHost#Map_Part_Definitions.md)
      * [Map Parts : General](Doc_Halo1_MapDownloadHost#Map_Parts_:_General.md)
      * [Map Parts : 3rd party host](Doc_Halo1_MapDownloadHost#Map_Parts_:_3rd_party_host.md)
      * [Map Parts : OS Dedi Server](Doc_Halo1_MapDownloadHost#Map_Parts_:_OS_Dedi_Server.md)
  * [Server Setup](Doc_Halo1_MapDownloadHost#Server_Setup.md)
    * [Ports](Doc_Halo1_MapDownloadHost#Ports.md)
    * [HTTP Server](Doc_Halo1_MapDownloadHost#HTTP_Server.md)
    * [Init.txt](Doc_Halo1_MapDownloadHost#Init.txt.md)
## Introduction ##
The map server embedded into the OpenSauce dedicated server provides the backend to the map downloads system available to the OpenSauce userbase.

## Your options ##
So you want to provide map downloads to your players eh? Well there are a number of options available to you to do this. Because the system uses standard HTTP to communicate, it is very flexible with regards to where the data actually comes from.

### OS Dedicated server & 3rd party file host ###
This is the prefered setup where you use the OpenSauce dedicated server with map downloads enabled, and the map files themselves are hosted on a 3rd party file host such as amazon's web services. The advantages of using this setup are that you are in control of what maps are available on each of your servers and the dedi is serving a very small amount of data to each client as the actual map parts are downloaded from a different server. The downside of this setup is that you are unable to use other server apps to add functionality that is currently unavailable in the OS Dedi.

### OS Dedicated server & dedi serves everything ###
This may be a viable solution for people who have a server hosting package with enough bandwidth to serve the map files as well as host the multiplayer server. It is important to enabled throttling on the HTTP server if this solution is used, otherwise the HTTP server will use all available bandwidth to serve the files, which would adversely affect the multiplayer server. You can see how to throttle the bandwidth in the [HTTP server documentation](Doc_Halo1_HTTPServer.md). Due to requiring to throttle the server bandwidth this is potentially the slowest of the solutions.

### Stock server & master server spokes ###
This is a more complicated setup meant for people hosting multiple dedis who want a single, centralized map server that all their dedi's redirect to. This setup requires you to run a small HTTP server along side the stock/non-OS dedi which redirects all requests to a web host running the master server application. The upside of this is that it lets you run any dedicated server but still provide map downloads to players. The downside is that it requires you to have your own web server running the master server application, and the added complexity of having a HTTP server running along side the dedi, which many game server hosts will likely not allow.

## OS Dedicated server setup ##
### Preparing your maps ###
The map download system requires the maps to be compressed so that the amount of data the player has to download is kept to a minimum. To compress your maps you will need to install OpenSauceIDE from the latest OS installer.
  1. Start OpenSauceIDE.
  1. Open the Server Map Download Tool
  1. Set a location to save the map part definitions to. These are small XML files that contain information about the map and its parts.
  1. Set a location for the map parts to be saved to.
  1. Select compress map and open the map you want to compress.

The tool will take a short while to compress your map and save the required data. Repeat the process for all of your maps.

If you are going to run a password protected server you could encrypt your maps to prevent people from downloading them (if it is an unreleased map you wish to test for instance). To do this enable the encrypt map checkbox and enter the 8 character server password before compressing your maps.

### Putting files in the right place ###
#### Map Part Definitions ####
The location you selected for saving the map part definitions to should now have a number of XML files in it. One for each map you compressed.

These files need to be in a location the dedicated server has permission to access. A good location would be in a folder under the server path (typically where your init.txt resides).

#### Map Parts : General ####
In the location you selected for saving the map parts to you will find a folder for each map you compressed. In those folders you will find the compressed map in a split 7z archive.

Whether you are serving your maps from a 3rd part file host or through the OS dedi you must be aware of the map part folder names. In the map part definition there is an attribute which contains the fodler the

#### Map Parts : 3rd party host ####
To use a third party file host you will need to choose one that allows direct linking without any intermediate website with countdowns and such. I.e. the service must allow you to put the URL for a file in your browser and immediately download the requested file.

The host should ideally support folders to allow easy file management, however this is not an absolute requirement.

On your file host you would typically have a folder for all of the maps you are serving such as:

```
HCEDedi
    immure
        immure.7z.001
        immure.7z.002
        immure.7z.003
        etc...
    portent
        portent.7z.001
        portent.7z.002
        etc...
```

If you take a look at a map part definition created by OSIDE you will see an attribute called "host\_directory". This attribute contains the path to the maps parts relative to the root URL you use on the file host. For instance if your file host directry structure is something like:

```
    www.myfilehost.com/user/002339/HCEDedi/immure
    www.myfilehost.com/user/002339/HCEDedi/portent
    www.myfilehost.com/user/002339/HCEDedi/sneak
```

Then your root URL would be `www.myfilehost.com/user/002339/HCEDedi/` and the host\_directory attribute would just contain the maps name as that is the default name of the parts folders for each map. However if for instance you have two servers and your file host directory structure is like this:

```
    www.myfilehost.com/user/002339/HCEDedi/server_1/immure
    www.myfilehost.com/user/002339/HCEDedi/server_1/portent
    www.myfilehost.com/user/002339/HCEDedi/server_2/sneak
```

...then your root URL would still be `www.myfilehost.com/user/002339/HCEDedi/` but the map part definitions host\_directory attributes would have to have `server_1/immure` as that would be the path to the maps parts relative to the root URL.

#### Map Parts : OS Dedi Server ####
If you do not have access to a 3rd party file host for your files you have the option of letting the OS dedi serve the map parts itself. To do this you need to create a folder somewhere the OS dedi can access. That folder will serve as the web server root directory. You need to copy the map part folders into that directory so that they can be served by the web server. You can set up the folder structure in the same way as you would on a 3rd party host, however you need to be aware of what your web root will be.

Lets say your folder structure is something like this:
```
C:\
    OpenSauce
        OS_Dedi
            www
                HCEDedi
                    immure
                        immure.7z.001
```

In this example the "www" folder is going to be used as the web server root folder. As far as the client is concerned the URL to get to the first immure map part would be (with your servers IP) `http://127.0.0.1:2304/HCEDedi/immure/immure.7z.001`.
So, when self serving the map parts your root URL would be `http://127.0.0.1:2304/HCEDedi`.

The same rules for the host\_directory attribute in the map part definitions apply when self serving. They must be changed to be relative to the root URL.

### Server Setup ###
#### Ports ####
The HTTP server will always listen on the port one up from the multiplayer server, so if your multiplayer server is listening on 2302 the HTTP server will listen on 2303. You can set it to listen on additional ports, but that is the only one the Halo CE client implicitly knows about so it will use that to communicate with the map server.

Therefore, if you are planning to run multiple dedicated servers on the same PC you must ensure that their ports do not overlap, for example you would need them running on 2302, 2304, 2306, etc. You will also need to open the HTTP server ports to allow traffic in and out.

#### HTTP Server ####
The map download system isn't going to function without the HTTP server setup and running. there are details on how to do this in the [HTTP server documentation](Doc_Halo1_HTTPServer.md).

#### Init.txt ####
The dedicated server has a few new commands for controlling the map server.

  * sv\_mapdownload\_start\_server - Starts the map server.
  * sv\_mapdownload\_stop\_server - Stops the map server.
  * sv\_mapdownload\_set\_part\_definitions\_path - Sets the relative or absolute path to the map part definitions.
  * sv\_mapdownload\_set\_host - Sets the root URL where map parts are being served from.
  * sv\_mapdownload\_reload\_map\_part\_definitions - Reloads the map part definitions located in the definitions folder.

Your typical init.txt will have something like the following:
```
sv_name "Fire's OS Server"
sv_public "1"
sv_maxplayers 8

sv_mapcycle_timeout 1

sv_mapcycle_add "deltaruins" "team king"
sv_mapcycle_add "WartornCove" ctf
sv_mapcycle_add "immure" "Team Slayer"
sv_mapcycle_add "portent" "Slayer"

sv_log_enabled true
sv_log_rotation_threshold 1024

sv_httpserver_log_enable true
sv_httpserver_set_throttle *=1m
sv_httpserver_set_thread_count 3
sv_httpserver_start

sv_mapdownload_set_part_definitions_path map_part_definitions
sv_mapdownload_set_host https://www.myfilehost.com/HCEDedi/
sv_mapdownload_start_server

sv_mapcycle_begin
```

Your part definitions path is either an absolute path, eg.:
> ` C:\Halo Custom Edition\Dedi\PartDefinitions `
Or a path relative to the server path (typically where your init.txt is).

The host variable must be set to the root URL of where your map parts are being served from. For instance it might be on a 3rd party file host, like so:
> ` https://www.myfilehost.com/HCEDedi/ `
If you are serving the map files using the OS Dedi you will need to set this to your server's IP and HTTP port, for instance:
> ` http://22.33.44.55:2307/HCEDedi `
When the map server gets a part download request it builds the redirect URL in 3 parts:
> ` *Root URL*/*Host Directory*/*Part Filename* `
Which results in something like:
> ` https://www.myfilehost.com/HCEDedi/immure/immure.7z.001 `
That is why you must use a file host that allows direct linking, and also why you must keep the host\_directory attribute in your map part definitions up to date.

When you start the map server it will be initialized and start listening for map download requests from the HTTP server. When you stop it the map server will stop listening for map download requests and will be unloaded. You cannot start or stop the map server whilst a multiplayer game is in progress. This is to prevent it from causing performance issues for the players.

It is possible to add maps to the servers list by just adding the map part definition into your definitions folder and running `sv_mapdownload_reload_map_part_definitions`. The OS dedi will read all of the map part definitions again, listing them in the command window.