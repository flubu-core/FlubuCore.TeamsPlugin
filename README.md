# FlubuCore.Teams

[![NuGet](https://img.shields.io/nuget/v/FlubuCore.Teams.svg)](https://www.nuget.org/packages/FlubuCore.Teams/)
[![Gitter](https://img.shields.io/gitter/room/FlubuCore/Lobby.svg)](https://gitter.im/FlubuCore/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![License](https://img.shields.io/github/license/flubu-core/flubuCore.CakePlugin.svg)](https://github.com/flubu-core/FlubuCore.TeamsPlugin/blob/master/LICENSE)

FlubuCore.Teams is a FlubuCore plugin that adds Microsoft teams specific tasks.

Plugin adds teams tasks to FlubuCore ```ITaskContext``` interface: 

```C# 
context.Tasks().Teams().SendMessage("IncomingWebHookUrl", new TeamsMessage { Text = "Hello world" }).Execute(context);
```

Plugin adds Following tasks:

SendMessage - Sends a message via incoming teams webhook
