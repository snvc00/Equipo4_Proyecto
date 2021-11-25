using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizController : MonoBehaviour
{
    public GameObject Option1;
    public GameObject Option2;
    public GameObject Score;
    public GameObject QuestionGO;
    public int OutroScene;

    private TextMeshProUGUI _myTextMeshProUGUI;
    private TextMeshProUGUI _option1TextMeshProUGUI;
    private TextMeshProUGUI _option2TextMeshProUGUI;
    private TextMeshProUGUI _questionTextMeshProUGUI;
    private TextMeshProUGUI _scoreTextMeshProUGUI;
    private Question[] _questions = new Question[5];
    private int _currentQuestion = 0;
    private bool _waiting = false;

    // Start is called before the first frame update
    public void Start()
    {
        _myTextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _option1TextMeshProUGUI = Option1.GetComponent<TextMeshProUGUI>();
        _option2TextMeshProUGUI = Option2.GetComponent<TextMeshProUGUI>();
        _questionTextMeshProUGUI = QuestionGO.GetComponent<TextMeshProUGUI>();
        _scoreTextMeshProUGUI = Score.GetComponent<TextMeshProUGUI>();

        _questions[0] = new Question() { Sentence = "¿Cuántas caras tiene un cubo?", Options = new string[] { "6", "8" }, RightOption = 0 };
        _questions[1] = new Question() { Sentence = "¿Cuántos vértices tiene una pirámide?", Options = new string[] { "12", "5" }, RightOption = 1 };
        _questions[2] = new Question() { Sentence = "¿Cuántas caras tiene un cilindro?", Options = new string[] { "4", "3" }, RightOption = 1 };
        _questions[3] = new Question() { Sentence = "¿Cuántas aristas tiene un cubo?", Options = new string[] { "12", "10" }, RightOption = 0 };
        _questions[4] = new Question() { Sentence = "Fórmula para calcular el volumen de la esfera:", Options = new string[] { "V = L^3", "V = ¾πr^3" }, RightOption = 1 };

        UpdateQuestion(_questions[_currentQuestion]);
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public IEnumerator OnPointerEnter()
    {
        int selectedOption = GetSelectedOption();
        // Check if there is not a selected option or the game already ended
        if (selectedOption != -1 && _currentQuestion < _questions.Length && _waiting == false)
        {
            if (_questions[_currentQuestion].RightOption == selectedOption)
            {
                // Right answer
                _myTextMeshProUGUI.color = Color.green;

                int updatedScore = int.Parse(_scoreTextMeshProUGUI.text) + 1;
                _scoreTextMeshProUGUI.text = updatedScore.ToString();
            }
            else
            {
                // Wrong answer
                _myTextMeshProUGUI.color = Color.red;
            }

            _waiting = true;
            yield return new WaitForSeconds(2);
            _waiting = false;

            // Update question, options and score
            _currentQuestion++;
            if (_currentQuestion == _questions.Length)
            {
                // Game ends
                _myTextMeshProUGUI.text = "¡Terminaste!";

                yield return new WaitForSeconds(4);

                SceneManager.LoadScene(OutroScene);
            }
            else
            {
                ResetTextColors();
                UpdateQuestion(_questions[_currentQuestion]);
            }
        }
    }

    private void UpdateQuestion(Question newQuestion)
    {
        _questionTextMeshProUGUI.text = newQuestion.Sentence;
        _option1TextMeshProUGUI.text = newQuestion.Options[0];
        _option2TextMeshProUGUI.text = newQuestion.Options[1];
    }

    private int GetSelectedOption()
    {
        if (_option1TextMeshProUGUI.color.Compare(Color.cyan))
        {
            return 0;
        }
        else if (_option2TextMeshProUGUI.color.Compare(Color.cyan))
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private void ResetTextColors()
    {
        _option1TextMeshProUGUI.color = Color.white;
        _option2TextMeshProUGUI.color = Color.white;
        _myTextMeshProUGUI.color = Color.white;
    }
}

public class Question
{
    public string Sentence;
    public string[] Options;
    public int RightOption;
}
