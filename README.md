### We have a ListView, each of its item contains:
1. Textbox
2. Label for the textbox

### We want to change the label color for every time the textbox changes (the color code)

### Start from the textbox, find its parent using:
```csharp
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
```
### Then find the label from the parent we've got from above method:
```csharp
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
```
### And after having above 2 functions, we have the textbox:
```csharp
private static TextBlock GetLabelTextBlock(DependencyObject txt)
{
    var listViewItem = FindAncestorOrSelf<ListViewItem>(txt);
    var contentPresenter = FindVisualChild<ContentPresenter>(listViewItem);
    var contentTemplate = contentPresenter.ContentTemplate;

    return contentTemplate.FindName("Lbl", contentPresenter) as TextBlock;
}
```
