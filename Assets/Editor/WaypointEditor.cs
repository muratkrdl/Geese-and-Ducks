using UnityEditor;
using UnityEngine;

namespace GLOBAL
{
    [CustomEditor(typeof(Waypoint))]
    public class WaypointEditor : Editor
    {
        Waypoint Waypoint => target as Waypoint;

        [System.Obsolete]
        private void OnSceneGUI()
        {
        
            Handles.color = Color.blue;
            for (int i = 0; i < Waypoint.Points.Length; i++)
            {
                EditorGUI.BeginChangeCheck();

                //Create Handles
                Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i];
                Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint ,
                    Quaternion.identity, 0.25f,
                    new Vector3 (0.3f, 0.3f, 0.3f) , Handles.SphereHandleCap );


                // Create Text
                GUIStyle textStyle = new GUIStyle();
                textStyle.fontStyle = FontStyle.Bold;
                textStyle.fontSize = 16;
                textStyle.normal.textColor = Color.yellow;

                Vector3 textAlliment = Vector3.down * 0.45f + Vector3.right * 0.45f;
                Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[i] +textAlliment , 
                    $"{i+1}");

                EditorGUI.EndChangeCheck();

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Free Move Handle");
                    Waypoint.Points[i] = newWaypointPoint-Waypoint.CurrentPosition;
                }
            }
        }
    }
}
