using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Plugin.Movesense.Api
{
    /// <summary>
    /// Contains context for a subscription to an MdsLib subscription resource
    /// </summary>
    public partial class MdsSubscription : IMdsSubscription
    {
        private object mNativeSubscription;

        /// <summary>
        /// Creates a context for a subscription to an MdsLib subscription resource
        /// </summary>
        /// <param name="nativeSubscription">(Android)Reference to the native MdsLib IMdsSubscription, (iOS)path to the subscrfiption for the device</param>
        public MdsSubscription(object nativeSubscription)
        {
            mNativeSubscription = nativeSubscription;
        }


    }
}
