# Pillar
Pillar3D is a game engine using ECS (Entity-Component System), designed to replace Unity3D.  The goals, or Pillars of Pillar, are as follows:
1.  Maintain high overall consistency, high versatility and low redundancy
2.  Use modern technology in a fashion such that swapping out for newer versions is easy enough for the end user to perform
3.  Use terminology and class names close to theory
4.  Prioritize source code access and modification for the purpose of customizing workflow

# Workflow
Pillar3D uses delegates explicity to manage Update behaviour.  The pros to this approach are increased efficiency and explicit control over update behaviour, however this approach places a large amount of trust in the end user.  
For example, if you do something like 
`Rails.PersistentUpdate = () => {}`
instead of 
`Rails.PersistentUpdate += () => {}`
the engine will be unable to update its internal state, and thus not function properly.  

The explicit nature of this means that you have some extremely powerful options in terms of update behaviour, particularly in modifying update behaviour from a coroutine:
```void SomeClass () : base(false) => RoutineRunner.StartRoutine(UpdateSwapper());
IENumerator<YieldInstruction> UpdateSwapper () {
  Rails.Update += Update1;
  yield return new WaitForSeconds(2f);
  Rails.Update = Rails.Update - Update1 + Update2;
}
```
which would completely switch out the Update function after 2 seconds, from whatever `Update1` is defined to do to whatever `Update2` is defined to do.

# Rails
Iteration is completely handled by the Rails class.  The Rails class has booleans for Pausing and Exiting, `Pause` and `Exit`, respectively.  
Rails also has two Delegates, `Update` and `PersistantUpdate`.  `Update` runs every frame that the game isn't paused, and `PersistantUpdate` runs every frame, regardless of pause state.

# Coroutines
Coroutines are run completely separate from Entities, instead ran from the `RoutineRunner` class with `RoutineRunner.RunRoutine(IEnumerator<YieldInstruction> routine)`
Custom `YieldInstruction` behaviours are easy to write, requiring just a method override for `public Instruction GetInstruction()` where `Instruction` dictates how the YieldInstruction should act relative to the Coroutine.  
`Instruction.Exit` will forcefully break from the Coroutine
`Instruction.Resume` will execute the following block up to the next yield statement
`Instruction.Wait` will do not update the Coroutine state, and the same `YieldInstruction` behaviour will execute next in-game frame

# Parenting and Transformation
Entity Parent-Child relationships exist even in the absence of `Transform` components.  All Entities in a level exist relative to a single Root entity, which can have components and a transform added to itself, if needed.
The root object can be accessed via `level.Root` for a given level.  Tree searches can be performed from that point.  
Transform components automatically manage parenting based on the nearest available Transform component above in the level tree.  If none is found, then the Transform will act as a root-level transform.

# Version Information
Pillar3D Engine (Alpha) Version 0.1.5
Pillar3D Editor (Alpha) Version 0.0
As of 29/07/2017 (DD/MM/YYYY)
