using RC2ClientLibrary;

namespace dvmgsmrcontroller
{
	internal class Connections
	{
		public bool stop = false;

		internal static async Task RC2(CancellationToken token, string RC2addr, int RC2port, int txaudio, int rxaudio)
		{
			// Create and configure client
			var client = new RC2Client(RC2addr, RC2port);
			client.SetMicrophoneDevice(txaudio);
			client.SetSpeakerDevice(rxaudio);

			// Subscribe to all events
			client.WebSocketConnected += (s, e) => Console.WriteLine("WS Connected");
			client.WebRtcConnected += (s, e) => Console.WriteLine("RTC Connected");
			client.StatusReceived += (s, status) =>
			{
				Console.WriteLine($"Status: {status.Name}");
				Console.WriteLine($"  Zone: {status.ZoneName}");
				Console.WriteLine($"  Channel: {status.ChannelName}");
				Console.WriteLine($"  State: {status.State}");
				Console.WriteLine($"  Caller: {status.CallerId}");

				//TODO: use above data to trigger stuff on the CH
			};
			client.AckReceived += (s, cmd) => Console.WriteLine($"✓ {cmd}");
			client.NackReceived += (s, cmd) => Console.WriteLine($"✗ {cmd}");
			client.ErrorOccurred += (s, err) => Console.WriteLine($"ERROR: {err}");

			// Connect
			if (await client.ConnectAsync())
			{
				Console.WriteLine("Connected successfully!\n");

				// Interactive command loop
				while (true)
				{
					if (token.IsCancellationRequested == true)
					{
						client.Disconnect();
					}
					/*
					client.QueryStatus();
					await Task.Delay(500);

					// Access stored data
					var status = client.CurrentStatus;
					Console.WriteLine($"Radio Name: {status.Name}");
					Console.WriteLine($"Current Channel: {status.ChannelName}");
					Console.WriteLine($"Current Zone: {status.ZoneName}");
					Console.WriteLine($"State: {status.State}");
					Console.WriteLine($"Is Transmitting: {client.IsTransmitting}");
					Console.WriteLine($"WebSocket Connected: {client.IsWebSocketConnected}");
					Console.WriteLine($"WebRTC Connected: {client.IsWebRtcConnected}");
					*/
					await Task.Delay(100);
				}
			}
		}
	}
}