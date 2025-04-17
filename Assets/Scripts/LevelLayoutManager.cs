using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class LevelLayoutManager : MonoBehaviour
{
    [System.Serializable]
    public class LayoutObject
    {
        public GameObject target;
        [HideInInspector] public Vector2 relativePosition;
        public Vector2 offset;
    }

    public List<LayoutObject> objects = new List<LayoutObject>();

    void Start()
    {
        if (Application.isPlaying)
            ApplyLayout();
    }

    private void ApplyLayout()
    {
        if (Camera.main == null) return;

        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        Vector3 camCenter = Camera.main.transform.position;

        foreach (var obj in objects)
        {
            if (obj.target == null) continue;

            float posX = obj.relativePosition.x * (width / 2f) + obj.offset.x;
            float posY = obj.relativePosition.y * (height / 2f) + obj.offset.y;

            obj.target.transform.position = new Vector3(
                posX + camCenter.x,
                posY + camCenter.y,
                obj.target.transform.position.z
            );
        }
    }

#if UNITY_EDITOR
    public void CaptureRelativePositions()
    {
        if (Camera.main == null) return;

        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        Vector3 camCenter = Camera.main.transform.position;

        foreach (var obj in objects)
        {
            if (obj.target == null) continue;

            Vector3 localPos = obj.target.transform.position - camCenter;

            obj.relativePosition = new Vector2(
                Mathf.Clamp(localPos.x / (width / 2f), -1f, 1f),
                Mathf.Clamp(localPos.y / (height / 2f), -1f, 1f)
            );
        }

        EditorUtility.SetDirty(this);
    }

    [CustomEditor(typeof(LevelLayoutManager))]
    public class LevelLayoutManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LevelLayoutManager manager = (LevelLayoutManager)target;

            GUILayout.Space(10);
            GUILayout.Label("🛠 Layout Tools", EditorStyles.boldLabel);

            if (GUILayout.Button("Add List"))
            {
                foreach (var obj in Selection.gameObjects)
                {
                    if (!manager.objects.Exists(o => o.target == obj))
                    {
                        manager.objects.Add(new LayoutObject { target = obj });
                    }
                }
                EditorUtility.SetDirty(manager);
            }

            if (GUILayout.Button("Set Pos"))
            {
                manager.CaptureRelativePositions();
            }
        }
    }
#endif
}
