using UnityEngine;
using System.Collections;

public class TestUIState : MonoBehaviour {
	// Create a global event system that is specific to the testui
	public static EventSystem.Dispatcher Events = new EventSystem.Dispatcher();
	// The ui will be driven by a state machine
	public FiniteStateMachine FSM = new FiniteStateMachine();

	public void Awake() {
		// Register several states with the machine
		FSM.Register("MainMenu", new MainMenuUI());
		FSM.Register("AudioMenu", new AudioMenuUI());
		FSM.Register("MainGame", new MainGame(FSM));
		FSM.Register("QuitGame", new QuitGameUI());
		// The entry point is implicitly set to the first item registered
		// This is just for demo purposes
		FSM.EntryPoint("MainMenu"); 

		// The main menu responds to the following actions: OPEN_AUDIO, PLAY_GAME, QUIT_GAME
		// OPEN_AUDIO and QUIT_GAME replace the top of the state stack. 
		// PLAY_GAME adds a new item to the state stack.
		FSM.State("MainMenu").On("OPEN_AUDIO").Enter("AudioMenu")
			.On("PLAY_GAME").Push("MainGame")
			.On("QUIT_GAME").Enter("QuitGame");

		// The quit menu responds to the PROCESS_QUIT action. If it is called with an
		// argument of true, this component is disabled. If false, the FSM switches
		// back to the main menu
		FSM.State("QuitGame").On("PROCESS_QUIT", delegate(bool sure) {
				if (sure) {
					gameObject.GetComponent<TestUIState>().enabled = false;
					Camera.main.backgroundColor = Color.black;
				} else { FSM.Enter("MainMenu"); }
			});

		// The game class is responsible for poping back here to the main menu
		// See MainGame.cs for how to pop back to the main menu

		// The audio menu holds it's own state, and handles volume logic, it only cares
		// about getting the player back to the main menu
		FSM.State("AudioMenu").On("BACK_TO_MENU").Enter("MainMenu");

		// Hook the event system up to state machine actions
		Events.On("OpenMainGame", delegate() { FSM.CurrentState.Trigger("PLAY_GAME"); });
		Events.On("OpenAudioMenu", delegate() { FSM.CurrentState.Trigger("OPEN_AUDIO"); });
		Events.On("QuitGame", delegate() { FSM.CurrentState.Trigger("QUIT_GAME"); });

		Events.On("ConfirmQuit", delegate() { FSM.CurrentState.Trigger("PROCESS_QUIT", true); });
		Events.On("CancelQuit", delegate() { FSM.CurrentState.Trigger("PROCESS_QUIT", false); });

		Events.On("BackToMenu", delegate() { FSM.CurrentState.Trigger("BACK_TO_MENU", false); });
	}

	public void Update() {
		// Update HAS TO BE CALLED!
		FSM.Update();
	}

	void OnGUI() {
		if (FSM.CurrentState == null)
			return;

		// This is how you can get your object back
		MenuUI ui = (MenuUI)FSM.CurrentState.StateObject;

		ui.DoGUI();
	}
}
