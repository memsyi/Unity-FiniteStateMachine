using UnityEngine;
using System.Collections;

class AudioMenuUI : MenuUI, IState {
	float volume = 0.5f;
	float backupVolume = 0.0f;

	public void OnEnter(string prevState) {
		backupVolume = volume;
	}
	
	public void OnExit(string nextState) {

	}
	
	public void OnUpdate() {
		
	}
	
	public override void DoGUI() {
		GUILayout.Space(25.0f);
		volume = GUILayout.HorizontalSlider(volume, 0.0f, 1.0f, GUILayout.Width(Screen.width));

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label("Volume: " + System.Convert.ToInt32(volume * 100.0f) + " %");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Cancel", GUILayout.Height(75.0f))) {
			volume = backupVolume;
			TestUIState.Events.Trigger("BackToMenu");
		}
		if (GUILayout.Button("Confirm", GUILayout.Height(75.0f))) {
			TestUIState.Events.Trigger("BackToMenu");
		}
		GUILayout.EndHorizontal();

	}
}