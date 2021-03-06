﻿using GalaSoft.MvvmLight;
using Plugin.BluetoothLE;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class SelectDevicePageViewModel : ViewModelBase
    {
        IDisposable scan;

        public IAdapter BleAdapter => CrossBleAdapter.Current;

        public ObservableCollection<MovesenseDeviceViewModel> Devices { private set; get; }

        public SelectDevicePageViewModel()
        {
            Devices = new ObservableCollection<MovesenseDeviceViewModel>();

            Application.Current.Properties.Remove("SelectedSensorId");
        }

        public void Init()
        {
            Application.Current.Properties.Remove("SelectedSensorId");
        }

        public void HandleSelectedItem(Guid id)
        {
            MovesenseDeviceViewModel item = Devices.First(d => d.Uuid == id);
            item.IsSelected = !item.IsSelected;
            if (item.IsSelected)
            {
                App.Locator.LinearAccelerationPageVM.MovesenseDeviceVM = item;
            }
            // If none selected, clear setting
            if (Devices.FirstOrDefault(x => x.IsSelected) == null)
            {
                App.Locator.LinearAccelerationPageVM.MovesenseDeviceVM = null;
            }
        }

        public void StartScanning()
        {
            this.Devices.Clear();

            CrossBleAdapter.Current.WhenStatusChanged().Subscribe(status =>
            {
                if (status == AdapterStatus.PoweredOn)
                {
                    scan = this.BleAdapter.Scan()
                    .Subscribe(this.OnScanResult);
                }
            });
        }

        public void StopScanning()
        {
            this.scan?.Dispose();
        }

        void OnScanResult(IScanResult result)
        {
            // Only interested in Movesense devices
            if (result.Device.Name != null)
            {
                if (result.Device.Name.StartsWith("Movesense"))
                {
                    var dev = this.Devices.FirstOrDefault(x => x.Uuid.Equals(result.Device.Uuid));
                    if (dev != null)
                    {
                        dev.TrySet(result);
                    }
                    else
                    {
                        dev = new MovesenseDeviceViewModel();
                        dev.TrySet(result);
                        this.Devices.Add(dev);
                    }
                }
            }
        }
    }
}
