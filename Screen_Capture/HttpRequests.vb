Imports System.Net.Http
Imports System.Text.RegularExpressions

Public Class HttpRequests
    Public Shared Sub AutoDirectOCR(image As Bitmap)
        Dim imageData As Byte() = ConvertToByteArray(image)
        AutoDirectOCR(imageData)
    End Sub

    Public Shared Sub AutoDirectOCR(image As Byte())
        Select Case Form1.Settings.Mode
            Case Form1.Mode.A9T9
                A9T9_OCR(image, Form1.Settings.A9T9.Apikey, Form1.Settings.A9T9.Lang.ToString, Form1.Settings.A9T9.TimeOut)
            Case Form1.Mode.Sogou
                Sogou_OCR(image, Form1.Settings.Sogou.TimeOut)
        End Select
    End Sub

    Public Shared Async Function A9T9_OCR(imageData As Byte(), apikey As String, language As String, timeOut As Integer) As Threading.Tasks.Task
        Initiating()
        Try
            Using httpClient As HttpClient = New HttpClient()
                httpClient.Timeout = New TimeSpan(0, 0, timeOut)
                Using form As MultipartFormDataContent = New MultipartFormDataContent()
                    form.Add(New StringContent(apikey), "apikey")
                    'Dim cmbLanguage As String = "eng"
                    Dim cmbLanguage As String = language
                    form.Add(New StringContent(cmbLanguage), "language")

                    form.Add(New ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg")

                    ' tray_text changes to program name after this step
                    Using response As HttpResponseMessage = Await httpClient.PostAsync("https://api.ocr.space/Parse/Image", form)
                        Dim strContent As String = Await response.Content.ReadAsStringAsync()

                        Debug.WriteLine(strContent)

                        Dim parsedText As String = GetParsedTextFromA9T9(strContent)
                        Clipboard.Clear()
                        Clipboard.SetText(parsedText)
                        If MessageBox.Show(parsedText, "A9T9 - " & language, MessageBoxButtons.OKCancel) = DialogResult.OK Then Clipboard.SetText(parsedText)  ' BUG: does not pop up. fix: wait. Cause: bad response

                        ' for RAM releases
                        parsedText = Nothing
                        strContent = Nothing

                    End Using
                    imageData = Nothing
                End Using
            End Using
        Catch exception As Exception
            MessageBox.Show("Ooops" & vbCrLf & exception.Message)
        End Try
        Finalizing()
    End Function

    Public Shared Async Function Sogou_OCR(imageData As Byte(), timeOut As Integer) As Threading.Tasks.Task
        Dim strContent As String
        Try
            Const url As String = "http://ocr.shouji.sogou.com/v2/ocr/json"
            Using httpClient As New HttpClient()
                httpClient.Timeout = New TimeSpan(0, 0, timeOut)  ' time out after no response 
                Using Form As New MultipartFormDataContent()  ' declare message body
                    Form.Add(New ByteArrayContent(imageData, 0, imageData.Length), "pic", "1111111.jpg")  ' add image to message body; BUG: file name may necessarily be "1111111.jpg"
                    Using response As HttpResponseMessage = Await httpClient.PostAsync(url, Form)  ' get response through POST method
                        strContent = Await response.Content.ReadAsStringAsync()
                        Dim parsedText As String = GetParsedTextFromSogou(strContent)
                        Clipboard.Clear()
                        Clipboard.SetText(parsedText)
                        If MessageBox.Show(parsedText, "Sogou", MessageBoxButtons.OKCancel) = DialogResult.OK Then Clipboard.SetText(strContent)

                    End Using
                    imageData = Nothing
                End Using
            End Using
        Catch ex As Exception  ' HttpRequestException not working
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Shared Sub Initiating()
        Form1.tray.Text = "Processing"
    End Sub

    Private Shared Sub Finalizing()
        GC.Collect()  ' uneffective
        Form1.tray.Text = "Screenshot OCR"
    End Sub

    ''' <summary>
    ''' convert Bitmap to Byte()
    ''' https://dotnettips.wordpress.com/2007/12/16/convert-bitmap-to-byte-array/
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Private Shared Function ConvertToByteArray(ByVal value As Bitmap) As Byte()
        Dim bitmapBytes As Byte()
        Using stream As New System.IO.MemoryStream()
            Try  'Since null value cannot be detected, use Try instead
                value.Save(stream, value.RawFormat)
            Catch ex As Exception
                value.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
            End Try
            bitmapBytes = stream.ToArray()
        End Using
        Return bitmapBytes
    End Function

    'analyses response documents and returns useful text
    Private Shared Function GetParsedTextFromA9T9(ByVal content As String) As String
        ' (?<="ParsedText"\s*:\s*").+?(?=(?<!\\)")
        Dim pattern As String = "(?<=" & Chr(34) & "ParsedText" & Chr(34) & "\s*:\s*" & Chr(34) & ").+?(?=(?<!\\)" & Chr(34) & ")"
        Dim match = Regex.Match(content, pattern)
        If match.Success Then
            Return match.Value.Replace("\r\n", vbCrLf).Replace("\" & Chr(34), Chr(34))
        Else
            ' https://stackoverflow.com/questions/13151322/how-to-raise-an-exception-in-vb-net
            Throw New System.Exception("Regex pattern did not match the responding text")
        End If
    End Function

    Private Shared Function GetParsedTextFromSogou(content As String) As String
        Dim pattern As String
        'Check if OCR succeeds. This may be unnecessary
        ' (?<="success"\s*:\s*).+?(?=\s*\})
        pattern = "(?<=" & Chr(34) & "success" & Chr(34) & "\s*:\s*).+?(?=\s*\})"
        Dim match = Regex.Match(content, pattern)
        If match.Success Then
            If Int(match.Value) = 0 Then Throw New System.Exception("OCR fails")
        Else
            ' https://stackoverflow.com/questions/13151322/how-to-raise-an-exception-in-vb-net
            Throw New System.Exception("Regex pattern did not match the responding text")
        End If

        'parse content
        Dim parsedText As String = ""
        ' (?<="content"\s*:\s*").+?(?=(?<!\\)")
        pattern = "(?<=" & Chr(34) & "content" & Chr(34) & "\s*:\s*" & Chr(34) & ").+?(?=(?<!\\)" & Chr(34) & ")"
        Dim matches = Regex.Matches(content, pattern)
        For Each match In matches
            parsedText &= match.Value.Replace("\n", vbNewLine).Replace("\" & Chr(34), Chr(34))
        Next
        Return parsedText
    End Function
End Class
