﻿using Android.Telephony;
using Android.Content;
using Android.App;
using Android.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(Xamarin.Forms.Labs.Services.PhoneService))]
namespace Xamarin.Forms.Labs.Services
{
    /// <summary>
    /// Android Phone service implements <see cref="IPhoneService"/>.
    /// </summary>
    public class PhoneService : IPhoneService
    {
        /// <summary>
        /// Gets the telephony manager for Android.
        /// </summary>
        /// <value>
        /// The manager.
        /// </value>
        public static TelephonyManager Manager
        {
            get
            {
                return Application.Context.GetSystemService(Context.TelephonyService) as TelephonyManager;
            }
        }

        /// <summary>
        /// Gets the connectivity manager.
        /// </summary>
        /// <value>
        /// The connectivity manager.
        /// </value>
        public static ConnectivityManager ConnectivityManager
        {
            get
            {
                return Application.Context.GetSystemService(Context.ConnectivityService) as ConnectivityManager;
            }
        }

        #region IPhone implementation
        /// <summary>
        /// Gets the cellular provider.
        /// </summary>
        /// <value>
        /// The cellular provider.
        /// </value>
        public string CellularProvider
        {
            get
            {
                return Manager.NetworkOperatorName;
            }
        }

        /// <summary>
        /// Gets the ISO Country Code.
        /// </summary>
        /// <value>
        /// The ISO Country Code.
        /// </value>
        public string ICC
        {
            get
            {
                return Manager.SimCountryIso;
            }
        }

        /// <summary>
        /// Gets the Mobile Country Code.
        /// </summary>
        /// <value>
        /// The Mobile Country Code.
        /// </value>
        public string MCC
        {
            get
            {
                return Manager.NetworkOperator.Remove(3, 3);
            }
        }

        /// <summary>
        /// Gets the Mobile Network Code.
        /// </summary>
        /// <value>
        /// The Mobile Network Code.
        /// </value>
        public string MNC
        {
            get
            {
                return Manager.NetworkOperator.Remove(0, 3);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has cellular data enabled.
        /// </summary>
        /// <value>
        /// Null if value cannot be determined, otherwise the status of cellular data.
        /// </value>
        /// <remarks>
        /// This feature will require the following Android permissions:
        ///     - android.permission.INTERNET
        ///     - android.permission.ACCESS_NETWORK_STATE 
        /// Please set them in the AndroidManifest.xml file.
        /// </remarks>
        public bool? IsCellularDataEnabled
        {
            get
            {
                // TODO: determine if mobile network actually indicates mobile data capability,
                // most likely this is not accurate way of getting the information and while this info
                // is provided by WP8 we might want to mark this as unsupported feature
                var mobileNetworkInfo = ConnectivityManager.GetNetworkInfo(ConnectivityType.Mobile);
                return mobileNetworkInfo != null && mobileNetworkInfo.IsConnected;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has cellular data roaming enabled.
        /// </summary>
        /// <value>
        /// Null if value cannot be determined, otherwise the status of cellular data roaming.
        /// </value>
        public bool? IsCellularDataRoamingEnabled
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether network is available.
        /// </summary>
        /// <value>
        /// The network availability.
        /// </value>
        /// <remarks>
        /// This feature will require the following Android permissions:
        ///     - android.permission.INTERNET
        ///     - android.permission.ACCESS_NETWORK_STATE 
        /// Please set them in the AndroidManifest.xml file.
        /// </remarks>
        public bool? IsNetworkAvailable
        {
            get
            {
                var activeNetworkInfo = ConnectivityManager.ActiveNetworkInfo;
                return activeNetworkInfo != null && activeNetworkInfo.IsConnected;
            }
        }

        /// <summary>
        /// Opens native dialog to dial the specified number.
        /// </summary>
        /// <param name="number">Number to dial.</param>
        public void DialNumber(string number)
        {
            number.StartActivity(new Intent(Intent.ActionDial, Uri.Parse("tel:" + number)));
        }
        #endregion
    }
}

