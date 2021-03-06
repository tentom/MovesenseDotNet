﻿using MdsLibrary;
using MdsLibrary.Model;
using Plugin.Movesense.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Movesense
{
    /// <summary>
    /// Movesense Plugin API
    /// </summary>
    public interface IMovesense
    {
        /// <summary>
        /// Gets the native MdsLib object
        /// </summary>
        object MdsInstance { get; }

        /// <summary>
        /// On Android, this must be set to the current Activity before first access of the library.
        /// </summary>
        object Activity { set; }

        /// <summary>
        /// Root of all paths to Movesense resources.
        /// </summary>
        string SCHEME_PREFIX { get; }

        /// <summary>
        /// Get the MdsConnectionListener instance.
        /// </summary>
        MdsConnectionListener ConnectionListener { get; }

        /// <summary>
        /// Connect a device to MdsLib
        /// </summary>
        /// <param name="Uuid">Uuid of the device</param>
        /// <returns>MdsMovesenseDevice for the device</returns>
        Task<IMovesenseDevice> ConnectMdsAsync(Guid Uuid);

        /// <summary>
        /// Disconnect a device from MdsLib
        /// </summary>
        /// <param name="Uuid">Uuid of the device</param>
        /// <returns>null</returns>
        Task<object> DisconnectMdsAsync(Guid Uuid);

        /// <summary>
        /// Disconnect a device from MdsLib
        /// </summary>
        /// <param name="mdsDevice">IMovesenseDevice of the device</param>
        /// <returns>null</returns>
        Task<object> DisconnectMdsAsync(IMovesenseDevice mdsDevice);

        /// <summary>
        /// Function to make Mds API call that does not return a value
        /// </summary>
        /// <param name="movesenseDevice">IMovesenseDevice for the device</param>
        /// <param name="restOp">The type of REST call to make to MdsLib</param>
        /// <param name="path">The path of the MdsLib resource</param>
        /// <param name="body">JSON body if any</param>
        /// <param name="prefixPath">optional prefix of the target URI before the device serial number (defaults to empty string)</param>
        Task ApiCallAsync(IMovesenseDevice movesenseDevice, MdsOp restOp, string path, string body = null, string prefixPath = "");

        /// <summary>
        /// Function to make Mds API call that returns a value of type T
        /// </summary>
        /// <param name="movesenseDevice">IMovesenseDevice for the device</param>
        /// <param name="restOp">The type of REST call to make to MdsLib</param>
        /// <param name="path">The path of the MdsLib resource</param>
        /// <param name="body">JSON body if any</param>
        /// <param name="prefixPath">optional prefix of the target URI before the device serial number (defaults to empty string)</param>
        Task<T> ApiCallAsync<T>(IMovesenseDevice movesenseDevice, MdsOp restOp, string path, string body = null, string prefixPath = "");

        /// <summary>
        /// Function to start a subscription to an Mds resource
        /// </summary>
        /// <param name="movesenseDevice">IMovesenseDevice for the device</param>
        /// <param name="path">The path of the MdsLib resource</param>
        /// <param name="notificationCallback">Callback function that takes parameter of type T, where T is the return type from the subscription notifications</param>
        Task<IMdsSubscription> ApiSubscriptionAsync<T>(IMovesenseDevice movesenseDevice, string path, Action<T> notificationCallback);

        #region Deprecated methods

        /// <summary>
        /// Function to make Mds API call that does not return a value
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="restOp">The type of REST call to make to MdsLib</param>
        /// <param name="path">The path of the MdsLib resource</param>
        /// <param name="body">JSON body if any</param>
        /// <param name="prefixPath">optional prefix of the target URI before the device serial number (defaults to empty string)</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use ApiCallAsync(MdsMovesenseDevice, ...) instead.")]
        Task ApiCallAsync(string deviceName, MdsOp restOp, string path, string body = null, string prefixPath = "");

        /// <summary>
        /// Function to make Mds API call that returns a value of type T
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="restOp">The type of REST call to make to MdsLib</param>
        /// <param name="path">The path of the MdsLib resource</param>
        /// <param name="body">JSON body if any</param>
        /// <param name="prefixPath">optional prefix of the target URI before the device serial number (defaults to empty string)</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use ApiCallAsync<T>(MdsMovesenseDevice, ...) instead.")]
        Task<T> ApiCallAsync<T>(string deviceName, MdsOp restOp, string path, string body = null, string prefixPath = "");
        
        /// <summary>
        /// Function to start a subscription to an Mds resource
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="path">The path of the MdsLib resource</param>
        /// <param name="notificationCallback">Callback function that takes parameter of type T, where T is the return type from the subscription notifications</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use ApiSubscriptionAsync<T>(MdsMovesenseDevice, ...) instead.")]
        Task<IMdsSubscription> ApiSubscriptionAsync<T>(string deviceName, string path, Action<T> notificationCallback);

        /// <summary>
        /// Create a new logbook entry resource (increment log Id). Returns the new log Id.
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <returns>new Log Id</returns>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.CreateLogEntryAsync instead.")]
        Task<CreateLogResult> CreateLogEntryAsync(string deviceName);

        /// <summary>
        /// Delete all the Logbook entries
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.DeleteLogEntriesAsync instead.")]
        Task DeleteLogEntriesAsync(string deviceName);

        /// <summary>
        /// Get Accelerometer configuration
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetAccInfoAsync instead.")]
        Task<AccInfo> GetAccInfoAsync(string deviceName);

        /// <summary>
        /// Get Battery level, CallAsync returns BatteryResult
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetBatteryLevelAsync instead.")]
        Task<BatteryResult> GetBatteryLevelAsync(string deviceName);

        /// <summary>
        /// Get info on the app running on the device
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetAppInfoAsync instead.")]
        Task<AppInfo> GetAppInfoAsync(string deviceName);

        /// <summary>
        /// Get device info
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetDeviceInfoAsync instead.")]
        Task<DeviceInfoResult> GetDeviceInfoAsync(string deviceName);

        /// <summary>
        /// Get Gyrometer configuration
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetGyroInfoAsync instead.")]
        Task<GyroInfo> GetGyroInfoAsync(string deviceName);

        /// <summary>
        /// Get IMU configuration
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetIMUInfoAsync instead.")]
        Task<IMUInfo> GetIMUInfoAsync(string deviceName);

        /// <summary>
        /// Get state of all Leds in the system
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetLedsStateAsync instead.")]
        Task<LedsResult> GetLedsStateAsync(string deviceName);

        /// <summary>
        /// Get LedState for an LED
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="ledIndex">Number of the Led</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetLedStateAsync(int) instead.")]
        Task<LedState> GetLedStateAsync(string deviceName, int ledIndex = 0);

        /// <summary>
        /// Get data from a Logbook entry in SBEM format by accessing the suunto://{serial}/Mem/Logbook/ByID/{ID}/Data REST endpoint
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="logId">Number of the entry to get</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetLogbookDataAsync(int) instead.")]
        Task<string> GetLogbookDataAsync(string deviceName, int logId);

        /// <summary>
        /// Get data from a Logbook entry as JSON by accessing the suunto://MDS/Logbook/{serial}>/ByID/{ID}/Data REST endpoint. 
        /// This MDS Logbook proxy service takes care of paging and also data-json conversion.  
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="logId">Number of the entry to get</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetLogbookDataJsonAsync(int) instead.")]
        Task<string> GetLogbookDataJsonAsync(string deviceName, int logId);

        /// <summary>
        /// Get Descriptors for a Logbook entry
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="logId">Logbook entry to get</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetLogbookDataJsonAsync(int) instead.")]
        Task<BaseResult> GetLogbookDescriptorsAsync(string deviceName, int logId);

        /// <summary>
        /// Get details of Logbook entries by accessing the suunto://MDS/Logbook/{serial}>/Entries" REST endpoint. 
        /// This MDS Logbook proxy service takes care of paging and also data-json conversion.
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetLogEntriesJsonAsync instead.")]
        Task<LogEntriesMDSResult> GetLogEntriesJsonAsync(string deviceName);

        /// <summary>
        /// Get details of Logbook entries by accessing the suunto://{serial}/Mem/Logbook/Entries REST endpoint
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetLogEntriesAsync instead.")]
        Task<LogEntriesResult> GetLogEntriesAsync(string deviceName);

        /// <summary>
        /// Get Logger status, CallAsync returns LogStatusResult object
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetLoggerStatusAsync instead.")]
        Task<LogStatusResult> GetLoggerStatusAsync(string deviceName);

        /// <summary>
        /// Get Magnetometer configuration
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetMagInfoAsync instead.")]
        Task<MagnInfo> GetMagInfoAsync(string deviceName);

        /// <summary>
        /// Gets current time in number of microseconds since epoch 1.1.1970 (UTC).
        /// If not explicitly set, contains number of seconds since reset.
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.GetTimeAsync instead.")]
        Task<TimeResult> GetTimeAsync(string deviceName);

        /// <summary>
        /// Sets state of an LED
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="ledIndex">Index of the Led - use 0 for standard Movesense sensor</param>
        /// <param name="ledOn">Set on or off</param>
        /// <param name="ledColor">[optional]value from LedColor enumeration - default is LedColor.Red</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.SetLedStateAsync(...) instead.")]
        Task SetLedStateAsync(string deviceName, int ledIndex, bool ledOn, LedColor ledColor = LedColor.Red);

        /// <summary>
        /// Set state of the Datalogger
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="start">Set true to start the datalogger, false to stop</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.SetLoggerStatusAsync(bool) instead.")]
        Task SetLoggerStatusAsync(string deviceName, bool start);

        /// <summary>
        /// Set clock time on the device to sync with the time on the phone, as number of microseconds since epoch 1.1.1970 (UTC).
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.SetTimeAsync instead.")]
        Task SetTimeAsync(string deviceName);

        /// <summary>
        /// Set configuration for the Datalogger - ONLY sets IMU9
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="freq">Sampling rate, e.g. 26 for 26Hz</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.SetupLoggerAsync(int) instead.")]
        Task SetupLoggerAsync(string deviceName, int freq = 26);

        /// <summary>
        /// Set configuration for the Datalogger
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="dataLoggerConfig">Configuration to apply to the DataLogger. Config is an array of structs containing paths to the subscription of data to log.
        /// For example:             
        /// DataLoggerConfig.DataEntry[] entries = { new DataLoggerConfig.DataEntry("/Meas/IMU9/" + freq) };
        /// DataLoggerConfig config = new DataLoggerConfig(new DataLoggerConfig.Config(new DataLoggerConfig.DataEntries(entries)));
        /// </param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.SetLoggerConfigAsync(DataLoggerConfig) instead.")]
        Task SetLoggerConfigAsync(string deviceName, DataLoggerConfig dataLoggerConfig);

        /// <summary>
        /// Subscribe to periodic linear acceleration measurements.
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="notificationCallback">Callback function to receive the AccData</param>
        /// <param name="sampleRate">Sampling rate, e.g. 26 for 26Hz</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.SubscribeAccelerometerAsync(...) instead.")]
        Task<IMdsSubscription> SubscribeAccelerometerAsync(string deviceName, Action<AccData> notificationCallback, int sampleRate = 26);

        /// <summary>
        /// Subscribe to periodic Gyrometer data
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="notificationCallback">Callback function to receive the GyroData</param>
        /// <param name="sampleRate">Sampling rate, e.g. 26 for 26Hz</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.SubscribeGyrometerAsync(...) instead.")]
        Task<IMdsSubscription> SubscribeGyrometerAsync(string deviceName, Action<GyroData> notificationCallback, int sampleRate = 26);

        /// <summary>
        /// Subscribe to periodic 6-axis IMU measurements (Acc + Gyro).
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="notificationCallback">Callback function to receive the IMU6Data</param>
        /// <param name="sampleRate">Sampling rate, e.g. 26 for 26Hz</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.SubscribeIMU6Async(...) instead.")]
        Task<IMdsSubscription> SubscribeIMU6Async(string deviceName, Action<IMU6Data> notificationCallback, int sampleRate = 26);

        /// <summary>
        /// Subscribe to periodic 9-axis IMU measurements.
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="notificationCallback">Callback function to receive the IMU9Data</param>
        /// <param name="sampleRate">Sampling rate, e.g. 26 for 26Hz</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.SubscribeIMU9Async(...) instead.")]
        Task<IMdsSubscription> SubscribeIMU9Async(string deviceName, Action<IMU9Data> notificationCallback, int sampleRate = 26);

        /// <summary>
        /// Subscribe to periodic Magnetometer data measurements
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="notificationCallback">Callback function to receive the MagnData</param>
        /// <param name="sampleRate">Sampling rate, e.g. 26 for 26Hz</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.SubscribeMagnetometerAsync(...) instead.")]
        Task<IMdsSubscription> SubscribeMagnetometerAsync(string deviceName, Action<MagnData> notificationCallback, int sampleRate = 26);

        /// <summary>
        /// Subscribe to device time notifications
        /// </summary>
        /// <param name="deviceName">Name of the device, e.g. Movesense 174430000051</param>
        /// <param name="notificationCallback">Callback function to receive the time data</param>
        [Obsolete("Methods specifying target device by deviceName are deprecated, please use MdsMovesenseDevice.SubscribeTimeAsync(...) instead.")]
        Task<IMdsSubscription> SubscribeTimeAsync(string deviceName, Action<TimeNotificationResult> notificationCallback);

        #endregion
    }
}
