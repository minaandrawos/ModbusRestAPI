using System;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using Modbus.Device;

namespace ModbusHandler
{

	public abstract class ModbusTypeAbstract<T> : IModbusType<T>
	{
		ConcurrentDictionary<string, IModbusMaster> connections = new ConcurrentDictionary<string, IModbusMaster>();

		public abstract T Read(request req);
		public abstract void Write(writerequest<T> wreq);

		private bool verifyIPConnectionString(string conn, out IPAddress ip, out int port)
		{
			port = 0;
			ip = null;
			var connectionarray = conn.Split(':');
			if (connectionarray.Length != 2)
			{
				return false;
			}

			if (!int.TryParse(connectionarray[1], out port))
			{
				return false;
			}
			if (!IPAddress.TryParse(connectionarray[0], out ip))
			{
				return false;
			}
			return true;
		}
		protected IModbusMaster GetOrAddConnection(ConnectionType ctype, string conn)
		{
			switch (ctype)
 			{
				case ConnectionType.SERIALRTU:
					return connections.GetOrAdd(conn, (arg) =>
						{
							 return ModbusSerialMaster.CreateRtu(new SerialPort(conn));
						});
				case ConnectionType.SERIALASCII:
					return connections.GetOrAdd(conn, (arg) =>
						{
								return ModbusSerialMaster.CreateAscii(new SerialPort(conn));
						});
				case ConnectionType.TCP:
					int port; IPAddress ip;
					if (!verifyIPConnectionString(conn, out ip, out port))
					{
						return null;
					}

					return connections.GetOrAdd(conn, (arg) =>
					{
						return ModbusIpMaster.CreateIp(new TcpClient(ip.ToString(), port));
					});
			}
			return null;

		}
	}
}

