using SlimGis.MapKit.Controls;
using SlimGis.MapKit.Geometries;
using SlimGis.MapKit.Layers;
using SlimGis.MapKit.Symbologies;
using SlimGis.MapKit.Utilities;
using SlimGis.MapKit.Wpf;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GPSTrackingDemo
{
    public partial class MainWindow : Window
    {
        private static readonly double speedKph = 140;
        private static readonly int refreshInterval = 16;
        private static readonly double rotationIncrement = 5;
        private GeoLine routeLine;
        private double routeLength;
        private bool keepHeadNorth;
        private double ellapsedTimeSpeedUpRatio = 10; // means 10 times faster than real time.
        private Proj4Projection projection;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void wpfMap_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeoUnit.Meter;

            LayerOverlay baseOverlay = new LayerOverlay();
            baseOverlay.Name = "Base Maps";
            Map1.Overlays.Add(baseOverlay);

            OpenStreetMapLayer baseLayer = new OpenStreetMapLayer();
            baseOverlay.Layers.Add(baseLayer);

            routeLine = new GeoLine("LINESTRING(2121735.25 6023143.5,2121641.25 6023344,2121161.25 6023667,2121192.75 6025693,2121255.5 6025818.5,2122127.25 6024946.5,2121986.25 6024802.5,2122262.25 6024536,2122014.5 6024363.5,2122691.75 6023497.5,2121754 6023137)");
            routeLength = routeLine.GetLength(GeoUnit.Meter, LengthUnit.Kilometer);
            MemoryLayer routeLayer = new MemoryLayer();
            routeLayer.Styles.Add(new LineStyle(GeoColor.FromRgba(GeoColors.SkyBlue, 180), 10));
            routeLayer.Features.Add(new Feature(routeLine));
            Map1.AddStaticLayers("Route Line", routeLayer);

            Marker vehicle = new Marker();
            vehicle.DropShadow = false;
            vehicle.OffsetY = 4;
            vehicle.RenderTransform = new RotateTransform { CenterX = 17, CenterY = 7, Angle = -90 };
            vehicle.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/vehicle-red.png", UriKind.RelativeOrAbsolute));
            vehicle.Location = routeLine.Coordinates.First();
            Map1.Placements.Add(vehicle);

            GeoBound bound = routeLayer.GetBound();
            bound.ScaleUp(40);
            Map1.ZoomTo(bound);
            projection = new Proj4Projection(SpatialReferences.GetSphericalMercator(), SpatialReferences.GetWgs84());
            UpdateLonLat(new GeoPoint(vehicle.Location));
        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Button playButton = (Button)sender;
            SettingLayout.IsEnabled = false;

            GeoCoordinate position = routeLine.Coordinates.First();

            int ellapsedTime = 0;
            GeoPoint previousePoint = null;
            bool quiteTracking = false;
            double targetRotation = 0;
            while (true)
            {
                double divenDistance = ellapsedTime / (1000 * 60 * 60 / ellapsedTimeSpeedUpRatio) * speedKph;
                if (divenDistance > routeLength)
                {
                    divenDistance = routeLength;
                    quiteTracking = true;
                }

                GeoPoint currentPosition = await Task.Run(() => routeLine.GetPoint(divenDistance, LengthUnit.Kilometer, GeoUnit.Meter));

                Marker vehicle = (Marker)Map1.Placements.First();
                vehicle.DropShadow = false;
                vehicle.Location = new GeoCoordinate(currentPosition);

                UpdateLonLat(currentPosition);

                MapViewport newViewport = new MapViewport(new GeoPoint(vehicle.Location), Map1.CurrentScale, Map1.Rotation);
                if (previousePoint != null)
                {
                    double angle = -Math.Atan2(currentPosition.Y - previousePoint.Y, currentPosition.X - previousePoint.X) * 180 / Math.PI;
                    angle = Math.Round(angle, 0);

                    if (keepHeadNorth) targetRotation = -angle - 90;
                    else
                    {
                        RotateTransform trans = (RotateTransform)vehicle.RenderTransform;
                        trans.Angle = angle;
                    }
                }

                if (keepHeadNorth)
                {
                    newViewport.Rotation = RotationHelper.GetRotatedAngle(targetRotation, Map1.Rotation, rotationIncrement);
                    Map1.Viewport = newViewport;
                }

                if (quiteTracking) break;

                previousePoint = currentPosition;
                await Task.Run(() => Thread.Sleep(refreshInterval));
                ellapsedTime += refreshInterval;
            }

            SettingLayout.IsEnabled = true;
        }

        private void UpdateLonLat(GeoPoint currentPosition)
        {
            GeoPoint positionInWgs84 = projection.ConvertToTarget(currentPosition);
            LonTextBox.Text = positionInWgs84.X.ToString("N4");
            LatTextBox.Text = positionInWgs84.Y.ToString("N4");
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            keepHeadNorth = ((CheckBox)sender).IsChecked.Value;
            if (keepHeadNorth)
            {
                Marker vehicle = (Marker)Map1.Placements.First();
                ((RotateTransform)vehicle.RenderTransform).Angle = -90;
            }
        }
    }
}
