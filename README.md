
# Learning F# through hands-on

In this exercise you'll learn some F# through modifying and extending a web app (both client and server), and doing a little scripting.

## Install pre-requisites

You'll need to install the following pre-requisites in order to build SAFE applications

* [.NET Core SDK](https://www.microsoft.com/net/download) 5.0 or higher
* [Node LTS](https://nodejs.org/en/download/)

You can use any editor, for this exercise we recommend
* [VS Code](https://code.visualstudio.com/), plus
* [The F# Extension for VS Code](https://ionide.io/) called Ionide (you will be prompted automatically)

## Starting the dojo

Clone:

```bash
git clone https://github.com/dsyme/learn-fsharp
cd learn-fsharp
```

Before you run the project **for the first time only** you must install dotnet "local tools" with this command:

```bash
dotnet tool restore
```

Open the editor:

```bash
code .
```

Build and run in watch mode use the following command:

```bash
dotnet run
```

You may have to allow `dotnet` or `Server` access to your public and/or private network. 

Now open `http://localhost:8080` in your browser. Arrange the windows so you can see both Code editor and a reduced web browser.

## Completing the tasks

Search files (Ctrl+Shift+F or Edit --> Find in Files) and search through for "TASK 1" to start completing the Dojo.

You will see

    Task 1.1
    Task 1.2 

and so on. Each task is explained in in general they are independently solvable.



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
