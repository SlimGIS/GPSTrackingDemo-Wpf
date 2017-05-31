using SlimGis.MapKit.Utilities;
using System;

namespace GPSTrackingDemo
{
    public static class RotationHelper
    {
        public static double GetRotatedAngle(double targetRotation, double currentRotation, double increment)
        {
            currentRotation = Math.Round(currentRotation, 0);
            if (targetRotation != currentRotation)
            {
                targetRotation = GeoCommonHelper.GetAngleInRange(targetRotation);
                currentRotation = GeoCommonHelper.GetAngleInRange(currentRotation);
                double rotationDiff = Math.Abs(targetRotation - currentRotation);
                if (rotationDiff < 180)
                {
                    if (targetRotation > currentRotation)
                    {
                        currentRotation += increment;
                        if (currentRotation > targetRotation) currentRotation = targetRotation;
                    }
                    else
                    {
                        currentRotation -= increment;
                        if (currentRotation < targetRotation) currentRotation = targetRotation;
                    }
                }
                else
                {
                    if (targetRotation > currentRotation)
                    {
                        currentRotation -= increment;
                        if (currentRotation < targetRotation - 360) currentRotation = targetRotation;
                    }
                    else
                    {
                        currentRotation += increment;
                        if (currentRotation > targetRotation + 360) currentRotation = targetRotation;
                    }
                }
            }

            return currentRotation;
        }
    }
}
