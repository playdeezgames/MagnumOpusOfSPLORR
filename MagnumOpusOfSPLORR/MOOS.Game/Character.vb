Imports MOOS.Data

Public Class Character
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub
    Property Location As Location
        Get
            Return New Location(CharacterData.ReadLocation(Id).Value)
        End Get
        Set(value As Location)
            CharacterData.WriteLocation(Id, value.Id)
        End Set
    End Property
    ReadOnly Property IsPlayerCharacter As Boolean
        Get
            Return Id = PlayerData.Read.Value
        End Get
    End Property
    ReadOnly Property UniqueName As String
        Get
            Dim result = $"{Name}(#{Id})"
            If IsPlayerCharacter Then
                result &= "(PC)"
            End If
            Return result
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
    Public Sub SetAsPlayerCharacter()
        PlayerData.Write(Id)
    End Sub
End Class
