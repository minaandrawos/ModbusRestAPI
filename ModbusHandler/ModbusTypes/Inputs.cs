using System;
namespace ModbusHandler
{
	public sealed class Inputs : ModbusTypeAbstract<bool[]>
	{
		private static readonly Inputs instance = new Inputs();
		static Inputs() { }
		private Inputs() { }
		public static Inputs Instance
		{
			get
			{
				return instance;
			}
		}

		public override bool[] Read(request req)
		{
			return GetOrAddConnection(req.connectiontype, req.destination)?.ReadInputs (req.slaveid, req.address, req.count);
		}


		public override void Write(writerequest<bool[]> wreq)
		{
			throw new NotSupportedException();
		}
	}
}

