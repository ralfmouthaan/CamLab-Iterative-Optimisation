' Ralf Mouthaan
' University of Cambridge
' April 2020
'
' Some matrix manipulation code
' Assumption is that matrices are stored in column-major format.
'   i.e., that the first index is how far down the matrix the element is, the second index is how far across the element is.

Option Explicit On
Option Strict On

Namespace MatrixOperations

    Module modMatrixOperations

        Public Function Multiply(ByVal Mat1(,) As Double, ByVal Mat2(,) As Double) As Double(,)

            If UBound(Mat1, 2) <> UBound(Mat2, 1) Then Call Err.Raise(-1, "Matrix Multiplication", "Dimensions do not match")

            Dim RetVal(UBound(Mat1, 1), UBound(Mat2, 2)) As Double

            For i = 0 To UBound(Mat1, 1)
                For j = 0 To UBound(Mat2, 2)

                    For k = 0 To UBound(Mat1, 2)
                        RetVal(i, j) += Mat1(i, k) * Mat2(k, j)
                    Next

                Next
            Next

            Return RetVal

        End Function
        Public Function Multiply(ByVal Mat1(,) As Numerics.Complex, ByVal Mat2(,) As Numerics.Complex) As Numerics.Complex(,)

            If UBound(Mat1, 2) <> UBound(Mat2, 1) Then Call Err.Raise(-1, "Matrix Multiplication", "Dimensions do not match")

            Dim RetVal(UBound(Mat1, 1), UBound(Mat2, 2)) As Numerics.Complex

            For i = 0 To UBound(Mat1, 1)
                For j = 0 To UBound(Mat2, 2)

                    For k = 0 To UBound(Mat1, 2)
                        RetVal(i, j) += Mat1(i, k) * Mat2(k, j)
                    Next

                Next
            Next

            Return RetVal

        End Function

        Public Function Inverse(ByVal Mat(,) As Double) As Double(,)

            'Uses Gauss-Jordan elimination as described in Numerial Recipes 2.1.
            'Other implementations available, but this one will do.

            If UBound(Mat, 1) <> UBound(Mat, 2) Then
                Call Err.Raise(-1, "Matrix Inverse", "Matrix is not square")
            End If

            Dim big As Double, dum As Double, pivinv As Double
            Dim icol As Integer, irow As Integer, n As Integer, m As Integer
            n = UBound(Mat, 1) + 1
            m = UBound(Mat, 2) + 1
            Dim indxc(n) As Integer, indxr(n) As Integer, ipiv(n) As Integer

            For j = 0 To n - 1
                ipiv(j) = 0
            Next

            For i = 0 To n - 1

                ' Find pivot element
                big = 0
                For j = 0 To n - 1
                    If ipiv(j) <> 1 Then
                        For k = 0 To n - 1
                            If ipiv(k) = 0 Then
                                If Math.Abs(Mat(j, k)) > big Then
                                    big = Math.Abs(Mat(j, k))
                                    irow = j
                                    icol = k
                                End If
                            End If
                        Next
                    End If
                Next
                ipiv(icol) += 1

                'Put pivot element on diagonal (or at least, relabel so that it looks like it)
                If irow <> icol Then
                    For l = 0 To n - 1
                        swap(Mat(irow, l), Mat(icol, l))
                    Next
                End If
                indxr(i) = irow
                indxc(i) = icol
                If Mat(icol, icol) = 0 Then Call Err.Raise(-1, "Matrix Inversion", "Singular Matrix")

                ' Pivot
                pivinv = 1 / Mat(icol, icol)
                Mat(icol, icol) = 1
                For l = 0 To n - 1
                    Mat(icol, l) *= pivinv
                Next
                For ll = 0 To n - 1
                    If ll <> icol Then
                        dum = Mat(ll, icol)
                        Mat(ll, icol) = 0
                        For l = 0 To n - 1
                            Mat(ll, l) -= Mat(icol, l) * dum
                        Next
                    End If
                Next

            Next

            'Unscramble column interchanges
            For l = n - 1 To 0 Step -1
                If indxr(l) <> indxc(l) Then
                    For k = 0 To n - 1
                        swap(Mat(k, indxr(l)), Mat(k, indxc(l)))
                    Next
                End If
            Next

            Return Mat

        End Function
        Public Function Inverse(ByVal Mat(,) As Numerics.Complex) As Numerics.Complex(,)

            'Uses Gauss-Jordan elimination as described in Numerial Recipes 2.1.
            'Other implementations available, but this one will do.

            If UBound(Mat, 1) <> UBound(Mat, 2) Then
                Call Err.Raise(-1, "Matrix Inverse", "Matrix is not square")
            End If

            Dim big As Numerics.Complex, dum As Numerics.Complex, pivinv As Numerics.Complex
            Dim icol As Integer, irow As Integer, n As Integer, m As Integer
            n = UBound(Mat, 1) + 1
            m = UBound(Mat, 2) + 1
            Dim indxc(n) As Integer, indxr(n) As Integer, ipiv(n) As Integer

            For j = 0 To n - 1
                ipiv(j) = 0
            Next

            For i = 0 To n - 1

                ' Find pivot element
                big = 0
                For j = 0 To n - 1
                    If ipiv(j) <> 1 Then
                        For k = 0 To n - 1
                            If ipiv(k) = 0 Then
                                If Mat(j, k).Magnitude > big.Magnitude Then
                                    big = Mat(j, k).Magnitude
                                    irow = j
                                    icol = k
                                End If
                            End If
                        Next
                    End If
                Next
                ipiv(icol) += 1

                'Put pivot element on diagonal (or at least, relabel so that it looks like it)
                If irow <> icol Then
                    For l = 0 To n - 1
                        swap(Mat(irow, l), Mat(icol, l))
                    Next
                End If
                indxr(i) = irow
                indxc(i) = icol
                If Mat(icol, icol) = 0 Then Call Err.Raise(-1, "Matrix Inversion", "Singular Matrix")

                ' Pivot
                pivinv = 1 / Mat(icol, icol)
                Mat(icol, icol) = 1
                For l = 0 To n - 1
                    Mat(icol, l) *= pivinv
                Next
                For ll = 0 To n - 1
                    If ll <> icol Then
                        dum = Mat(ll, icol)
                        Mat(ll, icol) = 0
                        For l = 0 To n - 1
                            Mat(ll, l) -= Mat(icol, l) * dum
                        Next
                    End If
                Next

            Next

            'Unscramble column interchanges
            For l = n - 1 To 0 Step -1
                If indxr(l) <> indxc(l) Then
                    For k = 0 To n - 1
                        swap(Mat(k, indxr(l)), Mat(k, indxc(l)))
                    Next
                End If
            Next

            Return Mat

        End Function
        Private Sub swap(ByRef Val1 As Double, ByRef Val2 As Double)
            Dim Temp As Double
            Temp = Val1
            Val1 = Val2
            Val2 = Temp
        End Sub
        Private Sub swap(ByRef Val1 As Numerics.Complex, ByRef Val2 As Numerics.Complex)
            Dim Temp As Numerics.Complex
            Temp = Val1
            Val1 = Val2
            Val2 = Temp
        End Sub

    End Module
End Namespace