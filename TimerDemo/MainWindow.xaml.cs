//-----------------------------------------------------------------------
// <copyright file="MainWindow.cs" company="Lifeprojects.de">
//     Class: MainWindow
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>29.01.2026</date>
//
// <summary>
// Timervarianten unter NET 10
// </summary>
//-----------------------------------------------------------------------

namespace TimerDemo
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const string DATEFORMAT = "dd.MM.yyyy HH:mm:ss";
        private DispatcherTimer dispatcherTimer;
        private Timer threadingTimer;
        private System.Timers.Timer systemTimersTimer;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            this.InitializeComponent();
            WeakEventManager<Window, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);
            WeakEventManager<Window, CancelEventArgs>.AddHandler(this, "Closing", this.OnWindowClosing);

            this.WindowTitel = "Timervarianten unter NET 10";
            this.DataContext = this;
        }

        private string _WindowTitel;

        public string WindowTitel
        {
            get { return _WindowTitel; }
            set
            {
                if (this._WindowTitel != value)
                {
                    this._WindowTitel = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private string _Timervarianten;

        public string Timervarianten
        {
            get { return _Timervarianten; }
            set
            {
                if (this._Timervarianten != value)
                {
                    this._Timervarianten = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private string _TimerResult;

        public string TimerResult
        {
            get { return _TimerResult; }
            set
            {
                if (this._TimerResult != value)
                {
                    this._TimerResult = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            WeakEventManager<Button, RoutedEventArgs>.AddHandler(this.BtnCloseApplication, "Click", this.OnCloseApplication);
            WeakEventManager<Button, RoutedEventArgs>.AddHandler(this.BtnDispatcherTimer, "Click", this.OnBtnDispatcherTimerClick);
        }

        private void OnCloseApplication(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = false;

            MessageBoxResult msgYN = MessageBox.Show("Wollen Sie die Anwendung beenden?", "Beenden", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msgYN == MessageBoxResult.Yes)
            {
                App.ApplicationExit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void OnBtnDispatcherTimerClick(object sender, RoutedEventArgs e)
        {
            this.InitDispatcherTimer();
        }

        private void InitDispatcherTimer()
        {
            if (this.dispatcherTimer == null)
            {
                this.dispatcherTimer = new DispatcherTimer();
            }

            if (this.dispatcherTimer.IsEnabled == false)
            {
                this.dispatcherTimer.IsEnabled = true;
                this.dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                this.dispatcherTimer.Start();
                this.dispatcherTimer.Tick += new EventHandler(
                    delegate (object s, EventArgs a)
                    {
                        this.TimerResult = DateTime.Now.ToString(DATEFORMAT, CultureInfo.CurrentCulture);
                    });

                this.Timervarianten = "DispatcherTimer gestartet.";
            }
            else
            {
                this.dispatcherTimer.IsEnabled = false;
                this.dispatcherTimer.Stop();
                this.dispatcherTimer = null;
                this.TimerResult = string.Empty;
                this.Timervarianten = string.Empty;
            }
        }

        #region INotifyPropertyChanged implementierung
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler == null)
            {
                return;
            }

            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }
        #endregion INotifyPropertyChanged implementierung
    }
}