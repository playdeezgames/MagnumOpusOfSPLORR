# Magnum Opus Of SPLORR!!

## Deliberate Decisions

- UI is Spectre.Console
- Data Store is SQLite
- Language is VB, the one true King of Clown Languages
- All Presentation layer is strictly in MagnumOpusOfSPLORR.vbproj
- All "Business" layer is strictly in MOOS.Game.vbproj
- All Persistence layer is strictly in MOOS.Data.vbproj

## TODO Decisions

- Locations and Characters
	- Must a character exist in a location?
		- if yes
			- CharacterId in Locations table
		- if no
			- either CharacterLocations 
			- or LocationCharacters table
	- May more than one character exist in a location?
		- if yes
			- either CharacterLocations
			- or LocationCharacters
			- or LocationId in Characters
		- if no
			- either CharacterLocations
			- or LocationCharacters
	- What is the relationship between locations?
		- Routes
			- either one way
			- or two way
	- Do locations have coordinates?
		- if they can have coordinates, MUST they have coordinates?
		- can they move?

## DB Schema

### Locations

- LocationId (PK AUTO)
- LocationType (INT)

### Characters

- CharacterId (PK AUTO)
- CharacterType (INT)

### Players

- PlayerId (PK, CK = 1)
- CharacterId FK Characters