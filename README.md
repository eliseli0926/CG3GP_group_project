# [TBD: Get Out?]
## Team Members
Elise Li: eliseli0926

Sophia Cho: scho324

## Game Summary

An eerie third-person horror puzzle game where the player wakes up alone in a decaying manor with no memory of how they got there. To escape alive, they must explore room by room, collect items, and use them to unlock the house's mechanisms. Along the way, they piece together notes left behind by previous "guests" and avoid the house's caretaker, who wanders the halls.

## Genres
- puzzle
- horror


## Story
The manor's owner "collects" visitors: people who come to the house are drugged and locked in a room, and none have ever left. The owner enjoys watching people attempt to escape. The player is the newest guest. Each previous captive tried to escape and left behind notes, scratched symbols, and half-finished attempts.

## Inspiration
### Where Winds Meet
Where Winds Meet is a wuxia open-world game, played in the third person POV. The main inspirations from this game are the puzzles in the dungeons, as well as puzzles that some chests/treasures are locked behind. Screenshots of a dungeon layout (for how the atmosphere of 
our game may look) and a puzzle lock (as an example of the kinds of puzzles we want to implement) are provided. 
<img width="2360" height="1538" alt="WWM_dungeon" src="https://github.com/user-attachments/assets/c2982cfa-ed27-476a-803f-c2079d7380e4" />
<img width="1338" height="1459" alt="WWM_puzzle" src="https://github.com/user-attachments/assets/7cc2eda4-e35e-4682-a28b-2ddabf801f8d" />

### Little Nightmares
Little Nightmares is a horror puzzle game played in a 2.5D fixed camera side scroll perspective. We plan to take inspirations from its player movement and interactions with items/the environment, simple puzzles, and successful artistic choice for creating an immersive horror gameplay experience. 
<img width="750" height="420" alt="image" src="https://github.com/user-attachments/assets/864fb21f-ce06-4ccb-a804-61c6a4ebc866" />
<img width="1000" height="563" alt="image" src="https://github.com/user-attachments/assets/ad0451f0-823f-40b0-95f5-ec7a571791b0" />



## Gameplay

- **Camera:** Third person, dynamic. The camera follows behind the player and the player rotates it around the character with the mouse.
- **Movement:** Simple directional movement (WASD) and jumping (Space). We might also add crouching and climbing.
- **Core puzzle mechanic** The player picks up an item and uses it on a matching object in the world:
  - key → locked door
  - fuse → fuse box (turns on the lights / powers an elevator)
  - torn page → diary (reveals a code or symbol needed elsewhere)
- **Story progression:** The player finds notes from previous guests that explain the house and hint at puzzle solutions.
- **Enemy:** The caretaker uses simple AI for now. It walks a predetermined path between waypoints, and if the player gets close enough it chases them in a straight line. Being caught sends the player back to the last checkpoint.
- **Checkpoints:** The game saves a checkpoint each time the player enters a new room, so getting caught does not restart the whole game.


## Development Plan
### Project Checkpoint 1-2: Basic Mechanics and Scripting (Ch 5-9)

<!-- [TODO] Strike through (~~like this~~) -->
 
- Implement player view and movement
- Item pickup and interaction
- Puzzle prototype and mechanics for at least one puzzle, depending on how many puzzle types we choose to implement
- Basic setting of the initial room the player wakes up in (e.g. furniture, decorations etc.)
- *Changes from feedback:* We decided on "collect and use" to be our basic puzzle mechanic , chose a third-person dynamic camera, and decided on room-based checkpoints instead of a one-shot run.

#### Additions
<!-- [TODO] List anything major you built that wasn't planned, otherwise keep "Not applicable." -->

### Project Part 2: 3D Scenes and Models (Ch 3+4, 10)
 
- Replace placeholder primitives with 3D models and textured materials for furniture, doors, and puzzle objects.
- Set up dim lighting, shadows, and fog to create the horror atmosphere.
- Add the caretaker enemy: a model that patrols waypoints and chases the player in a straight line when close.
- Implement 1–2 more re-skins of the collect-and-use puzzle (e.g. fuse box, crank and gate).
- Add room-based checkpoints and respawning when the player is caught.

## Development
### Project Checkpoint 1-2
**prefabs** for key, player, and room
