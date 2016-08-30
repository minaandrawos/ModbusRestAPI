using System;
namespace ModbusHandler
{
	public sealed class InputRegisters : ModbusTypeAbstract<ushort[]>
	{
		private static readonly InputRegisters instance = new InputRegisters();
		static InputRegisters() { }
		private InputRegisters() { }
		public static InputRegisters Instance
		{
			get
			{
				return instance;
			}
		}

		public override ushort[] Read(request req)
		{
			return GetOrAddConnection(req.connectiontype, req.destination)?.ReadInputRegisters(req.slaveid, req.address, req.count);
		}

		public override void Write(writerequest<ushort[]> wreq)
		{
			throw new NotSupportedException();
		}
	}
}

