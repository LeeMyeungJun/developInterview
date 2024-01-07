using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DataType
{
    Normal,
    Develop,
    Unity,
    END
}
public class GameManager : MonoBehaviour
{
    SoundManager sound;
    [SerializeField] DataFile[] datas = new DataFile[(int)DataType.END];
    GameObject menuCanvas;
    GameObject ivCanvas;

    DataFile mode = null;
    Text hintText;
    Text questionText;
    Image timeImage;
    float answerTime = 60.0f;
    float elapsedTime = 9999f;
    int questionIndex = -1;

    DataType CurrentType = DataType.END;
    private void Awake()
    {
        menuCanvas = GameObject.Find("MenuCanvas");
        ivCanvas = GameObject.Find("InterViewCanvas");
        sound = GameObject.FindObjectOfType<SoundManager>();

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
        CurrentType = DataType.Normal;
        InterviewMode();
    }

    public void InterviewDevelop()
    {
        mode.datas = new List<DataFile.Data>(datas[(int)DataType.Develop].datas);
        CurrentType = DataType.Develop;

        InterviewMode();
    }

    public void InterviewUnity()
    {
        mode.datas = new List<DataFile.Data>(datas[(int)DataType.Unity].datas);
        CurrentType = DataType.Unity;

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
        CurrentType = DataType.END;
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
            sound.Play(mode.datas[questionIndex].voiceKey, CurrentType);
            mode.datas.RemoveAt(questionIndex);
        }
    }
}
