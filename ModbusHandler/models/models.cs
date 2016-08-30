using System;
namespace ModbusHandler
{
	public class request
	{
		public string destination { get; set; }
		public ConnectionType connectiontype { get; set;}
		public byte slaveid { get; set; }
		public ushort address { get; set; }
		public ushort count { get; set; }
	}

	public class writerequest<T> : request
	{
		public T data { get; set; }
	}

}

