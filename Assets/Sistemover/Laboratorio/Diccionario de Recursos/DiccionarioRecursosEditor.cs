using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DiccionarioRecursos))]
public class DiccionarioRecursosEditor : Editor 
{
	DiccionarioRecursos myTarget;
	SerializedObject GetTarget;
	SerializedProperty ThisList;
	int ListSize;

	void OnEnable()
	{
		myTarget = (DiccionarioRecursos)target;
		GetTarget = new SerializedObject (myTarget);
		ThisList = GetTarget.FindProperty ("Diccionario");
	}

	public override void OnInspectorGUI()
	{
		//Actualiza el contenido del Inspector.
		GetTarget.Update ();

		EditorGUILayout.Space (); //Añade un Espacio en el Inspector.

		//Despliega el contenido en la ventana de Inspector.
		for (int i = 0; i < ThisList.arraySize; i++) 
		{
			//Captura las variables que se mostrarán en el Inspector.
			SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex (i);
			SerializedProperty MyRecursoNombre = MyListRef.FindPropertyRelative ("Nombre");
			SerializedProperty MyRecursoPath = MyListRef.FindPropertyRelative ("Path");

			EditorGUILayout.BeginHorizontal ();//Comienza sección Horizontal.

				//Instancia las Variables en el Editor.
			MyRecursoNombre.stringValue = EditorGUILayout.TextField
				(
					"",
					MyRecursoNombre .stringValue,
					GUILayout.MinWidth(100), 
					GUILayout.MaxWidth(200)
				);
			MyRecursoPath.stringValue = EditorGUILayout.TextField
				(
					"",
					MyRecursoPath .stringValue, 
					GUILayout.MinWidth(100)
				);

				//Añade botón para quitar de la lista.
			if (GUILayout.Button("Quitar", GUILayout.MaxWidth(45))) 
				{
					ThisList.DeleteArrayElementAtIndex (i);
				}
			EditorGUILayout.EndHorizontal ();//Finaliza el Horizontal.

			//EditorGUILayout.Space (); //Añade un Espacio en el Inspector.
		}

		//Añade botón de Añadir y se programa su función para la lista.
		if (GUILayout.Button("AÑADIR"))
		{
			myTarget.Diccionario.Add (new DiccionarioRecursos.Recurso ());
		}

		//Aplica los cambios a la lista.
		GetTarget.ApplyModifiedProperties();
	}
}
