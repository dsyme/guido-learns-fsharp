
# Learning F# through hands-on

In this exercise you'll learn some F#.

## Install pre-requisites

You'll need to install the following pre-requisites in order to build SAFE applications

* [.NET Core SDK](https://www.microsoft.com/net/download) 5.0 or higher
* [Node LTS](https://nodejs.org/en/download/)

You can use any editor, for this exercise we recommend
* [VS Code](https://code.visualstudio.com/)

## Starting the dojo

Before you run the project **for the first time only** you must install dotnet "local tools" with this command:

```bash
dotnet tool restore
```

To concurrently run the server and the client components in watch mode use the following command:

```bash
dotnet run
```

Then open `http://localhost:8080` in your browser.

The build project in root directory contains a couple of different build targets. You can specify them after `--` (target name is case-insensitive).

Then search for these to start completing the Dojo.

    Task 1
    Task 2
    Task 3
    Task 4

## Going further: running tests

To run concurrently server and client tests in watch mode (you can run this command in parallel to the previous one in new terminal):

```bash
dotnet run -- RunTests
```

Client tests are available under `http://localhost:8081` in your browser and server tests are running in watch mode in console.

## Going further: bundling your app

There is `Bundle` to package your app:

```bash
dotnet run -- Bundle
```
## Going further: deploying to Azure

This requires these prerequisites:
* [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli) 

First time run

    az login

Then set the name of your app in Build.fs:

```
    let web = webApp {
        name "feiew02"  // set the name of your app here
    ...
```

To deploy to Azure:

```bash
dotnet run -- Azure 
```

## Resources

* [Learning F#](https://dotnet.microsoft.com/learn/fsharp/)
* [SAFE documentation](https://safe-stack.github.io/docs/)
* [Fable](https://fable.io/docs/)
* [Elmish](https://elmish.github.io/elmish/)
