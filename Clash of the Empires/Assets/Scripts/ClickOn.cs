using UnityEngine;

public class ClickOn : MonoBehaviour
{
    [SerializeField]
    private Material notSelected;

    [SerializeField]
    private Material selected;

    private MeshRenderer myRend;

    [HideInInspector]
    public bool currentlySelected = false;

    private void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        Camera.main.gameObject.GetComponent<Movement>().selectableObjects.Add(this.gameObject); //Adds the unit to the selectableObjects list
        ClickMe();
    }

    public void ClickMe()
    {
        if (currentlySelected == false)
        {
            myRend.material = notSelected;
        }

        else
        {
            myRend.material = selected;
        }
    }
}