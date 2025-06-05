# GitHubApiCall Console Application

Project Task URL: https://roadmap.sh/projects/github-activity

.NET 8.0 Console app solution for the GitHub Activity [challenge](https://roadmap.sh/projects/github-user-activity) from [roadmap.sh](https://roadmap.sh/).

GitHub Activity Console Application is a simple tool designed to help you fetch and display GitHub activities for any user. This application interacts with the GitHub API to retrieve and categorize recent events related to a user's repositories.

## Features
- **Get user's events** : Retrieve recent GitHub events for a given user.
- **Filter on user events** : Filter on specific user's GitHub event type.
  
  - PushEvent
  - IssuesEvent
  - WatchEvent

- **Get user's profile** : Retrieve user's GitHub profile information.
- **User-Friendly Interface**: Interface is styled with **Spectre.Console** to give clear prompts and error messages.

## Installation

To run this application, follow these steps:

1. Clone this repository:
```
git clone https://github.com/khartoum-cyber/GitHubApiCall.git
```
2. Navigate to the project directory:
```
cd GitHubApiCall
```
3. Restore dependencies:
```
dotnet restore
```
4. Build the project:
```
dotnet build
```
5. Run the application:
```
dotnet run
```

## Usage

After running the application, you will be greeted with a welcome message. You can then start entering commands.

**Available Commands**

- help: Prints available commands.
- get-events [username]: Fetches and displays recent GitHub activities for the specified username.
- get-profile [username]: Fetches and displays username's profile.
- clear: clears console window.
- exit: exits the application.

**Example Usage**
```
Enter Command: get-profile khartoum-cyber
────────────────────────────────────────── GitHub Profile for khartoum-cyber ───────────────────────────────────────────
    GitHub Profile
┌───────────┬─────────┐
│ Field     │ Value   │
├───────────┼─────────┤
│ Name      │ bartek0 │
│ Company   │ Eviden  │
│ Location  │ Poland  │
│ Followers │ 0       │
│ Following │ 0       │
└───────────┴─────────┘

Enter Command: exit
```
