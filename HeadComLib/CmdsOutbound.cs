namespace HeadComLib
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
}
