using System;
using Nancy.ModelBinding;
using ModbusHandler;
using Nancy;

namespace ModbusHandler
{
	public class routes : Nancy.NancyModule
	{
		public routes()
		{
			Get["/"] = _ => "Hello to the Modbus REST API";

			Post["/Read/{devicetype}"] = parameters => 
			{
				string t = parameters.devicetype;
				var req = this.Bind<request>();
				object result = new object();
				try
				{
					switch (t.ToLower())
					{
						case "coils":
							result = Coils.Instance.Read(req);
							break;
						case "holdingregisters":
							result = HoldingRegisters.Instance.Read(req);
							break;
						case "inputregisters":
							result = InputRegisters.Instance.Read(req);
							break;
						case "inputs":
							result = Inputs.Instance.Read(req);
							break;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				return Response.AsJson(result);
			};

			Post["/Write/{devicetype}"] = parameters =>
			{
				string t = parameters.devicetype;
				switch (t.ToLower())
				{
					case "coils":
						var cwreq = this.Bind<writerequest<bool[]>>();
						Coils.Instance.Write(cwreq);
						break;
					case "holdingregisters":
						var rwreq = this.Bind<writerequest<ushort[]>>();
						HoldingRegisters.Instance.Write(rwreq);
						break;
				}
				return HttpStatusCode.OK;
			};
		}
	}
}

