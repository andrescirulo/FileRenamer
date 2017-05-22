Public Class TagDefaultItem
    Inherits UserControl

    Public Nombre As String
    Public TagName As String
    Public Agregado As Boolean
    Public Fijo As Boolean

    Public Sub New(Nombre As String, TagName As String)
        Me.New(Nombre, TagName, False)
    End Sub

    Public Sub New(Nombre As String, TagName As String, Fijo As Boolean)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.Nombre = Nombre
        Me.TagName = TagName
        Me.Fijo = Fijo
        lblTexto.Content = Me.Nombre
        lblArrow.Cursor = Cursors.Hand
        SetAgregado(False)
    End Sub

    Private Sub lblArrow_MouseEnter(sender As Object, e As MouseEventArgs) Handles lblArrow.MouseEnter
        lblArrow.Foreground = New SolidColorBrush(Color.FromRgb(80, 80, 80))
    End Sub

    Private Sub lblArrow_MouseLeave(sender As Object, e As MouseEventArgs) Handles lblArrow.MouseLeave
        lblArrow.Foreground = New SolidColorBrush(Color.FromRgb(0, 0, 0))
    End Sub

    Public Sub SetAgregado(agregado As Boolean)
        Me.Agregado = agregado
        If (Me.Agregado) Then
            lblArrow.Text = FontAwesome.Net.FontAwesome.fa_times
        Else
            lblArrow.Text = FontAwesome.Net.FontAwesome.fa_plus
        End If
    End Sub

    Private Sub lblArrow_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles lblArrow.MouseUp
        If (e.LeftButton = MouseButtonState.Released) Then
            If Me.Agregado Then
                TagsManager.EnviarAOrigen(Me)
            Else
                TagsManager.EnviarADestino(Me)
            End If
        End If
    End Sub



    Private Sub TagItem_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        MyBase.OnMouseMove(e)
        Dim item = DirectCast(sender, TagDefaultItem)
        If item IsNot Nothing AndAlso e.LeftButton = MouseButtonState.Pressed Then
            Dim info As New DataObject
            info.SetData("Object", Me)
            CreateDragDropWindow(Me)
            Dispatcher.BeginInvoke(New Action(Sub()
                                                  Dim effect As DragDropEffects = DragDrop.DoDragDrop(item, info, DragDropEffects.Copy)
                                                  If (effect = DragDropEffects.None) Then
                                                      CloseDragWindow()
                                                  End If
                                              End Sub))
        End If
    End Sub

    Dim _dragdropWindow As Window
    Private Sub CreateDragDropWindow(dragElement As UserControl)

        _dragdropWindow = New Window()
        _dragdropWindow.WindowStyle = WindowStyle.None
        _dragdropWindow.AllowsTransparency = True
        _dragdropWindow.AllowDrop = False
        _dragdropWindow.Background = Nothing
        _dragdropWindow.IsHitTestVisible = False
        _dragdropWindow.SizeToContent = SizeToContent.WidthAndHeight
        _dragdropWindow.Topmost = True
        _dragdropWindow.ShowInTaskbar = False

        Dim r As New Rectangle()
        r.Width = dragElement.ActualWidth
        r.Height = dragElement.ActualHeight
        r.Fill = New VisualBrush(dragElement)
        _dragdropWindow.Content = r

        Dim w32Mouse As POINTAPI = New POINTAPI()
        GetCursorPos(w32Mouse)

        _dragdropWindow.Left = w32Mouse.x
        _dragdropWindow.Top = w32Mouse.y
        _dragdropWindow.Show()
    End Sub

    Private Sub TagItem_GiveFeedback(sender As Object, e As GiveFeedbackEventArgs) Handles Me.GiveFeedback

        Dim w32Mouse As POINTAPI = New POINTAPI()
        GetCursorPos(w32Mouse)

        _dragdropWindow.Left = w32Mouse.x
        _dragdropWindow.Top = w32Mouse.y
    End Sub

    Public Sub CloseDragWindow()
        If (Not _dragdropWindow Is Nothing) Then
            _dragdropWindow.Close()
            _dragdropWindow = Nothing
        End If
    End Sub

    Structure POINTAPI ' This holds the logical cursor information
        Dim x As Integer
        Dim y As Integer
    End Structure

    Private Declare Function GetCursorPos Lib "user32" (ByRef point As POINTAPI) As Boolean
End Class
