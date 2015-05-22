# Map Download System : Master Server #

  * [Introduction](Doc_Halo1_MapDownloadMasterServer#Introduction.md)
  * [Infrastructure](Doc_Halo1_MapDownloadMasterServer#Infrastructure.md)
  * [The Server Application](Doc_Halo1_MapDownloadMasterServer#The_Server_Application.md)
    * [Overview](Doc_Halo1_MapDownloadMasterServer#Overview.md)
    * [Setup](Doc_Halo1_MapDownloadMasterServer#Setup.md)
      * [Database](Doc_Halo1_MapDownloadMasterServer#Database.md)
      * [Config](Doc_Halo1_MapDownloadMasterServer#Config.md)
      * [Scripts](Doc_Halo1_MapDownloadMasterServer#Scripts.md)
  * [Administration](Doc_Halo1_MapDownloadMasterServer#Administration.md)
    * [Adding Users](Doc_Halo1_MapDownloadMasterServer#Adding_Users.md)
    * [Editing Users](Doc_Halo1_MapDownloadMasterServer#Editing_Users.md)
    * [Removing Users](Doc_Halo1_MapDownloadMasterServer#Removing_Users.md)
    * [Changing Password](Doc_Halo1_MapDownloadMasterServer#Changing_Password.md)
    * [Adding Maps](Doc_Halo1_MapDownloadMasterServer#Adding_Maps.md)
    * [Deleting Entries](Doc_Halo1_MapDownloadMasterServer#Deleting_Entries.md)
  * [Getting Added](Doc_Halo1_MapDownloadMasterServer#Getting_Added.md)

## Introduction ##
The master server exists so that a normal web server with PHP and MySQL can be used as a map download server.

When the client needs to download a map it will first try to download it from the dedicated server it is connecting to. If the dedicated server is not set up to serve map downloads the client will then attempt to download the map from a list of "Master Servers". It is those servers that this document discusses.

The term "Master Server" has no real significance, they are just called that because they serve the entire userbase rather than individual multiplayer servers.

**IMPORTANT: I (TheFieryScythe) am no expert of MySQL, PHP, web security, etc. so you use this software at your own risk. I'm happy to hear of any problems, suggestions, improvements, etc. that you have so feel free to contact me of leave an issue on the google code page if it's problem you are having.**

## Infrastructure ##
The master server infrastructure relies upon website hosts being willing to volunteer some of their bandwidth to help make Halo Custom Edition more accessible to new players and encourage servers to run custom maps, which is the ultimate goal for the map download system.

If you want to be awesome by helping out in this, and you have a website with a decent amount of bandwidth to spare, you can do so by running a master server on your website.

**IMPORTANT: If you wish to host a master server you should expect to be doing so on a paid web hosting package. Also, check that your chosen web host's terms of service are appropriate for file serving of this kind.**

As a baseline we would like master servers to serve at least 5 of the more popular maps. Quality maps are preferred, and no duplicates or derivative works (such as chaosgulch\_CMT\_7 and such). Hosting a map that is already hosted on another master server is fine as the client will randomly choose which server to download from which helps to distribute the bandwidth cost.

## The Server Application ##
### Overview ###
The master server application is a collection of PHP scripts that provide the same functionality as the OS dedi with some management scripts thrown in for good measure.

### Setup ###
NOTE: I will assume you already have your PHP and MySQL set up as information to do that is beyond the scope of this document.

The first thing you will need to do is setup the application on your web server.

All the files you need are in the ` *OS Source Dir*\OpenSauce\Halo1\Halo1_CE_MapServer ` folder. Which you get when you sync/download the OpenSauce source code.

In the Halo1\_CE\_MapServer folder you will see two files and one folder:
  * config.ini
  * Halo1\_CE\_MapServer.sql
  * web\_root

#### Database ####
The Halo1\_CE\_MapServer.sql file defines the SQL map database that will be used to store information about the map's and their parts. You need to import it into your MySQL database. Once imported you will have a database called Halo1\_CE\_MapServer with three tables, map\_list, map\_part\_list and map\_server\_users.

_map\_list_
| Field                 | Type   | Description |
|:----------------------|:-------|:------------|
| file\_id               | int    | Auto incrementing index for the map entries  |
| map\_name              | string | The maps name without its extension |
| map\_compression\_stage | int    | Enum stating where the map processing got to in the event of a failure |
| map\_compression\_type  | int    | Enums stating what compression was used on the map |
| map\_md5               | string | The MD5 checksum of the map |
| map\_uncompressed\_size | string | The size of the map before being compressed |
| map\_compressed\_size | string | The size of the map after being compressed |
| map\_parts\_path        | string | URL to the maps parts, relative to the root URL |
| map\_part\_count        | int    | The number of parts the map has |

_map\_part\_list_
| map\_file\_id | int | ID of the map this part is associated with |
|:--------------|:----|:-------------------------------------------|
| part\_id      | int | The parts index within the maps parts      |
| part\_name    | string | Name of the part including its extension   |
| part\_md5     | string | The parts MD5 checksum                     |
| part\_size    | string | The size of the part                       |

_map\_server\_users_
| index | int | Auto incrementing user index |
|:------|:----|:-----------------------------|
| username | string | The accounts username        |
| password\_hash | string | A hash of the accounts password |
| user\_control\_permissions | int | Bitmask defining what controls this account has over the user database |
| map\_database\_permissions | int | Bitmask defining what controls this account has over the map database |

#### Config ####
You need to copy the config.ini file to somewhere on the web host that CANNOT be accessed through the website. You then need to edit it to fill in your custom configuration parameters.

_map\_database_
| Parameter | Usage |
|:----------|:------|
| username\_readonly | The username of the readonly MySQL account to use when reading from the map database |
| password\_readonly | The password for the readonly MySQL account |
| username  | The username of the MySQL account to use when reading from and writing to the map database |
| password  | The password for the MySQL account |
| data\_source\_name | The MySQL database location and name, e.g. "mysql:host=localhost;dbname=Halo1\_CE\_MapServer" |

_map\_server_
| Parameter | Usage |
|:----------|:------|
| map\_dir  | The absolute directory on the server that maps/definitions are saved to before being processed |
| map\_compressed\_dir | Location to use as an intermediate directory when compressing maps |
| map\_parts\_path | The folder to save map parts to |
| bandwidth\_cap\_soft | The daily bandwidth limit at which point maps stop being served |
| bandwidth\_cap\_hard | The daily bandwidth limit at which all data stops being served |

The two bandwidth cap values control the daily bandwidth limit for your server. The persistent data for this is saved to the state.ini file defined by you in server\_state.php.

The soft cap is the point at which map part definitions stop being served. It is called a soft cap because in the case of a map part definition being served just before the cap is reached, the user will be still able to continue to download the maps parts, and other downloads in progress will not be interrupted.

The hard limit is the opposite as once that cap is reached all following download requests will be rejected.

You should leave a reasonable margin between the soft and hard caps so that once the soft cap is reached there is enough bandwidth for any map downloads in progress to be completed. Your ideal margin will vary depending on the size of the maps you are serving and the amount of traffic you get.

If you do not want to use the bandwidth caps, set them to 0 and your bandwidth will be used without limits.

#### Scripts ####
To get the map server on your web host you need to copy the following into the directory on your web server that you want to use for map downloads:
```
web_root/
  hce_map_server/
    css/
      mainstyle.css
    map_database/
      map_entry_add.php
    server_admin.php
    server_login.php
  map_download.php
```

You will also need to copy the web\_root/hce\_map\_server/admin folder to somewhere on your web server outside of the web directory. For convenience this could be in the same folder as your config.ini.

Once your files are on your server you need to edit a number of files to set your servers file paths:

  * ` *admin folder*\common\config.php ` - Add the absolute path to your config.ini to the $config\_path variable on line 52.
  * ` *admin folder*\map_download\server_state.php ` - Add the absolute path to where your state.ini should be saved to the $state\_path variable on line 42.
  * ` *web directory*\map_download.php ` - Set the absolute path to the admin folder to set\_include\_path on line 11.
  * ` *web directory*\hce_map_server\server_login.php ` - Set the absolute path to the admin folder to set\_include\_path on line 11.
  * ` *web directory*\hce_map_server\server_admin.php ` - Set the absolute path to the admin folder to set\_include\_path on line 11.
  * ` *web directory*\hce_map_server\map_database\map_entry_add.php ` - Set the absolute path to the admin folder to set\_include\_path on line 11.

#### Complete ####
With your config.ini setup, your database imported and your scripts in place your map server is ready to have maps added.

### Administration ###
` *web directory*\hce_map_server\server_admin.php ` is the main administration script and controls the user accounts and map database.

To load the admin script simply enter the URL to your server\_admin.php in any browser.

The map server has a user account system for allowing access to the admin pages and controlling who can change what. When you imported the map servers sql database a default user account with full permissions was added so that you can get initial access to the admin pages.

| Username | admin |
|:---------|:------|
| Password | admin |

**IMPORTANT: You should create your own account as soon as you can and delete the admin account.**

**IMPORTANT: It is recommended that you use an encrypted connection when logging in and using the server administration pages.**

#### Adding Users ####
To add a user go to the User Control page and select Add User. Enter a username, select what permissions the user should have and select Add User.

NOTE: If you are creating an account to replace the admin account, give yourself full permissions.

A new user account will be added to the database. When a new user is added a random 8 character password will be generated for the account. This is only displayed to you once, so be sure to copy it and give it to the account owner. The account owner should then log in to their account and change their password using the My Account page.

#### Editing Users ####
To edit a users permissions select Edit User on the User Control page, change the permissions as necessary and select Save Changes. The changes will take effect the next time that users signs in.

NOTE: You cannot edit the currently signed in user account.

#### Removing Users ####
Should you want to remove a user, select Remove User on the User Control page. You will be asked to confirm or cancel the removal and the user will be deleted if you select Confirm.

NOTE: You cannot remove the currently signed in user account.

#### Changing Password ####
To change your password, go to the My Account page, fill in the current, new and repeated password fields and select Save Changes. If all fields are correct your password will be changed immediately.

NOTE: Only the account owner can change their password. If a user forgets their password, their account will have to be deleted and re-made.

#### Adding Maps ####
You have a couple of avenues for adding maps to the map server. For both you will need FTP access to the maps folder on your server (The map\_dir defined in your config.ini).

Your first option is to upload the map file directly to that folder. Once uploaded go to the Map Database page and you will see your map added to the "Map Files" table. Because the map was uploaded as-is it needs to be processed (compressed and split) to be made available to download. The server admin scripts do this when you select the "Add Entry" button.

Once selected the map is compressed using the Zip algorithm, split into 1MB chunks and copied to the map parts directory. Information about the map and its parts is added to the map database and made immediately available to clients.

**IMPORTANT: The Zip compression occurs on the server as part of a PHP script. This will take some time to complete and will also consume significant CPU resources, so make sure your web hosting package is suitable for the job.**

The second option available to you is to process the map yourself locally, and upload the map parts and definition to the server. This is the preferred method as 7zip compression is used by OpenSauceIDE which results in smaller file sizes and doesn't put any load on the webserver.

NOTE: The process for compressing a map can be found [here](Doc_Halo1_MapDownloadHost#Preparing_your_maps.md).

To upload the map data put the maps part definition and parts in the maps folder on your server, then go to the Map Database page. The map will appear in the list and you can select Add Entry to add it to the database. The definition will be loaded and it's information added to the database. The parts will be copied to the map parts directory under the web root.

Once your map has been added successfully, you can select Delete File(s) to remove it from the maps directory on the web server, or you can delete it via FTP.

#### Deleting Entries ####
If you want to remove a map from your map server and therefore stop people from downloading it you simply need to select Remove Entry on the map database table.

### Getting Added ###
When the client is loaded it downloads an XML from Halomods that lists the master servers that are available.  In order for your server to be used it needs to be on that list.

Once your server is set up you can ask Kornman or myself (TheFieryScythe) to add you to the pool. We need to know the following to process your request:
  1. The URL to the map\_download.php on your server e.g ` http://os.halomods.com/Halo1/CE/MapDownload/map_download.php `
  1. The initial set of maps you will be serving so we can test your server and check it is functioning correctly
  1. Contact details so that we can inform you of any problems that arise
  1. A string of text max. 63 characters long that will appear in-game as your servers name/title when your server is being used, which could be your websites address for instance.
  1. A string of text max. 255 characters long on a maximum of 3 lines that is displayed in-game when your server is used, could be something like "Please visit our site" for instance.

We will test your server to check the map downloads are working correctly and then we will add it to the server list.

NOTE: We reserve the right to remove your server from the pool at any time, though we would try to let you know if we do.

You will be free to add maps to your server without informing us, however we may request that you stop serving certain maps if there is a version conflict with other servers.