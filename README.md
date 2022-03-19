# Magnum Opus Of SPLORR!!

## Deliberate Decisions

- UI is Spectre.Console
- Data Store is SQLite
- Language is VB, the one true King of Clown Languages
- All Presentation layer is strictly in MagnumOpusOfSPLORR.vbproj
- All "Business" layer is strictly in MOOS.Game.vbproj
- All Persistence layer is strictly in MOOS.Data.vbproj

## TODO Decisions

Characters MUST exist at a Location
More than one Character MAY exist at a Location
One way Routes connection Locations

## DB Schema

### Locations

- LocationId (PK AUTO)
- LocationType (INT)

### Characters

- CharacterId (PK AUTO)
- CharacterType (INT)

### Players

- PlayerId (PK, CK = 1)
- CharacterId (FK Characters)

### Routes

- RouteId (PK)
- FromLocationId (FK Locations)
- ToLocationId (FK Locations)