using UnityEngine;
using System.Collections;

class QuitGameUI : MenuUI, IState {
	public void OnEnter(string prevState) {
		
	}
	
	public void OnExit(string nextState) {
		
	}
	
	public void OnUpdate() {
		
	}
	
	public override void DoGUI() {
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Confirm", GUILayout.Width(Screen.width / 2), GUILayout.Height(Screen.height))) {
			TestUIState.Events.Trigger("ConfirmQuit");
		}
		
		if (GUILayout.Button("Cancel", GUILayout.Width(Screen.width / 2), GUILayout.Height(Screen.height))) {
			TestUIState.Events.Trigger("CancelQuit");
		}
		GUILayout.EndHorizontal();
	}
}