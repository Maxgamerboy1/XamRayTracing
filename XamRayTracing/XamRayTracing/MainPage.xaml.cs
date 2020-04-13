using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace XamRayTracing
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            ovhRoom.FPSView = fpsView;
        }


        private double fov = 60;
        public double FOV
        {
            get => fov;
            set
            {
                value = Math.Ceiling(value);
                if (fov != value)
                {
                    fov = value;
                    ovhRoom.LightSource.SetRays(fov);
                    ovhRoom.InvalidateSurface();
                    OnPropertyChanged();
                }
            }
        }


        private double __Heading;
        public double Heading
        {
            get => __Heading;
            set
            {
                if (__Heading != value)
                {
                    __Heading = value;
                    ovhRoom.LightSource.SetDirection(__Heading);
                    ovhRoom.InvalidateSurface();
                    OnPropertyChanged();
                }
            }
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (sender is OverheadRoom _Room)
            {
                switch (e.StatusType)
                {
                    case GestureStatus.Started:
                        _Room.BeginTranslation();
                        break;
                    case GestureStatus.Running:
                        _Room.TranslateLightSource(e.TotalX, e.TotalY);
                        break;
                    case GestureStatus.Completed:
                    case GestureStatus.Canceled:
                    default:
                        _Room.EndTranslation();
                        break;
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is OverheadRoom _Room)
            {
                _Room.ChangeBoundaries();
            }
        }
    }
}
