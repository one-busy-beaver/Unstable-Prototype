# Unstable Prototype

A 2D low horror game featured with exploration, secrets, and combat. To check out the Unity web build, see [Unity Web Game](https://github.com/one-busy-beaver/Unstable-Web-Build).



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
        - key rebind
    - HUD
    - pause menu
    

### Character

- movement
    - walk & flip
    - jump & double jump
    - dash
    - swim & drown
    - rope climb
- action
    - attack
    - cast
    - interact
    - hurt
    - die
- player states
    - health
    - ammo
    - finite state logic
    - last-safe-ground storage
    - submerge sensor
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
    - one-way platform
- enemy
    - hitboxes
        - contact damage 
        - body volume (ignore physics with player)

### Scenes

- remote village
    - house
    - garage
    - sky secret
- tower front
    - mansion 1
    - mansion 2 
    - cabin
- tower 
    - entry
    - body
    - exterior
    - top
    - base

## TODO
    
- enemies
    - grass (flytrap) in TF
    - small animals in lower flooes
    - rotten animals in higher floors
    - non-agreesive animale outside the tower

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