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
Public Class Language
    
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
                Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("FileRenamer.Language", GetType(Language).Assembly)
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
    '''  Busca una cadena traducida similar a Hay una versión nueva disponible.
    '''</summary>
    Public Shared ReadOnly Property acerca_de_actualizaciones() As String
        Get
            Return ResourceManager.GetString("acerca_de_actualizaciones", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a No se pudieron obtener los cambios de la nueva version.
    '''</summary>
    Public Shared ReadOnly Property acerca_de_cambios_error() As String
        Get
            Return ResourceManager.GetString("acerca_de_cambios_error", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Por cualquier duda, sugerencia o error que desees reportar, escribe a andres.cirulo@gmail.com.
    '''</summary>
    Public Shared ReadOnly Property acerca_de_contacto() As String
        Get
            Return ResourceManager.GetString("acerca_de_contacto", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Descargando....
    '''</summary>
    Public Shared ReadOnly Property acerca_de_descargando() As String
        Get
            Return ResourceManager.GetString("acerca_de_descargando", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Detalles de la versión.
    '''</summary>
    Public Shared ReadOnly Property acerca_de_detalles() As String
        Get
            Return ResourceManager.GetString("acerca_de_detalles", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Instalar.
    '''</summary>
    Public Shared ReadOnly Property acerca_de_instalar() As String
        Get
            Return ResourceManager.GetString("acerca_de_instalar", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Tienes la versión más reciente.
    '''</summary>
    Public Shared ReadOnly Property acerca_de_no_actualizaciones() As String
        Get
            Return ResourceManager.GetString("acerca_de_no_actualizaciones", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Al hacer click derecho mientras se presiona la tecla Mayúsculas.
    '''</summary>
    Public Shared ReadOnly Property config_cuando_extendido() As String
        Get
            Return ResourceManager.GetString("config_cuando_extendido", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Al hacer click derecho.
    '''</summary>
    Public Shared ReadOnly Property config_cuando_normal() As String
        Get
            Return ResourceManager.GetString("config_cuando_normal", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Cuando Aparecer.
    '''</summary>
    Public Shared ReadOnly Property config_cuando_titulo() As String
        Get
            Return ResourceManager.GetString("config_cuando_titulo", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Al hacer click derecho sobre el nombre de una carpeta.
    '''</summary>
    Public Shared ReadOnly Property config_donde_carpeta() As String
        Get
            Return ResourceManager.GetString("config_donde_carpeta", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Al hacer click derecho en el fondo de una carpeta.
    '''</summary>
    Public Shared ReadOnly Property config_donde_fondo() As String
        Get
            Return ResourceManager.GetString("config_donde_fondo", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Donde Aparecer.
    '''</summary>
    Public Shared ReadOnly Property config_donde_titulo() As String
        Get
            Return ResourceManager.GetString("config_donde_titulo", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Guardar.
    '''</summary>
    Public Shared ReadOnly Property config_guardar() As String
        Get
            Return ResourceManager.GetString("config_guardar", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Menu Contextual de Subtitulos.
    '''</summary>
    Public Shared ReadOnly Property config_menu_contextual() As String
        Get
            Return ResourceManager.GetString("config_menu_contextual", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Habilitar menu contextual en el explorador de Windows.
    '''</summary>
    Public Shared ReadOnly Property config_menu_contextual_habilitar() As String
        Get
            Return ResourceManager.GetString("config_menu_contextual_habilitar", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Agregar.
    '''</summary>
    Public Shared ReadOnly Property renombrar_agregar() As String
        Get
            Return ResourceManager.GetString("renombrar_agregar", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Bajar.
    '''</summary>
    Public Shared ReadOnly Property renombrar_bajar() As String
        Get
            Return ResourceManager.GetString("renombrar_bajar", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Debes elegir al menos un archivo.
    '''</summary>
    Public Shared ReadOnly Property renombrar_error_archivos() As String
        Get
            Return ResourceManager.GetString("renombrar_error_archivos", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Debes agregar al menos una operación.
    '''</summary>
    Public Shared ReadOnly Property renombrar_error_operaciones() As String
        Get
            Return ResourceManager.GetString("renombrar_error_operaciones", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Incluir subcarpetas.
    '''</summary>
    Public Shared ReadOnly Property renombrar_incluir_subcarpetas() As String
        Get
            Return ResourceManager.GetString("renombrar_incluir_subcarpetas", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Operacion.
    '''</summary>
    Public Shared ReadOnly Property renombrar_operacion() As String
        Get
            Return ResourceManager.GetString("renombrar_operacion", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a por.
    '''</summary>
    Public Shared ReadOnly Property renombrar_por() As String
        Get
            Return ResourceManager.GetString("renombrar_por", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Quitar.
    '''</summary>
    Public Shared ReadOnly Property renombrar_quitar() As String
        Get
            Return ResourceManager.GetString("renombrar_quitar", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Reemplazar.
    '''</summary>
    Public Shared ReadOnly Property renombrar_reemplazar() As String
        Get
            Return ResourceManager.GetString("renombrar_reemplazar", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Carpeta No Seleccionada.
    '''</summary>
    Public Shared ReadOnly Property renombrar_sin_seleccionar() As String
        Get
            Return ResourceManager.GetString("renombrar_sin_seleccionar", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Subir.
    '''</summary>
    Public Shared ReadOnly Property renombrar_subir() As String
        Get
            Return ResourceManager.GetString("renombrar_subir", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Vista Previa.
    '''</summary>
    Public Shared ReadOnly Property renombrar_vista_previa() As String
        Get
            Return ResourceManager.GetString("renombrar_vista_previa", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Renombrar.
    '''</summary>
    Public Shared ReadOnly Property resultado_renombrar() As String
        Get
            Return ResourceManager.GetString("resultado_renombrar", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a FileRenamer - Vista Previa.
    '''</summary>
    Public Shared ReadOnly Property resultado_titulo() As String
        Get
            Return ResourceManager.GetString("resultado_titulo", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Vista previa del resultado.
    '''</summary>
    Public Shared ReadOnly Property resultado_vista_previa() As String
        Get
            Return ResourceManager.GetString("resultado_vista_previa", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Desmarcar Todos.
    '''</summary>
    Public Shared ReadOnly Property subtitulos_desmarcar_todos() As String
        Get
            Return ResourceManager.GetString("subtitulos_desmarcar_todos", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Debes elegir al menos un archivo.
    '''</summary>
    Public Shared ReadOnly Property subtitulos_error_archivos() As String
        Get
            Return ResourceManager.GetString("subtitulos_error_archivos", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Marcar Todos.
    '''</summary>
    Public Shared ReadOnly Property subtitulos_marcar_todos() As String
        Get
            Return ResourceManager.GetString("subtitulos_marcar_todos", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Renombrar Seleccionados.
    '''</summary>
    Public Shared ReadOnly Property subtitulos_renombrar() As String
        Get
            Return ResourceManager.GetString("subtitulos_renombrar", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Carpeta No Seleccionada.
    '''</summary>
    Public Shared ReadOnly Property subtitulos_sin_seleccionar() As String
        Get
            Return ResourceManager.GetString("subtitulos_sin_seleccionar", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Acerca de.
    '''</summary>
    Public Shared ReadOnly Property tab_acerca_de() As String
        Get
            Return ResourceManager.GetString("tab_acerca_de", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Configuración.
    '''</summary>
    Public Shared ReadOnly Property tab_configuracion() As String
        Get
            Return ResourceManager.GetString("tab_configuracion", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Renombrar Archivos.
    '''</summary>
    Public Shared ReadOnly Property tab_renombrar_archivos() As String
        Get
            Return ResourceManager.GetString("tab_renombrar_archivos", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Renombrar Subtitulos.
    '''</summary>
    Public Shared ReadOnly Property tab_renombrar_subtitulos() As String
        Get
            Return ResourceManager.GetString("tab_renombrar_subtitulos", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Busca una cadena traducida similar a Hay una nueva actualización disponible, haz click aquí.
    '''</summary>
    Public Shared ReadOnly Property toolbar_actualizacion() As String
        Get
            Return ResourceManager.GetString("toolbar_actualizacion", resourceCulture)
        End Get
    End Property
End Class
