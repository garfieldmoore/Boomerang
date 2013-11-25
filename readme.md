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

Register response to a single address;

    Boomerang.Server(5100).Get("/myaddress").Returns("my response body", 200);

Register response to multiple addresses;

    Boomerang.Server(5100).Get("anaddress").Returns("response body", 200).Get("anotheraddress").Returns("another response body", 201);

This registers the responses from the preceding address.  The address is relative to localhost on port 5100 in the examples (http://localhost:5100/myaddress in the first example).

Aknowledgments
--------------
I took imspiration from mimic - https://github.com/lukeredpath/mimic

