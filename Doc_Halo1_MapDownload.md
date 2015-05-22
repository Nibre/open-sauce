# Map Download Server : Overview #
## Introduction ##
The map download system works by having a HTTP server embedded into the OpenSauce dedicated server. The HTTP server is the conduit through which the client finds out whether a map is being served and where to get the map parts from.

## The Maps ##
The maps that are to be served are compressed and split into 1MB parts. When the map is split an XML file is generated that has information about the map and all of its parts, which is the first thing sent to the client so that it knows that a map can be downloaded, and also what it needs to download.

If a server is to be password protected you can encrypt your map so that only people who have the server password can download it. This can be useful when you want to test an unreleased map, but do not want the hassle of sending it to your testers manually.

## The Process ##
To download a map the client goes through a number of phases. They first attempt to connect to the multiplayer server, but because they do not have the current map they are disconnected. When this happens OS requests a file from the from the dedicated server using a URL such as the following:

`http://127.0.0.1:2303/map_download?map=immure`

If the dedi is serving the map it will respond with an xml file containing information about the map and its parts, such as:

```
<?xml version="1.0" encoding="UTF-8"?>
<osHTTPServer>
    <map_download algorithm="7zip" name="immure.map" md5="4549aa93a5cef6f6a3f4cbc71527bc5a" uncompressed_size="17718732" compressed_size="5349449">
        <part name="immure.7z.001" index="0" md5="e732fe285f16eeb60312bf497785b81f" size="1048576"/>
        <part name="immure.7z.002" index="1" md5="f580410ded399f4995c359f57e87929e" size="1048576"/>
        <part name="immure.7z.003" index="2" md5="916ad0c8ea9de6b3bfbbbb75cb329be8" size="1048576"/>
        <part name="immure.7z.004" index="3" md5="e2f729bea2b834a4a96109f4e7021650" size="1048576"/>
        <part name="immure.7z.005" index="4" md5="fd4f77d18bda23b4f1a70d5e60bd342e" size="1048576"/>
        <part name="immure.7z.006" index="5" md5="3d41c0633b4beeb125e51b53bcd8a266" size="106569"/>
    </map_download>
</osHTTPServer>
```

The client will parse the XML, and then start to download each part from the dedi. Each part is downloaded using a request such as the following:

`http://127.0.0.1:2303/map_download?map=immure&part=immure.7z.001`

When the map server receives such a request it will either redirect the client to a 3rd party file host, or serve the part itself. The client will download the part and check it's MD5 and if the MD5 does not match it will attempt to download the part again. If the map is encrypted the client will attempt to decrypt the part once downloaded using the server password. If the part is not decrypted successfully the download is failed.

The client repeats this process to download all of the maps parts. Then once all of the parts are downloaded they are combined into a single archive and the map is extracted. The extracted data is checked against the maps MD5 checksum and if it matches the map is saved to the users maps directory.

With the map now available in the maps directory it is CRC'd and added to Halo's map list. After a few seconds delay the client attempts to reconnect to the multiplayer server.