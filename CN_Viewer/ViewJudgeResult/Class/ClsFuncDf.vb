Public Class ClsFuncDf
    Private m_Type As defectType
    Private m_BlobX As Integer
    Private m_BlobY As Integer
    Private m_Area As Integer
    Private m_Data As Integer
    Private m_Gate As Integer
    Private m_PType As Integer
    Private m_Pattern As String
    Private m_History As String
    Private m_Graymean As Integer
    Private m_CCDNo As String
    Private m_ImageFilePath As String
    Private m_FileName As String
    Private m_FuncName As String

#Region "ClsFuncDf參數取得或設定"
    Public Property Type() As defectType
        Get
            Return Me.m_Type
        End Get
        Set(ByVal value As defectType)
            Me.m_Type = value
        End Set
    End Property

    Public Property BlobX() As Integer
        Get
            Return Me.m_BlobX
        End Get
        Set(ByVal value As Integer)
            Me.m_BlobX = value
        End Set
    End Property

    Public Property BlobY() As Integer
        Get
            Return Me.m_BlobY
        End Get
        Set(ByVal value As Integer)
            Me.m_BlobY = value
        End Set
    End Property

    Public Property Area() As Integer
        Get
            Return Me.m_Area
        End Get
        Set(ByVal value As Integer)
            Me.m_Area = value
        End Set
    End Property

    Public Property Data() As Integer
        Get
            Return Me.m_Data
        End Get
        Set(ByVal value As Integer)
            Me.m_Data = value
        End Set
    End Property

    Public Property Gate() As Integer
        Get
            Return Me.m_Gate
        End Get
        Set(ByVal value As Integer)
            Me.m_Gate = value
        End Set
    End Property

    Public Property PType() As Integer
        Get
            Return Me.m_PType
        End Get
        Set(ByVal value As Integer)
            Me.m_PType = value
        End Set
    End Property

    Public Property Pattern() As String
        Get
            Return Me.m_Pattern
        End Get
        Set(ByVal value As String)
            Me.m_Pattern = value
        End Set
    End Property

    Public Property History() As String
        Get
            Return Me.m_History
        End Get
        Set(ByVal value As String)
            Me.m_History = value
        End Set
    End Property

    Public Property GrayMean() As Integer
        Get
            Return Me.m_Graymean
        End Get
        Set(ByVal value As Integer)
            Me.m_Graymean = value
        End Set
    End Property

    Public Property CCDNo() As String
        Get
            Return Me.m_CCDNo
        End Get
        Set(ByVal value As String)
            Me.m_CCDNo = value
        End Set
    End Property

    Public Property ImageFilePath() As String
        Get
            Return Me.m_ImageFilePath
        End Get
        Set(ByVal value As String)
            Me.m_ImageFilePath = value
        End Set
    End Property

    Public Property FileName() As String
        Get
            Return Me.m_FileName
        End Get
        Set(ByVal value As String)
            Me.m_FileName = value
        End Set
    End Property

    Public Property FuncName() As String
        Get
            Return Me.m_FuncName
        End Get
        Set(ByVal value As String)
            Me.m_FuncName = value
        End Set
    End Property
#End Region

End Class
