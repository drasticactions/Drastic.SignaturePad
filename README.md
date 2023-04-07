[![NuGet Version](https://img.shields.io/nuget/v/Drastic.SignaturePad.svg)](https://www.nuget.org/packages/Drastic.SignaturePad/) ![License](https://img.shields.io/badge/License-MIT-blue.svg)

# Drastic.SignaturePad

Drastic.SignaturePad is a .NET/.NET MAUI Update to [SignaturePad](https://github.com/xamarin/SignaturePad), intended to help those who want to migrate to .NET, as well as an excuse for me to try and make a MAUI Handler.

WinUI/MAUI WinUI is not supported, as the `InkCanvas` control has not been ported to WinUI 3, making a current port impossible without rewriting.

# Setup

Open the `sample` directory for how to set it up for native platforms.

For .NET MAUI, 

```csharp
var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .AddSignaturePadSupport()
            ...
```

Add `AddSignaturePadSupport` to your builder. You should then be able to reference the `SignatureView` control.