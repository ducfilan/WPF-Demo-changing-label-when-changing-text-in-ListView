using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var items = new List<Model>
            {
                new Model {FieldA = "#FFF000", FieldB = "B1"},
                new Model {FieldA = "#BF5C19", FieldB = "B2"},
                new Model {FieldA = "#BF5C19", FieldB = "B3"},
                new Model {FieldA = "#BF5C19", FieldB = "B4"}
            };

            TestView.ItemsSource = items;
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var label = GetLabelTextBlock(textBox);

            label.Foreground = (Brush)new BrushConverter().ConvertFrom(textBox.Text);
        }

        private static TextBlock GetLabelTextBlock(DependencyObject txt)
        {
            var listViewItem = FindAncestorOrSelf<ListViewItem>(txt);
            var contentPresenter = FindVisualChild<ContentPresenter>(listViewItem);
            var contentTemplate = contentPresenter.ContentTemplate;

            return contentTemplate.FindName("Lbl", contentPresenter) as TextBlock;
        }

        public static T FindAncestorOrSelf<T>(DependencyObject obj) where T : DependencyObject
        {
            while (obj != null)
            {
                T objTest = obj as T;

                if (objTest != null)
                    return objTest;

                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T)
                {
                    return (T)child;
                }

                var childItem = FindVisualChild<T>(child);
                if (childItem != null) return childItem;
            }
            return null;
        }
    }
}
