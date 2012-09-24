using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
	
namespace qcCmd
{
    class Program
    {
        static void Main(string[] args)
        {
			/*
			System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort();
			List<string> ports = new List<string>();
			ports.AddRange(System.IO.Ports.SerialPort.GetPortNames());
			
			ports = ports;
			
			
                //setup com port
                port.BaudRate = 0x2580;
                //''mySerialPort.BaudRate = 0x38400;
                port.DataBits = 8;
                port.StopBits = StopBits.One;
                port.PortName = "/dev/ttyUSB0";//blech hardcoded test
                port.ReadTimeout = -1;
                port.WriteTimeout = -1;
                //port.ReceivedBytesThreshold = 1;
                //port.ParityReplace = 0x3F;
               // port.NewLine = ChrW(10);
                port.ReadBufferSize = 0x1000;
                port.WriteBufferSize = 0x800;

			port.Open();
			
			
			
			port.Write(byteArrayToTransmit, 0, byteArrayToTransmit.Length);
			
			//cdmaDevLib.cdmaTerm term = new cdmaDevLib.cdmaTerm();
			//term.connectSub("/dev/ttyUSB0");
			*/
		//term.sendTermCommand2(new Byte[]{0x20, 0x0, 0x33, 0xEF, 0xC6, 0x7E});
			
			 const int InfiniteTimeout = -1;
		const int DefaultReadBufferSize = 4096;
		const int DefaultWriteBufferSize = 2048;
		const int DefaultBaudRate = 9600;
		const int DefaultDataBits = 8;
		const Parity DefaultParity = Parity.None;
		const StopBits DefaultStopBits = StopBits.One;

		bool is_open;
		int baud_rate;
		Parity parity;
		StopBits stop_bits;
		Handshake handshake;
		int data_bits;
		bool break_state = false;
		bool dtr_enable = false;
		bool rts_enable = false;
		ISerialStream stream;
		Encoding encoding = Encoding.ASCII;
		string new_line = Environment.NewLine;
		string port_name;
		int read_timeout = InfiniteTimeout;
		int write_timeout = InfiniteTimeout;
		int readBufferSize = DefaultReadBufferSize;
		int writeBufferSize = DefaultWriteBufferSize;
		object error_received = new object ();
		object data_received = new object ();
		object pin_changed = new object ();
			
			
			SerialPort port = new SerialPort("/dev/ttyUSB0", 0x38400);
			byte[] byteArrayToTransmit = new Byte[]{0x20, 0x0, 0x33, 0xEF, 0xC6, 0x7E};
			
			stream = new SerialPortStream (port_name, baud_rate, data_bits, parity, stop_bits, dtr_enable,
					rts_enable, handshake, read_timeout, write_timeout, readBufferSize, writeBufferSize);
			
			
			port.Open();
			//port.Write(byteArrayToTransmit, 0, byteArrayToTransmit.Length);
			port.Close();
			
			
        }
		
		
		private SerialPort mySerial;
 
	public void Test()
	{
		if (mySerial != null)
			if (mySerial.IsOpen)
				mySerial.Close();
 
		mySerial = new SerialPort("/dev/ttyS0", 38400);
		mySerial.Open();
		mySerial.ReadTimeout = 400;
		SendData("ATI3\r");
 
                // Should output some information about your modem firmware
		Console.WriteLine(ReadData());  
	}
 
	public string ReadData()
	{
		byte tmpByte;
		string rxString = "";
 
		tmpByte = (byte) mySerial.ReadByte();
 
		while (tmpByte != 255) {
			rxString += ((char) tmpByte);
			tmpByte = (byte) mySerial.ReadByte();			
		}
 
		return rxString;
	}
 
	public bool SendData(string Data)
	{
		mySerial.Write(Data);
		return true;		
	}
    }
}
