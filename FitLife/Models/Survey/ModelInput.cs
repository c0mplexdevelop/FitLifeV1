using FitLife.Models.Exercises;
using FitLife.Models.Exercises.Enums;
using FitLife.Models.User.Enum;
using Microsoft.ML.Data;
using System.ComponentModel.DataAnnotations;

namespace FitLife.Models.Survey
{
    public class ModelInput
    {
        [LoadColumn(0)]
        public float UserAge { get; set; }

        [LoadColumn(1)]
        public float UserWeight { get; set; }

        [LoadColumn(2)]
        public float UserHeight { get; set; }

        [LoadColumn(3)]
        public string UserGender { get; set; } = default!;

        [LoadColumn(4)]
        public string UserActivityLevel { get; set; } = default!;

        [LoadColumn(5)]
        public float BMI { get; set; }

        [LoadColumn(6)]
        public string BMICategory { get; set; } = string.Empty;

        [LoadColumn(7)]
        public float StruggledPreviously { get; set; }

        [LoadColumn(8)]
        public string FitnessGoal { get; set; } = default!;

        [LoadColumn(9)]
        public string DietaryPreference { get; set; } = default!;

        [LoadColumn(10)]
        public string AllergiesRestrictions { get; set; } = default!;

        [LoadColumn(11)]
        public string MealsPerDay { get; set; } = default!;

        [LoadColumn(12)]
        public string SnacksPerDay { get; set; } = default!;

        [LoadColumn(13)]
        public string ExerciseID { get; set; } = default!;

        [LoadColumn(14)]
        public string ExerciseName { get; set; } = default!;

        [LoadColumn(15)]
        public string ExerciseType { get; set; } = default!;

        [LoadColumn(16)]
        public string ExerciseDifficulty { get; set; } = default!;

        [LoadColumn(17)]
        public string ExerciseTargetMuscleGroup { get; set; } = default!;

        [LoadColumn(18)]
        public float EquipmentNeeded { get; set; }

        [LoadColumn(19)]
        public float RecommendedSets { get; set; }

        [LoadColumn(20)]
        public float RecommendedReps { get; set; }

        [LoadColumn(21)]
        public float RecommendedDurationMinutes { get; set; }

        [LoadColumn(22)]
        public bool Label { get; set; }
    }
}
