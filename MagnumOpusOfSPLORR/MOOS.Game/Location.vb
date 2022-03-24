Imports MOOS.Data

Public Class Location
    ReadOnly Property Id As Long
    ReadOnly Property Name As String
        Get
            Return LocationData.ReadName(Id)
        End Get
    End Property
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    Sub New(locationId As Long)
        Id = locationId
    End Sub
    ReadOnly Property Characters As List(Of Character)
        Get
            Return CharacterData.ForLocation(Id).Select(Function(x) New Character(x)).ToList
        End Get
    End Property
End Class
