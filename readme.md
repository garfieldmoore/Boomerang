Boomerang
---------
---------

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

Register response to a single address;

    Boomerang.Server(5100)
		.Get("/myaddress").Returns("my response body", 200);

A request on any base address on the relative path '/myaddress' will now return 'my response body' and a status code of OK

Register response to multiple addresses;

    Boomerang.Server(5100)
		.Get("anaddress").Returns("response body", 200)
		.Get("anotheraddress").Returns("another response body", 201);

Register multiple responses for a single address;

You can specify different responses from the same address;

    Boomerang.Server(5100)
		.Get("anaddress").Returns("response body", 201)
		.Get("anaddress").Returns("another response body", 200);


Aknowledgments
--------------
I took inspiration from mimic - https://github.com/lukeredpath/mimic

