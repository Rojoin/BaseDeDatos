using System;
using System.Collections;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Login : MonoBehaviour
{
    
    public TMPro.TMP_InputField user;
    public TMPro.TMP_InputField pass;
    public TMPro.TextMeshProUGUI alertText;
    public TMPro.TMP_InputField confirmPass;
    public ServerAdminManager serverAdminManager;
    public CanvasGroup logIn;
    public CanvasGroup register;
    [SerializeField] private float maxTime = 5f;
    public UnityEvent onSuccesfullLogin;
    public CurrentUserName nameToSave;

    private void Awake()
    {
        OpenLog();
    }

    public void Logg()
    {
        string log = "`users` WHERE `user` LIKE '" + user.text + "' AND `pass` LIKE '" + pass.text + "'";

        MySqlDataReader result = serverAdminManager.Select(log);

        if (result.HasRows)
        {
            if (result.Read())
            {
                nameToSave.userId = result.GetInt32("idUser");
            }

            Debug.Log("Succesfull Login");
            onSuccesfullLogin.Invoke();
        }
        else
        {
            string error = "Bad password or username";
            StartCoroutine(errorText(error));
        }

        result.Close();
    }

    public void SignUp()
    {
        if (user.text.Length >= 25 || user.text.Length <= 2)
        {
            string error = "User must be between 3 and 24 chars";
            StartCoroutine(errorText(error));
        }
        else
        {
            if (pass.text == confirmPass.text)
            {
                string log = "`users` WHERE `user` LIKE '" + user.text + "' AND `pass` LIKE '" + pass.text + "'";
                MySqlDataReader result = serverAdminManager.Select(log);
                if (result.HasRows)
                {
                    string error = "User already exits";
                    StartCoroutine(errorText(error));
                    result.Close();
                }
                else
                {
                    result.Close();
                    log = "`users`(`idUser`, `user`, `pass`) VALUES (NULL,'" + user.text + "','" + pass.text + "')";
                    result = serverAdminManager.Insert(log);
                    string error = "User created succesfully!";
                    StartCoroutine(errorText(error));
                    OpenLog();
                    result.Close();
                }
            }
            else
            {
                string error = "The passwords are different";
                StartCoroutine(errorText(error));
            }
        }
    }

    public void ChangeCanvas(CanvasGroup newCanvas, bool isActive)
    {
        newCanvas.alpha = isActive ? 1 : 0;
        newCanvas.blocksRaycasts = isActive;
        newCanvas.interactable = isActive;
        newCanvas.ignoreParentGroups = isActive;
    }

    public void OpenLog()
    {
        ChangeCanvas(logIn, true);
        ChangeCanvas(register, false);
    }

    public void OpenRes()
    {
        ChangeCanvas(register, true);
        ChangeCanvas(logIn, false);
    }

    private IEnumerator errorText(string text)
    {
        alertText.text = text;
        alertText.gameObject.SetActive(true);
        yield return new WaitForSeconds(maxTime);
        alertText.gameObject.SetActive(false);
        yield break;
    }

    public void CloseApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
   Application.Quit();
#endif
    }
}