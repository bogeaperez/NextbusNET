NextbusNET
==========

NextbusNET is a little project that defines a client to access the Nextbus webservices.

For more information on the service itself refer to the Nextbus public feed documentation at:
http://www.nextbus.com/xmlFeedDocs/NextBusXMLFeed.pdf

Usage
-----

var client = new NextbusClient();

IEnumerable<Agency> agencies = client.GetAgencies();
