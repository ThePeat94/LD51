using Nidavellir.PathManagement;
using UnityEditor;
using UnityEngine;

namespace Nidavellir.Editor.PathManagement
{
    [CustomEditor(typeof(Path))]
    public class PathEditor : UnityEditor.Editor
    {
        private Path PathInstance => this.target as Path;

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Add new Waypoint"))
            {
                Debug.Log("Add new waypoint");
                var newWaypoint = new GameObject($"New Waypoint ({this.PathInstance.WayPoints.Count})");
                if (this.PathInstance.WayPoints.Count > 0)
                    newWaypoint.transform.position = this.PathInstance.WayPoints[^1].position + Vector3.forward;
                else
                    newWaypoint.transform.position = Vector3.zero;
                
                this.PathInstance.WayPoints.Add(newWaypoint.transform);
            }

            this.DrawDefaultInspector();
        }

        private void OnSceneGUI()
        {
            for (var i = 0; i < this.PathInstance.WayPoints.Count; i++)
            {
                var waypoint = this.PathInstance.WayPoints[i];
                EditorGUI.BeginChangeCheck();

                var currentWayPointPos = waypoint.position;
                var newWayPointPosition = Handles.FreeMoveHandle(currentWayPointPos, 0.7f, Vector3.one * 0.1f, Handles.SphereHandleCap);

                var labelStyle = new GUIStyle();
                labelStyle.fontStyle = FontStyle.Bold;
                labelStyle.fontSize = 18;

                Handles.Label(currentWayPointPos, $"Waypoint {i + 1}", labelStyle);

                EditorGUI.EndChangeCheck();

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(this.PathInstance, "Free move path waypoint");
                    waypoint.position = newWayPointPosition;
                }
            }
        }
    }
}