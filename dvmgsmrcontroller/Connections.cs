using SIPSorcery.Media;
using SIPSorcery.Net;
using WebSocketSharp;
using Windows.Media.Playback;
using Newtonsoft.Json;
using TinyJson;

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
					WS.OnOpen += OnConnect;
					WS.OnClose += OnClose;
					WS.OnError += OnError;
				}
			}
		}

		private void OnError(object? sender, WebSocketSharp.ErrorEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void OnClose(object? sender, CloseEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void OnConnect(object? sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		internal void Stop()
		{ }

		internal void OnMessage(object? sender, MessageEventArgs e)
		{
			var msg = e.Data;

			if (msg == null) { return; }

			Serilog.Log.Verbose($"Got client message from websocket: {msg}");
			dynamic jsonObj = JsonConvert.DeserializeObject(msg);

			if (jsonObj == null)
			{
				Serilog.Log.Logger.Warning("Unable to decode data from websocket!");
				return;
			}
		}
	}
}