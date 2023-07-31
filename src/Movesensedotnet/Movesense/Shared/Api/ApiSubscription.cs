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
        private static readonly int RETRY_DELAY = 5000; //5 sec
        private static int MAX_RETRY_COUNT = 2;
        private int retries = 0;
        private readonly string mSerial;
        private readonly string mPath;
        TaskCompletionSource<IMdsSubscription> mTcs;
        Action<T> mNotificationCallback;

        /// <summary>
        /// The context for the subscription
        /// </summary>
        public IMdsSubscription Subscription { get; private set; }

        /// <summary>
        /// Utility class for API subscriptions
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="path">The path of the MdsLib resource, for example "/Meas/Acc/52" to subscribe to accelerometer at 52Hz</param>
        [Obsolete("Passing argument of deviceName is deprecated, please use ApiSubscription(MdsConnectionContext, string) constructor instead.")]
        public ApiSubscription(string deviceName, string path)
        {
            if (string.IsNullOrEmpty(deviceName)) throw new InvalidOperationException("Required parameter deviceName must have value");
            if (string.IsNullOrEmpty(path)) throw new InvalidOperationException("Required parameter path must have value");

            mSerial = Util.GetVisibleSerial(deviceName);
            mPath = path;
            if (mPath.Substring(0, 1) != "/")
            {
                mPath = "/" + mPath;
            }

            // Define the built-in implementation of the retry function
            // This just retries 2 times, regardless of the exception thrown
            // The user may provide their own implementation of the Retry function to override this behavior
            RetryFunction = new Func<Exception, bool?>((Exception ex) =>
            {
                bool? cancel = false;
                if (++retries > MAX_RETRY_COUNT)
                {
                    cancel = true;
                }
                return cancel;
            }
        );
        }

        /// <summary>
        /// Utility class for API subscriptions
        /// </summary>
        /// <param name="movesenseDevice">IMovesenseDevice for the device</param>
        /// <param name="path">The path of the MdsLib resource, for example "/Meas/Acc/52" to subscribe to accelerometer at 52Hz</param>
        public ApiSubscription(IMovesenseDevice movesenseDevice, string path)
        {
            if (movesenseDevice is null) throw new InvalidOperationException("Required parameter connectionContext must have value");
            if (string.IsNullOrEmpty(path)) throw new InvalidOperationException("Required parameter path must have value");

            mSerial = movesenseDevice.Serial;
            mPath = path;
            if (mPath.Substring(0, 1) != "/")
            {
                mPath = "/" + mPath;
            }

            // Define the built-in implementation of the retry function
            // This just retries 2 times, regardless of the exception thrown
            // The user may provide their own implementation of the Retry function to override this behavior
            RetryFunction = new Func<Exception, bool?>((Exception ex) =>
            {
                bool? cancel = false;
                if (++retries > MAX_RETRY_COUNT)
                {
                    cancel = true;
                }
                return cancel;
            }
        );
        }

        /// <summary>
        /// Retry function, called after the function call fails.
        /// The built-in implementation retries 2 times, regardless of the exception thrown.
        /// Override the built-in implementation by setting this to your own implementation of the Retry function
        /// </summary>
        public Func<Exception, bool?> RetryFunction;

        /// <summary>
        /// Subscribe to the resource
        /// </summary>
        /// <param name="notificationCallback">Callback function that will receive periodic notifications with data from the subscription resource</param>
        /// <returns>The subscription context</returns>
        public async Task<IMdsSubscription> SubscribeWithRetryAsync(Action<T> notificationCallback)
        {
            TaskCompletionSource<IMdsSubscription> retryTcs = new TaskCompletionSource<IMdsSubscription>();
            IMdsSubscription result = null;
            bool doRetry = true;
            while (doRetry)
            {
                try
                {
                    result = await doSubscribe(notificationCallback).ConfigureAwait(false);
                    retryTcs.SetResult(result);
                    doRetry = false;
                }
                catch (Exception ex)
                {
                    bool? mCancelled = RetryFunction?.Invoke(ex);
                    if (mCancelled.HasValue && mCancelled.Value)
                    {
                        // User has cancelled - break out of loop
                        Debug.WriteLine($"MAX RETRY COUNT EXCEEDED giving up Mds Api Call after exception: {ex.ToString()}");
                        retryTcs.SetException(ex);
                        throw ex;
                    }
                    else
                    {
                        Debug.WriteLine($"RETRYING Mds Api Call after exception: {ex.ToString()}");
                        await Task.Delay(RETRY_DELAY).ConfigureAwait(false);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Subscribe to the resource
        /// </summary>
        /// <param name="notificationCallback">Callback function that will receive periodic notifications with data from the subscription resource</param>
        /// <returns>The subscription context</returns>
        public Task<IMdsSubscription> SubscribeAsync(Action<T> notificationCallback)
        {
            return doSubscribe(notificationCallback);
        }

        /// <summary>
        /// Unsubscribe from the MdsLib resource
        /// </summary>
        public void UnSubscribe()
        {
            Debug.WriteLine("Unsubscribing Mds api subscription");
            Subscription?.Unsubscribe();
            Subscription = null;
        }


        private string FormatContractToJson(string serial, string uri)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Uri\": \"");
            sb.Append(serial);
            sb.Append(uri);
            sb.Append("\"}");
            return sb.ToString();
        }
    }
}
