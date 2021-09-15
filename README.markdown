# Microsoft.AspNetCore.Authentication.OpenIdConnect and Microsoft.Identity.Web for authentication via Azure AD B2C Project

This project uses Microsoft Identity Web, OpenID Connect, and Azure AD B2C to register and authenticate users who can then access endpoints on a secured Api. After succcesful authentication the access JSON Web Token (JWT) provided by the Azure AD B2C scope is passed as an authorization header to any consumed Api calls.

## Azure AD B2C Tenant

This project requires the settings of an Azure AD B2C tenant with a user flow, two registered applications, and scopes.

## How to Use

Restore any necessary NuGet packages before building or deploying. Ensure that the settings in the appsettings.json are changed to point to match the settings of a provisioned Azure AD B2C tenant and associated endpoints. 

Open two instances of Visual Studio. With one instance select the ApplicationCoreIdentityAzureADB2C.Api project then build and run to start the Api on the default binding of https://localhost:44332/

In the other instance select the ApplicationCoreIdentityAzureADB2C.Web project then build and run to start the MVC application on the default binding of https://localhost:5000 

## Known Issues

In some cases the secured cookie used to persist authentication might need to be manually deleted before debugging the web project.

The Api has one call that assumes that the User's Object ID and Email Addresses are being returned to the application claims from the selected identity providers. 

## Copyright and Ownership

All terms used are copyright to their original authors.

