using UnityEngine;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class DatabaseViewer : MonoBehaviour
{
    private string connectionString = "Server=yourServerAddress;Database=yourDatabaseName;User Id=yourUsername;Password=yourPassword;";

    private List<string> databaseContents = new List<string>();

    void Start()
    {
        FetchDatabaseContents();
    }

    private void FetchDatabaseContents()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM YourTableName";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string rowData = "";

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            rowData += reader[i].ToString() + ", ";
                        }

                        databaseContents.Add(rowData);
                    }
                }
            }
        }

        DisplayDatabaseContentsInVR();
    }

    private void DisplayDatabaseContentsInVR()
    {
        foreach (string data in databaseContents)
        {
            Debug.Log(data);
        }
    }
}
