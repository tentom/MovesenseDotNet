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
    public partial class ApiSubscription<T> : 
        IApiSubscription<T>
    {

        private Task<IMdsSubscription> doSubscribe(Action<T> notificationCallback)
        {
            mTcs = new TaskCompletionSource<IMdsSubscription>();
            mNotificationCallback = notificationCallback;

            var mds = (Movesense.MDSWrapper)CrossMovesense.Current.MdsInstance;
            Movesense.MDSResponseBlock responseBlock = new Movesense.MDSResponseBlock((arg0) => OnSubscribeCompleted(arg0));
            Movesense.MDSEventBlock eventBlock = (Movesense.MDSEvent arg0) => OnSubscriptionEvent(arg0);

            string path = mSerial + mPath;
            mds.DoSubscribe(path, new Foundation.NSDictionary(), responseBlock, eventBlock);
            // Save the path to the subscription for the device in the MdsSubscription
            Subscription = new MdsSubscription(path);

            return mTcs.Task;
        }


        private void OnSubscribeCompleted(Movesense.MDSResponse response)
        {
            if (response.StatusCode == 200)
            {
                System.Diagnostics.Debug.WriteLine("Success subscription: " + response.Description);
                // Return the subscription to the awaiting caller
                mTcs.TrySetResult(Subscription);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Failed to subscribe: " + response.Description);
                mTcs.SetException(new MdsException(response.Description));
            }
        }

        /// <summary>
        /// Callback method that Mds calls with subscription data
        /// </summary>
        /// <param name="mdsevent">Data for the subscription notification</param>
        public void OnSubscriptionEvent(Movesense.MDSEvent mdsevent)
        {
            var data = mdsevent.BodyData;
            NSString s = new NSString(data, NSStringEncoding.UTF8);
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
                // First convert NSString result to a .NET string
                String netS = string.Empty;
                netS = s?.ToString();
                // Crazy code to convert a string to a 'T' where 'T' happens to be a string
                T result = (T)((object)netS);
                // Return the subscription to the awaiting caller
                mTcs.TrySetResult(Subscription);
                // Invoke the callers callback function
                mNotificationCallback?.Invoke(result);
            }
        }
    }
}
