' Ralf Mouthaan
' University of Cambridge
' April 2020
'
' Measurement routines for iterative optimisation

Option Explicit On
Option Strict On

Module modMeasurementRoutines

    Public NoMacropixels As Integer
    Public bw As New System.ComponentModel.BackgroundWorker

    Public Sub IterativeOptimise(sender As Object, e As System.ComponentModel.DoWorkEventArgs)

        'This approach changes the hologram and checks whether the result has improved.
        'It then adopts or rejects the change.
        'Note, some parameters are hard-coded at the moment.

        Randomize()

        Dim Holo As clsMacropixelHologram
        Dim Temperature As Double = 1
        Dim idx As Integer
        Dim Rnd As New Random
        Dim BestWeights(NoMacropixels - 1) As Double
        Dim CurrWeights(NoMacropixels - 1) As Double
        Dim CurrValue As Double
        Dim MaxValue As Double
        Dim Img(,) As Integer
        Dim x As Integer = 224, y As Integer = 224
        Dim IterNo As Integer

        'Set up hologram
        Holo = New clsMacropixelHologram
        Holo.RawWidth = 1500
        Holo.dblScale = 1
        Holo.LoadZernikes("D:\RPM Data Files\Tx1 Zernikes.txt")
        Holo.bolApplyZernikes = True
        Holo.bolVisible = True
        Holo.bolCircularAperture = False
        MeasurementSetup.SLM.lstHolograms.Clear()
        MeasurementSetup.SLM.lstHolograms.Add(Holo)

        'Random starting point
        For idx = 0 To NoMacropixels - 1
            BestWeights(idx) += Rnd.NextDouble * 2 * Math.PI
            BestWeights(idx) = BestWeights(idx) Mod 2 * Math.PI
        Next

        MeasurementSetup.StartUp()

        While Temperature > 0.05

            Temperature = Temperature / 1.5
            bw.ReportProgress(CInt(Temperature * 100))
            IterNo = 0

            While IterNo < 100

                BestWeights.CopyTo(CurrWeights, 0)
                For PixelNo = 1 To CInt(Temperature * NoMacropixels)
                    idx = CInt(Math.Round(Rnd.NextDouble * (NoMacropixels - 1)))
                    CurrWeights(idx) += Rnd.NextDouble * 2 * Math.PI * Temperature
                    CurrWeights(idx) = CurrWeights(idx) Mod 2 * Math.PI
                Next

                Holo.arrMacroPixels = CurrWeights
                MeasurementSetup.SLM.Refresh()
                Img = MeasurementSetup.Camera.GetOffAxisCroppedImage
                CurrValue = Img(x, y)
                If CurrValue > MaxValue Then
                    CurrWeights.CopyTo(BestWeights, 0)
                    MaxValue = Img(x, y)
                    Console.WriteLine(MaxValue)
                    IterNo = 0
                    If MaxValue > 240 Then
                        MeasurementSetup.Camera.Exposure /= 2
                        Holo.arrMacroPixels = BestWeights
                        MeasurementSetup.SLM.Refresh()
                        Img = MeasurementSetup.Camera.GetOffAxisCroppedImage
                        MaxValue = Img(x, y)
                    End If
                Else
                    IterNo = IterNo + 1
                End If

            End While
        End While

        'Update final hologram
        Holo.arrMacroPixels = BestWeights
        MeasurementSetup.SLM.Refresh()

        'Adjust exposure
        Img = MeasurementSetup.Camera.GetOffAxisCroppedImage
        MaxValue = Img(x, y)
        While MaxValue < 220
            MeasurementSetup.Camera.Exposure *= 1.3
            Img = MeasurementSetup.Camera.GetOffAxisCroppedImage
            MaxValue = Img(x, y)
        End While

        For i = 1 To 5
            Img = MeasurementSetup.Camera.GetOffAxisCroppedImage
            ImageProcessing.SaveImgToFile(Img, "Optimised Field - Exposure = " + MeasurementSetup.Camera.Exposure.ToString + ".txt")
            MeasurementSetup.Camera.Exposure /= 2
        Next

        Dim writer As New IO.StreamWriter("Holo.txt")
        For i = 0 To Holo.arrMacroPixels.Count - 1
            writer.WriteLine(Holo.arrMacroPixels(i).ToString)
        Next
        writer.Close()

        MeasurementSetup.Camera.Shutdown()

    End Sub

End Module
