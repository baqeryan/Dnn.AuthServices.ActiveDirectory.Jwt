# Dnn.AuthServices.Jwt
This repository is build on the [default DNN JWT Auth service (https://github.com/dnnsoftware/Dnn.Platform/tree/development/DNN%20Platform/Dnn.AuthServices.Jwt).
The only thing that is changed here is the way login credentials is passed to the `JwtAuth/API/mobile/login` endpoint.

Instead of passing the plain login credentials in the request body(used by DNN JWT Auth service), Basic Authentication technique is used here to pass the encoded login credentials in the request header.

## How to use this
If you need to use this modified version of DNN JWT Auth service, First install the default DNN JWT Auth service by following
[these steps](http://www.dnnsoftware.com/docs/administrators/jwt/jwt-user-credentials.html) then move the `Dnn.AuthServices.ActiveDirectory.Jwt.dll` to `bin` with the dll file that you will get from the build of this repo.

in web.config replace
```
<add name="JWTAuth" type="Dnn.AuthServices.ActiveDirectory.Jwt.Auth.JwtAuthMessageHandler, Dnn.AuthServices.ActiveDirectory.Jwt" enabled="true" defaultInclude="false" forceSSL="true" />
```
from
```
<add name="JWTAuth" type="Dnn.AuthServices.ActiveDirectory.Jwt.Auth.JwtAuthMessageHandler, Dnn.AuthServices.ActiveDirectory.Jwt" enabled="true" defaultInclude="false" forceSSL="true" />
```
add new key into `<appSettings>`
```
<add key="ActiveDirectory.Jwt.DomainName" value="MyDomainName" />
```
Sample Login request: 

```
   POST http://testsitece.lvh.me/DesktopModules/JwtAuth/API/mobile/login HTTP/1.1
   Host: testsitece.lvh.me
   Authorization: Basic aG9zdDpwYXNzd29yZA==
```
                
`aG9zdDpwYXNzd29yZA==` this is the encoded form of username and password seperated by colon(:)

`
encodedValue = window.btoa("host:password"); //aG9zdDpwYXNzd29yZA==
`
