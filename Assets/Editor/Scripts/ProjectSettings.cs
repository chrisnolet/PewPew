using UnityEditor;

public static class ProjectSettingsMenu {
  [MenuItem("Settings/Input")]
  static void Input() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Input");
  }

  [MenuItem("Settings/Tags and Layers")]
  static void TagsAndLayers() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Tags and Layers");
  }

  [MenuItem("Settings/Audio")]
  static void Audio() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Audio");
  }

  [MenuItem("Settings/Time")]
  static void Time() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Time");
  }

  [MenuItem("Settings/Player")]
  static void Player() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Player");
  }

  [MenuItem("Settings/Physics")]
  static void Physics() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Physics");
  }

  [MenuItem("Settings/Physics 2D")]
  static void Physics2D() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Physics 2D");
  }

  [MenuItem("Settings/Quality")]
  static void Quality() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Quality");
  }

  [MenuItem("Settings/Graphics")]
  static void Graphics() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Graphics");
  }

  [MenuItem("Settings/Network")]
  static void Network() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Network");
  }

  [MenuItem("Settings/Editor")]
  static void Editor() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Editor");
  }

  [MenuItem("Settings/Script Execution Order")]
  static void ScriptExecutionOrder() {
    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Script Execution Order");
  }
}
