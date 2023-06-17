using System;
using System.Collections.Generic;

using System.Windows.Forms;
using QuickFix;
using System.Threading;
using System.Text;


namespace FixInitiator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            ClientInitiator app = new ClientInitiator();
            SessionSettings settings = new SessionSettings(@"C:\Users\romar\Desktop\FixInitiator\initiator.cfg");
            QuickFix.Application application = new ClientInitiator();
            FileStoreFactory storeFactory = new FileStoreFactory(settings);
            ScreenLogFactory logFactory = new ScreenLogFactory(settings);
            MessageFactory messageFactory = new DefaultMessageFactory();

            SocketInitiator initiator = new SocketInitiator(application, storeFactory, settings, logFactory, messageFactory);
            initiator.start();
            Thread.Sleep(3000);
            System.Collections.ArrayList list = initiator.getSessions();

            SessionID sessionID = (SessionID)list[0];

            QuickFix42.NewOrderSingle order = new QuickFix42.NewOrderSingle(new ClOrdID("DLF"), new HandlInst(HandlInst.MANUAL_ORDER), new Symbol("DLF"), new Side(Side.BUY), new TransactTime(DateTime.Now), new OrdType(OrdType.LIMIT));
            order.set(new OrderQty(45));
            order.set(new Price(25.4d));
            Console.WriteLine("Sending Order to Server");
            Session.sendToTarget(order, sessionID);


            Console.ReadLine();

            initiator.stop();
        }
    }

    public class ClientInitiator : QuickFix.Application
    {

        public void onCreate(QuickFix.SessionID value)
        {
            //Console.WriteLine("Message OnCreate" + value.toString());
        }

        public void onLogon(QuickFix.SessionID value)
        {
            //Console.WriteLine("OnLogon" + value.toString());
        }

        public void onLogout(QuickFix.SessionID value)
        {
            // Console.WriteLine("Log out Session" + value.toString());
        }

        public void toAdmin(QuickFix.Message value, QuickFix.SessionID session)
        {
            //Console.WriteLine("Called Admin :" + value.ToString());
        }

        public void toApp(QuickFix.Message value, QuickFix.SessionID session)
        {
            //  Console.WriteLine("Called toApp :" + value.ToString());
        }

        public void fromAdmin(QuickFix.Message value, SessionID session)
        {
            // Console.WriteLine("Got message from Admin" + value.ToString());
        }

        public void fromApp(QuickFix.Message value, SessionID session)
        {
            if (value is QuickFix42.ExecutionReport)
            {
                QuickFix42.ExecutionReport er = (QuickFix42.ExecutionReport)value;
                ExecType et = (ExecType)er.getExecType();
                if (et.getValue() == ExecType.FILL)
                {
                    //TODO: implement code
                }
            }

            Console.WriteLine("Got Execution Report from Server \n" + value.ToString());
        }
    }
}
