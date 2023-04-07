using System;
using Drastic.SignaturePad.Maui.Handler;

namespace Drastic.SignaturePad.Maui
{
    public static class MauiAppBuilderExtensions
    {
        /// <summary>
        /// Adds support for Signature Pad in MAUI.
        /// </summary>
        /// <param name="builder"><see cref="MauiAppBuilder"/>.</param>
        /// <returns>MauiAppBuilder.</returns>
        public static MauiAppBuilder AddSignaturePadSupport(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(Drastic.SignaturePad.Maui.SignaturePadView), typeof(SignaturePadHandler));
            });

            return builder;
        }
    }
}

