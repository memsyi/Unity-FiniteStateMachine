using UnityEngine;
using System.Collections;

class MainGame : MenuUI, IState {
	protected FiniteStateMachine FSM;

	protected float Score = 0;

	public MainGame(FiniteStateMachine parentMachine) {
		FSM = parentMachine;
	}

	public void OnEnter(string prevState) {
		Score = 0;
	}
	
	public void OnExit(string nextState) {
		
	}
	
	public void OnUpdate() {
		
	}
	
	public override void DoGUI() {
		if (GUILayout.Button("Quit / Back To Menu", GUILayout.Width(Screen.width))) {
			FSM.Pop();
		}
		GUILayout.Space(25);
		GUILayout.Label("The waiting game!");
		GUILayout.Space(25);
		GUILayout.Label("CurrentScore: " + System.Convert.ToInt32(Score));

		Score += Time.deltaTime;
	}
}