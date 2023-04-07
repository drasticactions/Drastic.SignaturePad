using Drastic.SignaturePad.Maui;

namespace Drastic.SignaturePadSampleMaui;

public partial class MainPage : ContentPage
{
    int count = 0;

    private Point[] points;

    public MainPage()
    {
        InitializeComponent();

        UpdateControls();
    }

    private void UpdateControls()
    {
        btnSave.IsEnabled = !signatureView.IsBlank;
        btnSaveImage.IsEnabled = !signatureView.IsBlank;
        btnLoad.IsEnabled = points != null;
    }

    private void SaveVectorClicked(object sender, EventArgs e)
    {
        points = signatureView.Points.ToArray();
        UpdateControls();

        DisplayAlert("Signature Pad", "Vector signature saved to memory.", "OK");
    }

    private void LoadVectorClicked(object sender, EventArgs e)
    {
        signatureView.Points = points;
    }

    private async void SaveImageClicked(object sender, EventArgs e)
    {
        bool saved = false;
        using (var bitmap = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png, Color.FromArgb("#000000"), Color.FromArgb("#ffffff"), 1f))
        {
            //  saved = await App.SaveSignature(bitmap, "signature.png");
        }

        if (saved)
            await DisplayAlert("Signature Pad", "Raster signature saved to the photo library.", "OK");
        else
            await DisplayAlert("Signature Pad", "There was an error saving the signature.", "OK");
    }

    private void SignatureChanged(object sender, EventArgs e)
    {
        UpdateControls();
    }
}

