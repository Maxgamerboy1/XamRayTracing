using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            FOV = 60;
        }


        private double fov;
        public double FOV
        {
            get => fov;
            set
            {
                if (fov != value)
                {
                    fov = value;
                    ovhRoom.LightSource.SetRays(fov);
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
