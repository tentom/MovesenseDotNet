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

        /// <summary>
        /// Unsubscribe from the resource
        /// </summary>
        public void Unsubscribe()
        {
            ((Com.Movesense.Mds.IMdsSubscription)mNativeSubscription).Unsubscribe();
        }
    }
}
