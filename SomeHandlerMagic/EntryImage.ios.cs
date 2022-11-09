using System;
using System.Drawing;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace SomeHandlerMagic
{
	public static partial class EntryImageHandler
	{
		public static void RegisterHandler()
		{
			EntryHandler.PlatformViewFactory = (handler) =>
			{
				var textField = new MauiTextField();

				if (handler.VirtualView is not EntryImage element)
					return textField;

				if (!string.IsNullOrEmpty(element.Image))
				{
					switch (element.ImageAlignment)
					{
						case ImageAlignment.Left:
							textField.LeftViewMode = UITextFieldViewMode.Always;
							textField.LeftView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
							break;
						case ImageAlignment.Right:
							textField.RightViewMode = UITextFieldViewMode.Always;
							textField.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
							break;
					}
				}
				textField.BorderStyle = UITextBorderStyle.Line;
				textField.Layer.MasksToBounds = true;

				return textField;
			};
		}

		static UIView GetImageView(string imagePath, int height, int width)
		{
			var uiImageView = new UIImageView(UIImage.FromFile(imagePath))
			{
				Frame = new RectangleF(0, 0, width, height)
			};
			UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width + 10, height));
			objLeftView.AddSubview(uiImageView);

			return objLeftView;
		}
	}
}

