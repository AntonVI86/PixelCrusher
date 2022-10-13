using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicturesCreator : ObjectPool
{
    [SerializeField] private GameObject _spriteTemplate;
    [SerializeField] private Texture2D _picture;

    private int rowCount;
    private int colCount;

    public void GenerateGrid(float startXPos, float startYPos)
    {
        rowCount = _picture.width;
        colCount = _picture.height;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                startXPos += 0.1f;

                if (_picture.GetPixel(i, j).a != 0)
                {
                    GameObject newSprite = Instantiate(_spriteTemplate, transform);
                    newSprite.transform.position = new Vector2(startXPos, startYPos);
                    newSprite.GetComponent<SpriteRenderer>().color = _picture.GetPixel(i, j);
                }
            }

            startYPos += 0.1f;
            startXPos = transform.position.x;
        }
    }
}
