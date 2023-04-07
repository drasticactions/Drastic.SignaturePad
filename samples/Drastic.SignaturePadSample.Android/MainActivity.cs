using Android.Graphics;
using Xamarin.Controls;

namespace Drastic.SignaturePadSample.Android;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    private System.Drawing.PointF[] points;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);

        var signatureView = FindViewById<SignaturePadView>(Resource.Id.signatureView);

        var btnSave = FindViewById<Button>(Resource.Id.btnSave);
        var btnLoad = FindViewById<Button>(Resource.Id.btnLoad);
        var btnSaveImage = FindViewById<Button>(Resource.Id.btnSaveImage);

        btnSave.Click += delegate
        {
            points = signatureView.Points;

            Toast.MakeText(this, "Vector signature saved to memory.", ToastLength.Short).Show();
        };

        btnLoad.Click += delegate
        {
            if (points != null)
                signatureView.LoadPoints(points);
        };

        btnSaveImage.Click += async delegate
        {
            //var path = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures).AbsolutePath;
            //var file = System.IO.Path.Combine(path, "signature.png");

            //using (var bitmap = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png, Color.Black, Color.White, 1f))
            //using (var dest = File.OpenWrite(file))
            //{
            //    await bitmap.CopyToAsync(dest);
            //}

            //Toast.MakeText(this, "Raster signature saved to the photo gallery.", ToastLength.Short).Show();
        };
    }
}