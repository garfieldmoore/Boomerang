Boomerang
---------
---------

Boomerang is a framework for faking external web services in acceptance/integration tests.  

Getting started
---------------
To get started simply add the package to your application;

      nuget install-package boomerang

Now you can create a fake and define responses from requests;

Examples
--------

In the examples the address is relative to localhost on port 5100 in the examples (http://localhost:5100/myaddress in the first example).

Register response to a single address;

    Boomerang.Server(5100).Get("/myaddress").Returns("my response body", 200);

Register response to multiple addresses;

    Boomerang.Server(5100).Get("anaddress").Returns("response body", 200).Get("anotheraddress").Returns("another response body", 201);

Register multiple responses for a single address;

In this example, the first get to http://localhost:5100/anaddress will return HTTP status code CREATED whereas a second will retun OK
    Boomerang.Server(5100).Get("anaddress").Returns("response body", 201).Get("anaddress").Returns("another response body", 200);


Aknowledgments
--------------
I took inspiration from mimic - https://github.com/lukeredpath/mimic

