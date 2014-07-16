Liscence
========
This, like all other original works on my github is public domain. Do with it as you will.
About
=====
I've never been able to find a Fnite State Machine implementation in unity that suits my fancy. They are usually too simple, or too complicated. I tried to make one that fits my needs. The main focus of my FSM implementation is to decople states from the controlling logic.

This project was built using Unity3D version 4.5.0f6, run the ```TestUIState``` scene for a demo.

API & Usage
===========
I'm going to try to explain the usage of my FSM in the readme, but the best explanation is the [TestUIScene.cs](https://github.com/gszauer/Unity-FiniteStateMachine/blob/master/Assets/Testing/Scripts/TestUIState.cs) file.

A ```FiniteStateMachine``` object is just that, an object. Monobehaviours should contain one or more FSM objects. Each machine needs to have one or more states registered to it via the ```Register``` method. States registered must implement the ```IState``` interface. The ```IState``` interface only has three methods: ```OnEnter```, ```OnExit``` and ```OnUpdate```. These methods are called automatically by the machine. ```OnEnter``` and ```OnExit``` each take a string representing the previous state that was loaded and the new state that is going to be loaded respectiveley. The default state of the machine is the first state registered. You can change this with the ```EntryPoint``` function. Once states are registered connections must be hooked up. To get a reference to one of the states in the machine use the ```State``` function.

To register a response to an event use the ```FSState``` classes ```On``` function. To get a reference to an ```FSState``` use the ```FiniteStateMachine``` classes ```State``` function. If you call ```On``` with just one string argument, it will return a ```FSEvent``` object. Use this event object to specify how the state change should behave. Valid options are ```Enter``` which will replace the top element of the state stack, ```Push``` which will push a new state onto the stack and ```Pop``` which will pop a state from the stack. Whatever action is selected, ```FSEvent``` will return the ```FSState``` object that owns it; this allows you to chain methods like: ```FSM.State("MainMenu").On("OPEN_AUDIO").Enter("AudioMenu").On("PLAY_GAME").Push("MainGame");```

Optionally you can call ```On``` with a delegate for it's second argument arguments. When using ```On``` with the delegate arguments it no longer returns a ```FSEvent```, instead it returns a ```FSState```. The delegate can take up to three arguments. This delegate is an evaluator. When the action specified as the first argument of ```On``` happens, this delegate is executed.

Actions (or events, whatever vocabulary you want to use) must be triggered with the ```Trigger``` method. ```Trigger``` takes a string that is the action name as it's first argument, and up to three optional arguments. If any optional arguments are passed, we assume that the action is an evaluator.

The ```Update``` method of the ```FiniteStateMachine``` should be called every frame. You can get the ```FSState``` of the current frame trough the ```CurrentState``` accessor. Within the ```FSState``` state there is a ```StateObject``` accessor. You can cast ```StateObject``` to whatever base object it actually is.

Future Work
===========
I'm considering making the OnEnter and OnExit functions asynch. It might help to have those functions wait while a transition is compleate, without updating the current state. This however, i'm not sure about just yet.
