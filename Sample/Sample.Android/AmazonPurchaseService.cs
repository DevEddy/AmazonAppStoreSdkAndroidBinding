﻿using Android.Content;
using Com.Amazon.Device.Iap;
using Com.Amazon.Device.Iap.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Droid
{
    public class AmazonPurchaseService
    {
        private readonly Context _context;
        private readonly MyPurchasingListener _myPurchasingListener;

        public AmazonPurchaseService(Context context)
        {
            _context = context;

            _myPurchasingListener = new MyPurchasingListener();
            PurchasingService.RegisterListener(context, _myPurchasingListener);
        }

        public async Task<IList<Receipt>> GetPurchaseReceipts()
        {
            var tcs = new TaskCompletionSource<IList<Receipt>>();
            var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(10));
            cts.Token.Register(() => tcs.TrySetCanceled(), false);

            var requestId = PurchasingService.GetPurchaseUpdates(true);

            _myPurchasingListener.OnPurchaseUpdatesResponseAction = async (response) =>
            {
                try
                {
                    await Task.Run(() =>
                    {
                        try
                        {
                            if (response.RequestId != requestId)
                                return;

                            var status = response.GetRequestStatus();
                            if (status != PurchaseUpdatesResponse.RequestStatus.Successful)
                                throw new Exception($"Get purchases request failed with response status: {status}");

                            var receipts = response.Receipts;
                            tcs.TrySetResult(receipts);
                        }
                        catch (Exception ex)
                        {
                            tcs.TrySetException(ex);
                        }
                    }, cts.Token);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
                finally
                {
                    _myPurchasingListener.OnPurchaseUpdatesResponseAction = null;
                }
            };
            return await tcs.Task;
        }
    }

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

}