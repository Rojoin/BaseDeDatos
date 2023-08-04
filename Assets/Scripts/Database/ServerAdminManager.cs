using System;
using MySql.Data.MySqlClient;
using UnityEngine;

public class ServerAdminManager : MonoBehaviour
{
    public string serverDataBase;
    public string nameDataBase;
    public string userDataBase;
    public string passDataBase;

    private string connectionData;
    private MySqlConnection connection;
    void Start()
    {
        connectionData             = "Server=" + serverDataBase
                                   + ";Database=" + nameDataBase
                                   + ";Uid=" + userDataBase
                                   + ";Pwd=" + passDataBase
                                   + ";";
        ConnectWithServerDataBase();
    }

    private void ConnectWithServerDataBase()
    {
        connection = new MySqlConnection(connectionData);
        try
        {
            connection.Open();
            Debug.Log("DataBaseOpen!");
        }
        catch (MySqlException e)
        {
            Debug.Log("Could´nt connect to DataBase.");
            throw;
        }
    }
    public MySqlDataReader Select(string select)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM " + select;
        MySqlDataReader result = command.ExecuteReader();
        return result;
    }
    public MySqlDataReader Insert(string select)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO " + select;
        MySqlDataReader result = command.ExecuteReader();
        return result;
    }
    public MySqlDataReader Create(string select)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "CREATE TABLE " + select;
        MySqlDataReader result = command.ExecuteReader();
        return result;
    }
}
