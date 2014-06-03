Boomerang
---------
---------
[![Build Status](http://teamcity.codebetter.com/app/rest/builds/buildType:%28id:bt1142%29/statusIcon)](http://teamcity.codebetter.com/viewType.html?buildTypeId=bt1142&guest=1)

Boomerang is a framework for faking external web services in acceptance/integration tests.  

Getting started
---------------
To get started simply add the package to your application;

      nuget install-package boomerang

Now you can create a fake and define responses from requests;

Features
--------
Supported verbs; GET, POST, PUT, DELETE

Examples
--------
The below examples use http://localhost:5100 as the base address for all requests;

Register a single response to a single address;

    Boomerang.Server(5100)
		.Get("/myaddress").Returns("my response body", 200);

A request on the relative path '/myaddress' will now return 'my response body' and a status code of OK.  Only the first request will return the configured response. Subsequent requets will respond with a 400 error.  A response will need to be configured for every expected HTTP request.

Register multiple responses to a single address;

    Boomerang.Server(5100)
		.Get("/myaddress").Returns("my response body", 200);
		.Get("/myaddress").Returns("not found", 404);

Register single response to multiple addresses;

You can specify different responses from the same address;

    Boomerang.Server(5100)
		.Get("anaddress").Returns("response body", 200)
		.Get("anotheraddress").Returns("another response body", 201);

Aknowledgments
--------------
I took inspiration from mimic - https://github.com/lukeredpath/mimic

