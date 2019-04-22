//Assets/Editor/KeywordReplace.cs
using UnityEngine;
using UnityEditor;
using System.Collections;

public class KeywordReplace : UnityEditor.AssetModificationProcessor
{

   public static void OnWillCreateAsset ( string path )
   {
     path = path.Replace( ".meta", "" );
     int index = path.LastIndexOf( "." );
     string file = path.Substring( index );
     if ( file != ".cs" && file != ".js" && file != ".boo" ) return;
     index = Application.dataPath.LastIndexOf( "Assets" );
     path = Application.dataPath.Substring( 0, index ) + path;
     file = System.IO.File.ReadAllText( path );

     file = file.Replace( "#CREATIONDATE#", System.DateTime.Now + "" );
     file = file.Replace( "#PROJECTNAME#", PlayerSettings.productName );
     file = file.Replace( "#SMARTDEVELOPERS#", PlayerSettings.companyName );

     System.IO.File.WriteAllText( path, file );
     AssetDatabase.Refresh();
   }
}

using UnityEngine;
using UnityEditor;

public class AddNameSpace : UnityEditor.AssetModificationProcessor {

    public static void OnWillCreateAsset(string path) {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        if(index < 0) return;
        string file = path.Substring(index);
        if(file != ".cs" && file != ".js" && file != ".boo") return;
        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        file = System.IO.File.ReadAllText(path);

        string lastPart = path.Substring(path.IndexOf("Assets"));
        string _namespace = lastPart.Substring(0, lastPart.LastIndexOf('/'));
        _namespace = _namespace.Replace('/', '.');
        file = file.Replace("#NAMESPACE#", _namespace);

        System.IO.File.WriteAllText(path, file);
        AssetDatabase.Refresh();
    }
}