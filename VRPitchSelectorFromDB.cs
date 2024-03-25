using UnityEngine;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class VRPitchSelector : MonoBehaviour
{
    private string connectionString = "Server=yourServerAddress;Database=yourDatabaseName;User Id=yourUsername;Password=yourPassword;";
    private List<Pitch> pitches = new List<Pitch>();

    private class Pitch
    {
        public int PitchNo;
        public string Date;
        public string Time;
        // Add more properties as needed
    }

    void Start()
    {
        FetchPitchesFromDatabase();
    }

    private void FetchPitchesFromDatabase()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM PitchData";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pitch pitch = new Pitch();
                        pitch.PitchNo = reader.GetInt32(reader.GetOrdinal("PitchNo"));
                        pitch.Date = reader.GetString(reader.GetOrdinal("Date"));
                        pitch.Time = reader.GetString(reader.GetOrdinal("Time"));
                        // Populate more properties as needed

                        pitches.Add(pitch);
                    }
                }
            }
        }

        DisplayPitchesInVR();
    }

    private void DisplayPitchesInVR()
    {
        foreach (Pitch pitch in pitches)
        {
            // Display pitch in VR UI (e.g., buttons to select pitches)
            Debug.Log($"Pitch No: {pitch.PitchNo}, Date: {pitch.Date}, Time: {pitch.Time}");
        }
    }

    // Method to save selected pitch
    public void SaveSelectedPitch(int selectedPitchIndex)
    {
        // Check if selectedPitchIndex is within valid range
        if (selectedPitchIndex >= 0 && selectedPitchIndex < pitches.Count)
        {
            // Save the selected pitch to be accessed by other scripts
            Pitch selectedPitch = pitches[selectedPitchIndex];
            // Example: You can store it in a global variable, or a list accessible by other scripts
            // GlobalVariable.SelectedPitch = selectedPitch;
            // GlobalVariable.SelectedPitchList.Add(selectedPitch);
        }
        else
        {
            Debug.LogError("Invalid pitch index selected.");
        }
    }
}
