Public Class CharacterType
    ReadOnly Property Id As Long
    Sub New(characterTypeId As Long)
        Id = characterTypeId
    End Sub
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    Property Name As String
        Get
            Return CharacterTypeData.ReadName(Id)
        End Get
        Set(value As String)
            CharacterTypeData.WriteName(Id, value)
        End Set
    End Property
    ReadOnly Property CanDestroy As Boolean
        Get
            Return Not CharacterData.ReadForCharacterType(Id).Any
        End Get
    End Property
    Public Sub Destroy()
        If CanDestroy Then
            CharacterTypeData.Clear(Id)
        End If
    End Sub
    Public Shared Operator =(first As CharacterType, second As CharacterType) As Boolean
        Return first.Id = second.Id
    End Operator
    Public Shared Operator <>(first As CharacterType, second As CharacterType) As Boolean
        Return first.Id <> second.Id
    End Operator
End Class
