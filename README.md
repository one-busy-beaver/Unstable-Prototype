# Unstable Prototype

A 2D low horror game featured with exploration, secrets, and combat. To check out the Unity web build, see [Unity Web Game](https://github.com/one-busy-beaver/Unity-Web-Game).



## Features Implemented

### System

- camera follow
- scene transition
    - scene loader
    - exit-spawn id match
    - gated exit
    - persistent items

### Character

- movements
    - walk & flip
    - jump & double jump
    - dash
    - swim & drown
    - rope climb
    - attack (working)
    - interact
- data
    - player abilities (persistent)
    - player states
    - last safe ground storage
- visualization
    - line renderer

### Environment

- tilemap
- ground 
- water
- rope

### Interactables

- door
- ability pickup
- npc

### Scenes

- remote village
    - house
    - garage
- tower front
    - mansion 1
    - mansion 2 
- tower entry

### UI

- main menu
- pause menu (ESC)

### Dev Helpers

- bootstrap scene
- scene enum generator
- align and sync text for display
- auto asset stack (row and column)



## Features to be Implemented

- decouple in-world UI with event trigger
- one-way platform (fix rope/stair)
- water surface (not submerged) jumpr force should be larger

### Scenes

- remote village:
    - path to TF
    - house: 
        - npc throws key (item pickup)
    - garage: 
        - lock and unlock gate
        - dash from box
- tower front: 
    - path to RV
        - more deco's
    - mansion 2: 
        - lower the rope
- tower basement
    - locked soul
- tower entry
    - sub rooms
    - enemies
    - spikes
    - moving platforms
- tower body
    - tear boss
- tower exterior
    - innocent enemies
    - double jump?
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
- item attacks
- wishlist
    - (ledge climb)
    - (glide)
    - (zipline) 
    - (run)
    - (wall jump)
    - (wall climb)

### Fun Stuff

- stochastic / cellular automata corruption
- harmonic resonance platforms
- particle systems
- the "anxiety" vector field
- procedural animation
- roguelike map

### System 

- inventory
- camera confiner
    - find player
    - polygon collider outline
- shader / illumination
- audio player
- save file

### UI

- main canvas
    - scene transition fade
    - ability and item unlock
    - inventory and abilities
- main menu
    - control remap page
    - load save