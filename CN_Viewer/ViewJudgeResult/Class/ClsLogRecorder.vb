Imports System.IO

Public Class ClsLogRecorder

#Region "Variable"
    Private m_FileStream As FileStream
    Private m_Writer As StreamWriter

    Private m_Filepath As String
    Private m_Filename As String
    Private m_LogDate As Date

    Private m_Title As String
#End Region

#Region "Property"
    Public Property Title() As String
        Set(ByVal value As String)
            Me.m_Title = value
        End Set
        Get
            Return Me.m_Title
        End Get
    End Property
#End Region

    Public Sub New()
        Me.m_Filepath = ""
        Me.m_Filename = ""
        Me.m_Title = ""
    End Sub

    Public Sub SetTitle(ByVal title As String)
        Me.m_Title = title
    End Sub

    Public Sub OpenNew(ByVal filePath As String, ByVal PanelID As String)
        Dim fileName As String
        Dim blnHadExist As Boolean

        Me.m_Filepath = filePath
        'Me.m_Filename = prefixFilename
        'Me.m_LogDate = logDate.Date

        If filePath.Substring(filePath.Length - 1, 1) <> "\" Then filePath &= "\"
        Me.m_Filepath = Me.m_Filepath & Format(Now, "yyyyMMdd") & "\"
        If Not Directory.Exists(Me.m_Filepath) Then
            Try
                Directory.CreateDirectory(Me.m_Filepath)
            Catch ex As Exception
                'Throw New Exception("[PoLogJudgeResult]建立Judge Result " & Str() & "目錄失敗!")
            End Try
        End If

        fileName = Me.m_Filepath & PanelID & ".log"

        If System.IO.File.Exists(fileName) Then
            Dim i As Integer
            'fileName = Me.m_Filepath & PanelID & "-" & i & ".log"
            Do
                i = i + 1
                fileName = Me.m_Filepath & PanelID & "-" & i & ".log"
            Loop Until (Not File.Exists(fileName))
        End If

        blnHadExist = System.IO.File.Exists(fileName)

        Me.m_FileStream = New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read)
        Me.m_Writer = New StreamWriter(Me.m_FileStream, System.Text.Encoding.UTF8)
        Me.m_Writer.AutoFlush = True

        '--- if is new file and had title then write 1st line---
        If (Me.m_Title <> "") And (Not blnHadExist) Then
            Me.m_FileStream.Seek(0, System.IO.SeekOrigin.Begin)
            Me.m_Writer.WriteLine(Me.m_Title)
        End If
        Me.m_FileStream.Seek(0, System.IO.SeekOrigin.End)
    End Sub

    Public Sub Open(ByVal filePath As String, ByVal prefixFilename As String)
        Me.Open(filePath, prefixFilename, Now)
    End Sub

    Public Sub Open(ByVal filePath As String, ByVal prefixFilename As String, ByVal logDate As Date)
        Dim fileName As String
        Dim blnHadExist As Boolean

        Me.m_Filepath = filePath
        Me.m_Filename = prefixFilename
        Me.m_LogDate = logDate.Date

        If Not Directory.Exists(filePath) Then
            Directory.CreateDirectory(filePath)
        End If

        If filePath.Substring(filePath.Length - 1, 1) <> "\" Then filePath &= "\"
        fileName = filePath & prefixFilename & "_" & Format(logDate, "yyyyMMdd") & ".log"

        If Not File.Exists(fileName) Then
            File.Create(fileName).Dispose()
        End If

        blnHadExist = System.IO.File.Exists(fileName)

        Me.m_FileStream = New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read)
        Me.m_Writer = New StreamWriter(Me.m_FileStream, System.Text.Encoding.UTF8)
        Me.m_Writer.AutoFlush = True

        '--- if is new file and had title then write 1st line---
        If (Me.m_Title <> "") And (Not blnHadExist) Then
            Me.m_FileStream.Seek(0, System.IO.SeekOrigin.Begin)
            Me.m_Writer.WriteLine(Me.m_Title)
        End If
        Me.m_FileStream.Seek(0, System.IO.SeekOrigin.End)
    End Sub

    Public Sub Close()

        If Me.m_Writer IsNot Nothing Then
            Me.m_Writer.Close()
            Me.m_Writer = Nothing
        End If

        If Me.m_FileStream IsNot Nothing Then
            Me.m_FileStream.Close()
            Me.m_FileStream = Nothing
        End If
    End Sub

    Public Function ReadLog() As ArrayList
        Dim aryList As ArrayList
        Dim Reader As StreamReader

        aryList = New ArrayList
        Reader = New StreamReader(Me.m_FileStream, System.Text.Encoding.UTF8)

        Me.m_FileStream.Seek(0, IO.SeekOrigin.Begin)

        While Not Reader.EndOfStream
            aryList.Add(Reader.ReadLine)
        End While

        Me.m_FileStream.Seek(0, IO.SeekOrigin.End)

        Return aryList
    End Function

    Public Sub WriteLog(ByVal log As String)
        Me.WriteLog(Now, log)
    End Sub

    Public Sub WriteLog(ByVal logDate As Date, ByVal log As String)

        If DateTime.Compare(Me.m_LogDate.Date, logDate.Date) <> 0 Then
            Me.Close()
            Me.Open(Me.m_Filepath, Me.m_Filename, logDate)
        End If
        Me.m_Writer.WriteLine(log)
    End Sub

    Public Sub WriteLogTime(ByVal log As String)

        If DateTime.Compare(Me.m_LogDate.Date, Now.Date) <> 0 Then
            Me.Close()
            Me.Open(Me.m_Filepath, Me.m_Filename, Now)
        End If
        Me.m_Writer.WriteLine(Format(Now, "yyyy/MM/dd HH:mm:ss ") & log)
    End Sub

    Public Sub WriteLogTimeMs(ByVal log As String)

        If DateTime.Compare(Me.m_LogDate.Date, Now.Date) <> 0 Then
            Me.Close()
            Me.Open(Me.m_Filepath, Me.m_Filename, Now)
        End If
        Me.m_Writer.WriteLine(Format(Now, "yyyy/MM/dd HH:mm:ss.fff ") & log)
    End Sub

    Public Sub WritePointLog(ByVal log As String)
        Me.m_Writer.WriteLine(Format(Now, "yyyy/MM/dd HH:mm:ss.fff ") & log)
    End Sub


End Class

