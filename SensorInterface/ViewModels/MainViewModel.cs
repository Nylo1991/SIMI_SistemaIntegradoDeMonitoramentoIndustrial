using SensorInterface.ViewModels;
using SensorInterface.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shared;

namespace SensorInterface.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<double> Temperaturas { get; set; }

        public ICommand CarregarSensoresCommand { get; }

        public MainViewModel()
        {
            Temperaturas = new ObservableCollection<double>();

            // Comandos:
            CarregarSensoresCommand = new RelayCommand(CarregarSensores);
        }

        private async void CarregarSensores()
        {
            var http = new HttpClient();

            var dados = await http.GetFromJsonAsync<List<SensorData>>(
                "https://localhost:7007/api/v1/sensores");

            Temperaturas.Clear();

            foreach (var sensor in dados)
            {
                Temperaturas.Add(sensor.Temperatura);
            }
        }
    }
}