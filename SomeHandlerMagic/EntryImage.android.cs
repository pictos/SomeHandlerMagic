using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.Content;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace SomeHandlerMagic
{
	public static partial class EntryImageHandler
	{
		public static void RegisterHandler()
		{
			EntryHandler.PlatformViewFactory = (handler) =>
			{
				var editText = new AppCompatEditText(handler.Context);
				if (handler.VirtualView is not EntryImage element)
					return editText;

				if (!string.IsNullOrEmpty(element.Image))
				{
					switch (element.ImageAlignment)
					{
						case ImageAlignment.Left:
							editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image, handler.Context, element), null, null, null);
							break;
						case ImageAlignment.Right:
							editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.Image, handler.Context, element), null);
							break;
					}
				}

				editText.CompoundDrawablePadding = 25;
				editText.Background.SetColorFilter(Colors.White.ToPlatform(), PorterDuff.Mode.SrcAtop);

				return editText;
			};
		}

		static BitmapDrawable GetDrawable(string imageEntryImage, Context context, EntryImage element)
		{
			int resID = context.Resources.GetIdentifier(imageEntryImage, "drawable", context.PackageName);
			var drawable = ContextCompat.GetDrawable(context, resID);
			var bitmap = drawableToBitmap(drawable);

			return new BitmapDrawable(Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
		}

		static Bitmap drawableToBitmap(Drawable drawable)
		{
			if (drawable is BitmapDrawable)
			{
				return ((BitmapDrawable)drawable).Bitmap;
			}

			int width = drawable.IntrinsicWidth;
			width = width > 0 ? width : 1;
			int height = drawable.IntrinsicHeight;
			height = height > 0 ? height : 1;

			Bitmap bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
			Canvas canvas = new Canvas(bitmap);
			drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
			drawable.Draw(canvas);

			return bitmap;
		}
	}
}

