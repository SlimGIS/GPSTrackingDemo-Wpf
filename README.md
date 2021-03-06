Keywords: gps tracking, vehicle track, vehicle animation, rotate map

# Smooth GPS Tracking with animation

<desc>GPS Tracking application is a nice but normal scenario. For example, lock vehicle head to north while moving; map direction automatically rotates with the vehicle's direction etc. This project will introduce you how easily to build this kind of map application with Map Kit GPS Scene.</desc> The items we are trying to build are listed below.

- Lock vehicle's head north; then the map rotates and moves along with the vehicle
- Map rotates with transition animation
- Vehicle's head to the direction where it is moving
- Smooth panning the map while the vehicle is moving
- Smooth animation for the running vehicle

This guide introduces a demo project that you could find everything I mentioned above. Hope it is useful for your project.

- [GPSTrackingDemo-Wpf](https://github.com/SlimGIS/GPSTrackingDemo-Wpf)

Here is a video to show you how the sample works.

<iframe width="560" height="315" src="https://www.youtube.com/embed/gXhg8Bnbf3o" frameborder="0" allowfullscreen></iframe>

[Watch on YouTube](https://youtu.be/gXhg8Bnbf3o)

Here are some screenshots.
![GPSTracking_Normal](https://raw.githubusercontent.com/SlimGIS/GPSTrackingDemo-Wpf/master/Scrennshots/Screenshot_Normal.png)

![GPSTracking_HeadNorth](https://raw.githubusercontent.com/SlimGIS/GPSTrackingDemo-Wpf/master/Scrennshots/Screenshot_HeadNorth.png)

In this project, the time frame is 10 times faster than the real world. You could adjust the `ellapsedTimeSpeedUpRatio` field.

## Related Resources
- [Source Code](https://github.com/SlimGIS/GPSTrackingDemo-Wpf)
- [Video](https://youtu.be/gXhg8Bnbf3o)
- [Multiple vehicles tracking sample](https://slimgis.com/documents/gps-tracking-multi-vehicle-wpf)
