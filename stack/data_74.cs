using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UISegmentedControlButton : Button {

public Sprite offSprite;
public Sprite onSprite;
public Color offTextColor = Color.white;
public Color onTextColor = Color.white;

private bool isSelected;
public bool IsSelected {
    get {
        return isSelected;
    }
    set {
        isSelected = value;

        Text text = this.transform.GetComponentInChildren<Text>();

        if (value) {
            this.GetComponent<Image>().sprite = onSprite;
            text.color = onTextColor;
        } else {
            this.GetComponent<Image>().sprite = offSprite;
            text.color = offTextColor;
        }
    }
}




public override void OnPointerClick(PointerEventData eventData) {

    this.transform.parent.GetComponent<UISegmentedControl>().SelectSegment(this);
    base.OnPointerClick(eventData);
}

}
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

[CustomEditor(typeof(UISegmentedControlButton))]
public class UISegmentedControlButtonEditor : UnityEditor.UI.ButtonEditor {


public override void OnInspectorGUI() {

    UISegmentedControlButton component = (UISegmentedControlButton)target;

    base.OnInspectorGUI();

    component.onSprite = (Sprite)EditorGUILayout.ObjectField("On Sprite", component.onSprite, typeof(Sprite), true);
    component.onTextColor = EditorGUILayout.ColorField("On text colour", component.onTextColor);
    component.offSprite = (Sprite)EditorGUILayout.ObjectField("Off Sprite", component.offSprite, typeof(Sprite), true);
    component.offTextColor = EditorGUILayout.ColorField("Off text colour", component.offTextColor);

}
}