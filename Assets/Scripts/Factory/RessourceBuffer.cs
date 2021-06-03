using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourceBuffer : MonoBehaviour
{
    private List<GameObject> monsterWaitingBuffer;

    public GameObject imagePrefab;

    public Sprite reconditeSprite;
    public Sprite sodaSprite;
    public Sprite meatSprite;
    public Sprite weedSprite;
    public Sprite fungusSprite;
    public Sprite purpleCristalSprite;

    // Start is called before the first frame update
    void Start()
    {
        monsterWaitingBuffer = new List<GameObject>();
    }

    // Update is called once per frame
    public void UpdateMonsterWaitingBuffer(Queue<Factory.Ressource> monsterWaiting)
    {
        foreach (GameObject go in monsterWaitingBuffer)
        {
            Destroy(go);
        }

        monsterWaitingBuffer = new List<GameObject>();

        foreach (Factory.Ressource ressource in monsterWaiting)
        {

            GameObject img = (GameObject) Instantiate(imagePrefab, this.gameObject.transform);
            switch (ressource)
            {
                case Factory.Ressource.RECONDITE:
                    img.GetComponent<Image>().sprite = reconditeSprite;
                    break;

                case Factory.Ressource.FUNGUS:
                    img.GetComponent<Image>().sprite = fungusSprite;
                    break;

                case Factory.Ressource.SODA:
                    img.GetComponent<Image>().sprite = sodaSprite;
                    break;
            }
            monsterWaitingBuffer.Add(img);
        }
    }
}
