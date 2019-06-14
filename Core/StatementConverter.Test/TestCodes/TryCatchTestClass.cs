using System;

namespace StatementConverter.Test
{
    public class TryCatchTestClass
    {
        public static void NoException()
        {
            try
            {
                Tracker.Call("hello");
            }
            catch (Exception ex)
            {
                Tracker.Call("hello1");
            }
        }

        public static void Exception()
        {
            try
            {
                throw new Exception();
                Tracker.Call("hello1");
            }
            catch (Exception ex)
            {
                Tracker.Call("hello");
            }
        }

        public static void MultipleCatchStatement()
        {
            try
            {
                throw new NotSupportedException();
                Tracker.Call("hello1");
            }
            catch (NotSupportedException ex)
            {
                Tracker.Call("hello");
            }
            catch (Exception ex)
            {
                Tracker.Call("hello2");
            }
        }

        public static void FinallyBlockWithNoException()
        {
            try
            {
                Tracker.Call("hello1");
            }
            catch (Exception ex)
            {
                Tracker.Call("hello2");
            }
            finally
            {
                Tracker.Call("hello");
            }
        }

        public static void FinallyBlockWithException()
        {
            try
            {
                throw new Exception();
                Tracker.Call("hello1");
            }
            catch (Exception ex)
            {
                Tracker.Call("hello2");
            }
            finally
            {
                Tracker.Call("hello");
            }
        }

        public static void AccessCatchVariable()
        {
            try
            {
                throw new Exception("hello");
            }
            catch(Exception ex)
            {
                Tracker.Call(ex.Message);
            }
        }

        public static void CatchWithoutVariable()
        {
            try
            {
                throw new Exception();
            }
            catch
            {
                Tracker.Call("hello");
            }
        }

        public static void CatchFilter()
        {
            try
            {
                throw new Exception("hello");
            }
            catch(Exception ex) when (ex.Message == "hello")
            {
                Tracker.Call(ex.Message);
            }
            catch(Exception)
            {
                Tracker.Call("hello1");
            }
        }
    }
}
