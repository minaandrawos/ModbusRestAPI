using System;
using System.Collections.Concurrent;
using Modbus.Device;
using ModbusHandler;

namespace ModbusHandler
{
	public sealed class Coils : ModbusTypeAbstract<bool[]>
	{
		private static readonly Coils instance = new Coils();
		static Coils(){}
		private Coils(){}
		public static Coils Instance
		{
			get
			{
				return instance;
			}
		}

		public override bool[] Read(request req)
		{
			return GetOrAddConnection(req.connectiontype,req.destination)?.ReadCoils(req.slaveid, req.address, req.count);
		}

		public override void Write(writerequest<bool[]> wreq)
		{
			GetOrAddConnection(wreq.connectiontype, wreq.destination)?.WriteMultipleCoils(wreq.slaveid, wreq.address, wreq.data);
		}

	}
}

