# IX Console template 

**IMPORTANT!!! When you create the project from Visual Studio, you will need to run `install.ps1` manually to finish the creation of the project.**



## Setting up the connection

### .NET


Go to [Entry.cs](ixconsole.twin/Entry.cs) and setup the following parameters

~~~C#
private const string TargetIp = "192.168.0.1"; // <- replace by your IP 
private const string UserName = "Everybody"; //<- replate by user name you have set up in your WebAPI settings
private const string Pass = ""; // <- Pass in the password that you have set up for the user. NOT AS PLAIN TEXT! Use user secrets instead.
private const bool IgnoreSslErrors = true; // <- When you have your certificates in order set this to false.
~~~

You will need to use TIA Portal to enable WebAPI interface [see here](https://console.simatic-ax.siemens.io/docs/hwld/PlcWebServer) and [here](https://youtu.be/d9EX2FixY1A?t=151) is a very informative youtube video.


### AX

Go to [apax.yml](ax/apax.yml) file and adjust the parameters

~~~yml
.
.
.
scripts:
  download :   
    # Here you will need to set the argumen -t to your plc OP and -i to platfrom you are dowloading to
    # --default-server-interface is a must if you are using WebAPI
    - apax sld --accept-security-disclaimer -t 192.168.0.1 -i .\\bin\\1500\\ -r --default-server-interface
.
.
.
.
~~~


> **NOTE: Security is set to a minimal level for a speedy start. Make sure you set the security appropriately**.


## Resources

Documentation sources: https://github.com/ix-ax/ix/tree/dev/docs
