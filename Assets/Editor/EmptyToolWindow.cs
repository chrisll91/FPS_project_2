using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelDesignerTool : EditorWindow
{
    private bool hasValidSelection;
    private bool isPreviewMode = true;
    private bool showConfirmDialog;
    private bool shouldChildToOriginal = false;


    // for the user to change
    private float spacing = 0f;
    private int leftCount = 0;
    private int rightCount = 0;
    private string copyNamePrefix = "Copy";


    // track previous spacing for updating
    private float previousSpacing;
    private int previousLeftCount;
    private int previousRightCount;

    //copies list
    private readonly List<GameObject> previewCopies = new();
    private readonly List<GameObject> committedCopies = new();

    //
    private GameObject selectedObject;
    private GameObject previousSelectedObject;
    private Renderer selectedRenderer;
    private string selectionMessage;
    private Vector3 objectSize;
    private Material selectedMaterial;
    private GUIStyle statusStyle;



    [MenuItem("Tools/Level Designer Tool")]
    public static void OpenWindow()
    {
        var window = GetWindow<LevelDesignerTool>();
        window.titleContent = new GUIContent("Level Designer Tool");
        window.minSize = new Vector2(350, 250);
    }

    private void OnGUI()
    {
        // Handle hotkeys first so changes are applied immediately.
        HandleHotkeys();
        DrawHeader();
        DrawInstructions();
        DrawSelectionStatus();
        CreateCopiesOfItem();
        DrawObjectSize();
    }

    private void DrawHeader()
    {
        GUILayout.Label("Level Designer Utility Tool", EditorStyles.boldLabel);

        // Tooltip text appears when hovering over "Hotkeys".
        GUILayout.Label(new GUIContent("Hotkeys", "Ctrl + Right = +1 Right\nCtrl + Left = -1 Right\nCtrl + Up = +1 Left\nCtrl + Down = -1 Left"));
        GUILayout.Space(15);
    }

    private void DrawInstructions()
    {
        EditorGUILayout.HelpBox(
            "1. Select one or more GameObjects in the scene.\n" +
            "2. The tool will validate your selection.\n" +
            "3. Actions will become available when the selection is valid.",
            MessageType.Info);
    }

    private void DrawSelectionStatus()
    {
        // If selection changes, clear previews and reset values.
        if (Selection.activeGameObject != previousSelectedObject)
        {
            previewCopies.ForEach(DestroyImmediate);
            previewCopies.Clear();

            rightCount = 0;
            leftCount = 0;
            spacing = 0f;
            selectedMaterial = null;
            isPreviewMode = true;

            previousSpacing = spacing;
            previousLeftCount = leftCount;
            previousRightCount = rightCount;

            previousSelectedObject = Selection.activeGameObject;
        }

        selectedObject = null;
        selectedRenderer = null;
        hasValidSelection = false;

        // Validate selection
        if (Selection.gameObjects.Length == 0)
        {
            selectionMessage = "No object selected";

        }
        else if (Selection.gameObjects.Length > 1)
        {
            selectionMessage = "Select ONE object ONLY please";
            //EditorUtility.DisplayDialog("Invalid Selection", "You must select ONLY ONE object.", "OK");
        }
        else
        {
            selectedObject = Selection.activeGameObject;
            selectedRenderer = selectedObject.GetComponent<Renderer>();

            if (selectedRenderer == null)
            {
                selectionMessage = "Selected object doesnt have a renderer";
                //EditorUtility.DisplayDialog("Invalid Selection", "You must select an object with a renderer.", "OK");
            }
            else
            {
                hasValidSelection = true;
                selectionMessage = "Valid object selected";
            }
        }

        // Display selection status
        GUI.color = hasValidSelection ? Color.green : Color.red;
        GUILayout.Label(selectionMessage);
        GUI.color = Color.white;

        GUILayout.Label("Selected Object: " +
            (selectedObject != null ? selectedObject.name : "None"));

        GUILayout.Space(10);
    }

    private void CreateCopiesOfItem()
    {
        // Material picker (ObjectField)
        selectedMaterial = (Material)EditorGUILayout.ObjectField(
            new GUIContent("Material", "Change material for original + copies"),
            selectedMaterial, typeof(Material), false);

        // Apply material to original + preview copies immediately when changed.
        if (selectedMaterial != null)
        {
            ApplyMaterialToAll(selectedMaterial);
        }

        // Toggle for parenting copies to original when committed
        shouldChildToOriginal = GUILayout.Toggle(
            shouldChildToOriginal,
            new GUIContent("Child to Parent", "If enabled, copies will be parented to the original object when committed."));

        // Name prefix for the copies
        copyNamePrefix = EditorGUILayout.TextField("Copy Name", copyNamePrefix);

        // Commit button opens confirmation dialog
        if (GUILayout.Button("Commit Copies"))
        {
            showConfirmDialog = true;
        }

        // Confirmation dialog
        if (showConfirmDialog)
        {
            if (EditorUtility.DisplayDialog("Confirm Commit",
                "Are you sure you want to commit the copies?",
                "Yes", "No"))
            {
                isPreviewMode = false;
                committedCopies.AddRange(previewCopies);

                // If parenting is enabled, parent each copy to the original
                if (shouldChildToOriginal)
                {
                    foreach (var copy in previewCopies)
                    {
                        Undo.SetTransformParent(copy.transform, selectedObject.transform, "Parent Copy");
                    }
                }

                previewCopies.Clear();
            }

            showConfirmDialog = false;
        }

        // Only show controls if selection is valid and preview mode is active
        if (hasValidSelection && isPreviewMode)
        {
            spacing = EditorGUILayout.FloatField("Extra Spacing", spacing);
            spacing = Mathf.Max(0f, spacing);

            leftCount = EditorGUILayout.IntField("Left X Count", leftCount);
            leftCount = Mathf.Max(0, leftCount);

            int newCount = EditorGUILayout.IntField("Right X Axis Count", rightCount);
            newCount = Mathf.Max(0, newCount);

            // If values changed, recreate preview copies
            if (newCount != previousRightCount || leftCount != previousLeftCount || spacing != previousSpacing)
            {
                // delete all old preview copies
                for (int i = previewCopies.Count - 1; i >= 0; i--)
                    DestroyImmediate(previewCopies[i]);

                previewCopies.Clear();

                // generate left copies
                for (int i = 1; i <= leftCount; i++)
                {
                    Vector3 offset = Vector3.left * (objectSize.x + spacing) * i;

                    GameObject copy = Instantiate(selectedObject,
                        selectedObject.transform.position + offset,
                        selectedObject.transform.rotation);

                    Undo.RegisterCreatedObjectUndo(copy, "Create Copy");
                    copy.name = $"{copyNamePrefix}_L{i}";
                    previewCopies.Add(copy);
                }

                // generate right copies
                for (int i = 1; i <= newCount; i++)
                {
                    Vector3 offset = Vector3.right * (objectSize.x + spacing) * i;

                    GameObject copy = Instantiate(selectedObject,
                        selectedObject.transform.position + offset,
                        selectedObject.transform.rotation);

                    Undo.RegisterCreatedObjectUndo(copy, "Create Copy");
                    copy.name = $"{copyNamePrefix}_R{i}";
                    previewCopies.Add(copy);
                }

                rightCount = newCount;

                previousSpacing = spacing;
                previousLeftCount = leftCount;
                previousRightCount = newCount;
            }
        }
    }

    private void DrawObjectSize()
    {
        if (!hasValidSelection) return;

        objectSize = selectedRenderer.bounds.size;

        EditorGUILayout.LabelField("Object Size (World Units)", EditorStyles.boldLabel);
        EditorGUILayout.Vector3Field("Size", objectSize);
    }

    private void HandleHotkeys()
    {
        Event e = Event.current;

        // Only respond to key presses while Ctrl is held
        if (e.type != EventType.KeyDown || !e.control) return;

        // Hotkey logic
        if (e.keyCode == KeyCode.RightArrow) rightCount++;
        if (e.keyCode == KeyCode.LeftArrow) rightCount = Mathf.Max(0, rightCount - 1);
        if (e.keyCode == KeyCode.UpArrow) leftCount++;
        if (e.keyCode == KeyCode.DownArrow) leftCount = Mathf.Max(0, leftCount - 1);

        e.Use(); // consume the event
    }

    private void ApplyMaterialToAll(Material mat)
    {
        if (selectedObject == null) return;

        // Apply to original
        var originalRenderer = selectedObject.GetComponent<Renderer>();
        if (originalRenderer != null)
        {
            Undo.RecordObject(originalRenderer, "Change Material");
            originalRenderer.sharedMaterial = mat;
        }

        // Apply to all preview copies
        foreach (var copy in previewCopies)
        {
            var rend = copy.GetComponent<Renderer>();
            if (rend != null)
            {
                Undo.RecordObject(rend, "Change Material");
                rend.sharedMaterial = mat;
            }
        }
    }
}
