using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBox : MonoBehaviour
{

    [SerializeField]
    private RectTransform selectSquireImage;

    private GameObject gameObjectRef;

    Vector3 startPos;
    Vector3 endPos;

    private Player player;

    // Use this for initialization
    void Start()
    {
        gameObjectRef = GameObject.FindGameObjectWithTag("SelectBox");
        selectSquireImage.gameObject.SetActive(false);
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameIsPaused && gameObjectRef.activeSelf == true)
        {
            gameObjectRef.SetActive(false);
        }

        if (!player.gameIsPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    startPos = Input.mousePosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                selectSquireImage.gameObject.SetActive(false);
            }

            if (Input.GetMouseButton(0))
            {
                if (!selectSquireImage.gameObject.activeInHierarchy)
                {
                    selectSquireImage.gameObject.SetActive(true);
                }

                endPos = Input.mousePosition;

                Vector3 squareStart = startPos;

                squareStart.z = 0f;

                Vector3 center = (squareStart + endPos) / 2f;

                selectSquireImage.position = center;

                float sizeX = Mathf.Abs(squareStart.x - endPos.x);
                float sizeY = Mathf.Abs(squareStart.y - endPos.y);

                selectSquireImage.sizeDelta = new Vector2(sizeX, sizeY);
            }

        }
    }
}
