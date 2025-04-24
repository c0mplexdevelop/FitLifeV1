using FitLife.Models.Exercises;
using FitLife.Models.User.Enum;
using System.ComponentModel.DataAnnotations;

namespace FitLife.Models.Survey;

public class SurveyModel
{
    [Required]
    [Range(0, 125, ErrorMessage = "Age cannot be negative")]
    public int Age { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Height cannot be negative")]
    public float Height { get; set; } // Height in cm

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Weight cannot be negative")]
    public float Weight { get; set; }
    public float Gender { get; set; } // Will be converted from Enum to string, enum from the database
    public ActivityLevel ActivityLevel { get; set; } // Will be converted from Enum to string, enum from the dropdown
    public float BMI => Weight / ((Height) / 100 * (Height) / 100); // BMI = weight(kg) / height(m)^2
    public string BMIStatus
    {
        get
        {
            if (BMI < 18.5)
                return "Underweight";
            else if (BMI >= 18.5 && BMI < 24.9)
                return "Normal";
            else if (BMI >= 25 && BMI < 29.9)
                return "Overweight";
            else
                return "Obese";
        }
    }

    [Required]
    public int StruggledPreviously { get; set; } // Will be converted from bool to int, bool for the buttons
    [Required]
    // possible values: "Weight Loss", "Muscle Gain", "Maintenance", "General Fitness"
    public string FitnessGoal { get; set; } = string.Empty; // Will be converted from Enum to string, enum for the buttons

    /*
     * Might actually just flatten this out and just use the fields itself, or do another class that is like this
     * but flattened and that will be fed to the model instead. if so, turn this to List
     */
    public List<Exercise> Exercises { get; set; } = new List<Exercise>(); // Will be converted from string to object, object for the dropdown
    public float Label { get; set; } // Will be converted from string to object, object for the dropdown



}
