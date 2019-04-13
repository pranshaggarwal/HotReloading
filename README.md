# Hot-Reloading tool for .Net Platforms
This tool is suppose to work on all the .net platforms which support dynamic code execution but currently all the testing is done on Xamarin.Forms

![preview](/gif/preview.gif)

## Current Features
1. Update exiting methods and preview the changes
2. Support loop statements
3. Support branch statements
4. Support Async/Await statements and many more statements....

## Its Future
This tool can be extended for many cases like
1. Add new methods
2. Add new fields/properties
3. Add new classes
4. Implement interface to existing classes
5. Dynamically load nuget package
6. Dynamically load newly added image on Xamarin platforms
8. Support for Xaml update
9. Support for html/javascript/rajor (not sure if it will be needed)
7. Support for debugger (may be it will required update on .net Runtime)
8. Support for reflection api (this may also required update on .net Runtime)
9. Support for various platform like Xamarin.iOS, Xamarin.Android, Asp.Net, WPF, Test Runner etc.

## Demo
https://youtu.be/iIYNJheYxFo

## Attribution
Special thanks to [@bartdesmet](https://github.com/bartdesmet) because of his work on [ExpressionFutures](https://github.com/bartdesmet/ExpressionFutures/tree/master/CSharpExpressions) I was able to add support for async/await.
Also thanks to [@ylatuya](https://twitter.com/ylatuya) I used his work on [XAMLator](https://github.com/ylatuya/XAMLator) to established communication between IDE and devices.
