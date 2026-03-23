# Unstable Prototype

A 2D low horror game featured with exploration, secrets, and combat. To check out the Unity web build, see [Unity Web Game](https://github.com/one-busy-beaver/Unity-Web-Game).



## Features Implemented

### System

- camera follow
    - zoom
    - pan
- scene transition
    - scene loader
    - exit-spawn id match
    - gated exit
    - persistent items
- saves
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
    - finite states logic
    - last safe ground storage
- visualization
    - line renderer

### Environment

- tilemap
    - ground 
    - water
    - play-plane
    - background
- rope/stair

### Interactables

- door
- ability pickup
- npc
- release rope (event)

### Scenes

- remote village
    - house
    - garage
- tower front
    - mansion 1
    - mansion 2 
- tower entry

### Dev Helpers

- bootstrap scene
- scene enum generator
- align and sync text for display
- auto asset stack (row and column)



## Features to be Implemented

- decouple in-world UI with event trigger
    - combine sync text and UI
- one-way platform (fix rope/stair)
- water surface (not submerged) jump force should be larger
- improve spawn-exit pair
- pause menu bug: plays animation when paused
- last safe ground: need to be smarter
- climbing and double jump animation?

### Scenes

- remote village
    - house
        - npc throws key (item pickup)
    - garage
        - lock and unlock gate
        - dash from box
- tower front
    - path to RV
        - more deco's
    - mansion 2
        - lower the rope
- tower basement
- tower entry
    - spikes
    - moving platforms
    - enemies
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
    - jump not moving forward bug
    - opposite key pressed not moving bug
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
- camera confiner
    - find player
    - polygon collider outline
- UI
    - scene transition fade
    - ability and item unlock
    - inventory and abilities
- nice-to-have
    - audio player
    - save / load save