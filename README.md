# Unstable Prototype

A 2D low horror game featured with exploration, secrets, and combat. To check out the Unity web build, see [Unity Web Game](https://github.com/one-busy-beaver/Unity-Web-Game).



## Features Implemented

### System

- camera
    - confiner
    - pan
    - zoom (not working with confiner)
- scene
    - scene loading
    - exit-spawn pair
        - using unity's GUID for stability
    - parallex effect
    - bootstrap
- game states
    - player inventory
        - abilities
        - items
    - world states
        - events
        - picked-up items
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
    - ability/item pickup
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



## TODO

- changes
    - change default key bindings
        - A - dash
        - S - cast/tool
        - D - attack
    - scenes
        - change RV's gate to sideways, and make it auto-transition
        - change all trigger texts to be only triggerable when on ground
    - tilemap
        - change player layer to default
        - silhoutte to pure black

- fix
    - fix rope and water jump height
    - fix rope/stair: implement one-way platform
    - fix pause menu: pause flip and animation when paused

- character
    - more movements
        - attack
        - death
        - health
        - hurt
        - tools/cast
        - wall jump
    - bug fix
        - jump buffer
        - opposite key pressed not moving bug 
        - last safe ground
            - need to account for continuous jumping
            - ledge not detecting as ground
    - climbing animation
    
- enemies
    - grass (flytrap) in TF
    - small animals in lower flooes
    - rotten animals in higher floors
    - non-agreesive animale outside the tower

- particle systems    
    - double jump particles
    - enemies throw particles when head

- UI
    - scene transition fade
    - ability and item unlock
    - inventory and abilities
    
- addition
    - npc throws key
    - locked door
    - adjust zoom to confiner

### Scenes

- tower entry
    - spikes
        - forces you to use dash
    - moving platforms
        - harmonic resonance platforms???
- tower body
    - the "anxiety" vector field
        - bullet dodging??
    - tear boss?
    - gain swim ability
- tower exterior
    - innocent enemies
    - platforming
    - gain double jump
- tower basement
    - weapon upgrade
- tower top
    - loose light
    - stochastic / cellular automata corruption
    - final boss

### Character

- later
    - ledge climb, glide, zipline, run, parry
- tool effects
    - double jump, dash cd, jump height, swim speed/time, health, dps, weapon effect