' Ralf Mouthaan
' University of Cambridge
' April 2020
'
' Main form for instrument control code for generic iterative optimisation. 
' The SLM pattern Is iteratively changed To Try To optimise for some target.

Option Explicit On
Option Strict On

Public Class frmMain

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        MeasurementSetup = New modMeasurementSetup
        ssLabel.Text = ""

        For i = 1 To 100
            cmbNoModes.Items.Add((i ^ 2).ToString)
        Next
        cmbNoModes.SelectedItem = "100"

    End Sub

    Private Sub EnableControls()

        cmbNoModes.Enabled = True
        cmdIterativeOptimisation.Enabled = True
        cmdCaptureSingleImage.Enabled = True

    End Sub
    Private Sub DisableControls()

        cmbNoModes.Enabled = False
        cmdIterativeOptimisation.Enabled = False
        cmdCaptureSingleImage.Enabled = False

    End Sub


    Private Sub cmdIterativeOptimisation_Click(sender As Object, e As EventArgs) Handles cmdIterativeOptimisation.Click

        DisableControls()
        modMeasurementRoutines.NoMacropixels = CInt(cmbNoModes.Text)

        bw.WorkerReportsProgress = True
        AddHandler bw.DoWork, AddressOf modMeasurementRoutines.IterativeOptimise
        AddHandler bw.ProgressChanged, Sub(_sender As System.Object, _e As System.ComponentModel.ProgressChangedEventArgs)
                                           ssLabel.Text = "Iterative Optimisation - Temperature = " + (_e.ProgressPercentage / 100).ToString
                                       End Sub
        AddHandler bw.RunWorkerCompleted, Sub()
                                              EnableControls()
                                              ssLabel.Text = ""
                                          End Sub

        bw.RunWorkerAsync()

    End Sub

    Private Sub cmdCaptureSingleImage_Click(sender As Object, e As EventArgs) Handles cmdCaptureSingleImage.Click

        Dim img(,) As Integer
        Dim Holo As clsMacropixelHologram
        Dim CurrWeights(CInt(cmbNoModes.SelectedItem.ToString) - 1) As Double

        DisableControls()
        'Set up hologram
        Holo = New clsMacropixelHologram
        Holo.RawWidth = 1000
        Holo.dblScale = 1
        Holo.LoadZernikes("D:\RPM Data Files\Tx1 Zernikes.txt")
        Holo.bolApplyZernikes = True
        Holo.bolVisible = True
        Holo.bolCircularAperture = False
        MeasurementSetup.SLM.lstHolograms.Clear()
        MeasurementSetup.SLM.lstHolograms.Add(Holo)

        MeasurementSetup.StartUp()

        Holo.arrMacroPixels = CurrWeights
        MeasurementSetup.SLM.Refresh()

        img = MeasurementSetup.Camera.GetOffAxisCroppedImage()
        img(200, 140) = 500
        ImageProcessing.SaveImgToFile(img, "Test Image.txt")
        MeasurementSetup.Shutdown()

        EnableControls()

    End Sub
End Class
