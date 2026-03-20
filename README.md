# Unstable Prototype

A 2D low horror game featured with exploration, secrets, and combat. To check out the Unity web build, see [Unity Web Game](https://github.com/one-busy-beaver/Unity-Web-Game).



## Features Implemented

### System

- camera follow
- scene transition
    - scene loader
    - exit-spawn id match
    - gated exit
    - persistant items

### Character

- movements
    - walk & flip (A, D)
    - jump & double jump (Space)
    - dash (Shift)
    - swim & drown (Space)
    - rope climb (W, S)
    - interaction (E)
- data
    - player abilities (persistant)
    - player states (FSM)
    - last safe ground storage
- visualization
    - line renderer

### Environment

- tile map
- ground 
- water
- rope

### Interactables

- door
- ability pickup

### Scenes

- main menu
- remote village
    - house
    - garage
- tower front
- tower entry (working)

### Dev Helpers

- bootstrap scene
- scene enum generator
- align and sync text for display
- auto asset stack (row and column)



## Features to be Implemented

### Interactables

- item pickup
- locked door

### Environment

- spikes
- moving platforms
- one-way platforms

### Scenes

- tower front 
    - mansion
- tower entry
    - sub rooms
- tower staircase
- tower exterior
- tower top
- tower basement

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

### NPC

- enemy
- boss

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
    - pause menu (return to main menu)
    - scene transition fade
    - ability and item unlock
    - inventory and abilities
- main menu
    - control remap page
    - load save