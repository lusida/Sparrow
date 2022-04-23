using System;
using Sparrow.Wizard.Engine;
using Sparrow.Net.SignalR;

var client = new SignalRRemoteClient("https://localhost:5001");

var engine = client.GetService<IWizardEngine>();

var items = await engine.GetItemsAsync(CancellationToken.None);

Console.WriteLine(items);

Console.ReadKey();