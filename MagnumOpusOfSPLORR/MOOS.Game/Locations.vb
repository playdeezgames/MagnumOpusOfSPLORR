Imports MOOS.Data

Public Module Locations
    ReadOnly Property AllLocations As List(Of Location)
        Get
            Return LocationData.All().Select(Function(id) New Location(id)).ToList
        End Get
    End Property
End Module
