# TinyOS .NET

<b>TinyOS .NET</b> is an advanced way to develop applications with .NET technology for Tiny OS.

## Prerequisites

**- Visual Studio 2022**
  * To create TinyOS .NET application with .NET 8, you need the latest version of [Visual Studio 2022](https://visualstudio.microsoft.com/)


**- .NET 8 SDK**
  * [Linux / Windows / macOS](https://dotnet.microsoft.com/download/dotnet/8.0)
  

**- TinyOS SDK**
  * [TinyOS Extensions for Visual Studio](https://marketplace.visualstudio.com/items?itemName=tinyos-extensions-visual-studio)

**- TinyOS .NET Workload**
  * [TinyOS .NET Workload]()

## Getting Started with CLI

#### 1. Check the TinyOS templates before creating a new TinyOS Project
You can see the TinyOS template as follows if it is properly installed.
```sh
dotnet new --list
Template Name                                 Short Name      Language    Tags                  
--------------------------------------------  --------------  ----------  ----------------------
Console Application                           console         [C#],F#,VB  Common/Console        
Class Library                                 classlib        [C#],F#,VB  Common/Library        
Worker Service                                worker          [C#],F#     Common/Worker/Web     
MSTest Test Project                           mstest          [C#],F#,VB  Test/MSTest           
NUnit 3 Test Item                             nunit-test      [C#],F#,VB  Test/NUnit            
NUnit 3 Test Project                          nunit           [C#],F#,VB  Test/NUnit            
xUnit Test Project                            xunit           [C#],F#,VB  Test/xUnit            
*TinyOS Application**                    *tinyos*        *[C#]*      *TinyOS/Console*
Razor Component                               razorcomponent  [C#]        Web/ASP.NET           
Razor Page                                    page            [C#]        Web/ASP.NET           
...

```  

#### 2. Creates a New Project
```sh
dotnet new tinyos -n HelloTinyOS
```
When the project is successfully created, the following files are created.
```sh
└── HelloTinyOS
    ├── HelloTinyOS.csproj
    ├── Main.cs
```

#### 3. Build the application
```sh
dotnet build 
```

```sh
Microsoft (R) Build Engine version 16.10.0-preview-21181-07+073022eb4 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

... 

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:04.83
```

#### 4. Deploy the application 
```sh
dotnet build HelloTinyOS/TinyOS.csproj
```