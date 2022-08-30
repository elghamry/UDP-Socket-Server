using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;



namespace Server
{
    public class UdpServerWithSocket
    {

        public delegate void ClientDisConnected();
        public event ClientDisConnected ClientDisConnectedEvent;
        public delegate void MessageSentCounter();
        public event MessageSentCounter MessageSentCounterEvent;
        public int sampleUdpPort = 6500;
        public int freq = 0;
        public String message = "";
        public bool isClientConnected = false;
        public Thread sampleUdpThread;
           Socket soUdp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

       

        EndPoint remoteEP;
        IPHostEntry localHostEntry;


        public UdpServerWithSocket(int portNo, int freq, String message)
        {
            sampleUdpPort = portNo;
            this.freq = freq;
            this.message = message;
            

            try
            {

                //Starting the UDP Server thread.
                sampleUdpThread = new Thread(new ThreadStart(startReceive));
                sampleUdpThread.Start();
                Debug.WriteLine("Started SampleTcpUdpServer's UDP Receiver Thread!\n");
            }
            catch (Exception e)
            {
                Debug.WriteLine("An UDP Exception has occurred!" + e.ToString());
                sampleUdpThread.Abort();
            }

            try
            {
                //Starting the UDP Server thread.
                sampleUdpThread = new Thread(new ThreadStart(startReceive));
                sampleUdpThread.Start();
                Debug.WriteLine("Started SampleTcpUdpServer's UDP Receiver Thread!\n");
            }
            catch (Exception e)
            {
                Debug.WriteLine("An UDP Exception has occurred!" + e.ToString());
                sampleUdpThread.Abort();
            }




        }


        public void startReceive()
        {

            try
            {
                //Create a UDP socket.

                try
                {
                    localHostEntry = Dns.GetHostByName(Dns.GetHostName());
                }
                catch (Exception)
                {
                    Debug.WriteLine("Local Host not found"); // fail
                    return;
                }
                //initate server listen on port .....
                IPEndPoint localIpEndPoint = new IPEndPoint(localHostEntry.AddressList[0], sampleUdpPort);
                soUdp.Bind(localIpEndPoint);
                Byte[] received = new Byte[4];
                IPEndPoint tmpIpEndPoint = new IPEndPoint(IPAddress.Any, sampleUdpPort);
                remoteEP = (tmpIpEndPoint);
                //while (true)
                //{
                //isClientConnected = false;
                //waiting for client
                //   int bytesReceived = soUdp.ReceiveFrom(received, ref remoteEP);


                // fill out the event arguments and callback method
                var rxEventArgs = new SocketAsyncEventArgs();
                rxEventArgs.RemoteEndPoint = remoteEP;
                //4
                rxEventArgs.SetBuffer(new byte[2048], 0, 2048);
                rxEventArgs.Completed += UdpReceiveCallback;

                // initiate a receive operation
                soUdp.ReceiveFromAsync(rxEventArgs);

                // soUdp.ReceiveFrom(received, ref remoteEP);




                //}
            }
            catch (SocketException se)
            {
                Debug.WriteLine("A Socket Exception has occurred!" + se.ToString());
            }
        }

        public void stopReceive()
        {

            try
            {

                if (soUdp.IsBound)
                {
                    soUdp.Close();
                    soUdp.Dispose();
                }

                if (sampleUdpThread != null)
                    sampleUdpThread.Abort();

            }
            catch (Exception e)
            {

            }





        }

        public void sendMessage(String msg)
        {


            try
            {

                if (soUdp != null && soUdp.IsBound && isClientConnected)
                {
                    Byte[] returningByte = System.Text.Encoding.ASCII.GetBytes(msg);
                    soUdp.SendTo(returningByte, remoteEP);
                    MessageSentCounterEvent();




                }
            }
            catch (Exception se)
            {



            }

        }

        public void UdpReceiveCallback(object sock, SocketAsyncEventArgs rxArgs)
        {
            // the SockReceiver is the original socket that initiated the receive
            // the rxArgs are the same ones passed in by the original caller

            if (rxArgs.SocketError == SocketError.Success)
            {
                // fire an event for asynch processing elsewhere

                string dataReceived = System.Text.Encoding.ASCII.GetString(rxArgs.Buffer);
                remoteEP = rxArgs.RemoteEndPoint;
                //client connect onn us && send message to us
                //Debug.WriteLine("SampleClient is connected through UDP.");
                //Debug.WriteLine(dataReceived);
                //notify client with a message that iam okay 
                //comment 25-7-2022
                //sendMessage("Connected" + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                isClientConnected = true;
                Debug.WriteLine("Shimaa Time " + dataReceived);
                Debug.WriteLine("Abdelrahman Time" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

          
               
                


                // ReceiveFromAsync returns true if it will notify you in the future
                var willRaiseEvent = ((Socket)sock).ReceiveFromAsync(rxArgs);

                // if it returns false it means it will not raise a future event because 
                // it has received a packet already. Since the listener can get a burst
                // of packets from apps on the localhost, the loop below hands the
                // processing off to another thread until ReceiveFromAsync finally
                // returns true and says that it will raise an event in the future
                // when the next packet arrives.
                //while (!willRaiseEvent)
                //{
                //    // fire an event for asynch processing elsewhere
                //    dataReceived = System.Text.Encoding.ASCII.GetString(rxArgs.Buffer);
                //    Debug.Print("Shimaa Time" + dataReceived);
                //    Debug.Print("Abdelrahman Time" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                //    //remoteEP = rxArgs.RemoteEndPoint;
                //    ////client connect onn us && send message to us
                //    ////Debug.WriteLine("SampleClient is connected through UDP.");
                //    ////Debug.WriteLine(dataReceived);
                //    ////notify client with a message that iam okay 

                //    //sendMessage(dataReceived);



                //    //  ClientDisConnectedEvent();
                //    // isClientConnected = false;
                //    //eturn;



                //}
            }
            else
            {
               // if (isClientConnected && ! IsStoppedFromServer) ClientDisConnectedEvent();
                
                if(isClientConnected == true)
                {

                    isClientConnected = false;
                    ClientDisConnectedEvent();
                   

                }
                else
                {
                    isClientConnected = false;
                }
               

            }
        }


    }




}

