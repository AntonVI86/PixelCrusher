using System.Collections.Generic;
using UnityEngine;

public class PicturesCreator : MonoBehaviour
{
    [SerializeField] private ElementCollision _spriteTemplate;
    [SerializeField] private Texture2D _picture;

    public List<ElementCollision> _elements = new List<ElementCollision>();

    private int rowCount;
    private int colCount;

    public void GenerateGrid(float startXPos, float startYPos)
    {
        float step = 0.1f;

        rowCount = _picture.width;
        colCount = _picture.height;

        for (int i = 0; i < colCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                startXPos += step;

                if (_picture.GetPixel(j, i).a != 0)
                {
                    ElementCollision newSprite = Instantiate(_spriteTemplate, transform);
                    newSprite.transform.position = new Vector2(startXPos, startYPos);
                    newSprite.GetColor(_picture, j, i);
                    _elements.Add(newSprite);
                }
            }

            startYPos += step;
            startXPos = transform.position.x;
        }
    }
}
