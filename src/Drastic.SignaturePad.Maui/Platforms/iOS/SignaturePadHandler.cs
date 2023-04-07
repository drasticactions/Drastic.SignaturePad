using System;
using Microsoft.Maui.Handlers;

namespace Drastic.SignaturePad.Maui.Handler
{
    public partial class SignaturePadHandler : ViewHandler<Drastic.SignaturePad.Maui.SignaturePadView, Xamarin.Controls.SignaturePadView>
    {
        protected override Xamarin.Controls.SignaturePadView CreatePlatformView() => new Xamarin.Controls.SignaturePadView();

        protected override void ConnectHandler(Xamarin.Controls.SignaturePadView platformView)
        {
            base.ConnectHandler(platformView);

            // Perform any control setup here
        }

        protected override void DisconnectHandler(Xamarin.Controls.SignaturePadView platformView)
        {
            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }
    }
}

