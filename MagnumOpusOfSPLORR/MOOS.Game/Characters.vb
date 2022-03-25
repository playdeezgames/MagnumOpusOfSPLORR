Imports MOOS.Data

Public Module Characters
    ReadOnly Property AllCharacters As List(Of Character)
        Get
            Return CharacterData.All.Select(Function(id) New Character(id)).ToList
        End Get
    End Property
End Module
