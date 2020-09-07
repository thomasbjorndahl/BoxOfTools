# BofOfTools

A (soon to be) collection of usefull tools

- **dbping**
_Tool used to ping a database_
- **ADMembers**
_Tool for checking AD membership_
- **PingTester**
_Tool for logging/testing network connections and routes_
- **GetVSConfiguration**
_Tool for simply get whether the active configuration is Debug/Release or something else_

## DBPING
How to use "dbping.exe"

Parameters:

- -c : The connection, example: -c "dbserver" or -c dbserver\instanvce
- -p : Pause after execution
- -r : Number of times to repeat on non connection
- -d : Database. (Initial catalog)
- -? : Show HELP page

## GetVSConfiguration
How to use "GetVSConfiguration.exe"

First (and only) parameter to this program, is a link to a folder where a *.csproj.user file can be found
