using UnityEditor;

public class ReactionEditorIndex{}

[CustomEditor(typeof(CanvasReaction))]
public class CanvasReactionEditor : ReactionEditor 
{
	protected override string GetFoldoutLabel ()
	{
		return "Canvas Reaction";
	}
}

[CustomEditor(typeof(DestroyGameObjectReaction))]
public class DestroyGameObjectEditor : ReactionEditor 
{
	protected override string GetFoldoutLabel ()
	{
		return "Destroy GameObject Reaction";
	}
}