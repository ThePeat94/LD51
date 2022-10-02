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
                var newWaypoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                newWaypoint.name = $"New Waypoint ({this.PathInstance.WayPoints.Count})";
                newWaypoint.transform.SetParent(this.PathInstance.transform);
                if (this.PathInstance.WayPoints.Count > 0)
                    newWaypoint.transform.position = this.PathInstance.WayPoints[^1]
                        .position + Vector3.forward;
                else
                    newWaypoint.transform.position = Vector3.zero;

                this.PathInstance.WayPoints.Add(newWaypoint.transform);
            }
            else if (GUILayout.Button("Clear Waypoints"))
            {
                this.PathInstance.WayPoints.Clear();
                while (this.PathInstance.transform.childCount > 0)
                {
                    Object.DestroyImmediate(this.PathInstance.transform.GetChild(0).gameObject);
                }
                
            }
            else if (GUILayout.Button("Ground Paths"))
            {
                foreach (var wayPoint in this.PathInstance.WayPoints)
                {
                    var currentPos = wayPoint.position;
                    wayPoint.position = new Vector3(currentPos.x, 0f, currentPos.z);
                }
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
                    waypoint.position = new Vector3(newWayPointPosition.x, 1f, newWayPointPosition.z);
                }
            }
        }
    }
}