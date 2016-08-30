using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using Modbus.Device;


namespace ModbusHandler
{
	public interface IModbusType<T>
	{
		T Read(request req);
		void Write(writerequest<T> wreq);
	}

}

