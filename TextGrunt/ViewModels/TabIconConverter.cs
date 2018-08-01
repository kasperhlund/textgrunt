using System;
using System.Globalization;
using System.Windows.Data;

namespace TextGrunt.ViewModels
{
    public class TabIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var icon = value as IHaveIcon;

            if (icon?.IconType == IconType.ClipBoard)
                return "../Assets/scissors.Png";

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}