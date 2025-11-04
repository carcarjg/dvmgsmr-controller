// %%%%%%    @%%%%%@
//%%%%%%%%   %%%%%%%@
//@%%%%%%%@  %%%%%%%%%        @@      @@  @@@      @@@ @@@     @@@ @@@@@@@@@@   @@@@@@@@@
//%%%%%%%%@ @%%%%%%%%       @@@@@   @@@@ @@@@@   @@@@ @@@@   @@@@ @@@@@@@@@@@@@@@@@@@@@@@ @@@@
// @%%%%%%%%  %%%%%%%%%      @@@@@@  @@@@  @@@@  @@@@   @@@@@@@@@     @@@@    @@@@         @@@@
//  %%%%%%%%%  %%%%%%%%@     @@@@@@@ @@@@   @@@@@@@@     @@@@@@       @@@@    @@@@@@@@@@@  @@@@
//   %%%%%%%%@  %%%%%%%%%    @@@@@@@@@@@@     @@@@        @@@@@       @@@@    @@@@@@@@@@@  @@@@
//    %%%%%%%%@ @%%%%%%%%    @@@@ @@@@@@@     @@@@      @@@@@@@@      @@@@    @@@@         @@@@
//    @%%%%%%%%% @%%%%%%%%   @@@@   @@@@@     @@@@     @@@@@ @@@@@    @@@@    @@@@@@@@@@@@ @@@@@@@@@@
//     @%%%%%%%%  %%%%%%%%@  @@@@    @@@@     @@@@    @@@@     @@@@   @@@@    @@@@@@@@@@@@ @@@@@@@@@@@
//      %%%%%%%%@ @%%%%%%%%
//      @%%%%%%%%  @%%%%%%%%
//       %%%%%%%%   %%%%%%%@
//         %%%%%      %%%%
//
// (C) Nyx Gallini 2025
//

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

		internal static bool TXR;

		internal static bool TXSR;

		internal static bool RXC = false;

		internal static bool TXC = false;

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
				string schs = status.ChannelName.Substring(0, 4);
				if (schs == "ID: ")
				{
					CID = status.ChannelName.Substring(4);
				}

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
					if (client.CurrentStatus.State == RadioState.Receiving)
					{
						RXC = true;
					}
					else if (RXC == true)
					{
						RXC = false;
					}

					if (TXR == true)
					{
						//TXRequested
						client.StartTransmit();
						TXR = false;
					}
					if (TXSR == true)
					{
						client.StopTransmit();
						TXSR = false;
					}

					await Task.Delay(100);
				}
			}
		}
	}
}