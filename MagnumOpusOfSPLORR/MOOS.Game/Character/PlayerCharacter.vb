Public Class PlayerCharacter
    Inherits Character
    Sub New()
        MyBase.New(PlayerData.Read().Value)
    End Sub
    ReadOnly Property DidWin As Boolean
        Get
            Return Location.IsWinningLocation
        End Get
    End Property
End Class
