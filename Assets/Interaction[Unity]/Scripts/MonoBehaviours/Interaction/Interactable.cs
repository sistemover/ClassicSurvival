using UnityEngine;
using ClassicSurvival;

public class Interactable : MonoBehaviour
{
    public ConditionCollection[] conditionCollections = new ConditionCollection[0];
    public ReactionCollection defaultReactionCollection;

	private Pickups m_reactionObjects;
	public Pickups ReactionObjects
	{
		get
		{
			if (m_reactionObjects == null)
				m_reactionObjects = gameObject.GetComponentInChildren<Pickups> ();
			return m_reactionObjects;
		}
	}

	//**************************************************************************************************************

    public void Interact ()
    {
        for (int i = 0; i < conditionCollections.Length; i++)
        {
            if (conditionCollections[i].CheckAndReact ())
                return;
        }

        defaultReactionCollection.React ();
    }
}
