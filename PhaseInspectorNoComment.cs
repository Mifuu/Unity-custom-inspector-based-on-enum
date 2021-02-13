using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Phase))]
public class PhaseInspector : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        position.height = 20;

        //-----phaseName-----
        var phaseNameProperty = property.FindPropertyRelative("phaseName");
        phaseNameProperty.stringValue = EditorGUI.TextField(position, "Phase Name", phaseNameProperty.stringValue); 
        position.y += position.height;

        //-----movementType-----
        var movementTypeProperty = property.FindPropertyRelative("movementType");
        Phase.PhaseMovementType movementType = (Phase.PhaseMovementType)movementTypeProperty.enumValueIndex;
        movementType = (Phase.PhaseMovementType)EditorGUI.EnumPopup(position, "", movementType);
        movementTypeProperty.enumValueIndex = (int)movementType;
        property.isExpanded = EditorGUI.Foldout (position, property.isExpanded, label);


        if (property.isExpanded) {
            position.y += position.height;
            if (movementType == Phase.PhaseMovementType.Waypoint) {
                //properties special for Waypoint
                //-----a-----
                var aProperty = property.FindPropertyRelative("a");
                aProperty.intValue = EditorGUI.IntField(position, "a", aProperty.intValue);
                position.y += position.height;

                //-----b-----
                var bProperty = property.FindPropertyRelative("b");
                bProperty.intValue = EditorGUI.IntField(position, "b", bProperty.intValue);
                position.y += position.height;
            } else if (movementType == Phase.PhaseMovementType.SetInitial) {
                //properties special for SetInitial
                //-----c-----
                var cProperty = property.FindPropertyRelative("c");
                cProperty.intValue = EditorGUI.IntField(position, "c", cProperty.intValue);
                position.y += position.height;
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return property.isExpanded ? 20*5 : 20*3;
    }
}
