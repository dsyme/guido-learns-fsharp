# Instructions

## Prologue: Build and Run

1. Clone this repo

1. Ensure that you have installed all the pre-requisites in `README.md`

1. If this is your first time doing this, run `dotnet tool restore` command in your terminal.

1. Start the server and client by navigating to the repository root in your terminal and run `dotnet run`. The first time you run this, it may take a few seconds - it needs to download all the NPM dependencies as well as compile and run both client and server.

1. Open the web page by navigating to http://localhost:8080 to see the app running

1. You should see a search entry box with a Submit button. Enter the Dutch postcode `1011` and hit Submit: you will see several empty spaces and some basic Location details on the postcode.

Also note the following:

1. When you run the application, the [dotnet watch](https://docs.microsoft.com/en-us/aspnet/core/tutorials/dotnet-watch) tool is also run which detects file changes and automatically recompiles and reloads the server-side application. For example, in `DataAccess.fs`, try temporarily changing the value of `Town` (line 15) to a string such as `"A Town"` instead of `postcode.Result.AdminDistrict`. The server  application will automatically restart, and the next time you make a query you will see your hard-coded text appear.

1. The front-end application also supports [hot module reloading](https://webpack.js.org/concepts/hot-module-replacement/). Try changing the text `"SAFE Dojo"` to something else in the `src/Client/Index.fs` file; save the file and see how the front-end automatically updates in the browser whilst still retaining the application state. There's no need to rebuild the application.

This method of rapid, iterative development is a powerful tool for SAFE apps.

## 1. Search for tasks and follow the instructions

Search for "Task" throughout the code and do them in sequence order. Each
describes the task to be performed.



