# LeaseWeb Microsoft .NET API

This is still just a basic setup of a Microsoft .NET library that can be used to query the LeaseWeb API to retrieve information about services hosted at LeaseWeb. Not all methods have been implemented yet and the code may not be bug free. Using the already existing code it should be easy for you to add the missing API calls though. Feel free to use it and adjust it to your liking. Let me know in case you find something which you believe could be improved. It uses the LeaseWeb API version 1 as documented at http://developer.leaseweb.com/document/api-documentation/

You can also pull this API in as a NuGet package by adding the following NuGet repository to Visual Studio:
http://nuget.koenzomers.nl/nuget

## Version History

August 7, 2017 - Version 1.1.0.0

- Updated Newtonsoft.JSON package to v10
- Fixed the /colocation/<serverid>/ips call as that was no longer valid on the LeaseWeb API
- Added sample App.sample.config files. Rename them to App.config and enter your LeaseWeb API key in it in order for the API to work.