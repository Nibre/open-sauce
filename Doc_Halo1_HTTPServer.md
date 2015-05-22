# OS Dedi HTTP Server #
## Index ##
  * [Introduction](Doc_Halo1_HTTPServer#Introduction.md)
  * [Setup](Doc_Halo1_HTTPServer#Setup.md)
  * [Logging](Doc_Halo1_HTTPServer#Logging.md)
  * [Banning](Doc_Halo1_HTTPServer#Banning.md)
## Introduction ##
The OpenSauce Halo1 CE dedicated server now has a HTTP server embedded in it, which makes custom communications between the client and server possible.

NOTE: This is unrelated to and not suitable for multiplayer game syncing, so don't be thinking that it is.

For now, this is the main addition that makes the Map Download system possible, but it also has the potential to be used for other things such as querying server stats, web based server administration and such.

The HTTP server itself is called mongoose, which is an open source HTTP server which you can read about on their [Google Code site](http://code.google.com/p/mongoose/).

## Setup ##
### Functions ###
The mongoose server has a large number of configuration options, but for OpenSauce only the relevant ones are available.

The server is setup using the following script functions:

| **Function** | **Arguments** | **Description** |
|:-------------|:--------------|:----------------|
| sv\_httpserver\_set\_thread\_count | short         | Sets the number of threads the HTTP server should start for handling requests. |
| sv\_httpserver\_set\_root | string        | If the server needs to serve files from disk this is set to the path that will be used as the root web directory. |
| sv\_httpserver\_set\_throttle | string        | This controls bandwidth restrictions on the server and has a specific format which is detailed below. |
| sv\_httpserver\_set\_ports | string        | Sets which ports to open and listen on. This has a specific formatting which is detailed below. |
| sv\_httpserver\_show\_config | N/A           | Displays the current HTTP server configuration. |
| sv\_httpserver\_start | N/A           | Starts the HTTP server. |
| sv\_httpserver\_stop | N/A           | Stops the HTTP server.|

#### Thread count ####
The default number of threads the server will create is 4. This should be be enough for most uses, but if you think you will be getting significant traffic or connections may be made for lengthy periods you may need to increase the thread count.

#### Web Root ####
If the server needs to server files on the disk you will need to set the web root to the path on your PC to serve files from.

**IMPORTANT: The path that you point to will be made available to anybody on the internet so do not point this to somewhere containing sensitive information, or anywhere that may compromise your security.**

#### Throttle ####
Since the HTTP server and multiplayer server are running side by side they will both be using the same internet connection. This can be an issue when the HTTP server gets a lot of traffic or has to serve large files as it will consume all the bandwidth it can to do so. When this happens, the people on your multiplayer server will experience lag and disconnections due to the reduced bandwidth available.

You can mitigate this problem significantly by throttling the HTTP server's bandwidth and therefore keeping enough bandwidth available for the multiplayer server.

The throttling that is available with mongoose very flexible, allowing individual throttling for certain URL's and subnets. The throttling parameters are set using a specially formatted string as described in the [mongoose manual](https://github.com/valenok/mongoose/wiki/Mongoose%20Manual):
> Throttle is a comma-separated list of key=value pairs, where key could be a '`*`' character (limit for all connections), a subnet in form x.x.x.x/mask (limit for a given subnet, for example 10.0.0.0/8), or an URI prefix pattern (limit for the set of URIs, for example /foo/`**`). The value is a floating-point number of bytes per second, optionally followed by a 'k' or 'm' character, meaning kilobytes and megabytes respectively. A limit of 0 means unlimited rate. The last matching rule wins. For example, "`*`=1k,10.0.0.0/8" means limit everybody to 1 kilobyte per second, but give people from 10/8 subnet unlimited speed.
NOTE: If the formatting you use is incorrect, the server may crash when a connection is attempted.

#### Ports ####
The HTTP server can listen for connections on any ports you wish, however it will always listen on one port up from the multiplayer server. This cannot and should not be changed as that is the only port the Halo CE client will implicitly know about.

The string format for defining which ports to listen on is detailed in the [mongoose manual](https://github.com/valenok/mongoose/wiki/Mongoose%20Manual):
> Comma-separated list of ports to listen on. If the port is SSL, a  letter 's' must be appended, for example, "80,443s" will open port 80 and port 443, and connections on port 443 will be SSL-ed. It is possible to specify an IP address to bind to. In this case, an IP address and a colon must be prepended to the port number. For example, to bind to a loopback interface on port 80 and to all interfaces on HTTPS port 443, use "127.0.0.1:80,443s".

NOTE: The HTTP server in the OS dedi does not currently support SSL so setting a port as being SSL enabled will have undefined results.

#### Start/Stop ####
You cannot change any of the server configuration parameters whilst the server is running so set up your options before start the server.

The server cannot be stopped whilst any HTTP services are running, so you need so stop such services as the map download server before stopping the HTTP server.

### Init.txt ###
Typical set up:
```
sv_httpserver_set_throttle "*=0,/map_parts=250k"
sv_httpserver_set_root "/www"
sv_httpserver_set_thread_count 6
sv_httpserver_start
```

## Logging ##
The HTTP server has the option to output logging information into the server log, which would write HTTP requests and responses to the log file. This may be useful to see if an IP is making an unnecessary number of requests for instance.

As this is a new type of entry into the server log it is possible that any existing programs made to parse server logs would be unable to read the HTTP entries, which is why it is optional.

To enable/disable HTTP logging use the following function:

| **Function** | Arguments | Description |
|:-------------|:----------|:------------|
| sv\_httpserver\_log\_enable | bool      | Sets whether to output HTTP logging to the server log. |

### Init.txt ###
Typical set up:
```
sv_httpserver_log_enable true
```

## Banning ##
The HTTP server has a banning system to try and mitigate the effect of someone "hammering" the server with requests.

The system has two types of ban. Connection bans and permanent bans.

With connection bans the system will allow IP's a limited number of requests with in a specific time frame, after which there is a cooldown period during which requests will be denied. The system records the number of requests made by each IP and the cooldown time defines how long it takes for that count to be decreased by one. So if an IP reaches the limit and the cooldown time elapses it will only be able to make one successful request before reaching its limit again. After a certain amount of inactivity the IP will be forgotten.

The system can also permanently ban IP's from making any requests.

The functions for controlling this are as follows:
| Function | Arguments | Description |
|:---------|:----------|:------------|
| sv\_httpserver\_set\_connection\_ban | long, real, real | Sets the connection ban parameters. |
| sv\_httpserver\_banlist | N/A       | Displays the current permanent ban list |
| sv\_httpserver\_banlist\_file | string    | Sets the suffix for the ban list file. |
| sv\_httpserver\_ban\_ip | bool, short x 8 | Adds an IP to the HTTP server ban list |
| sv\_httpserver\_unban\_ip | short     | Removes an IP from the banlist at the specified index |

**sv\_httpserver\_set\_connection\_ban arguments**
| Argument | Type | Description |
|:---------|:-----|:------------|
| max\_connections | long | The maximum number of requests an IP can make before being denied. |
| connection\_cooloff | real | The time to wait before decrementing the IP's connection count by one. |
| connection\_forget\_time | real | The time to wait before forgetting an inactive IP |

**sv\_httpserver\_ban\_ip arguments**
| Argument | Type | Description |
|:---------|:-----|:------------|
| ip\_version | bool | False for ipv4, true for ipv6 |
| a        | short | Part a of the IP |
| b        | short | Part b of the IP |
| c        | short | Part c of the IP |
| d        | short | Part d of the IP |
| e        | short | Part e of the IP |
| f        | short | Part f of the IP |
| g        | short | Part g of the IP |
| h        | short | Part h of the IP |

NOTE: IPv6 is not currently supported by OS or mongoose, but its best to be prepared.

### Init.txt ###
Typical set up:
```
sv_httpserver_banlist_file "-1"
sv_httpserver_set_connection_ban 30 1 60
```