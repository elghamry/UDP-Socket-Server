using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;


namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow

    {
        UdpServerWithSocket test,test1,test2;
        public int messageCounter  = 0;
        public int messageLimit = 90;
        bool start_flag,start_flag1,start_flag2 = false;
        String message, message1 , message2 , message3 , message4 , message5 , message6 , message7 , message8 = "";
        int freq , freq1 , freq2 , freq3 , freq4 , freq5 , freq6 , freq7 , freq8 = 0;
        Thread sendMsgThread, sendMsgThread1, sendMsgThread2, sendMsgThread3, sendMsgThread4, sendMsgThread5, sendMsgThread6, sendMsgThread7, sendMsgThread8;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Restart()
        {
                Stop();
                Start();
      
        
         



        }

        private void Restart1()
        {
        
        
                Stop1();
                Start1();
            
      



        }

        private void Restart2()
        {
      
           
                Stop2();
                Start2();
           



        }

        //private void Restart()
        //{

        //    Stop();
        //    Start();


        //}



        private void DisableEt(int field_no)
        {
            if(field_no == 0)
            {
                etFreq.IsEnabled = false;
                etPort.IsEnabled = false;
                etMessage.IsEnabled = false;
            }

            if (field_no == 1)
            {
                etFreq1.IsEnabled = false;
                etPort1.IsEnabled = false;
                etMessage1.IsEnabled = false;
            }

            if (field_no == 2)
            {
                etFreq2.IsEnabled = false;
                etPort2.IsEnabled = false;
                etMessage2.IsEnabled = false;
            }

        }

        private void EnableEt(int field_no)
        {


            if (field_no == 0)
            {
                etFreq.IsEnabled = true;
                etPort.IsEnabled = true;
                etMessage.IsEnabled = true;
            }


            if (field_no == 1)
            {
                etFreq1.IsEnabled = true;
                etPort1.IsEnabled = true;
                etMessage1.IsEnabled = true;
            }


            if (field_no == 2)
            {
                etFreq2.IsEnabled = true;
                etPort2.IsEnabled = true;
                etMessage2.IsEnabled = true;
            }


        }

        private void startClicked(object sender, RoutedEventArgs e)

        {
            
            if (!start_flag)
            {
                Start();
            }
            else
            {

                if (test != null)
                    test.isClientConnected = false;
         
                Stop();
  
           

            }
        }

        private void startClicked1(object sender, RoutedEventArgs e)

        {
            if (!start_flag1)
            {
                Start1();
            }
            else
            {

                if (test1 != null)
                    test1.isClientConnected = false;

                Stop1();



            }
        }



        private void startClicked2(object sender, RoutedEventArgs e)

        {

            
            if (!start_flag2)
            {
                Start2();
            }
            else
            {

                if (test2 != null)
                    test.isClientConnected = false;

                Stop2();



            }
        }



        

        public void sendMessageWithFreq(String message , int freq,UdpServerWithSocket test ,bool start_flag)
        {

            int i = 0;

            while (true)
            {


                try
                {
                    //   string tt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); ;

                    if (start_flag)
                    {
                        if(test!=null)
                        if ( message != "" && freq != 0 && message != null && test.isClientConnected)
                        //test.sendMessage(test.message);
                        {
                            test.sendMessage(message +" "+ i.ToString() +" " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                            i++;
                            if(test!= null)
                            if (test.isClientConnected)
                                Thread.Sleep(freq);
                        }

                    }

                }
                catch (SocketException se)
                {



                }

            }
        }

        private void Start()
        {

            

         
                
            this.Dispatcher.Invoke(() =>
            {
                EnableEt(0);
                if (!etPort.IsNullTextVisible && !etFreq.IsNullTextVisible && !etMessage.IsNullTextVisible)
                {
                    test = new UdpServerWithSocket(int.Parse(etPort.Text.ToString()), int.Parse(etFreq.Text.ToString()), etMessage.Text);
                    test.ClientDisConnectedEvent += Restart;
                    test.MessageSentCounterEvent += CountMessages;

                    if (!etMessage.IsNullTextVisible)
                    message = etMessage.Text;
               
          

                    if(!etFreq.IsNullTextVisible)
                    freq = int.Parse(etFreq.Text.ToString());
            



                    startBtn.Content = "Stop";
                    DisableEt(0);

                    start_flag = true;
                    

                }

            });

            messageCounter = 0;

            //here is messages with frequ
            //commented 25-7-2002 10AM // backtoit
            if (message != "" && freq != 0)
            {
                sendMsgThread = new Thread(() => sendMessageWithFreq(message, freq,test,start_flag));
                sendMsgThread.Start();
            }





        }



        private void Start1()
        {





            this.Dispatcher.Invoke(() =>
            {
                EnableEt(1);
                if (!etPort1.IsNullTextVisible && !etFreq1.IsNullTextVisible && !etMessage1.IsNullTextVisible)
                {
                    test1 = new UdpServerWithSocket(int.Parse(etPort1.Text.ToString()), int.Parse(etFreq1.Text.ToString()), etMessage1.Text);
                    test1.ClientDisConnectedEvent += Restart1;
                    test1.MessageSentCounterEvent += CountMessages;

             
                    if (!etMessage1.IsNullTextVisible)
                        message1 = etMessage1.Text;
           


                   
                    if (!etFreq1.IsNullTextVisible)

                        freq1 = int.Parse(etFreq1.Text.ToString());
           





                    startBtn1.Content = "Stop";
                    DisableEt(1);

                    start_flag1 = true;


                }

            });

            messageCounter = 0;

            //here is messages with frequ
            //commented 25-7-2002 10AM // backtoit
  


            if (message1 != "" && freq1 != 0)
            {


                sendMsgThread1 = new Thread(() => sendMessageWithFreq(message1, freq1,test1,start_flag1));
                sendMsgThread1.Start();
            }

    


        }



        private void Start2()
        {





            this.Dispatcher.Invoke(() =>
            {
                EnableEt(2);
                if (!etPort2.IsNullTextVisible && !etFreq2.IsNullTextVisible && !etMessage2.IsNullTextVisible)
                {
                    test2 = new UdpServerWithSocket(int.Parse(etPort2.Text.ToString()), int.Parse(etFreq2.Text.ToString()), etMessage2.Text);
                    test2.ClientDisConnectedEvent += Restart2;
                    test2.MessageSentCounterEvent += CountMessages;

                  
                    if (!etMessage2.IsNullTextVisible)

                        message2 = etMessage2.Text;


               
                    if (!etFreq2.IsNullTextVisible)

                        freq2 = int.Parse(etFreq2.Text.ToString());





                    startBtn2.Content = "Stop";
                    DisableEt(2);

                    start_flag2 = true;


                }

            });

            messageCounter = 0;

      

            if (message2 != "" && freq2 != 0)
            {

                sendMsgThread2 = new Thread(() => sendMessageWithFreq(message2, freq2,test2,start_flag2));
                sendMsgThread2.Start();

            }


        }



        private void CountMessages()
        {

            messageCounter++;



        }

        private void Stop()
        {

            try
            {
                if (test != null)
                    test.stopReceive();
               
                

                this.Dispatcher.Invoke(() =>
                {
                    startBtn.Content = "Start";
                    EnableEt(0);
                    start_flag = false;
                });

          
                if (sendMsgThread != null)
                {
                    sendMsgThread.Abort();

                    sendMsgThread = null;
                    test = null;
                }

               



            }
            catch (Exception e)
            {


            }

           
        }


        private void Stop1()
        {

            try
            {
                if (test1 != null)
                    test1.stopReceive();

                this.Dispatcher.Invoke(() =>
                {
                    startBtn1.Content = "Start";
                    EnableEt(1);
                    start_flag1 = false;
                });


        

                if (sendMsgThread1 != null)
                {
                    sendMsgThread1.Abort();

                    sendMsgThread1 = null;
                    test1 = null;

                }




            }
            catch (Exception e)
            {


            }


        }



        private void Stop2()
        {

            try
            {
                if (test2 != null)
                    test2.stopReceive();
                

                this.Dispatcher.Invoke(() =>
                {
                    startBtn2.Content = "Start";
                    EnableEt(2);
                    start_flag2 = false;
                });




                if (sendMsgThread2 != null)
                {
                    sendMsgThread2.Abort();

                    sendMsgThread2 = null;
                    test2 = null;
                }




            }
            catch (Exception e)
            {


            }


        }





        private void ThemedWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //close any connection
            //stop listening for server
            //abort sending thread
               if(test!=null)
            test.stopReceive();        
        }
    }
}
