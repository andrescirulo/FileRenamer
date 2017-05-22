Public Class TagsManager

    Private Shared TagsSrc As TagsContainer
    Private Shared TagsDest As TagsContainer
    Private Shared ResultadoListener As TagResultadoListener

    Public Shared Sub Inicializar(TagsSource As TagsContainer, TagsDestino As TagsContainer, listener As TagResultadoListener)
        TagsSrc = TagsSource
        TagsDest = TagsDestino
        ResultadoListener = listener
    End Sub

    Public Shared Sub AddToContainer(item As TagDefaultItem, container As TagsContainer)
        container.PanelContainer.Children.Add(item)
    End Sub
    Public Shared Sub RemoveFromParent(item As TagDefaultItem)
        Dim parentItem = DirectCast(item.Parent, WrapPanel)
        parentItem.Children.Remove(item)
    End Sub

    Friend Shared Sub EnviarAOrigen(tagItem As TagDefaultItem)
        EnviarAContainer(tagItem, TagsSrc)
    End Sub

    Friend Shared Sub EnviarADestino(tagItem As TagDefaultItem)
        EnviarAContainer(tagItem, TagsDest)
    End Sub

    Friend Shared Sub EnviarAContainer(tagItem As TagDefaultItem, tagsContainer As TagsContainer)
        'If (tagItem.Fijo) Then
        '    Dim tagNuevo As New TagDefaultItem(tagItem.Nombre, tagItem.TagName)
        '    AddToContainer(tagNuevo, tagsContainer)
        '    tagNuevo.SetAgregado(tagsContainer.Equals(TagsDest))
        'Else
        'RemoveFromParent(tagItem)
        '    AddToContainer(tagItem, tagsContainer)
        '    tagItem.SetAgregado(tagsContainer.Equals(TagsDest))
        'End If

        'Dim resultado As String = ""
        'For Each elem In TagsDest.PanelContainer.Children
        '    Dim item As TagDefaultItem = TryCast(elem, TagDefaultItem)
        '    If Not item Is Nothing Then
        '        resultado &= item.TagName & " "
        '    End If
        'Next
        'resultado = resultado.Trim
        ResultadoListener.OnResultadoChange(tagItem.TagName)
    End Sub

End Class
