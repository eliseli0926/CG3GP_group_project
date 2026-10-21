# Get Out

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
our game may look) and a puzzle lock (as an example of the kinds of puzzles we were originally interested in) are provided. As the kinds of puzzles in this game are currently out-of-scope, we mainly focus on the third person POV and darker atmosphere/lighting as our inspirations.
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

- ~~Implement player view and movement~~
- ~~Item pickup and interaction~~
- ~~Puzzle prototype and mechanics for at least one puzzle, depending on how many puzzle types we choose to implement~~
- ~~Basic setting of the initial room the player wakes up in (e.g. furniture, decorations etc.)~~
- _Changes from feedback:_ We decided on "collect and use" to be our basic puzzle mechanic, chose a third-person dynamic camera, and decided on room-based checkpoints instead of a one-shot run.

#### Additions

- We added a basic UI for checking the player's inventory so that the player can see what they have. We also added a basic popup that shows when the player tries to interact with something that requires an item they don't currently have.

### Project Part 2: 3D Scenes and Models (Ch 3+4, 10)

- ~~Replace placeholder primitives with 3D models and textured materials for furniture, doors, and puzzle objects.~~
- Set up dim lighting, shadows, and fog to create the horror atmosphere.
  - _Partially completed:_ we added candle models with flame particles and placed them around the first room, but we have not yet done a full lighting pass or added fog. Since lighting and post-processing are covered in textbook Chapters 12 and 13, we moved the full atmosphere pass to Project Part 3 so we can do it properly with those techniques (baked lighting, fog, post-processing).
- ~~Add the caretaker enemy: a model that patrols waypoints and chases the player in a straight line when close.~~
  - _Note:_ instead of a straight line, the caretaker chases the player along the NavMesh, so it can follow the player around corners and through doorways without getting stuck on walls.
- Implement 1–2 more re-skins of the collect-and-use puzzle (e.g. fuse box, crank and gate).
  - _Not completed:_ this submission focused on building out the enemy AI, the lose/respawn system, and the textured environment, since the hallway and caretaker needed to exist before we could place new puzzles outside the first room. The cabinet-key-door chain in the first room still demonstrates the collect-and-use mechanic. We moved the new puzzle re-skins to Project Part 3, where they will also benefit from the visual effects (e.g. sparks on the fuse box).
- ~~Add room-based checkpoints and respawning when the player is caught.~~

Based on project Part 1-2 Feedback, adding:

- ~~Adjust the trigger colliders for interactable objects so the user can interact with them in a less strict manner~~
- ~~Adjust the jumping gravity to be more realistic~~

#### Additions

- **Sight-based enemy detection:** the caretaker has a view distance and view angle, and walls/obstacles block its line of sight. It only chases the player when it can actually see them, and only catches the player when it can see them while touching them, so the player can hide behind obstacles or slip past it from behind.
- **Lose screen with two options:** when caught, the game freezes and shows a lose screen where the player can either restart the whole game or respawn at the last checkpoint with their progress kept.
- **Door opening mechanism:** **player can click on the door to open/close it. It opens/closes by rotating around the vertical axis where its hinges are supposed to be.**
- **Expanded environment:** textured walls, floor, and ceiling, extra furniture, a hallway outside the first room, and candles with particle-system flames.

### Project Part 3: Visual Effects

**Particle Systems and VFX Graph (Ch 11)**
 
- Add ambient dust particles floating in the rooms and a low ground-fog particle effect in the hallway.
- Add spark particles to the fuse box puzzle and a small burst effect when a locked object is unlocked, so the player gets visual feedback.

**Lighting with URP (Ch 12)**
  
- Do the full horror lighting pass carried over from Part 2: dim ambient light, warm point lights on the candles, and darker hallways so the caretaker is harder to spot.
- Bake lighting for static furniture and walls (with light probes for the player and caretaker) to get softer shadows with better performance.
- Add scene fog through the Lighting settings.

**Fullscreen Post-processing (Ch 13)**
 
- Add a Global Volume with bloom (so candle flames glow), a vignette, film grain, and color adjustments for a cold, desaturated look.
- Make the vignette tighten and/or add a red tint while the caretaker is chasing the player, to build tension.


**Carried over from Part 2**
 
- 1–2 re-skins of the collect-and-use puzzle (e.g. fuse box, crank and gate) placed outside the first room.
- More checkpoints as new rooms are added.

## Development

### Project Checkpoint 1-2

**Prefabs**
We created prefabs for key, player, room, and door, plus basic furniture like table, shelf, and armchair. There is a basic NPC prefab which we will later use to make variants for NPCs in various positions and write scripts for NPC interaction.

- Player: The player prefab is the character the user controls. It has a Rigidbody and a Capsule Collider. The PlayerMovement script reads WASD input from our Input Actions asset and moves the player relative to the direction the camera is facing, so W always moves away from the camera. Space makes the player jump.
- Key: our first example of an interactable object, and also of an item that can be picked up to be held in the inventory. Along with the PickupItem script, it has two different colliders; one is a trigger collider for detecting if the player is close enough to reasonably interact with it, and the other is for actual player interaction.
- Door: our first example of a 'puzzle', and is also an interactable object with a close-enough range. A door can optionally have a required item to be opened. If the user does not have an item of that particular name, it will not move. It opens/closes instantly when unlocked with a basic rotation transformation, but we plan to later add animations to its movement once we learn.

**Scripts**

- We have the IInteractable interface so that any interactable object will implement the Interact() function with their own behavior upon being clicked on.

- InventoryManager stores the items the player has picked up, as a list of item names. There is only one InventoryManager in the scene (on our GameManager object), and other scripts access it through a static Instance property instead of needing a reference dragged into each one. It provides three methods: AddItem() (called by PickupItem when the player picks something up), HasItem() (used by doors and other locks to check whether the player has the required item), and RemoveItem() (used to consume an item once it unlocks something). The item list is a serialized field, so we can watch it update in the Inspector while testing. It is also exposed as a read-only Items property, which the inventory UI uses to display item names without being able to change them. In the future, we plan to store more than just names, such as an icon and description for each item.

- InventoryUI lets the player see what they are carrying. Pressing Esc switches from play mode to menu mode: the mouse cursor becomes visible, and the camera and click interactions pause so the player can use the UI. Clicking the Inventory button in the top-right corner opens a panel that lists the names of all items currently in the InventoryManager. Other scripts check its static CursorFree property to know whether the player is in menu mode.

- PlayerInteractor handles clicking on objects in the world. It sits on the Main Camera, and when the player left-clicks, it casts a ray from the camera through the center of the screen, where a small crosshair dot is drawn. If the ray hits an object that implements IInteractable, it calls that object's Interact() function. The ray ignores the Player layer so the player's own body doesn't block it.

- PickupItem is the main script for key and any future items that can be put into the inventory, and implements the IInteractable interface. It uses the object's trigger collider to check if the player is close enough (so they can't pick something up from the other end of the room), and if so, its implemented Interact() will put its name into the inventory and delete the object so it disappears and is no longer interactable.

- DoorInteractable is the main script for any doors, and implements the IInteractable interface. If the player is close enough, we can open/close any unlocked doors. If the door requires an item, the player must have the matching one in their inventory to unlock and open it.

- PopupMessage is the script for popup messages that show when the user tries to interact with an object and can't do anything yet. The attempted interacted object controls the message that will be shown in the popup, so we can have custom messages.

- PlayerMovement is the script used for player movement (WASD/jump) based on the camera's 3rd-person POV.

- ThirdPersonCamera handles camera movement, orbiting around the player based on where the user's mouse moves.

**Demonstration**

- Player movement and 3rd-person camera with crosshair
  https://github.com/user-attachments/assets/e00beb14-d44a-4ae1-a6a7-ebd8236761f8
- Picking up items and checking the inventory
  https://github.com/user-attachments/assets/4009a711-2ff2-4369-b73f-bb322651e58e
- Locked door message popup when the user does not have the right item
  https://github.com/user-attachments/assets/30d4fe0a-7936-45c8-8ab3-eb7130a2e018
- Locked door unlocking when the user does have the right item, consuming the related item from the inventory. It is then permanently unlocked.
  https://github.com/user-attachments/assets/66790141-174f-4cde-8b48-3b888313a12d
- Current basic appearance of the first room. There is a door to leave the room which is locked; the key is in a locked shelf/cabinet, and that key can be found on the floor next to the NPC.
  <img width="506" height="284" alt="image" src="https://github.com/user-attachments/assets/f4d643cf-49ef-4747-bc95-45b0f01cf2f3" />

### Project Part 2: 3D Scenes and Models

- Replaced the placeholder primitives in the first room with 3D models and textured materials from the Dark Fantasy Kit [Lite]. The walls, floor, and ceiling now use the kit's textures, and we added more furniture to make the room feel lived-in.
- Built a hallway outside the first room for the caretaker to patrol.
- Added candles with particle-system flames around the room for a dim, candlelit look.
- All materials use the URP Lit shader.

**Caretaker Enemy**
 
The caretaker is a NavMesh-based enemy with two states: **Patrol** and **Chase Player**.
 
- While patrolling, it walks between waypoints placed along the hallway (currently two), pausing for a few seconds at each one before moving on.
- It has a sight sensor with a view distance and view angle. Walls and other obstacles block its line of sight, so it can't see the player through walls.
- When it sees the player, it switches to chasing and speeds up, following the player along the NavMesh. If it loses sight of the player, it returns to its patrol route.
- If the caretaker touches the player while it can see them, the player is caught.

**Lose Screen and Checkpoints**
 
- When the player is caught, the game freezes and a lose screen appears with two buttons:
  - **Restart:** reloads the whole scene, so all progress is lost (keys are back in their original spots and the first room's door is locked again).
  - **Back to Checkpoint:** keeps all progress (inventory, unlocked doors and cabinets) and only respawns the player at the checkpoint in the first room. The caretaker is also sent back to its starting point so the player isn't caught again immediately.
- Right now there is one checkpoint, in the first room. More will be added as we add rooms.


**Door Interaction**
 
- In Checkpoint 1-2, a door's "close enough" range was a trigger collider on the door. Once the door opened, that trigger rotated with it, so the player often couldn't close the door again. Doors now measure the player's distance to the door's pivot (its hinge), which doesn't move when the door swings. The range is adjustable per door and shown as a yellow sphere in the Scene view.
- Doors still open and close instantly with a rotation. We plan to add a proper opening animation later.


**Scripts**
 
- **EnemyFSM** is the caretaker's finite state machine, with Patrol and ChasePlayer states. In Patrol, it moves the NavMeshAgent through an array of patrol point Transforms in order and loops back to the first, waiting a set time at each point. In ChasePlayer, it sets the agent's destination to the player's position every frame. Patrol and chase speeds are set separately in the Inspector. 

- **Sight** is the caretaker's vision sensor. Every frame, it finds colliders on the target layers within its view distance, checks whether each one is inside its view angle, and then does a linecast to make sure no obstacle is in the way. The first object that passes all three checks is stored as the detected object, which EnemyFSM uses to decide whether to chase.

- **EnemyTouchPlayer** sits on the caretaker's root object, which has a trigger collider and a kinematic Rigidbody. While the player is inside the trigger and the caretaker's Sight sensor can see them, it tells the GameManager that the player lost. It also stores the caretaker's starting position and rotation so the caretaker can be warped back there when the player respawns.

- **GameManager** handles the loss condition and both ways of continuing. It is a single scene-wide manager accessed through a static Instance property, and it logs a warning if a duplicate is found.
  - Lose() shows the lose panel, pauses the game by setting the time scale to 0, and frees the mouse cursor. A flag makes sure it only runs once per loss, so the player can't be caught multiple times.
  - Respawn() (the Restart button) reloads the whole scene, which resets all progress.
  - RespawnFromCheckpoint() (the Back to Checkpoint button) gets the active checkpoint's respawn point from the CheckpointManager, teleports the player there, sends every caretaker back to its start, hides the lose panel, and unpauses the game. The inventory and anything already unlocked stay as they are.

- **Checkpoint** goes on a trigger collider in each room. When the player enters it, it registers itself as the active checkpoint with the CheckpointManager. Each checkpoint can optionally use a child object as a custom respawn point. Otherwise, the player respawns at the checkpoint's own position.

**Demonstration**
<img width="1680" height="1050" alt="Screenshot 2026-10-21 at 09 30 07" src="https://github.com/user-attachments/assets/9a059e28-7ff7-406a-ae62-8bf3ed0406af" />
<img width="1680" height="1050" alt="Screenshot 2026-10-21 at 09 30 26" src="https://github.com/user-attachments/assets/94aa1df2-6565-4d32-8a66-33a553c15c98" />
<img width="1680" height="1050" alt="Screenshot 2026-10-21 at 09 30 43" src="https://github.com/user-attachments/assets/0a5ec16c-661c-4147-b56f-7e287e4b1918" />
<img width="1680" height="1050" alt="Screenshot 2026-10-21 at 09 30 54" src="https://github.com/user-attachments/assets/e080018e-4140-46a5-a526-ff578b82d26b" />

**Assets Used**
- [Dark Fantasy Kit [Lite]](https://assetstore.unity.com/packages/3d/environments/fantasy/dark-fantasy-kit-lite-127925): building and furniture models and textures

## Instructions for Testing the Project
 
- **Player controls:**
  - WASD for horizontal movement
  - Space to jump
  - Move the mouse to orbit the camera around the player
  - Align the crosshair dot with an interactable object and left-click to interact
  - Esc, then click the Inventory button in the top right to see your inventory. Click outside the gray UI to close it.
- **Current interactables:**
  - Keys (picked up into the inventory)
  - Cabinets/lockers (may be locked and require a key)
  - Doors (may be locked and require a key)
- **Walkthrough of the first room:**
  1. Pick up the key on the floor next to the dummy sitting in the corner.
  2. Of the two lockers (cabinets with shelves), open the **left** one with that key. Clicking it without the key shows a "locked" popup.
  3. Pick up the second key inside the locker.
  4. Use the second key to open the room's door.
- **Testing the caretaker:**
  - Step into the hallway. The caretaker patrols between two points, pausing at each.
  - Walk into its view. It speeds up and chases you. Break line of sight behind a wall and it returns to its route.
  - Let it touch you while it can see you to bring up the lose screen.
- **Testing the lose screen:**
  - **Back to Checkpoint:** you respawn in the first room with your keys and unlocked doors kept, and the caretaker goes back to its starting point.
  - **Restart:** the whole game resets. Keys are back in their original spots and the doors are locked again.
