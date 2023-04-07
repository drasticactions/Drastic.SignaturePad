using System;
using Photos;
using Xamarin.Controls;

namespace Drastic.SignaturePadApple
{
    public class TestUIViewController : UIViewController
    {
        private CGPoint[] points;
        private readonly UIButton btnSave = UIButton.FromType(UIButtonType.RoundedRect);
        private readonly UIButton btnLoad = UIButton.FromType(UIButtonType.RoundedRect);
        private readonly UIButton btnSaveImage = UIButton.FromType(UIButtonType.RoundedRect);
        private readonly SignaturePadView signatureView = new SignaturePadView();

        public TestUIViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            setupUI();
            setupLayout();

            signatureView.Layer.BorderColor = UIColor.FromRGBA(184, 134, 11, 255).CGColor;
            signatureView.Layer.BorderWidth = 1f;

            signatureView.StrokeCompleted += (sender, e) => UpdateControls();
            signatureView.Cleared += (sender, e) => UpdateControls();

            UpdateControls();
        }

        private void setupLayout()
        {
            btnSaveImage.TrailingAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.TrailingAnchor).Active = true;
            signatureView.TopAnchor.ConstraintEqualTo(TopLayoutGuide.BottomAnchor, 20).Active = true;
            btnLoad.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;
            signatureView.TrailingAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.TrailingAnchor).Active = true;
            BottomLayoutGuide.TopAnchor.ConstraintEqualTo(btnSaveImage.BottomAnchor, 20).Active = true;
            btnSave.LeadingAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeadingAnchor).Active = true;
            BottomLayoutGuide.TopAnchor.ConstraintEqualTo(btnLoad.BottomAnchor, 20).Active = true;
            signatureView.LeadingAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeadingAnchor).Active = true;
            btnSave.TopAnchor.ConstraintEqualTo(signatureView.BottomAnchor, 20).Active = true;
            BottomLayoutGuide.TopAnchor.ConstraintEqualTo(btnSave.BottomAnchor, 20).Active = true;
            btnSaveImage.WidthAnchor.ConstraintEqualTo(50).Active = true;
            btnLoad.WidthAnchor.ConstraintEqualTo(50).Active = true;
            btnSave.WidthAnchor.ConstraintEqualTo(50).Active = true;
        }

        private void setupUI()
        {
            View.AddSubviews(btnSave, btnLoad, btnSaveImage, signatureView);

            signatureView.BackgroundColor = UIColor.FromRGBA(0.98039215686274506f, 0.98039215686274506f, 0.82352941176470584f, 1);
            signatureView.TranslatesAutoresizingMaskIntoConstraints = false;
            signatureView.SetValueForKey(NSObject.FromObject(0), new NSString("BackgroundImageContentMode"));
            signatureView.SetValueForKey(UIColor.FromRGBA(0.72156862745098038f, 0.52549019607843139f, 0.043137254901960784f, 1), new NSString("CaptionTextColor"));
            signatureView.SetValueForKey(UIColor.FromRGBA(0.72156862745098038f, 0.52549019607843139f, 0.043137254901960784f, 1), new NSString("ClearLabelTextColor"));
            signatureView.SetValueForKey(UIColor.FromRGBA(0.72156862745098038f, 0.52549019607843139f, 0.043137254901960784f, 1), new NSString("SignatureLineColor"));
            signatureView.SetValueForKey(UIColor.FromRGBA(0.72156862745098038f, 0.52549019607843139f, 0.043137254901960784f, 1), new NSString("SignaturePromptTextColor"));

            btnSaveImage.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            btnSaveImage.TitleLabel.LineBreakMode = UILineBreakMode.MiddleTruncation;
            btnSaveImage.TranslatesAutoresizingMaskIntoConstraints = false;
            btnSaveImage.SetTitle("Export", UIControlState.Normal);
            btnSaveImage.TouchUpInside += SaveImageClicked;

            btnLoad.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            btnLoad.TitleLabel.LineBreakMode = UILineBreakMode.MiddleTruncation;
            btnLoad.TranslatesAutoresizingMaskIntoConstraints = false;
            btnLoad.SetTitle("Load", UIControlState.Normal);
            btnLoad.TouchUpInside += LoadVectorClicked;

            btnSave.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            btnSave.TitleLabel.LineBreakMode = UILineBreakMode.MiddleTruncation;
            btnSave.TranslatesAutoresizingMaskIntoConstraints = false;
            btnSave.SetTitle("Save", UIControlState.Normal);
            btnSave.TouchUpInside += SaveVectorClicked;
        }

        private void UpdateControls()
        {
            btnSave.Enabled = !signatureView.IsBlank;
            btnSaveImage.Enabled = !signatureView.IsBlank;
            btnLoad.Enabled = points != null;
        }

        private void SaveVectorClicked(object sender, EventArgs e)
        {
            points = signatureView.Points;
            UpdateControls();

            ShowToast("Vector signature saved to memory.");
        }

        private void LoadVectorClicked(object sender, EventArgs e)
        {
            signatureView.LoadPoints(points);
        }

        async private void SaveImageClicked(object sender, EventArgs e)
        {
            UIImage image;
            using (var bitmap = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png, UIColor.Black, UIColor.White, 1f))
            using (var data = NSData.FromStream(bitmap))
            {
                image = UIImage.LoadFromData(data);
            }

            var status = await PHPhotoLibrary.RequestAuthorizationAsync();
            if (status == PHAuthorizationStatus.Authorized)
            {
                image.SaveToPhotosAlbum((i, error) =>
                {
                    image.Dispose();

                    if (error == null)
                        ShowToast("Raster signature saved to the photo library.");
                    else
                        ShowToast("There was an error saving the signature: " + error.LocalizedDescription);
                });
            }
            else
            {
                ShowToast("Permission to save to the photo library was denied.");
            }
        }

        private async void ShowToast(string message)
        {
            var toast = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            await PresentViewControllerAsync(toast, true);
            await Task.Delay(1000);
            await toast.DismissViewControllerAsync(true);
        }
    }
}

