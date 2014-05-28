HttpHandlerForWatermarkImage Usage
===================================

Installation
============

PM> Install-Package WatermarkingImage

Configuration
=============

Create a new class that is derived from ImageHandler class.

```
 public class SampleImageHelper : ImageHandler
    {
        public SampleImageHelper()
            : base("My watermark!")
        {
        }
    }
```


Find the following configuration section on web.config file,

```xml
  <system.web>
    <httpHandlers>
      <add verb="GET" type="WebSampleProject.SampleImageHelper" path="*.jpg,*.png,*.gif,*.bmp"/>
    </httpHandlers>
  </system.web>
```

