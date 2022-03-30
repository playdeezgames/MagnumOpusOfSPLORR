Public Module Characters
    ReadOnly Property AllCharacters As List(Of Character)
        Get
            Return CharacterData.All.Select(Function(id) New Character(id)).ToList
        End Get
    End Property
    Function CreateCharacter(characterName As String, location As Location, characterType As CharacterType) As Character
        Return New Character(CharacterData.Create(location.Id, characterName, characterType.Id))
    End Function
End Module
