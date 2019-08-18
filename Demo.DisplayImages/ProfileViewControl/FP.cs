namespace MedDispense.UI
{
	using System;
	using System.Runtime;
	using System.Runtime.InteropServices;

	/// <summary>
	/// Summary description for FP.
	/// </summary>
	public class FP
	{
		const uint _controlword = 0x0008001F; // _MCW_EM
		const uint _mask = 0x00000010; // _EM_INVALID

		[DllImport("msvcrt.dll")]
		public static extern uint _controlfp(uint controlword, uint mask);

		static public void ControlFP()
		{
			_controlfp(_controlword, _mask);
		}
	}
}
