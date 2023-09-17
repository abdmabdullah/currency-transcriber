# Currency Transcriber
Enter an amount in numeric dollars and receive a word representation of the said amount.

## Project setup
- The UI is created in **WPF** and the backend is written in a **.NET 6 Web API**.
- There's an endpoint called _GetTranscribedCurrency_ that takes a string input through a query string, and returns a string output as content of the response.

## Running the project
- Before running the project, ensure that you have the following dependencies installed on your machine:
  - Visual Studio
  - Components for WPF
  - .NET Runtime
- Once you have all the dependencies, you can perform the following steps to run the project:
  - Clone the github repository.
  - One by one, right click the projects _CurrencyTranscriberApi_ and _CurrencyTranscriberClient_ and click _Restore Nuget Packages_.
  - Right click the _CurrencyTranscriber_ solution file and go to _Configure Startup Projects_. Select both projects to _Start_.
  - Click the play button on Visual Studio or press _F5_ if using Windows.
  - Once both projects are loaded, follow the steps on the UI window to use the utility.

**NOTE: If you run the API project under a different port, please ensure to update the ApiUrl field in _CurrencyTranscriberClient -> Properties -> Settings.settings_**

Please feel free to reach out at **abdmabdullah@gmail.com** in case of any questions or concerns.
