using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movesense;

namespace Plugin.Movesense.Api
{
    /// <summary>
    /// Contains context for a subscription to an MdsLib subscription resource
    /// </summary>
    public partial class MdsSubscription : IMdsSubscription
    {

        /// <summary>
        /// Unsubscribe from the resource
        /// </summary>
        public void Unsubscribe()
        {
            var mds = (MDSWrapper)CrossMovesense.Current.MdsInstance;
            // On iOS, the 'nativeSubscription' refers to the Uri for the subscription
            string path = (string)mNativeSubscription;
            mds.DoUnsubscribe(path);
        }
    }
}
