namespace dvmgsmrcontrollertester
{
	public static class CmdsOutbound
	{
		/// Outbound
		//Outbound cmds to set the text for lines 0,1,2,and 3
		public const string OOPDEF = "at#att";

		public const string OOPLine0 = "at$l00";
		public const string OOPLine1 = "at$l01";
		public const string OOPLine2 = "at$l02";
		public const string OOPLine3 = "at$l03";

		//Outbound cmd to flash Emrg button led
		public const string OOPFlEmrg = "at$fem";

		//Outbound cmd to flash Disp button led
		public const string OOPFlDisp = "at$fdp";

		//Outbound cmd to steady Emrg button led
		public const string OOPSdyEmrg = "at$sem";

		//Outbound cmd to steady Disp button led
		public const string OOPSdyDisp = "at$sdp";

		//Outbound cmd to turn off Emrg button led
		public const string OOPOffEmrg = "at$oem";

		//Outbound cmd to turn off Disp button led
		public const string OOPOffDisp = "at$odp";

		//Outbound cmd to power off CH
		public const string OOPpwrOff = "at!pwr";

		//Outbound cmd to power on CH
		public const string OOPpwrOn = "at$pwr";

		//Outbound cmd to run DSD behavior
		public const string OOPdsdOn = "at$dsd";

		//Outbound cmd to stop DSD behavior
		public const string OOPdsdOff = "at!dsd";

		//Outbound cmd to RequestStatus
		public const string OOPReqStatus = "at?ack";

		//Outbound cmd to RequestSelfcheck
		public const string OOPReqSlfChk = "at?slf";

		//Outbound cmd for ControllerReady
		public const string OOPControllerReady = "at&cok";

		//SetHeadcode
		public const string OOPHeadcode = "at$hdc";
		//SetChannel
		public const string OOPChan = "at$cha";
		//SetRXRID
		public const string OOPRxRID = "at$rid";
		//SetRXMode
		public const string OOPRxCall = "at$rxm";
		//ClearRXMode
		public const string OOPNoRXCall = "at!rxm";
		//SetTxMode
		public const string OOPTxMOde = "at$txm";
		//ClearTxMode
		public const string OOPNoTXMOde = "at!txm";
	}

	public static class CmdsInbound
	{
		///Inbound
		//Inbound cmd to ack
		public const string IOPack = "at&ack";

		//Inbound cmd to nack
		public const string IOPnack = "at!ack";

		//Inbound Head Ready CMD
		public const string IOPHeadReady = "at&hok";

		//Inbound cmd Button1
		public const string IOPb1 = "at#b1";
		//Inbound cmd Button2
		public const string IOPb2 = "at#b2";
		//Inbound cmd Button3
		public const string IOPb3 = "at#b3";
		//Inbound cmd Button4
		public const string IOPb4 = "at#b4";
		//Inbound cmd Button5
		public const string IOPb5 = "at#b5";
		//Inbound cmd Button6
		public const string IOPb6 = "at#b6";
		//Inbound cmd Button7
		public const string IOPb7 = "at#b7";
		//Inbound cmd Button8
		public const string IOPb8 = "at#b8";
		//Inbound cmd Button9
		public const string IOPb9 = "at#b9";
		//Inbound cmd Button10
		public const string IOPb10 = "at#b10";
		//Inbound cmd Button11
		public const string IOPb11 = "at#b11";
		//Inbound cmd Button12
		public const string IOPb12 = "at#b12";
		//Inbound cmd Button13
		public const string IOPb13 = "at#b13";
		//Inbound cmd Button14
		public const string IOPb14 = "at#b14";
		//Inbound cmd Button15
		public const string IOPb15 = "at#b15";
		//Inbound cmd Button16
		public const string IOPb16 = "at#b16";
	}
}