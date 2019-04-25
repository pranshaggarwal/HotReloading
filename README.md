# C# HOT-RELOADING
Allow Developer to Update C# code at runtime

![preview](/gif/preview.gif)

## Current Features
1. Update exiting methods and preview the changes.
2. Add new methods.
3. Create View from C# and see the changes immediately.
4. Update business logic at while app is running.
5. Write Custom Renderers while app is running.

## SETUP
#### VS for MAC
Download **HotReloading** extension from extension manager or directly from here: http://addins.monodevelop.com/Project/Index/377

#### VS for Window
Extension will be available soon.

#### Code Setup
1. Install pre-release [HotReloading](https://www.nuget.org/packages/HotReloading) [![NuGet](https://img.shields.io/nuget/v/HotReloading.svg)](https://www.nuget.org/packages/HotReloading) package on all your project where you want to support Hot-Reloading
2. Install pre-release [HotReloading.Forms](https://www.nuget.org/packages/HotReloading.Forms) [![NuGet](https://img.shields.io/nuget/v/HotReloading.Forms.svg)](https://www.nuget.org/packages/HotReloading.Forms) package on all platform project and Xamarin.Forms project which has App.xaml.cs
3. Call ```Initializer.Init()``` on each project

```csharp

//Xamarin.Forms
public partial class App : Application
    {
        .......................

        protected override void OnStart()
        {
#if DEBUG
            HotReloading.Forms.Initialiser.Init();
#endif
        }
        .......................
    }
    
//Xamarin.Android
protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

#if DEBUG
            HotReloading.Android.Initialiser.Init(this, savedInstanceState);
#endif

            LoadApplication(new App());
        }
        
//Xamarin.iOS
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

#if DEBUG
            HotReloading.iOS.Initialiser.Init();
#endif

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
```

## Android Emulator Setup
To run it in the android emulator you will have to reverse the TCP port 8555 so the emulator can reach the IDE when connection to localhost:8555

```bash
$ adb reverse tcp:8555 tcp:8555
```

## Demo
https://youtu.be/nG2JSg-vZyk

## Contribution
Feel free to create issue and feature request. It is just first version of the tool and with the support of community we can extend it to full fledged hot-reloading.

## Attribution
Special thanks to [@bartdesmet](https://github.com/bartdesmet) because of his work on [ExpressionFutures](https://github.com/bartdesmet/ExpressionFutures/tree/master/CSharpExpressions) I was able to add support for async/await.
Also thanks to [@ylatuya](https://twitter.com/ylatuya) I used his work on [XAMLator](https://github.com/ylatuya/XAMLator) to established communication between IDE and devices.
