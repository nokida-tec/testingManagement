using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using XT_CETC23.DataManager;
using XT_CETC23.Common;
using XT_CETC23.Model;
using XT_CETC23.DataCom;

namespace XT_CETC23
{
    class Logger
    {
        static Logger sInstance = null;
        static Object sLockInstance = new Object();

        static public Logger getInstance()
        {
            if (sInstance == null)
            {
                lock (sLockInstance)
                {
                    if (sInstance == null)
                    {
                        sInstance = new Logger();
                    }
                }
            }
            return sInstance;
        }

        private StreamWriter mStreamWriter = null;
        public delegate void delegateShowMessage(string message);
        private delegateShowMessage mDelegateOfShow = null;

        private Logger ()
        {
            try
            {
                mStreamWriter = File.AppendText(Config.Config.getInstance().logPath + @"\\syslog-" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".txt");
            }
            catch (Exception e)
            {
                writeLine(e);
                
                for (Char disk = 'f'; disk >= 'c'; disk --)
                {
                    try
                    {
                        mStreamWriter = File.AppendText(disk + @":\syslog-" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".txt");
                        break;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            finally
            {
                Console.SetOut(mStreamWriter);
            }
        }

        public void RegistryDelegate(delegateShowMessage delegateOfShow)
        {
            mDelegateOfShow = delegateOfShow;
        }

        public void UnregistryDelegate(delegateShowMessage delegateOfShow)
        {
            mDelegateOfShow = null;
        }

        public void writeLine(Exception e)
        {
            writeLine(true, e);
        }

        public void writeLine(bool showInScreen, Exception e)
        {
            try
            {
                String line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  ") + e.Message;
                Console.WriteLine(" ***** " + line);
                Console.WriteLine(" ***** " + e.StackTrace);
                mStreamWriter.Flush();
                if (showInScreen && mDelegateOfShow != null)
                {
                    mDelegateOfShow(line);
                    mDelegateOfShow(e.StackTrace);
                }
            }
            catch (Exception)
            {
            }
        }

        public void writeLine(String message)
        {
            writeLine(true, message);
        }

        public void writeLine(bool showInScreen, String message)
        {
            try
            {
                String line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  ") + message;
                Console.WriteLine(" ***** " + line);
                mStreamWriter.Flush();
                if (showInScreen && mDelegateOfShow != null)
                {
                    mDelegateOfShow(line);
                }
            }
            catch (Exception e)
            {

            }
        }

        public void writeLineWithStack(String message)
        {
            try
            {
                throw new Exception(message);
            }
            catch (Exception e)
            {
                writeLine(e);
            }
        }

        static public void WriteLine(Exception e)
        {
            Logger.getInstance().writeLine(e);
        }

        static public void WriteLine(String message)
        {
            Logger.getInstance().writeLine(message);
        }

        static public void WriteLineWithStack(String message)
        {
            Logger.getInstance().writeLineWithStack(message);
        }

    }
}
