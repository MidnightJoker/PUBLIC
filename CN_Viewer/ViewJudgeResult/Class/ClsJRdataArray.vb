Public Class ClsJRdataArray
    Private m_JRdata As ArrayList

    Public Sub New()
        Me.m_JRdata = New ArrayList
    End Sub

    Public Sub Clear()
        Dim i As Integer
        For i = 0 To Me.m_JRdata.Count - 1
            Me.Index(i).Clear()
        Next i
        Me.m_JRdata.Clear()
    End Sub

    Public Sub Add(ByRef p As ClsJRdata)
        Me.m_JRdata.Add(p)
    End Sub

    Public Function Index(ByVal idx As Integer) As ClsJRdata
        Return Me.m_JRdata(idx)
    End Function

    Public Function Count() As Integer
        Return Me.m_JRdata.Count
    End Function

End Class
