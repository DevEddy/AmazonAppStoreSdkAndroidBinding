# Amazon App Store SDK - Android Binding
This is a binding project for the Amazon App Store SDK. 

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
    /// <exception cref="NotImplementedException"></exception>
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
        OnUserDataResponseAction.Invoke(response);
    }
}


var myPurchasingListener = new MyPurchasingListener();
PurchasingService.RegisterListener(activity, myPurchasingListener);

myPurchasingListener.OnPurchaseResponseAction = async (purchaseResponse) =>
{
  ...
}
 myPurchasingListener.OnPurchaseResponseAction = null;

 ```
