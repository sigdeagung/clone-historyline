using System;
using UnityEngine;

[Serializable]
public class ControlPoint
{
  [SerializeField] Vector2 _position;
  [SerializeField] bool _smooth;
  [SerializeField] Vector2 _tangentBack;
  [SerializeField] Vector2 _tangentFront;

  public Vector2 position
  {
    get { return _position; }
    set { _position = value; }
  }

  public bool smooth
  {
    get { return _smooth; }
    set { if (_smooth = value) _tangentBack = -_tangentFront; }
  }

  public Vector2 tangentBack
  {
    get { return _tangentBack; }
    set
    {
      _tangentBack = value;
      if (_smooth) _tangentFront = _tangentFront.magnitude * -value.normalized;
    }
  }

  public Vector2 tangentFront
  {
    get { return _tangentFront; }
    set
    {
      _tangentFront = value;
      if (_smooth) _tangentBack = _tangentBack.magnitude * -value.normalized;
    }
  }

  public ControlPoint(Vector2 position, bool smooth = true)
  {
    this._position = position;
    this._smooth = smooth;
    this._tangentBack = -Vector2.one;
    this._tangentFront = Vector2.one;
  }
}

using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ControlPoint))]
public class ControlPointDrawer : PropertyDrawer
{
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {

    EditorGUI.BeginProperty(position, label, property);
    int indent = EditorGUI.indentLevel;
    EditorGUI.indentLevel = 0; //-= 1;
    var propPos = new Rect(position.x, position.y, position.x + 18, position.height);
    var prop = property.FindPropertyRelative("_smooth");
    EditorGUI.PropertyField(propPos, prop, GUIContent.none);
    propPos = new Rect(position.x + 20, position.y, position.width - 20, position.height);
    prop = property.FindPropertyRelative("_position");
    EditorGUI.PropertyField(propPos, prop, GUIContent.none);
    EditorGUI.indentLevel = indent;
    EditorGUI.EndProperty();
  }

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
  {
    return EditorGUIUtility.singleLineHeight;
  }
}

using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Path
{
  [SerializeField] List<ControlPoint> _points;

  [SerializeField] bool _loop = false;

  public Path(Vector2 position)
  {
    _points = new List<ControlPoint>
    {
      new ControlPoint(position),
      new ControlPoint(position + Vector2.right)
    };
  }

  public bool loop { get { return _loop; } set { _loop = value; } }

  public ControlPoint this[int i] { get { return _points[(_loop && i == _points.Count) ? 0 : i]; } }

  public int NumPoints { get { return _points.Count; } }

  public int NumSegments { get { return _points.Count - (_loop ? 0 : 1); } }

  public ControlPoint InsertPoint(int i, Vector2 position, bool smooth)
  {
    _points.Insert(i, new ControlPoint(position, smooth));
    return this[i];
  }
  public ControlPoint RemovePoint(int i)
  {
    var item = this[i];
    _points.RemoveAt(i);
    return item;
  }
  public Vector2[] GetBezierPointsInSegment(int i)
  {
    var pointBack = this[i];
    var pointFront = this[i + 1];
    return new Vector2[4]
    {
      pointBack.position,
      pointBack.position + pointBack.tangentFront,
      pointFront.position + pointFront.tangentBack,
      pointFront.position
    };
  }

  public ControlPoint MovePoint(int i, Vector2 position)
  {
    this[i].position = position;
    return this[i];
  }

  public ControlPoint MoveTangentBack(int i, Vector2 position)
  {
    this[i].tangentBack = position;
    return this[i];
  }

  public ControlPoint MoveTangentFront(int i, Vector2 position)
  {
    this[i].tangentFront = position;
    return this[i];
  }
}

using UnityEngine;

public class PathCreator : MonoBehaviour
{

  public Path path;

  public Path CreatePath()
  {
    return path = new Path(Vector2.zero);
  }

  void Reset()
  {
    CreatePath();
  }
}

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathCreatorEditor : Editor
{
  PathCreator creator;
  Path path;
  SerializedProperty property;

  public override void OnInspectorGUI()
  {
    serializedObject.Update();
    EditorGUI.BeginChangeCheck();
    EditorGUILayout.PropertyField(property, true);
    if (EditorGUI.EndChangeCheck()) serializedObject.ApplyModifiedProperties();
  }

  void OnSceneGUI()
  {
    Input();
    Draw();
  }

  void Input()
  {
    Event guiEvent = Event.current;
    Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
    mousePos = creator.transform.InverseTransformPoint(mousePos);
    if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
    {
      Undo.RecordObject(creator, "Insert point");
      path.InsertPoint(path.NumPoints, mousePos, false);
    }
    else if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.control)
    {
      for (int i = 0; i < path.NumPoints; i++)
      {
        if (Vector2.Distance(mousePos, path[i].position) <= .25f)
        {
          Undo.RecordObject(creator, "Remove point");
          path.RemovePoint(i);
          break;
        }
      }
    }
  }

  void Draw()
  {
    Handles.matrix = creator.transform.localToWorldMatrix;
    var rot = Tools.pivotRotation == PivotRotation.Local ? creator.transform.rotation : Quaternion.identity;
    var snap = Vector2.zero;
    Handles.CapFunction cap = Handles.CylinderHandleCap;
    for (int i = 0; i < path.NumPoints; i++)
    {
      var pos = path[i].position;
      var size = .1f;
      Handles.color = Color.red;
      Vector2 newPos = Handles.FreeMoveHandle(pos, rot, size, snap, cap);
      if (pos != newPos)
      {
        Undo.RecordObject(creator, "Move point position");
        path.MovePoint(i, newPos);
      }
      pos = newPos;
      if (path.loop || i != 0)
      {
        var tanBack = pos + path[i].tangentBack;
        Handles.color = Color.black;
        Handles.DrawLine(pos, tanBack);
        Handles.color = Color.red;
        Vector2 newTanBack = Handles.FreeMoveHandle(tanBack, rot, size, snap, cap);
        if (tanBack != newTanBack)
        {
          Undo.RecordObject(creator, "Move point tangent");
          path.MoveTangentBack(i, newTanBack - pos);
        }
      }
      if (path.loop || i != path.NumPoints - 1)
      {
        var tanFront = pos + path[i].tangentFront;
        Handles.color = Color.black;
        Handles.DrawLine(pos, tanFront);
        Handles.color = Color.red;
        Vector2 newTanFront = Handles.FreeMoveHandle(tanFront, rot, size, snap, cap);
        if (tanFront != newTanFront)
        {
          Undo.RecordObject(creator, "Move point tangent");
          path.MoveTangentFront(i, newTanFront - pos);
        }
      }
    }
  }

  [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
  static void DrawGizmo(PathCreator creator, GizmoType gizmoType)
  {
    Handles.matrix = creator.transform.localToWorldMatrix;
    var path = creator.path;
    for (int i = 0; i < path.NumSegments; i++)
    {
      Vector2[] points = path.GetBezierPointsInSegment(i);
      Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
    }
  }

  void OnEnable()
  {
    creator = (PathCreator)target;
    path = creator.path ?? creator.CreatePath();
    property = serializedObject.FindProperty("path");
  }
}