using System.IO.Ports;

namespace GXPEngine
{
    public class Controller
    {

        /// <summary>
        /// Connects to a specified port on the arduino board.
        /// </summary>
        /// <param name="portName">The port name. This is mentioned in the arduino manual and on the board itself.</param>
        /// <param name="baudRate">The baud rate of the arduino board.</param>
        /// <param name="requestToSend">Gets or sets a value indicating whether the (RTS) is enabled during serial communication</param>
        /// <param name="dataTerminalReady">Gets or sets a value indicating whether the (DTR) is enabled during serial communication</param>
        /// <returns></returns>
        public SerialPort ConnectToPort(string portName, int baudRate = 9600, bool requestToSend = true, bool dataTerminalReady = true)
        {
            SerialPort port = new SerialPort();
            port.PortName = portName;
            port.BaudRate = baudRate;
            port.RtsEnable = requestToSend;
            port.DtrEnable = dataTerminalReady;
            port.Open();
            return port;
        }

        /// <summary>
        /// Only meant for buttons. Checks if a button has been pressed.
        /// </summary>
        /// <param name="port">An established port with a running connection.</param>
        /// <returns></returns>
        public bool IsButtonPressed(SerialPort port)
        {
            if (port.ReadExisting() is null)
            {
                return false;
            }
            else return true;
        }
    }
}