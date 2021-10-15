Public Class ClsJRdata
    Private m_FileName As String
    Private m_Text As String
    Private m_FuncDfList As ArrayList
    Private m_MuraDfList As ArrayList

#Region "ClsJRdata參數取得或設定"
    Public Property FileName() As String
        Get
            Return Me.m_FileName
        End Get
        Set(ByVal value As String)
            Me.m_FileName = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return Me.m_Text
        End Get
        Set(ByVal value As String)
            Me.m_Text = value
        End Set
    End Property
#End Region

    Public Sub New()
        Me.m_FileName = ""
        Me.m_Text = ""
        Me.m_FuncDfList = New ArrayList
        Me.m_MuraDfList = New ArrayList
    End Sub

    Public Sub Clear()
        Me.m_FileName = ""
        Me.m_Text = ""
        Me.m_FuncDfList.Clear()
        Me.m_MuraDfList.Clear()
    End Sub

    '---Func Defect ArrayList---
    Public Sub AddFunc(ByRef p As ClsFuncDf)
        Me.m_FuncDfList.Add(p)
    End Sub

    Public Function GetFuncDf(ByVal index As Integer) As ClsFuncDf
        Return Me.m_FuncDfList(index)
    End Function

    Public Function FuncCount() As Integer
        Return Me.m_FuncDfList.Count
    End Function

    '---Mura Defect ArrayList---
    Public Sub AddMura(ByRef p As ClsMuraDf)
        Me.m_MuraDfList.Add(p)
    End Sub

    Public Function GetMuraDf(ByVal index As Integer) As ClsMuraDf
        Return Me.m_MuraDfList(index)
    End Function

    Public Function MuraCount() As Integer
        Return Me.m_MuraDfList.Count
    End Function

    Public Function GetMuraAry() As ArrayList
        Return Me.m_MuraDfList
    End Function

    Public Property MuraDfList() As ArrayList
        Get
            Return Me.m_MuraDfList
        End Get
        Set(ByVal value As ArrayList)
            Me.m_MuraDfList = value
        End Set
    End Property
End Class
