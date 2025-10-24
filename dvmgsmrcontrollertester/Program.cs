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
using SerialPortLib;
using System.IO.Ports;
using System.Timers;
using HeadComLib;

namespace dvmgsmrcontrollertester
{
	internal class Program
	{
		private static System.Timers.Timer StatTimer;

		private static SerialPort _serialPort;

		private bool HeadhelthOk;

		private bool runonce;

		public static void Main(string[] args)
		{
			Program MP = new Program();

			StatTimer = new System.Timers.Timer();

			_serialPort = new SerialPort();
			_serialPort.PortName = "COM3";//Set your board COM
			_serialPort.BaudRate = 115200;
			_serialPort.Open();
			MP.Setupstatustimer();

			while (true)
			{
				string a = _serialPort.ReadExisting();
				if (a != null && a != "" && a != " ") { MP.CmdRx(a.Substring(0, 6)); }

				Thread.Sleep(200);
				if (MP.runonce == false)
				{
					MP.runonce = true;
					MP.CmdTx(CmdsOutbound.OOPHeadcode + "  77X7");
					Thread.Sleep(3000);
					MP.CmdTx(CmdsOutbound.OOPChan + "CM_RAIL");
					Thread.Sleep(5000);
					MP.CmdTx(CmdsOutbound.OOPRxCall);
					MP.CmdTx(CmdsOutbound.OOPRxRID + "900F");
					Thread.Sleep(10000);
					MP.CmdTx(CmdsOutbound.OOPNoRXCall);
					Thread.Sleep(2000);
					MP.CmdTx(CmdsOutbound.OOPTxMOde);
					Thread.Sleep(10000);
					MP.CmdTx(CmdsOutbound.OOPNoTXMOde);
					Thread.Sleep(10000);
					MP.CmdTx(CmdsOutbound.OOPdsdOn);
					Thread.Sleep(5000);
					MP.CmdTx(CmdsOutbound.OOPdsdOff);
				}
			}
		}

		private void Setupstatustimer()
		{
			CmdTx(CmdsOutbound.OOPControllerReady);
			StatTimer = new System.Timers.Timer(5000);
			StatTimer.Elapsed += StatusCheck;
			StatTimer.AutoReset = true;
			StatTimer.Enabled = true;
			Console.WriteLine("Setup Completed");
		}

		internal void CmdRx(string cmd)
		{
			switch (cmd)
			{
				case CmdsInbound.IOPnack:
					break;

				case CmdsInbound.IOPack:
					HeadhelthOk = true;
					break;

				case CmdsInbound.IOPHeadReady:
					CmdTx(CmdsOutbound.OOPControllerReady);
					break;

				case CmdsInbound.IOPb1:
					Console.WriteLine("Button1");
					break;

				case CmdsInbound.IOPb2:
					Console.WriteLine("Button2");
					break;

				case CmdsInbound.IOPb3:
					Console.WriteLine("Button3");
					break;

				case CmdsInbound.IOPb4:
					Console.WriteLine("Button4");
					break;

				case CmdsInbound.IOPb5:
					Console.WriteLine("Button5");
					break;

				case CmdsInbound.IOPb6:
					Console.WriteLine("Button6");
					break;

				case CmdsInbound.IOPb7:
					Console.WriteLine("Button7");
					break;

				case CmdsInbound.IOPb8:
					Console.WriteLine("Button8");
					break;

				case CmdsInbound.IOPb9:
					Console.WriteLine("Button9");
					break;

				case CmdsInbound.IOPb10:
					Console.WriteLine("Button10");
					break;

				case CmdsInbound.IOPb11:
					Console.WriteLine("Button11");
					break;

				case CmdsInbound.IOPb12:
					Console.WriteLine("Button12");
					break;

				case CmdsInbound.IOPb13:
					Console.WriteLine("Button13");
					break;

				case CmdsInbound.IOPb14:
					Console.WriteLine("Button14");
					break;

				case CmdsInbound.IOPb15:
					Console.WriteLine("Button15");
					break;

				case CmdsInbound.IOPb16:
					Console.WriteLine("Button16");
					break;
			}
		}

		internal void CmdTx(string cmd)
		{
			Console.WriteLine(cmd);
			_serialPort.WriteLine(cmd);
		}

		internal void StatusCheck(object sender, ElapsedEventArgs e)
		{
			HeadhelthOk = false;
			CmdTx(CmdsOutbound.OOPReqStatus);
			Thread.Sleep(500);
			if (HeadhelthOk == false) { Console.WriteLine("***Lost Head***"); }
			else { Console.WriteLine("Head Health Ok"); }
		}
	}
}