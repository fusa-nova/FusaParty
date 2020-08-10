using UnityEngine;

public class CameraRayCast : MonoBehaviour
{



    private Game1Manager game1Manager;
    private string objectName;
    private int objectNumber;


    void Start()
    {
        game1Manager = GameObject.Find("Game1Manager").GetComponent<Game1Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (1 == 0)
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hit;

        //        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //        {
        //            objectName = hit.collider.gameObject.name.Substring(0, 4);
        //            objectNumber = int.Parse(hit.collider.gameObject.name.Substring(4, 1));
        //            switch (objectName)
        //            {
        //                case "Cube":
        //                    if (game1Manager.playerAnswerCube == 0)
        //                    {
        //                        game1Manager.playerAnswerCube = objectNumber;
        //                        hit.collider.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
        //                    }
        //                    else
        //                    {
        //                        game1Manager.playerAnswerCube = 0;
        //                        hit.collider.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
        //                    }
        //                    break;
        //                case "Quad":
        //                    if (game1Manager.playerAnswerQuad == 0)
        //                    {
        //                        game1Manager.playerAnswerQuad = objectNumber;
        //                        hit.collider.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
        //                    }
        //                    else
        //                    {
        //                        game1Manager.playerAnswerQuad = 0;
        //                        hit.collider.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //            Debug.Log("쿼드선택" + game1Manager.playerAnswerQuad);
        //            Debug.Log("큐브선택" + game1Manager.playerAnswerCube);
        //            if (game1Manager.playerAnswerCube != 0 && game1Manager.playerAnswerQuad != 0)
        //            {
        //                if (game1Manager.answerCube == game1Manager.playerAnswerCube && game1Manager.answerQuad == game1Manager.playerAnswerQuad)
        //                {
        //                    Debug.Log("정답입니다");
        //                }
        //                else
        //                {
        //                    Debug.Log("오답입니다");
        //                }
        //            }
        //        }
        //    }
    }
}
