using Android.App;
using Android.Content;

namespace Com.Amazon.Device.Iap
{
    [BroadcastReceiver(Name = "com.amazon.device.iap.ResponseReceiver")]
    [IntentFilter(new[] { "com.amazon.inapp.purchasing.NOTIFY" })]
    public partial class ResponseReceiver
    {

    }
}
