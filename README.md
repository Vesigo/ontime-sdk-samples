# OnTime Extension SDK Samples
This repository contains sample projects in both C# and VB.NET.  These sample projects illustrate common scenarios for which extensions may be created:

1. **Example Project 1**: This example illustrates basic use of the OnTime Extension SDK. The example initializes an independent extension and demonstrates a simple create/update operation on the Customers table of an OnTime account.
2. **Example Project 2**: This example illustrates the use of the SDK example in the context of the billing view on OnTime Management Suite. The example initializes a billing extension and demonstrates a simple CSV (comma separated value) export operation from the Invoices table of an OnTime account.
3. **Example Project 3**: This example illustrates the use of the SDK example in the context of the tracking view on OnTime Management Suite or OnTime Dispatch. The example initializes a tracking extension and demonstrates a simple CSV (comma separated value) export operation from the Tracking table of an OnTime account.

## Using the Sample Projects
These sample projects should be used along with the [OnTime Extensions SDK](https://www.ontime360.com/developer/) The SDK can also be downloaded through NuGet at https://www.nuget.org/packages/OnTime.Extension.SDK/.

Documentation for the use of these samples is available at the [OnTime 360 Developer Resources](https://www.ontime360.com/developer/) page.

To make compiling and packings these samples easier, try the [OnTime Extension Packager for Visual Studio](https://marketplace.visualstudio.com/items?itemName=vesigo-studios.ontime-extension-packager), available on the Visual Studio Marketplace.


## What are OnTime Extensions?

OnTime Extensions behave as plug-ins or add-ins within the [OnTime courier software](https://www.ontime360.com) desktop applications. These extensions are a great way extend the functionality of OnTime. The most typical scenario for an extension is to connect OnTime with another software system. Here are some examples:

* Connecting OnTime’s invoice and payment data with a specific accounting system
* Transferring data to and from an EDI based system
* Uploading order data in a specific format to a FTP server
* Importing or exporting data to or from external sources

Once installed, an extension appears as part of the OnTime desktop application in the form of a button in the ribbon or in a context menu.

## How to Create OnTime Extensions

The OnTime Extension SDK provides a powerful application programming interface (API) that allows a developer to control many of the basic functions of an OnTime account. This makes it possible to control how data is read, created, and updated.

Using this approach, a programmer can easily extend the functionality of the OnTime desktop applications (OnTime Management Suite and OnTime Dispatch). These extensions become part of the OnTime application and can offer deep integration with other software systems.
