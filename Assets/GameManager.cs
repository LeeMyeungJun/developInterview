using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    enum DataType
    {
        Normal,
        Develop,
        Unity,
        END
    }
    [SerializeField] DataFile[] datas = new DataFile[(int)DataType.END];
    GameObject menuCanvas;
    GameObject ivCanvas;

    DataFile mode = null;
    Text hintText;
    Text questionText;
    Image timeImage;
    float answerTime = 20.0f;
    float elapsedTime = 9999f;
    int questionIndex = -1;
    private void Awake()
    {
        menuCanvas = GameObject.Find("MenuCanvas");
        ivCanvas = GameObject.Find("InterViewCanvas");

        GameObject.Find("InterviewNormalBtn").GetComponent<Button>().onClick.AddListener(InterviewNormal);
        GameObject.Find("InterviewDevelopBtn").GetComponent<Button>().onClick.AddListener(InterviewDevelop);
        GameObject.Find("InterviewUnityBtn").GetComponent<Button>().onClick.AddListener(InterviewUnity);

        GameObject.Find("NextButton").GetComponent<Button>().onClick.AddListener(CreateQuestion);
        GameObject.Find("BackButton").GetComponent<Button>().onClick.AddListener(MenuMode);

        questionText = GameObject.Find("QuestionText").GetComponent<Text>();
        hintText = GameObject.Find("HintText").GetComponent<Text>();
        timeImage = GameObject.Find("TimeOverlay").GetComponent<Image>();

        menuCanvas.SetActive(true);
        ivCanvas.SetActive(false);
        mode = new DataFile();
    }

    public void InterviewNormal()
    {
        mode.datas =new List<DataFile.Data>(datas[(int)DataType.Normal].datas);
        InterviewMode();
    }

    public void InterviewDevelop()
    {
        mode.datas = new List<DataFile.Data>(datas[(int)DataType.Develop].datas);
        InterviewMode();
    }

    public void InterviewUnity()
    {
        mode.datas = new List<DataFile.Data>(datas[(int)DataType.Unity].datas);

        InterviewMode();
    }

    void InterviewMode()
    {
        menuCanvas.SetActive(false);
        ivCanvas.SetActive(true);
        elapsedTime = 9999f;
        StopAllCoroutines();
        StartCoroutine(Action());
    }

    void MenuMode()
    {
        questionIndex = -1;
        menuCanvas.SetActive(true);
        ivCanvas.SetActive(false);
        StopAllCoroutines();
    }

    
    IEnumerator Action()
    {
        while (true)
        {
            if(elapsedTime >= answerTime)
            {
                CreateQuestion();

                if(questionIndex == -1)
                {
                    MenuMode();
                    break;
                }

                elapsedTime = 0.0f;
            }
            elapsedTime += Time.deltaTime;

            timeImage.fillAmount = elapsedTime / answerTime;
            yield return null;
        }
    }

    void CreateQuestion()
    {
        if (mode.datas.Count == 0)
        {
            MenuMode();
        }
        else
        {
            elapsedTime = 0f;
            questionIndex = Random.Range(0, mode.datas.Count);
            questionText.text = mode.datas[questionIndex].question;
            hintText.text = mode.datas[questionIndex].hint;
            mode.datas.RemoveAt(questionIndex);
        }
    }
}
