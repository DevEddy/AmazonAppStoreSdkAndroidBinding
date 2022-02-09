# Amazon App Store SDK - Android Binding
This is a binding project for the Amazon App Store SDK. 

# Nuget
[![NuGet](https://img.shields.io/nuget/v/Eddys.Amazon.AppStoreSdk.Binding.svg?maxAge=259200)](https://www.nuget.org/packages/Eddys.Amazon.AppStoreSdk.Binding/)

# Amazon Documentation 
https://developer.amazon.com/de/docs/in-app-purchasing/integrate-appstore-sdk.html

# Sample
 ```csharp
using Com.Amazon.Device.Iap;
using Com.Amazon.Device.Iap.Model;

public class MyPurchasingListener : Java.Lang.Object, IPurchasingListener
{
    public Action<PurchaseResponse> OnPurchaseResponseAction { get; set; }
    public Action<ProductDataResponse> OnProductDataResponseAction { get; set; }
    public Action<PurchaseUpdatesResponse> OnPurchaseUpdatesResponseAction { get; set; }
    public Action<UserDataResponse> OnUserDataResponseAction { get; set; }
    
    public MyPurchasingListener()
    {

    }
    /// <summary>
    /// This is the callback for PurchasingService.GetProductData
    /// </summary>
    /// <param name="response"></param>
    public void OnProductDataResponse(ProductDataResponse response)
    {
        OnProductDataResponseAction?.Invoke(response);
    }
    /// <summary>
    /// This is the callback for PurchasingService.GetPurchaseUpdates
    /// </summary>
    /// <param name="response"></param>
    public void OnPurchaseResponse(PurchaseResponse response)
    {
        OnPurchaseResponseAction?.Invoke(response);
    }

    /// <summary>
    /// This is the callback for PurchasingService.GetPurchaseUpdates
    /// </summary>
    /// <param name="response"></param>
    public void OnPurchaseUpdatesResponse(PurchaseUpdatesResponse response)
    {
        OnPurchaseUpdatesResponseAction?.Invoke(response);
    }
    /// <summary>
    /// This is the callback for PurchasingService.Purchase
    /// </summary>
    /// <param name="response"></param>
    public void OnUserDataResponse(UserDataResponse response)
    {
        OnUserDataResponseAction?.Invoke(response);
    }
}


var myPurchasingListener = new MyPurchasingListener();
PurchasingService.RegisterListener(activity, myPurchasingListener);

myPurchasingListener.OnPurchaseResponseAction = async (purchaseResponse) =>
{
   ...  
   myPurchasingListener.OnPurchaseResponseAction = null;
}

 ```

# Source for Binding a .JAR-File
Microsoft Doc: https://docs.microsoft.com/de-de/xamarin/android/platform/binding-java-library/

InputJar vs EmbeddedJar: https://stackoverflow.com/questions/59825410/xamarin-android-bindings

# Testing
Refer to the Amazon testing doc: https://developer.amazon.com/de/docs/in-app-purchasing/iap-app-tester-user-guide.html

Connect to the testing device via adb and execute the commands as needed.
```
adb -s [ID_OF_DEVICE] tcpip 5555
adb -s [ID_OF_DEVICE] connect 192.168.178.25:5555
adb -s [ID_OF_DEVICE] shell 

setprop debug.amazon.sandboxmode debug
setprop debug.amazon.sandboxmode none
```