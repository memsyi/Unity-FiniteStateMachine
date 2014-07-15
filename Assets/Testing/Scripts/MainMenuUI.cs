using UnityEngine;
using System.Collections;

public class MainMenuUI : MenuUI, IState {
	public void OnEnter(string prevState) {
		
	}
	
	public void OnExit(string nextState) {
		
	}
	
	public void OnUpdate() {
		
	}
	
	public override void DoGUI() {
		if (GUILayout.Button("Play Game", GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 3))) {
			TestUIState.Events.Trigger("OpenMainGame");
		}
		
		if (GUILayout.Button("Audio Menu", GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 3))) {
			TestUIState.Events.Trigger("OpenAudioMenu");
		}
		
		if (GUILayout.Button("Quit Game", GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 3))) {
			TestUIState.Events.Trigger("QuitGame");
		}
	}
}