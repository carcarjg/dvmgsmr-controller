using SIPSorcery.Media;
using SIPSorcery.Net;
using WebSocketSharp;
using Windows.Media.Playback;

namespace dvmgsmrcontroller
{
	internal class Connections
	{
		internal int WEBSOCKET_PORT { get; set; }

		internal string WEBSOCKET_URL { get; set; }

		internal WebSocket WS { get; set; }

		internal void Start()
		{
			if (WEBSOCKET_URL != null)
			{
				if (WEBSOCKET_PORT != 0)
				{
					WS = new WebSocket("ws://" + WEBSOCKET_URL + WEBSOCKET_PORT);

					//ws://URL:PORT/rtc for RTC
					WS.OnMessage += OnMessage;
				}
			}
		}

		internal void Stop()
		{ }

		internal void OnMessage(object? sender, MessageEventArgs e)
		{
		}
	}
}