#if IOS || MACCATALYST
using PlatformView = Xamarin.Controls.SignaturePadView;
#elif ANDROID
using PlatformView = Xamarin.Controls.SignaturePadView;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif

using Microsoft.Maui.Handlers;

namespace Drastic.SignaturePad.Maui.Handler
{
    public partial class SignaturePadHandler
    {
        public SignaturePadHandler(IPropertyMapper mapper, CommandMapper commandMapper = null)
            : base(mapper, commandMapper)
        {
        }

        public SignaturePadHandler() : base(PropertyMapper)
        {
        }

        public static IPropertyMapper<SignaturePadView, SignaturePadHandler> PropertyMapper = new PropertyMapper<SignaturePadView, SignaturePadHandler>()
        {
        };
    }
}

