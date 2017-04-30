Public Class FAButton

    Public Shared ReadOnly ButtonTextProperty As DependencyProperty
    Public Shared ReadOnly FAIconProperty As DependencyProperty

    Public Event InnerButtonClick As UserControlClickHandler
    Public Delegate Sub UserControlClickHandler(sender As Object, e As EventArgs)

    Shared Sub New()
        ButtonTextProperty = DependencyProperty.RegisterAttached("ButtonText", GetType(String), GetType(FAButton), New FrameworkPropertyMetadata(Nothing))

        FAIconProperty = DependencyProperty.RegisterAttached("FAIcon", GetType(String), GetType(FAButton), New FrameworkPropertyMetadata(Nothing))
    End Sub


    Public Shared Function GetFAIcon(obj As DependencyObject) As String
        Return obj.GetValue(FAIconProperty)
    End Function

    Public Shared Sub SetFAIcon(obj As DependencyObject, value As String)
        obj.SetValue(FAIconProperty, value)
    End Sub

    Public Shared Function GetButtonText(obj As DependencyObject) As String
        Return obj.GetValue(ButtonTextProperty)
    End Function

    Public Shared Sub SetButtonText(obj As DependencyObject, value As String)
        obj.SetValue(ButtonTextProperty, value)
    End Sub

    Private Sub btnFa_Click(sender As Object, e As RoutedEventArgs) Handles btnFa.Click
        RaiseEvent InnerButtonClick(sender, e)
    End Sub
End Class