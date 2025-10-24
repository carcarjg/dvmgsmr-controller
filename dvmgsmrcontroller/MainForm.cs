using System.ComponentModel.Design;
using System.IO.Ports;
using AudioSwitcher.AudioApi.CoreAudio;
using HeadComLib;
using RC2ClientLibrary;

namespace dvmgsmrcontroller
{
	public partial class MainForm : Form
	{
		private SerialPort _serialPort;

		private bool hconnectstatus;

		private string currentcmd;

		private string headcode;

		private string[] headcoderegproc = { "", "", "", "", "", "", "", "" };

		private string afftg;

		private static readonly CancellationTokenSource cts = new CancellationTokenSource();

		private int kp2press;

		private int kp3press;

		private int kp4press;

		private int kp5press;

		private int kp6press;

		private int kp7press;

		private int kp8press;

		private int kp9press;

		private int lastkppressreg;

		private CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;

		private int gsmrctry;

		private bool headstatus;

		private DateTime lasttimeack;

		private int counttoStart = 10;

		/// <summary>
		/// Regprocess
		/// Stage 0 - Not in progress
		/// Stage 1 - Code Entry
		/// Stage 2 - Waiting for ack from network
		/// Stage 3 - "Checking HeadCode"
		/// </summary>
		private int reginprocess;

		/// <summary>
		/// DeRegProcess
		/// Stage 0 - Not in progress
		/// Stage 1 - Confirm
		/// Stage 2 - Waiting for network services to stop
		/// </summary>
		private int deregprocess;

		private bool regstatus;

		public MainForm()
		{
			InitializeComponent();
			regstatus = false;
			reginprocess = 0;
			deregprocess = 0;
			headcode = string.Empty;
			afftg = string.Empty;
			string[] ports = SerialPort.GetPortNames();
			foreach (string port in ports)
			{
				serialportlistbox.Items.Add(port);
			}

			// Show available audio devices
			foreach (var dev in RC2Client.GetInputDevices())
			{
				txaudioCMBO.Items.Insert(dev.DeviceNumber, dev.Name);
			}

			foreach (var dev in RC2Client.GetOutputDevices())
			{
				rxaudioCMBO.Items.Insert(dev.DeviceNumber, dev.Name);
			}

			currentcmd = "hcnk";
			try
			{
				daemonaddrTXT.Text = Properties.Settings.Default.daemonaddr;
				daemonptTXT.Text = Properties.Settings.Default.daemonport;
				countryTXT.Text = Properties.Settings.Default.gsmrctry;

				serialportlistbox.SelectedIndex = -1;

				for (int index = 0; index < serialportlistbox.Items.Count; index++)
				{
					serialportlistbox.SelectedIndex = index;
					if (serialportlistbox.Text == Properties.Settings.Default.serialport)
					{
						serialportlistbox.SelectedIndex = index;
						break;
					}
				}
			}
			catch (Exception ex) { }
			autoconnectTMR.Enabled = true;
			autoconnectTMR.Start();
		}

		private void connectbutton_Click(object sender, EventArgs e)
		{
			_serialPort = new SerialPort();
			_serialPort.PortName = serialportlistbox.Text;//Set your board COM
			_serialPort.BaudRate = 115200;
			_serialPort.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);
			_serialPort.Open();
			connectbutton.Enabled = false;
			autoconnectTMR.Enabled = false;
			autoconnectTMR.Stop();
			_serialPort.WriteLine(CmdsOutbound.OOPControllerReady);
		}

		private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			string rxdata;
			rxdata = _serialPort.ReadExisting();
			Console.WriteLine(rxdata);
			string rxcmd = "";
			try { rxcmd = rxdata.Substring(0, 6); } catch (Exception) { }
			try { rxdata = rxdata.Substring(7, rxdata.Length); } catch (Exception) { }

			switch (rxcmd)
			{
				case CmdsInbound.IOPack:
					headstatus = true;
					lasttimeack = DateTime.Now;
					break;

				case CmdsInbound.IOPHeadReady:
					_serialPort.WriteLine(CmdsOutbound.OOPControllerReady);
					_serialPort.WriteLine(CmdsOutbound.OOPCountrySet + Properties.Settings.Default.gsmrctry);
					hconnectstatus = true;
					break;

				case CmdsInbound.IOPnack:
					break;

				case CmdsInbound.IOPb1:
					break;

				case CmdsInbound.IOPb2:
					break;

				case CmdsInbound.IOPb3:
					break;

				case CmdsInbound.IOPb4:
					break;

				case CmdsInbound.IOPb5:
					break;

				case CmdsInbound.IOPb6:
					break;

				case CmdsInbound.IOPb7:
					break;

				case CmdsInbound.IOPb8:
					volumedn();

					//Vol Dn
					break;

				case CmdsInbound.IOPb9:
					volumeup();

					//Vol Up
					break;

				case CmdsInbound.IOPb10:
					break;

				case CmdsInbound.IOPb11:
					break;

				case CmdsInbound.IOPb12:
					break;

				case CmdsInbound.IOPb13:
					break;

				case CmdsInbound.IOPb14:
					break;

				case CmdsInbound.IOPb15:
					if (regstatus == false)
					{
						if (reginprocess == 0)
						{
							//Set Reg Stage 1
							reginprocess = 1;
							_serialPort.WriteLine(CmdsOutbound.OOPLine1 + "Registration code");
						}
					}
					else
					{
						if (reginprocess != 0)
						{
							if (deregprocess == 0)
							{
								//Set Dereg Stage 1
								deregprocess = 1;
								_serialPort.WriteLine(CmdsOutbound.OOPLine1 + "Confirm deregister?");
							}
						}
					}
					break;

				case CmdsInbound.IOPb16:
					break;

				case CmdsInbound.IOPkpCheck:
					if (reginprocess == 1)
					{
						int inthc = int.Parse(headcode);
						if (inthc > 00000000 && inthc < 16077700)
						{
							reginprocess = 2;

							//todo remove this
							reginprocess = 0;
							regstatus = true;
							string tmphex = inthc.ToString("X");
							switch (tmphex.Length)
							{
								case 1:
									tmphex = "     " + tmphex;
									break;

								case 2:
									tmphex = "    " + tmphex;
									break;

								case 3:
									tmphex = "   " + tmphex;
									break;

								case 4:
									tmphex = "  " + tmphex;
									break;

								case 5:
									tmphex = " " + tmphex;
									break;
							}
							_serialPort.WriteLine(CmdsOutbound.OOPHeadcode + tmphex);
						}
						else
						{
							reginprocess = 0;
							headcode = "";
							string[] HCPROCRESET = { "", "", "", "", "", "", "", "" };
							headcoderegproc = HCPROCRESET;
							_serialPort.WriteLine(CmdsOutbound.OOPErrorAndClear + "Invalid ID");
						}

						//string hexValue = headcode.ToString("X");

						//TODO: Registrationcode
					}
					else if (deregprocess == 2)
					{
						//TODO: Deregistration Code
					}

					break;

				case CmdsInbound.IOPkpCross:
					break;

				#region buttons

				case CmdsInbound.IOPkp1:
					if (reginprocess == 1)
					{
						if (headcode.Length < 6)
						{
							if (headcoderegproc[0].Length == 0)
							{
								headcoderegproc[0] = "1";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb0 + headcoderegproc[0]);
								headcode = headcode + headcoderegproc[0];
								lastkppressreg = 1;
							}
							else if (headcoderegproc[1].Length == 0)
							{
								headcoderegproc[1] = "1";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb1 + headcoderegproc[1]);
								headcode = headcode + headcoderegproc[1];
								lastkppressreg = 1;
							}
							else if (headcoderegproc[2].Length == 0)
							{
								headcoderegproc[2] = "1";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb2 + headcoderegproc[2]);
								headcode = headcode + headcoderegproc[2];
								lastkppressreg = 1;
							}
							else if (headcoderegproc[3].Length == 0)
							{
								headcoderegproc[3] = "1";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb3 + headcoderegproc[3]);
								headcode = headcode + headcoderegproc[3];
								lastkppressreg = 1;
							}
							else if (headcoderegproc[4].Length == 0)
							{
								headcoderegproc[4] = "1";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb4 + headcoderegproc[4]);
								headcode = headcode + headcoderegproc[4];
								lastkppressreg = 1;
							}
							else if (headcoderegproc[5].Length == 0)
							{
								headcoderegproc[5] = "1";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb5 + headcoderegproc[5]);
								headcode = headcode + headcoderegproc[5];
								lastkppressreg = 1;
							}
							else if (headcoderegproc[6].Length == 0)
							{
								headcoderegproc[6] = "1";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb6 + headcoderegproc[6]);
								headcode = headcode + headcoderegproc[6];
								lastkppressreg = 1;
							}
							else if (headcoderegproc[7].Length == 0)
							{
								headcoderegproc[7] = "1";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb7 + headcoderegproc[7]);
								headcode = headcode + headcoderegproc[7];
								lastkppressreg = 1;
							}
						}
					}
					break;

				case CmdsInbound.IOPkp2:
					if (reginprocess == 1)
					{
						if (headcode.Length <= 7)
						{
							if (headcoderegproc[0].Length == 0)
							{
								headcoderegproc[0] = "2";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb0 + headcoderegproc[0]);
								headcode = headcode + headcoderegproc[0];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[1].Length == 0)
							{
								headcoderegproc[1] = "2";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb1 + headcoderegproc[1]);
								headcode = headcode + headcoderegproc[1];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[2].Length == 0)
							{
								headcoderegproc[2] = "2";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb2 + headcoderegproc[2]);
								headcode = headcode + headcoderegproc[2];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[3].Length == 0)
							{
								headcoderegproc[3] = "2";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb3 + headcoderegproc[3]);
								headcode = headcode + headcoderegproc[3];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[4].Length == 0)
							{
								headcoderegproc[4] = "2";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb4 + headcoderegproc[4]);
								headcode = headcode + headcoderegproc[4];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[5].Length == 0)
							{
								headcoderegproc[5] = "2";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb5 + headcoderegproc[5]);
								headcode = headcode + headcoderegproc[5];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[6].Length == 0)
							{
								headcoderegproc[6] = "2";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb6 + headcoderegproc[6]);
								headcode = headcode + headcoderegproc[6];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[7].Length == 0)
							{
								headcoderegproc[7] = "2";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb7 + headcoderegproc[7]);
								headcode = headcode + headcoderegproc[7];
								lastkppressreg = 2;
							}
						}
					}
					break;

				case CmdsInbound.IOPkp3:
					if (reginprocess == 1)
					{
						if (headcode.Length <= 7)
						{
							if (headcoderegproc[0].Length == 0)
							{
								headcoderegproc[0] = "3";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb0 + headcoderegproc[0]);
								headcode = headcode + headcoderegproc[0];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[1].Length == 0)
							{
								headcoderegproc[1] = "3";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb1 + headcoderegproc[1]);
								headcode = headcode + headcoderegproc[1];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[2].Length == 0)
							{
								headcoderegproc[2] = "3";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb2 + headcoderegproc[2]);
								headcode = headcode + headcoderegproc[2];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[3].Length == 0)
							{
								headcoderegproc[3] = "3";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb3 + headcoderegproc[3]);
								headcode = headcode + headcoderegproc[3];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[4].Length == 0)
							{
								headcoderegproc[4] = "3";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb4 + headcoderegproc[4]);
								headcode = headcode + headcoderegproc[4];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[5].Length == 0)
							{
								headcoderegproc[5] = "3";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb5 + headcoderegproc[5]);
								headcode = headcode + headcoderegproc[5];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[6].Length == 0)
							{
								headcoderegproc[6] = "3";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb6 + headcoderegproc[6]);
								headcode = headcode + headcoderegproc[6];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[7].Length == 0)
							{
								headcoderegproc[7] = "3";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb7 + headcoderegproc[7]);
								headcode = headcode + headcoderegproc[7];
								lastkppressreg = 2;
							}
						}
					}
					break;

				case CmdsInbound.IOPkp4:
					if (reginprocess == 1)
					{
						if (headcode.Length <= 7)
						{
							if (headcoderegproc[0].Length == 0)
							{
								headcoderegproc[0] = "4";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb0 + headcoderegproc[0]);
								headcode = headcode + headcoderegproc[0];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[1].Length == 0)
							{
								headcoderegproc[1] = "4";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb1 + headcoderegproc[1]);
								headcode = headcode + headcoderegproc[1];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[2].Length == 0)
							{
								headcoderegproc[2] = "4";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb2 + headcoderegproc[2]);
								headcode = headcode + headcoderegproc[2];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[3].Length == 0)
							{
								headcoderegproc[3] = "4";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb3 + headcoderegproc[3]);
								headcode = headcode + headcoderegproc[3];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[4].Length == 0)
							{
								headcoderegproc[4] = "4";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb4 + headcoderegproc[4]);
								headcode = headcode + headcoderegproc[4];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[5].Length == 0)
							{
								headcoderegproc[5] = "4";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb5 + headcoderegproc[5]);
								headcode = headcode + headcoderegproc[5];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[6].Length == 0)
							{
								headcoderegproc[6] = "4";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb6 + headcoderegproc[6]);
								headcode = headcode + headcoderegproc[6];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[7].Length == 0)
							{
								headcoderegproc[7] = "4";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb7 + headcoderegproc[7]);
								headcode = headcode + headcoderegproc[7];
								lastkppressreg = 2;
							}
						}
					}
					break;

				case CmdsInbound.IOPkp5:
					if (reginprocess == 1)
					{
						if (headcode.Length <= 7)
						{
							if (headcoderegproc[0].Length == 0)
							{
								headcoderegproc[0] = "5";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb0 + headcoderegproc[0]);
								headcode = headcode + headcoderegproc[0];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[1].Length == 0)
							{
								headcoderegproc[1] = "5";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb1 + headcoderegproc[1]);
								headcode = headcode + headcoderegproc[1];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[2].Length == 0)
							{
								headcoderegproc[2] = "5";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb2 + headcoderegproc[2]);
								headcode = headcode + headcoderegproc[2];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[3].Length == 0)
							{
								headcoderegproc[3] = "5";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb3 + headcoderegproc[3]);
								headcode = headcode + headcoderegproc[3];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[4].Length == 0)
							{
								headcoderegproc[4] = "5";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb4 + headcoderegproc[4]);
								headcode = headcode + headcoderegproc[4];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[5].Length == 0)
							{
								headcoderegproc[5] = "5";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb5 + headcoderegproc[5]);
								headcode = headcode + headcoderegproc[5];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[6].Length == 0)
							{
								headcoderegproc[6] = "5";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb6 + headcoderegproc[6]);
								headcode = headcode + headcoderegproc[6];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[7].Length == 0)
							{
								headcoderegproc[7] = "5";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb7 + headcoderegproc[7]);
								headcode = headcode + headcoderegproc[7];
								lastkppressreg = 2;
							}
						}
					}
					break;

				case CmdsInbound.IOPkp6:
					if (reginprocess == 1)
					{
						if (headcode.Length <= 7)
						{
							if (headcoderegproc[0].Length == 0)
							{
								headcoderegproc[0] = "6";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb0 + headcoderegproc[0]);
								headcode = headcode + headcoderegproc[0];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[1].Length == 0)
							{
								headcoderegproc[1] = "6";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb1 + headcoderegproc[1]);
								headcode = headcode + headcoderegproc[1];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[2].Length == 0)
							{
								headcoderegproc[2] = "6";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb2 + headcoderegproc[2]);
								headcode = headcode + headcoderegproc[2];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[3].Length == 0)
							{
								headcoderegproc[3] = "6";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb3 + headcoderegproc[3]);
								headcode = headcode + headcoderegproc[3];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[4].Length == 0)
							{
								headcoderegproc[4] = "6";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb4 + headcoderegproc[4]);
								headcode = headcode + headcoderegproc[4];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[5].Length == 0)
							{
								headcoderegproc[5] = "6";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb5 + headcoderegproc[5]);
								headcode = headcode + headcoderegproc[5];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[6].Length == 0)
							{
								headcoderegproc[6] = "6";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb6 + headcoderegproc[6]);
								headcode = headcode + headcoderegproc[6];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[7].Length == 0)
							{
								headcoderegproc[7] = "6";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb7 + headcoderegproc[7]);
								headcode = headcode + headcoderegproc[7];
								lastkppressreg = 2;
							}
						}
					}
					break;

				case CmdsInbound.IOPkp7:
					if (reginprocess == 1)
					{
						if (headcode.Length <= 7)
						{
							if (headcoderegproc[0].Length == 0)
							{
								headcoderegproc[0] = "7";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb0 + headcoderegproc[0]);
								headcode = headcode + headcoderegproc[0];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[1].Length == 0)
							{
								headcoderegproc[1] = "7";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb1 + headcoderegproc[1]);
								headcode = headcode + headcoderegproc[1];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[2].Length == 0)
							{
								headcoderegproc[2] = "7";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb2 + headcoderegproc[2]);
								headcode = headcode + headcoderegproc[2];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[3].Length == 0)
							{
								headcoderegproc[3] = "7";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb3 + headcoderegproc[3]);
								headcode = headcode + headcoderegproc[3];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[4].Length == 0)
							{
								headcoderegproc[4] = "7";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb4 + headcoderegproc[4]);
								headcode = headcode + headcoderegproc[4];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[5].Length == 0)
							{
								headcoderegproc[5] = "7";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb5 + headcoderegproc[5]);
								headcode = headcode + headcoderegproc[5];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[6].Length == 0)
							{
								headcoderegproc[6] = "7";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb6 + headcoderegproc[6]);
								headcode = headcode + headcoderegproc[6];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[7].Length == 0)
							{
								headcoderegproc[7] = "7";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb7 + headcoderegproc[7]);
								headcode = headcode + headcoderegproc[7];
								lastkppressreg = 2;
							}
						}
					}
					break;

				case CmdsInbound.IOPkp8:
					if (reginprocess == 1)
					{
						if (headcode.Length <= 7)
						{
							if (headcoderegproc[0].Length == 0)
							{
								headcoderegproc[0] = "8";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb0 + headcoderegproc[0]);
								headcode = headcode + headcoderegproc[0];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[1].Length == 0)
							{
								headcoderegproc[1] = "8";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb1 + headcoderegproc[1]);
								headcode = headcode + headcoderegproc[1];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[2].Length == 0)
							{
								headcoderegproc[2] = "8";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb2 + headcoderegproc[2]);
								headcode = headcode + headcoderegproc[2];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[3].Length == 0)
							{
								headcoderegproc[3] = "8";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb3 + headcoderegproc[3]);
								headcode = headcode + headcoderegproc[3];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[4].Length == 0)
							{
								headcoderegproc[4] = "8";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb4 + headcoderegproc[4]);
								headcode = headcode + headcoderegproc[4];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[5].Length == 0)
							{
								headcoderegproc[5] = "8";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb5 + headcoderegproc[5]);
								headcode = headcode + headcoderegproc[5];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[6].Length == 0)
							{
								headcoderegproc[6] = "8";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb6 + headcoderegproc[6]);
								headcode = headcode + headcoderegproc[6];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[7].Length == 0)
							{
								headcoderegproc[7] = "8";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb7 + headcoderegproc[7]);
								headcode = headcode + headcoderegproc[7];
								lastkppressreg = 2;
							}
						}
					}
					break;

				case CmdsInbound.IOPkp9:
					if (reginprocess == 1)
					{
						if (headcode.Length <= 7)
						{
							if (headcoderegproc[0].Length == 0)
							{
								headcoderegproc[0] = "9";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb0 + headcoderegproc[0]);
								headcode = headcode + headcoderegproc[0];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[1].Length == 0)
							{
								headcoderegproc[1] = "9";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb1 + headcoderegproc[1]);
								headcode = headcode + headcoderegproc[1];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[2].Length == 0)
							{
								headcoderegproc[2] = "9";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb2 + headcoderegproc[2]);
								headcode = headcode + headcoderegproc[2];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[3].Length == 0)
							{
								headcoderegproc[3] = "9";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb3 + headcoderegproc[3]);
								headcode = headcode + headcoderegproc[3];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[4].Length == 0)
							{
								headcoderegproc[4] = "9";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb4 + headcoderegproc[4]);
								headcode = headcode + headcoderegproc[4];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[5].Length == 0)
							{
								headcoderegproc[5] = "9";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb5 + headcoderegproc[5]);
								headcode = headcode + headcoderegproc[5];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[6].Length == 0)
							{
								headcoderegproc[6] = "9";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb6 + headcoderegproc[6]);
								headcode = headcode + headcoderegproc[6];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[7].Length == 0)
							{
								headcoderegproc[7] = "9";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb7 + headcoderegproc[7]);
								headcode = headcode + headcoderegproc[7];
								lastkppressreg = 2;
							}
						}
					}
					break;

				case CmdsInbound.IOPkp0:
					if (reginprocess == 1)
					{
						if (headcode.Length <= 7)
						{
							if (headcoderegproc[0].Length == 0)
							{
								headcoderegproc[0] = "0";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb0 + headcoderegproc[0]);
								headcode = headcode + headcoderegproc[0];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[1].Length == 0)
							{
								headcoderegproc[1] = "0";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb1 + headcoderegproc[1]);
								headcode = headcode + headcoderegproc[1];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[2].Length == 0)
							{
								headcoderegproc[2] = "0";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb2 + headcoderegproc[2]);
								headcode = headcode + headcoderegproc[2];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[3].Length == 0)
							{
								headcoderegproc[3] = "0";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb3 + headcoderegproc[3]);
								headcode = headcode + headcoderegproc[3];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[4].Length == 0)
							{
								headcoderegproc[4] = "0";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb4 + headcoderegproc[4]);
								headcode = headcode + headcoderegproc[4];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[5].Length == 0)
							{
								headcoderegproc[5] = "0";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb5 + headcoderegproc[5]);
								headcode = headcode + headcoderegproc[5];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[6].Length == 0)
							{
								headcoderegproc[6] = "0";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb6 + headcoderegproc[6]);
								headcode = headcode + headcoderegproc[6];
								lastkppressreg = 2;
							}
							else if (headcoderegproc[7].Length == 0)
							{
								headcoderegproc[7] = "0";
								_serialPort.WriteLine(CmdsOutbound.OOPHcb7 + headcoderegproc[7]);
								headcode = headcode + headcoderegproc[7];
								lastkppressreg = 2;
							}
						}
					}
					break;

					#endregion buttons
			}
		}

		private void volumedn()
		{
			double vol = defaultPlaybackDevice.Volume;
			if (vol == 0.0)
			{
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar0);
			}
			else if (vol <= 20.0 && vol >= 1.0)
			{
				defaultPlaybackDevice.Volume = 0.0;
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar0);
			}
			else if (vol <= 40.0 && vol >= 21.0)
			{
				defaultPlaybackDevice.Volume = 20.0;
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar1);
			}
			else if (vol <= 60.0 && vol >= 41.0)
			{
				defaultPlaybackDevice.Volume = 40.0;
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar2);
			}
			else if (vol <= 80.0 && vol >= 71.0)
			{
				defaultPlaybackDevice.Volume = 60.0;
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar3);
			}
			else if (vol <= 100.0 && vol >= 81.0)
			{
				defaultPlaybackDevice.Volume = 80.0;
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar4);
			}

			//defaultPlaybackDevice.Volume = 80;
		}

		private void volumeup()
		{
			double vol = defaultPlaybackDevice.Volume;
			if (vol == 0.0)
			{
				defaultPlaybackDevice.Volume = 20.0;
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar1);
			}
			else if (vol <= 20.0 && vol >= 1.0)
			{
				defaultPlaybackDevice.Volume = 40.0;
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar2);
			}
			else if (vol <= 40.0 && vol >= 21.0)
			{
				defaultPlaybackDevice.Volume = 60.0;
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar3);
			}
			else if (vol <= 60.0 && vol >= 41.0)
			{
				defaultPlaybackDevice.Volume = 80.0;
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar4);
			}
			else if (vol <= 80.0 && vol >= 71.0)
			{
				defaultPlaybackDevice.Volume = 100.0;
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar5);
			}
			else if (vol <= 100.0 && vol >= 81.0)
			{
				_serialPort.WriteLine(CmdsOutbound.OOPVolBar5);
			}
		}

		private void daemonconnectbut_Click(object sender, EventArgs e)
		{
			int RXA = 0;
			int TXA = 0;
			int WSP = 0;
			string WSA = daemonaddrTXT.Text;

			RXA = rxaudioCMBO.SelectedIndex;
			TXA = txaudioCMBO.SelectedIndex;
			try { WSP = int.Parse(daemonptTXT.Text); }
			catch (Exception ex)
			{
				MessageBox.Show("Please enter a Daemon address and port, k thx bye");
			}

			if (WSA != null)
			{
				Connections.RC2(cts.Token, WSA, WSP, TXA, RXA);
			}
			else
			{
				MessageBox.Show("Please enter a Daemon address");
			}
		}

		private void saveBUT_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.daemonaddr = daemonaddrTXT.Text;
			Properties.Settings.Default.daemonport = daemonptTXT.Text;
			Properties.Settings.Default.serialport = serialportlistbox.Text;
			Properties.Settings.Default.gsmrctry = countryTXT.Text;
			Properties.Settings.Default.Save();
		}

		private void autoconnectTMR_Tick(object sender, EventArgs e)
		{
			if (counttoStart == 0)
			{
				autoconnectTMR.Stop();
				autoconnectTMR.Enabled = false;
				connectbutton_Click(sender, e);
				daemonconnectbut_Click(sender, e);
			}
			else { counttoStart--; asCountLAB.Text = counttoStart.ToString(); }
		}

		private void headtimeoutTMR_Tick(object sender, EventArgs e)
		{
			if (headstatus == false && hconnectstatus == true)
			{
				Console.Beep();
			}
			TimeSpan timeoutDuration = TimeSpan.FromSeconds(30);
			if (DateTime.Now - lasttimeack > timeoutDuration && hconnectstatus == true)
			{
				headstatus = false;
				_serialPort.WriteLine(CmdsOutbound.OOPReqStatus);
			}
		}

		private void ASstopButt_Click(object sender, EventArgs e)
		{
			autoconnectTMR.Stop();
			counttoStart = 10;
			asCountLAB.Text = "NA";
		}
	}
}