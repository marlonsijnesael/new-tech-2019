# new tech 2019 
# Project Evodex
prototype uses vuforia AR in combination with Unity3D.
## Goal
The goal of the experiment is to create an interactive augmented pokedex.<br/>

## How does it work?
- All pokemon-data is loaded from the pokemon.json file.<br/>
= This json file contains all of the information of the pokemon, which is to be displayed in the world.<br/>
- When a pokemon is first scanned, it will be added to a dictionary of loaded pokemon.<br/><br/>
- When a pokemon is scanned ,and present in the loaded-Pokemon dictionary, it will be enabled instead of instantiated again.<br/>
- The reason for loading the pokemon from the json file is that I wanted scalability to be a key factor in my experiment.<br/>
All the user has to do is to point the camera at a pokemon card.<br/>
## Implemented
Recognise pokemon displayed on card <br/>
Get the desired pokemon data from the pokemon.JSON file <br/>
Display 3D version of the pokemon displayed on the card <br/>

## Not implemented
Display Information about the pokemon on worldspace UI elements <br/>



