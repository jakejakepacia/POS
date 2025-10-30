using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Extensions
{
    public static class GridExtension
    {
        public static readonly BindableProperty RowHeightsProperty =
      BindableProperty.CreateAttached(
          "RowHeights",
          typeof(IEnumerable<GridLength>),
          typeof(GridExtensions),
          null,
          propertyChanged: OnRowHeightsChanged);

        public static void SetRowHeights(BindableObject view, IEnumerable<GridLength> value) =>
            view.SetValue(RowHeightsProperty, value);

        public static IEnumerable<GridLength> GetRowHeights(BindableObject view) =>
            (IEnumerable<GridLength>)view.GetValue(RowHeightsProperty);

        private static void OnRowHeightsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Grid grid && newValue is IEnumerable<GridLength> heights)
            {
                grid.RowDefinitions.Clear();
                foreach (var height in heights)
                    grid.RowDefinitions.Add(new RowDefinition { Height = height });
            }
        }
    }
}
