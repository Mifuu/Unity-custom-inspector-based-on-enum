using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Phase))]
public class PhaseInspector : PropertyDrawer
{
    //I use PropertyDrawer because Phase does not derived from MonoBehavior
    //I don'tuse monobehavior because I want Phase to be serializable


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        //set height
        position.height = 20;//If you want line to be closer to each other in y direction, change here.

        //-----phaseName-----
        var phaseNameProperty = property.FindPropertyRelative("phaseName");
        phaseNameProperty.stringValue = EditorGUI.TextField(position, "Phase Name", phaseNameProperty.stringValue); 
        position.y += position.height;//add height after phaseName

        //-----movementType-----
        //get movementType property
        var movementTypeProperty = property.FindPropertyRelative("movementType");
        //make a new movementType variable as a Phase.PhaseMovementType and set it equal to the property after convert
        Phase.PhaseMovementType movementType = (Phase.PhaseMovementType)movementTypeProperty.enumValueIndex;//get Index and type cast it back to enum
        //create popup to get the new value
        movementType = (Phase.PhaseMovementType)EditorGUI.EnumPopup(position, "", movementType);//""(label) is important. Else the title of the popup will appear
        //send back the new value
        movementTypeProperty.enumValueIndex = (int)movementType;
        //position.y += position.height;//add height after movementType field (not needed if the label is "")

        property.isExpanded = EditorGUI.Foldout (position, property.isExpanded, label);
        //using property.isExpanded is very convenient, but there's a catch. I can not have multiple foldout, because there's only 1 variable.
        //If I want multiple foldout, I may need a dictionary or something to store it instead.
        //However, since I only want 1 foldout (setting) that differ through each enum, it will works fine for now.

        //the foldout will be different for every type of movementType
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

    //GetPropertyHeight is like getting the height of the whole box of script in the inspector before filling in
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return property.isExpanded ? 20*5 : 20*3;
        //kind of inconvenient but works for now. If I have more variable I have to adjust these number myself.
    }
}
