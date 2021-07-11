using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDistortion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subTitletext;
    [SerializeField] private bool distortText = false;
    void Start()
    {
        //  StartDistortText();
        if (distortText) 
            StartDistortText();
    }

    internal void StartDistortText()
    {
        distortText = true;
        StartCoroutine("textDistort");
    }

    internal void StopDistortText()
    {
        distortText = false;
    }

    IEnumerator textDistort()
    {
        while (distortText)
        {
            int random = Random.Range(0, 3);

            switch (random)
            {
                case 0:
                    subTitletext.alignment = TextAlignmentOptions.Left;
                    break;

                case 1:
                    subTitletext.alignment = TextAlignmentOptions.Right;
                    break;

                case 2:
                    subTitletext.alignment = TextAlignmentOptions.Justified;
                    break;

                case 3:
                    subTitletext.alignment = TextAlignmentOptions.Flush;
                    break;

                default:
                    subTitletext.alignment = TextAlignmentOptions.Center;
                    break;
            }

            int random0 = Random.Range(0, 3);

            switch (random0)
            {
                case 0:
                    subTitletext.alignment = TextAlignmentOptions.Top;
                    break;

                case 1:
                    subTitletext.alignment = TextAlignmentOptions.Midline;
                    break;

                case 2:
                    subTitletext.alignment = TextAlignmentOptions.Right;
                    break;

                case 3:
                    subTitletext.alignment = TextAlignmentOptions.Capline;
                    break;

                default:
                    subTitletext.alignment = TextAlignmentOptions.Center;
                    break;
            }


            int random2 = Random.Range(0, 4);

            switch (random2)
            {
                case 0:
                    subTitletext.fontStyle = FontStyles.Underline;
                    break;

                case 1:
                    subTitletext.fontStyle = FontStyles.Strikethrough;
                    break;

                case 2:
                    subTitletext.fontStyle = FontStyles.Italic;
                    break;

                case 3:
                    subTitletext.fontStyle = FontStyles.Bold;
                    break;

                default:
                    subTitletext.fontStyle = FontStyles.Subscript;
                    break;
            }

            int random3 = Random.Range(0, 2);

            switch (random3)
            {
                case 0:
                    subTitletext.characterSpacing = -110;
                    break;

                case 1:
                    subTitletext.characterSpacing = 0;
                    break;

                case 2:
                    subTitletext.characterSpacing = 115;
                    break;

                case 3:

                default:
                    subTitletext.characterSpacing = 0;
                    break;
            }

            yield return new WaitForSeconds(.05f);
        }

        subTitletext.alignment = TextAlignmentOptions.Left;
        subTitletext.characterSpacing = 0;
        subTitletext.fontStyle = FontStyles.Normal;
        subTitletext.alignment = TextAlignmentOptions.Midline;
    }
}
