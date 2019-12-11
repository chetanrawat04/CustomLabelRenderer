using DemoOfLabelRenderer.iOS;
using Foundation;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace DemoOfLabelRenderer.iOS
{
    public class CustomLabelRenderer : ViewRenderer<Label, UITextView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new UITextView());
                }

                UpdateText();
            }

            base.OnElementChanged(e);
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(Label.Text))
            {
                UpdateText();
            }
        }
        void UpdateText()
        {
            if (string.IsNullOrWhiteSpace(Element?.Text))
            {
                Control.Text = string.Empty;
                return;
            }

            NSError error = null;
            Control.AttributedText = new NSAttributedString(NSData.FromString(Element.Text),
                                                            new NSAttributedStringDocumentAttributes { DocumentType = NSDocumentType.PlainText },
                                                            ref error);

            switch (Element.FontAttributes)
            {
                case FontAttributes.None:
                    Control.Font = UIFont.SystemFontOfSize(new nfloat(Element.FontSize));
                    break;
                case FontAttributes.Bold:
                    Control.Font = UIFont.BoldSystemFontOfSize(new nfloat(Element.FontSize));
                    break;
                case FontAttributes.Italic:
                    Control.Font = UIFont.ItalicSystemFontOfSize(new nfloat(Element.FontSize));
                    break;
                default:
                    Control.Font = UIFont.BoldSystemFontOfSize(new nfloat(Element.FontSize));
                    break;
            }
            Control.Selectable = true;
            Control.Editable = false;
            Control.ScrollEnabled = false;
            Control.ShouldInteractWithUrl += delegate { return true; };
        }
    }
}