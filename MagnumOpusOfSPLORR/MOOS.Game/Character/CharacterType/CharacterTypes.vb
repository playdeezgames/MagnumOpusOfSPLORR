Public Module CharacterTypes
    ReadOnly Property AllCharacterTypes As List(Of CharacterType)
        Get
            Return CharacterTypeData.All.Select(Function(id) New CharacterType(id)).ToList
        End Get
    End Property
    Function FindCharacterTypeByUniqueName(characterTypeName As String) As CharacterType
        Return AllCharacterTypes.SingleOrDefault(Function(x) x.UniqueName = characterTypeName)
    End Function
    Function CreateCharacterType(characterTypeName As String, health As Long, damageDice As String) As CharacterType
        Return New CharacterType(CharacterTypeData.Create(characterTypeName, health, damageDice))
    End Function
End Module
