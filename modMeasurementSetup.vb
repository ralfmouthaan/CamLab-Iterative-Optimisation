' Ralf Mouthaan
' University of Cambridge
' February 2020
'
' Measurement setup class

Option Explicit On
Option Strict On

Public Module Globals
    Public MeasurementSetup As modMeasurementSetup
End Module

Public Class modMeasurementSetup

    Public SLM As clsSLM
    Public Camera As clsThorlabsCamDC

    Public Sub New()

        SLM = New clsSLM
        Camera = New clsThorlabsCamDC

        SLM.intScreenNo = 1

        Dim strCameraFile As String
        strCameraFile = "D:\RPM Data Files\Output Camera Pol 1.txt"
        If IO.File.Exists(strCameraFile) Then
            Camera.Load(strCameraFile)
        Else
            Camera = New clsThorlabsCamDC
        End If


    End Sub

#Region "Startup, Shutdown"
    Public Sub StartUp()

        If SLM.bolConnectionOpen = False Then
            SLM.StartUp()
        End If
        If Camera.bolActive = True Then Camera.Startup()

    End Sub
    Public Sub Shutdown()

        SLM.ShutDown()
        Camera.Shutdown()

    End Sub
#End Region



End Class