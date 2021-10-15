Public Class ClsMuraDf
    Private m_MuraType As String
    Private m_Data As Integer
    Private m_Gate As Integer
    Private m_Area As Integer
    Private m_JND As Single
    Private m_Rank As String
    Private m_X As Integer
    Private m_Y As Integer
    Private m_MinX As Integer
    Private m_MinY As Integer
    Private m_MaxX As Integer
    Private m_MaxY As Integer
    Private m_Score As Double
    Private m_Pattern As String
    Private m_Width As Double
    Private m_ChipID As String
    Private m_CCDNo As String
    Private m_MuraName As String
    Private m_ImageFilePath As String
    Private m_FileName As String
    Private m_CheckMura As String
    Private m_Block As Integer

    Public Sub New()
        Me.m_MuraType = ""
        Me.m_Data = 0
        Me.m_Gate = 0
        Me.m_Area = 0
        Me.m_JND = 0
        Me.m_Rank = ""
        Me.m_X = 0
        Me.m_Y = 0
        Me.m_MinX = 0
        Me.m_MinY = 0
        Me.m_MaxX = 0
        Me.m_MaxY = 0
        Me.m_Score = 0
        Me.m_Pattern = ""
        Me.m_Width = 0
        Me.m_ChipID = ""
        Me.m_CCDNo = ""
        Me.m_MuraName = ""
        Me.m_ImageFilePath = ""
        Me.m_FileName = ""
        Me.m_CheckMura = "True"
        Me.m_Block = 0
    End Sub

#Region "ClsMuraDf參數取得或設定"

    Public Property Block() As Integer
        Get
            Return Me.m_Block
        End Get
        Set(ByVal value As Integer)
            Me.m_Block = value
        End Set
    End Property
    Public Property MuraType() As String
        Get
            Return Me.m_MuraType
        End Get
        Set(ByVal value As String)
            Me.m_MuraType = value
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

    Public Property Area() As Integer
        Get
            Return Me.m_Area
        End Get
        Set(ByVal value As Integer)
            Me.m_Area = value
        End Set
    End Property

    Public Property JND() As Single
        Get
            Return Me.m_JND
        End Get
        Set(ByVal value As Single)
            Me.m_JND = value
        End Set
    End Property

    Public Property Rank() As String
        Get
            Return Me.m_Rank
        End Get
        Set(ByVal value As String)
            Me.m_Rank = value
        End Set
    End Property

    Public Property X() As Integer
        Get
            Return Me.m_X
        End Get
        Set(ByVal value As Integer)
            Me.m_X = value
        End Set
    End Property

    Public Property Y() As Integer
        Get
            Return Me.m_Y
        End Get
        Set(ByVal value As Integer)
            Me.m_Y = value
        End Set
    End Property

    Public Property MinX() As Integer
        Get
            Return Me.m_MinX
        End Get
        Set(ByVal value As Integer)
            Me.m_MinX = value
        End Set
    End Property

    Public Property MinY() As Integer
        Get
            Return Me.m_MinY
        End Get
        Set(ByVal value As Integer)
            Me.m_MinY = value
        End Set
    End Property

    Public Property MaxX() As Integer
        Get
            Return Me.m_MaxX
        End Get
        Set(ByVal value As Integer)
            Me.m_MaxX = value
        End Set
    End Property

    Public Property MaxY() As Integer
        Get
            Return Me.m_MaxY
        End Get
        Set(ByVal value As Integer)
            Me.m_MaxY = value
        End Set
    End Property

    Public Property Score() As Double
        Get
            Return Me.m_Score
        End Get
        Set(ByVal value As Double)
            Me.m_Score = value
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

    Public Property Width() As Double
        Get
            Return Me.m_Width
        End Get
        Set(ByVal value As Double)
            Me.m_Width = value
        End Set
    End Property

    Public Property MuraName() As String
        Get
            Return Me.m_MuraName
        End Get
        Set(ByVal value As String)
            Me.m_MuraName = value
        End Set
    End Property

    Public Property ChipID() As String
        Get
            Return Me.m_ChipID
        End Get
        Set(ByVal value As String)
            Me.m_ChipID = value
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

    Public Property CheckMura() As String
        Get
            Return Me.m_CheckMura
        End Get
        Set(ByVal value As String)
            Me.m_CheckMura = value
        End Set
    End Property

#End Region

End Class

Public Class ClsMuraJudgePart
    Public TotalPartition As Integer
    Public Partition1 As Boolean
    Public Partition1_Pattern As String
    Public Partition2 As Boolean
    Public Partition2_Pattern As String
    Public Partition3 As Boolean
    Public Partition3_Pattern As String
    Public Partition4 As Boolean
    Public Partition4_Pattern As String
    Public Partition5 As Boolean
    Public Partition5_Pattern As String
    Public Partition6 As Boolean
    Public Partition6_Pattern As String
    Public Partition7 As Boolean
    Public Partition7_Pattern As String
    Public Partition8 As Boolean
    Public Partition8_Pattern As String
    Public Partition9 As Boolean
    Public Partition9_Pattern As String

    Public Sub New()
        Me.TotalPartition = 1
        Me.Partition1 = False
        me.Partition1_Pattern= ""
        Me.Partition2 = False
        Me.Partition2_Pattern = ""
        Me.Partition3 = False
        Me.Partition3_Pattern = ""
        Me.Partition4 = False
        Me.Partition4_Pattern = ""
        Me.Partition5 = False
        Me.Partition5_Pattern = ""
        Me.Partition6 = False
        Me.Partition6_Pattern = ""
        Me.Partition7 = False
        Me.Partition7_Pattern = ""
        Me.Partition8 = False
        Me.Partition8_Pattern = ""
        Me.Partition9 = False
        Me.Partition9_Pattern = ""
    End Sub

    Public Sub Clear()
        Me.TotalPartition = 1
        Me.Partition1 = False
        Me.Partition1_Pattern = ""
        Me.Partition2 = False
        Me.Partition2_Pattern = ""
        Me.Partition3 = False
        Me.Partition3_Pattern = ""
        Me.Partition4 = False
        Me.Partition4_Pattern = ""
        Me.Partition5 = False
        Me.Partition5_Pattern = ""
        Me.Partition6 = False
        Me.Partition6_Pattern = ""
        Me.Partition7 = False
        Me.Partition7_Pattern = ""
        Me.Partition8 = False
        Me.Partition8_Pattern = ""
        Me.Partition9 = False
        Me.Partition9_Pattern = ""
    End Sub

End Class