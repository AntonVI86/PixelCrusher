using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicturesCreator : MonoBehaviour
{
    [SerializeField] private GameObject _spriteTemplate;
    [SerializeField] private Texture2D _picture;

    public List<GameObject> _elements = new List<GameObject>();

    private int rowCount;
    private int colCount;

    public void GenerateGrid(float startXPos, float startYPos)
    {
        float step = 0.09f;

        rowCount = _picture.width;
        colCount = _picture.height;

        for (int i = 0; i < colCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                startXPos += step;

                if (_picture.GetPixel(j, i).a != 0)
                {
                    GameObject newSprite = Instantiate(_spriteTemplate, transform);
                    newSprite.transform.position = new Vector2(startXPos, startYPos);
                    newSprite.GetComponent<SpriteRenderer>().color = _picture.GetPixel(j, i);
                    _elements.Add(newSprite);
                }
            }

            startYPos += step;
            startXPos = transform.position.x;
        }
    }
}
