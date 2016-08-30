using System;
using System.Collections.Concurrent;
using Modbus.Device;
using ModbusHandler;

namespace ModbusHandler
{
	public sealed class HoldingRegisters : ModbusTypeAbstract<ushort[]> 
	{
		private static readonly HoldingRegisters instance = new HoldingRegisters();
		static HoldingRegisters(){}
		private HoldingRegisters(){}
		public static HoldingRegisters Instance
		{
			get
			{
				return instance;
			}
		}
		public override ushort[] Read(request req)
		{
			return GetOrAddConnection(req.connectiontype, req.destination)?.ReadHoldingRegisters(req.slaveid, req.address, req.count);
		}

		public override void Write(writerequest<ushort[]> wreq)
		{
			GetOrAddConnection(wreq.connectiontype,wreq.destination)?.WriteMultipleRegisters(wreq.slaveid, wreq.address, wreq.data);
		}

	}
}

