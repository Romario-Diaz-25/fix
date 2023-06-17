using System;
using System.Collections.Generic;

using System.Text;
using QuickFix;

namespace FixAcceptor
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                SessionSettings settings = new SessionSettings(@"C:\Users\romar\Desktop\FixInitiator\acceptor.cfg");
                FixServerApplication application = new FixServerApplication();
                FileStoreFactory storeFactory = new FileStoreFactory(settings);
                ScreenLogFactory logFactory = new ScreenLogFactory(settings);
                MessageFactory messageFactory = new DefaultMessageFactory();
                SocketAcceptor acceptor
                  = new SocketAcceptor(application, storeFactory, settings, logFactory, messageFactory);

                acceptor.start();
                Console.WriteLine("press <enter> to quit");
                Console.Read();
                acceptor.stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
    public class FixServerApplication : MessageCracker, QuickFix.Application
    {
        public void onCreate(SessionID sessionID) { }
        public void onLogon(SessionID sessionID) { }
        public void onLogout(SessionID sessionID) { }
        public void toAdmin(Message message, SessionID sessionID) { }
        public void toApp(Message message, SessionID sessionID) { }
        public void fromAdmin(Message message, SessionID sessionID) { }
        public void fromApp(Message message, SessionID sessionID)
        { crack(message, sessionID); }


        public override void onMessage(QuickFix40.NewOrderSingle order, SessionID sessionID)
        {
            Symbol symbol = new Symbol();
            Side side = new Side();
            OrdType ordType = new OrdType();
            OrderQty orderQty = new OrderQty();
            Price price = new Price();
            ClOrdID clOrdID = new ClOrdID();

            order.get(ordType);

            if (ordType.getValue() != OrdType.LIMIT)
                throw new IncorrectTagValue(ordType.getField());

            order.get(symbol);
            order.get(side);
            order.get(orderQty);
            order.get(price);
            order.get(clOrdID);

            QuickFix40.ExecutionReport executionReport = new QuickFix40.ExecutionReport
                (genOrderID(),
                  genExecID(),
                  new ExecTransType(ExecTransType.NEW),
                  new OrdStatus(OrdStatus.FILLED),
                  symbol,
                  side,
                  orderQty,
                  new LastShares(orderQty.getValue()),
                  new LastPx(price.getValue()),
                  new CumQty(orderQty.getValue()),
                  new AvgPx(price.getValue()));

            executionReport.set(clOrdID);

            if (order.isSetAccount())
                executionReport.set(order.getAccount());

            try
            {
                Session.sendToTarget(executionReport, sessionID);
            }
            catch (SessionNotFound) { }
        }

        public override void onMessage(QuickFix41.NewOrderSingle order, SessionID sessionID)
        {
            Symbol symbol = new Symbol();
            Side side = new Side();
            OrdType ordType = new OrdType();
            OrderQty orderQty = new OrderQty();
            Price price = new Price();
            ClOrdID clOrdID = new ClOrdID();

            order.get(ordType);

            if (ordType.getValue() != OrdType.LIMIT)
                throw new IncorrectTagValue(ordType.getField());

            order.get(symbol);
            order.get(side);
            order.get(orderQty);
            order.get(price);
            order.get(clOrdID);

            QuickFix41.ExecutionReport executionReport = new QuickFix41.ExecutionReport
                (genOrderID(),
                  genExecID(),
                  new ExecTransType(ExecTransType.NEW),
                  new ExecType(ExecType.FILL),
                  new OrdStatus(OrdStatus.FILLED),
                  symbol,
                  side,
                  orderQty,
                  new LastShares(orderQty.getValue()),
                  new LastPx(price.getValue()),
                  new LeavesQty(0),
                  new CumQty(orderQty.getValue()),
                  new AvgPx(price.getValue()));

            executionReport.set(clOrdID);

            if (order.isSetAccount())
                executionReport.set(order.getAccount());

            try
            {
                Session.sendToTarget(executionReport, sessionID);
            }
            catch (SessionNotFound) { }
        }

        public override void onMessage(QuickFix42.NewOrderSingle order, SessionID sessionID)
        {
            Console.WriteLine("Got Order from session {0}", sessionID.toString());
            Symbol symbol = new Symbol();
            Side side = new Side();
            OrdType ordType = new OrdType();
            OrderQty orderQty = new OrderQty();
            Price price = new Price();
            ClOrdID clOrdID = new ClOrdID();

            order.get(ordType);

            if (ordType.getValue() != OrdType.LIMIT)
                throw new IncorrectTagValue(ordType.getField());

            order.get(symbol);
            order.get(side);
            order.get(orderQty);
            order.get(price);
            order.get(clOrdID);

            QuickFix42.ExecutionReport executionReport = new QuickFix42.ExecutionReport
                                                    (genOrderID(),
                                                      genExecID(),
                                                      new ExecTransType(ExecTransType.NEW),
                                                      new ExecType(ExecType.FILL),
                                                      new OrdStatus(OrdStatus.FILLED),
                                                      symbol,
                                                      side,
                                                      new LeavesQty(0),
                                                      new CumQty(orderQty.getValue()),
                                                      new AvgPx(price.getValue()));

            executionReport.set(clOrdID);
            executionReport.set(orderQty);
            executionReport.set(new LastShares(orderQty.getValue()));
            executionReport.set(new LastPx(price.getValue()));

            if (order.isSetAccount())
                executionReport.set(order.getAccount());

            try
            {
                Session.sendToTarget(executionReport, sessionID);
            }
            catch (SessionNotFound) { }
        }

        public override void onMessage(QuickFix43.NewOrderSingle order, SessionID sessionID)
        {
            Symbol symbol = new Symbol();
            Side side = new Side();
            OrdType ordType = new OrdType();
            OrderQty orderQty = new OrderQty();
            Price price = new Price();
            ClOrdID clOrdID = new ClOrdID();

            order.get(ordType);

            if (ordType.getValue() != OrdType.LIMIT)
                throw new IncorrectTagValue(ordType.getField());

            order.get(symbol);
            order.get(side);
            order.get(orderQty);
            order.get(price);
            order.get(clOrdID);

            QuickFix43.ExecutionReport executionReport = new QuickFix43.ExecutionReport
                                                    (genOrderID(),
                                                      genExecID(),
                                                      new ExecType(ExecType.FILL),
                                                      new OrdStatus(OrdStatus.FILLED),
                                                      side,
                                                      new LeavesQty(0),
                                                      new CumQty(orderQty.getValue()),
                                                      new AvgPx(price.getValue()));

            executionReport.set(clOrdID);
            executionReport.set(symbol);
            executionReport.set(orderQty);
            executionReport.set(new LastQty(orderQty.getValue()));
            executionReport.set(new LastPx(price.getValue()));

            if (order.isSetAccount())
                executionReport.set(order.getAccount());

            try
            {
                Session.sendToTarget(executionReport, sessionID);
            }
            catch (SessionNotFound) { }
        }

        public override void onMessage(QuickFix44.NewOrderSingle order, SessionID sessionID)
        {
            Symbol symbol = new Symbol();
            Side side = new Side();
            OrdType ordType = new OrdType();
            OrderQty orderQty = new OrderQty();
            Price price = new Price();
            ClOrdID clOrdID = new ClOrdID();

            order.get(ordType);

            if (ordType.getValue() != OrdType.LIMIT)
                throw new IncorrectTagValue(ordType.getField());

            order.get(symbol);
            order.get(side);
            order.get(orderQty);
            order.get(price);
            order.get(clOrdID);

            QuickFix44.ExecutionReport executionReport = new QuickFix44.ExecutionReport
              (genOrderID(),
              genExecID(),
              new ExecType(ExecType.FILL),
              new OrdStatus(OrdStatus.FILLED),
              side,
              new LeavesQty(0),
              new CumQty(orderQty.getValue()),
              new AvgPx(price.getValue()));

            executionReport.set(clOrdID);
            executionReport.set(symbol);
            executionReport.set(orderQty);
            executionReport.set(new LastQty(orderQty.getValue()));
            executionReport.set(new LastPx(price.getValue()));

            if (order.isSetAccount())
                executionReport.setField(order.getAccount());

            try
            {
                Session.sendToTarget(executionReport, sessionID);
            }
            catch (SessionNotFound) { }
        }

        //public override void onMessage(QuickFix50.NewOrderSingle order, SessionID sessionID)
        //{
        //    Symbol symbol = new Symbol();
        //    Side side = new Side();
        //    OrdType ordType = new OrdType();
        //    OrderQty orderQty = new OrderQty();
        //    Price price = new Price();
        //    ClOrdID clOrdID = new ClOrdID();

        //    order.get(ordType);

        //    if (ordType.getValue() != OrdType.LIMIT)
        //        throw new IncorrectTagValue(ordType.getField());

        //    order.get(symbol);
        //    order.get(side);
        //    order.get(orderQty);
        //    order.get(price);
        //    order.get(clOrdID);

        //    QuickFix50.ExecutionReport executionReport = new QuickFix50.ExecutionReport
        //     (genOrderID(),
        //      genExecID(),
        //      new ExecType(ExecType.FILL),
        //      new OrdStatus(OrdStatus.FILLED),
        //      side,
        //      new LeavesQty(0),
        //      new CumQty(orderQty.getValue()));

        //    executionReport.set(clOrdID);
        //    executionReport.set(symbol);
        //    executionReport.set(orderQty);
        //    executionReport.set(new LastQty(orderQty.getValue()));
        //    executionReport.set(new LastPx(price.getValue()));
        //    executionReport.set(new AvgPx(price.getValue()));

        //    if (order.isSetAccount())
        //        executionReport.setField(order.getAccount());

        //    try
        //    {
        //        Session.sendToTarget(executionReport, sessionID);
        //    }
        //    catch (SessionNotFound) { }
        //}

        private OrderID genOrderID()
        {
            return new OrderID((++m_orderID).ToString());
        }

        private ExecID genExecID()
        {
            return new ExecID((++m_execID).ToString());
        }

        private int m_orderID;
        private int m_execID;
    }
}
