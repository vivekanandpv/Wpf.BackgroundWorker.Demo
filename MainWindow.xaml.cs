using System;
using System.Collections.Generic;
using System.Linq;
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

namespace BackgroundWorker.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.ComponentModel.BackgroundWorker _backgroundWorker;
        public MainWindow()
        {
            InitializeComponent();
            _backgroundWorker = new System.ComponentModel.BackgroundWorker();
            _backgroundWorker.WorkerReportsProgress = true;
            
            statusLabel.Content = "Click Start for the Demo";

            _backgroundWorker.DoWork += (sender, args) =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(100);
                    _backgroundWorker.ReportProgress(i);
                }

                if (_backgroundWorker.CancellationPending)
                {
                    args.Cancel = true;
                    _backgroundWorker.ReportProgress(0);
                }
            };

            _backgroundWorker.ProgressChanged += (sender, args) =>
            {
                progressBar.Value = args.ProgressPercentage;
                statusLabel.Content = $"Progress: {args.ProgressPercentage}%";
            };

            _backgroundWorker.RunWorkerCompleted += (sender, args) =>
            {
                MessageBox.Show("Done!");
                progressBar.Value = 0;
                statusLabel.Content = $"Click Start for the Demo";
                startButton.IsEnabled = true;
            };

            startButton.Click += (sender, args) =>
            {
                startButton.IsEnabled = false;
                _backgroundWorker.RunWorkerAsync();
            };

            this.Closed += (sender, args) =>
            {
                Application.Current.Shutdown();
            };
        }
    }
}
