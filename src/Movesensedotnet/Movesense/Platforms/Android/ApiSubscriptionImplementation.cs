using MdsLibrary.Helpers;
using Plugin.Movesense;
using Plugin.Movesense.Api;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
#if __IOS__
using Foundation;
#endif

namespace MdsLibrary.Api
{
    /// <summary>
    /// Makes a subscription to an MdsLib resource
    /// </summary>
    public class ApiSubscription<T> :
        Java.Lang.Object, Com.Movesense.Mds.IMdsNotificationListener,
        IApiSubscription<T>
    {

        private static readonly string URI_EVENTLISTENER = "suunto://MDS/EventListener";



        private Task<IMdsSubscription> doSubscribe(Action<T> notificationCallback)
        {
            mTcs = new TaskCompletionSource<IMdsSubscription>();
            mNotificationCallback = notificationCallback;

            var mds = (Com.Movesense.Mds.Mds)CrossMovesense.Current.MdsInstance;
            var subscription = mds.Subscribe(
                URI_EVENTLISTENER, 
                FormatContractToJson(mSerial, mPath), 
                this
                );
            Subscription = new MdsSubscription(subscription);

            return mTcs.Task;
        }


        /// <summary>
        /// Callback method that Mds calls with subscription data
        /// </summary>
        /// <param name="s"></param>
        public void OnNotification(string s)
        {
            Debug.WriteLine($"NOTIFICATION data = {s}");
            if (typeof(T) != typeof(String))
            {
                T result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(s);
                // Return the subscription to the awaiting caller
                mTcs.TrySetResult(Subscription);
                // Invoke the callers callback function
                mNotificationCallback?.Invoke(result);
            }
            else
            {
                // Crazy code to convert a string to a 'T' where 'T' happens to be a string
                T result = (T)((object)s);
                // Return the subscription to the awaiting caller
                mTcs.TrySetResult(Subscription);
                // Invoke the callers callback function
                mNotificationCallback?.Invoke(result);
            }
        }

        /// <summary>
        /// Error callback called by Mds when an error is encoubntered reading subscription data
        /// </summary>
        /// <param name="e"></param>
        public void OnError(Com.Movesense.Mds.MdsException e)
        {
            Debug.WriteLine($"ERROR error = {e.ToString()}");
            mTcs.SetException(new MdsException(e.ToString(), e));
        }
    }
}
