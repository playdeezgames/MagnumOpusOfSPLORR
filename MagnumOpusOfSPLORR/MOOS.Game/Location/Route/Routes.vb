Public Module Routes
    Function CreateRoute(fromLocation As Location, toLocation As Location, direction As Direction) As Route
        Return New Route(RouteData.Create(fromLocation.Id, toLocation.Id, direction.Id))
    End Function
End Module
