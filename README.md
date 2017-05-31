# Smooth GPS Tracking with animation
GPS Tracking application is a nice but normal scenario. In our working experience before, there are always some difficulty standing on my way. The components we used have more or less some thing that cannot implement; or maybe the experience is too bad. Some points like:

- Lock vehicle's head north; then the map rotates and moves along with the vehicle
- Map rotates with transition animation
- Vehicle's head to the direction where it is moving
- Smooth panning the map while the vehicle is moving
- Smooth animation for the running vehicle

Today, we share a demo project that you could find everything I mentioned above. Hope it is useful for your project.

- [GPSTrackingDemo-Wpf](https://github.com/SlimGIS/GPSTrackingDemo-Wpf)

Here is a video to show you how the sample works.

<iframe width="560" height="315" src="https://www.youtube.com/embed/gXhg8Bnbf3o" frameborder="0" allowfullscreen></iframe>

[Watch on YouTube](https://youtu.be/gXhg8Bnbf3o)

Here are some screenshots.
![GPSTracking_Normal](https://raw.githubusercontent.com/SlimGIS/GPSTrackingDemo-Wpf/master/Scrennshots/Screenshot_Normal.png)

![GPSTracking_HeadNorth](https://raw.githubusercontent.com/SlimGIS/GPSTrackingDemo-Wpf/master/Scrennshots/Screenshot_HeadNorth.png)

In this project, the time frame is 10 times faster than the real world. You could adjust the `ellapsedTimeSpeedUpRatio` field.

Happy Mapping!
SlimGIS
