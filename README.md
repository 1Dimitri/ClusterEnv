# ClusterEnv
When running on a Windows Failover cluster node, this tool  runs a command under a resource name as computername if the application uses the standard APIs.


## Usage
```
clusterenv CLUSTERRESMQ -- powershell -command "ls Env:"
```
```
clusterenv  RES2 mmc compmgmt.msc
```
Usage scenarios include [troubleshooting MSMQ in clustered environments](https://docs.microsoft.com/en-us/archive/blogs/johnbreakwell/clustering-msmq-applications-rule-1).


## Notes
As mentioned in the [GetHostName](https://docs.microsoft.com/en-us/windows/win32/api/winsock/nf-winsock-gethostname) documentation, when you are running on a cluster, you can trick the application into using the clustered name resource as the computer name instead of using the node's name.
Please note that the Microsoft documentation at this stage is inaccurate and that the environment variable should have a leading and trailing underscore as you can check with the hostname command.

