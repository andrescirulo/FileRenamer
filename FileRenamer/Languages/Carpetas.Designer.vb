﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión de runtime:4.0.30319.42000
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System


'StronglyTypedResourceBuilder generó automáticamente esta clase
'a través de una herramienta como ResGen o Visual Studio.
'Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
'con la opción /str o recompile su proyecto de VS.
'''<summary>
'''  Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
'''</summary>
<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
 Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
 Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
Public Class Carpetas
    
    Private Shared resourceMan As Global.System.Resources.ResourceManager
    
    Private Shared resourceCulture As Global.System.Globalization.CultureInfo
    
    <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
    Friend Sub New()
        MyBase.New
    End Sub
    
    '''<summary>
    '''  Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
    '''</summary>
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Public Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
        Get
            If Object.ReferenceEquals(resourceMan, Nothing) Then
                Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("FileRenamer.Carpetas", GetType(Carpetas).Assembly)
                resourceMan = temp
            End If
            Return resourceMan
        End Get
    End Property
    
    '''<summary>
    '''  Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
    '''  búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
    '''</summary>
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Public Shared Property Culture() As Global.System.Globalization.CultureInfo
        Get
            Return resourceCulture
        End Get
        Set
            resourceCulture = value
        End Set
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Downloads.
    '''</summary>
    Public Shared ReadOnly Property Descargas() As String
        Get
            Return ResourceManager.GetString("Descargas", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Documents.
    '''</summary>
    Public Shared ReadOnly Property Documentos() As String
        Get
            Return ResourceManager.GetString("Documentos", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a This Pc.
    '''</summary>
    Public Shared ReadOnly Property Equipo() As String
        Get
            Return ResourceManager.GetString("Equipo", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Desktop.
    '''</summary>
    Public Shared ReadOnly Property Escritorio() As String
        Get
            Return ResourceManager.GetString("Escritorio", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Pictures.
    '''</summary>
    Public Shared ReadOnly Property Imagenes() As String
        Get
            Return ResourceManager.GetString("Imagenes", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Music.
    '''</summary>
    Public Shared ReadOnly Property Musica() As String
        Get
            Return ResourceManager.GetString("Musica", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Videos.
    '''</summary>
    Public Shared ReadOnly Property Videos() As String
        Get
            Return ResourceManager.GetString("Videos", resourceCulture)
        End Get
    End Property
End Class
