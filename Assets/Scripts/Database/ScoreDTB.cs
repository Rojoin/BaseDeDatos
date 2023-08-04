using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEngine.SocialPlatforms.Impl;


public class ScoreDTB : MonoBehaviour
{
    private static MySqlConnection connection;
    private string connectionString = "Server=localhost;Database=users_tothemoon;Uid=root;Pwd=1011Julio;";
    public List<DataObject> obj;
    public CurrentUserName nameTosave;

    private void Awake()
    {
        obj = GetAllDataFromDatabase();
        GetNameFromDataBase(obj);
    }

    // Call this method to query the database and retrieve all the data.
    public List<DataObject> GetAllDataFromDatabase()
    {
        List<DataObject> dataList = new List<DataObject>();

        try
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            string sqlQuery = "SELECT * FROM score;";

            MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DataObject data = new DataObject();
                data.id = reader.GetInt32("idUser");
                data.score = reader.GetInt32("score");

                Debug.Log(data.score);
                dataList.Add(data);
            }

            reader.Close();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("MySQL Error: " + ex.ToString());
        }
        finally
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }

        return dataList;
    }

    public void GetNameFromDataBase(List<DataObject> list)
    {
        List<dataName> names = new List<dataName>();

        try
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            string sqlQuery = "SELECT * FROM users;";

            MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dataName data = new dataName();
                data.id = reader.GetInt32("idUser");
                data.name = reader.GetString("user");
                names.Add(data);
            }

            reader.Close();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("MySQL Error: " + ex.ToString());
        }
        finally
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }

        foreach (var da in list)
        {
            foreach (var VARIABLE in names)
            {
                if (da.id == VARIABLE.id)
                {
                    da.name = VARIABLE.name;
                }
            }
        }

        list.Sort((a, b) => b.score.CompareTo(a.score));
    }

    public void AddNewScore()
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO `score` (`idScore`, `idUser`, `score`) VALUES (NULL, '" + nameTosave.userId +
                              "', '" + nameTosave.score + "')";
        MySqlDataReader result = command.ExecuteReader();
        result.Close();
    } 
    public static void AddNewScoreStatic(CurrentUserName nameTosave)
    {
        string connectionString = "Server=localhost;Database=users_tothemoon;Uid=root;Pwd=1011Julio;";
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO `score` (`idScore`, `idUser`, `score`) VALUES (NULL, '" + nameTosave.userId +
                              "', '" + nameTosave.score + "')";
        MySqlDataReader result = command.ExecuteReader();
        result.Close();
        connection.Close();
    }
    public void DeleteScores()
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM score";
        MySqlDataReader result = command.ExecuteReader();
        result.Close();
        connection.Close();
    }
}


[Serializable]
public class DataObject
{
    public int id;
    public string name;
    public int score;
}

public class dataName
{
    public int id;
    public string name;
}