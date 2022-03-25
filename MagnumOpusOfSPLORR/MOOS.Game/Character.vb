Imports MOOS.Data

Public Class Character
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub
    ReadOnly Property Location As Location
        Get
            Return New Location(CharacterData.ReadLocation(Id).Value)
        End Get
    End Property
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    Property Name As String
        Get
            Return CharacterData.ReadName(Id)
        End Get
        Set(value As String)
            CharacterData.WriteName(Id, value)
        End Set
    End Property
End Class
