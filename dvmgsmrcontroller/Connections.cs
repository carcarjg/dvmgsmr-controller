using RC2ClientLibrary;

namespace dvmgsmrcontroller
{
	internal class Connections
	{
		internal static bool WSC;

		internal static bool RTCC;

		internal static bool RTTX;

		internal static string CCH = "";

		internal static string CID = "";

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
				/*
				Console.WriteLine($"Status: {status.Name}");
				Console.WriteLine($"  Zone: {status.ZoneName}");
				Console.WriteLine($"  Channel: {status.ChannelName}");
				Console.WriteLine($"  State: {status.State}");
				Console.WriteLine($"  Caller: {status.CallerId}"); */

				CID = status.CallerId;
				CCH = status.ChannelName;
				WSC = client.IsWebSocketConnected;
				RTCC = client.IsWebRtcConnected;
				RTTX = client.IsTransmitting;

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
					await Task.Delay(100);
				}
			}
		}
	}
}