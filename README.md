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
    - ground sensor
    - submerge sensor
- others
    - damage upgrade
    - line renderer (visualization)

### Environment

- tilemap
    - ground 
    - water
    - play-plane
    - background
    - silhouette
- props
    - exit/door
    - rope/stair
    - npc
    - one-way platform
- event
    - trigger
    - storage
    - locked door
- ability/item
    - pickup
    - storage
    - session vs persistant
    - breakable box
- enemy
    - contact damage 
    - body volume (ignore physics with player)
    - session storage

### Scenes

- level 1
    - remote village
    - house
    - garage
    - sky secret
- level 2
    - tower front
    - mansion 1
    - mansion 2 
    - cabin
- level 3 
    - tower entry
    - tower exterior
    - tower top
    - tower basement

## Later

- character
    - ledge climb, glide, zipline, run, parry
- tool effects
    - double jump, dash cd, jump height, swim speed/time, health, dps, weapon effect