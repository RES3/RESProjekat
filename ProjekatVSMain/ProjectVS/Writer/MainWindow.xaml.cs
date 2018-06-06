using ClassLibrary;
using projekatRES3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Writer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static ILoadBalancerContract proxy = new DuplexChannelFactory<ILoadBalancerContract>(new InstanceContext(new WriterCallBack()),new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:8018/LoadBalancer")).CreateChannel();

        private ObservableCollection<WorkerModel> workersList = new ObservableCollection<WorkerModel>();

        private bool aktivirajbuttonEnabled;
        private bool deaktivirajbuttonEnabled;

        #region Properties
        public bool AktivirajButtonEnabled
        {
            get { return aktivirajbuttonEnabled; }
            set
            {
                aktivirajbuttonEnabled = value;
                OnPropertyChanged("AktivirajButtonEnabled");
            }
        }
        public bool DeaktivirajButtonEnabled
        {
            get { return deaktivirajbuttonEnabled; }
            set
            {
                deaktivirajbuttonEnabled = value;
                OnPropertyChanged("DeaktivirajButtonEnabled");
            }
        }

        public ObservableCollection<WorkerModel> WorkersList
        {
            get { return workersList; }
            set
            {
                workersList = value;
                OnPropertyChanged("WorkersList");
            }
        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Thread myThread = new Thread(GenerateData);
            myThread.Start();
        }
        public void GenerateData()
        {
            while (true)
            {
                Random random = new Random();
                try
                {
                    proxy.WriteToLoadBalancer((Code)random.Next(8), random.Next(10000));
                }
                catch (Exception)
                {
                    Console.WriteLine("Load Balancer nije dostupan");
                    RecreateChannel();
                }

                Thread.Sleep(400); //salje svake dvije sekunde
            }
        }
        public void RecreateChannel()
        {
            proxy = new DuplexChannelFactory<ILoadBalancerContract>(new InstanceContext(new WriterCallBack()), new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:8018/LoadBalancer")).CreateChannel();
        }

        public void refresh_click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WorkerModel> pom = proxy.GetAllWorkers();
                WorkersList.Clear();
                pom.ForEach(x => WorkersList.Add(x));
            }
            catch (Exception)
            {
                Console.WriteLine("Bad connection");
            }
        }

        private void activiraj_click(object sender, RoutedEventArgs e)
        {
            if (workers.SelectedItem != null)
            {
                WorkerModel wm = workers.SelectedItem as WorkerModel;
                proxy.RequestForTurnOnOff(true, wm.Ime);
                Thread.Sleep(500);
                refresh_click(null, null);
            }
        }

        private void deaktiviraj_click(object sender, RoutedEventArgs e)
        {
            if (workers.SelectedItem != null)
            {
                WorkerModel wm = workers.SelectedItem as WorkerModel;
                proxy.RequestForTurnOnOff(false, wm.Ime);
                Thread.Sleep(500);
                refresh_click(null, null);
            }
        }

        private void dataGridSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (workers.SelectedItem != null)
            {
                WorkerModel wm = workers.SelectedItem as WorkerModel;
                if (wm.Activan == true)
                {
                    AktivirajButtonEnabled = false;
                    DeaktivirajButtonEnabled = true;
                }
                else
                {
                    AktivirajButtonEnabled = true;
                    DeaktivirajButtonEnabled = false;
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
