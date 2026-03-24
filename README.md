# Unstable Prototype

A 2D low horror game featured with exploration, secrets, and combat. To check out the Unity web build, see [Unity Web Game](https://github.com/one-busy-beaver/Unity-Web-Game).



## Features Implemented

### System

- camera
    - confiner
    - pan
    - zoom
- scene
    - scene transition
    - exit-spawn pair
        - use unity's GUID to keep it stable
    - parallex effect
    - bootstrap
- game states
    - player abilities
    - world states
- UI
    - main menu
    - pause menu
    - key rebind

### Character

- movements
    - walk & flip
    - jump & double jump
    - dash
    - swim & drown
    - rope climb
    - interact
- player states
    - finite state logic
    - last-safe-ground storage
- visualization
    - line renderer

### Environment

- tilemap
    - ground 
    - water
    - play-plane
    - background
    - silhouette
- trigger
    - exit/door
    - rope/stair
    - ability pickup
    - npc
    - event

### Scenes

- remote village
    - house
    - garage
    - sky secret
- tower front
    - mansion 1
    - mansion 2 
- tower entry



## Features to be Implemented

- one-way platform (fix rope/stair)
- water surface (not submerged) jump force should be larger
- pause menu bug: plays animation when paused
- last safe ground: need to be smarter
- climbing animation
- double jump particles
- adjust zoom to confiner

### Scenes

- remote village
    - fish in the water
    - house
        - npc throws key (item pickup)
    - garage
        - locked door
- tower front
    - cabin
    - mansion background
- tower basement
- tower entry
    - spikes
    - moving platforms
    - enemies
    - gain cast???
- tower body
    - tear boss?
    - gain swim ability
- tower exterior
    - innocent enemies
    - gain double jump
- tower top
    - final boss

### Character

- bug fix
    - jump buffer
    - jump not moving forward bug?
    - opposite key pressed not moving bug
    - ledge not detecting ground 
- attack
- death
- health
- hurt
- cast
- wall jump
- (ledge climb)
- (glide)
- (zipline) 
- (run)

### Fun Stuff

- stochastic / cellular automata corruption
- harmonic resonance platforms
- particle systems
- the "anxiety" vector field
- procedural animation
- roguelike map

### System 

- shader / illumination
- UI
    - scene transition fade
    - ability and item unlock
    - inventory and abilities
- nice-to-have
    - audio player
    - save / load save