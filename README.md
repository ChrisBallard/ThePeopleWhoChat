TPWC is a simple chat engine based on top of ReST and RavenDB, and written in F#.

### Development Prerequisites
* Visual Studio 2012
* NuGet package manager v2.0 or greater (make sure you enable NuGet package restore on the solution)
* RavenDB [build 2261](http://hibernatingrhinos.com/builds/ravendb-stable/2261) - just unzip the files to a local directory

### Building TPWC
With the above set up, you should be able to launch in VS2012, and rebuild all. This will bring down the required NuGet packages (you may need to click Accept on some license agreements) and then build everything needed to run the service locally.

### Running TPWC
* Launch RavenDB server - simplest solution: run `RavenDir/Server/Raven.Server.exe` to host Raven in a basic command line application.
* Run `ProjectDir/ThePeopleWhoChat.AdminTool/bin/Debug/ThePeopleWhoChat.AdminTool.exe`
* Type `connectdb`
* This connects you directly to the database (not via the TPWC ReST service) and allows you to perform initial setup. This first time, it will prompt you to create a password for the `root` account.
* Type `login root` and retype the password in order to log in
* Type `help` and explore from there
