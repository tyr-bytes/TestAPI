namespace NullableProject
{

    internal class Program
    {
        private static void Main(string[] args)
        {

            var surveyRun = new SurveyRun();
            surveyRun.AddQuestion(QuestionType.YesNo, "Has your code ever " +
                "thrown a NullReferenceException ? ");
            surveyRun.AddQuestion(new SurveyQuestion(QuestionType.Number, "How many" +
                " times(to the nearest 100) has that happened ? "));
            surveyRun.AddQuestion(QuestionType.Text, "What is your favorite color?");
            surveyRun.PerformSurvey(50);
            // generates warning
            // surveyRun.AddQuestion(QuestionType.Text, default);

            foreach (var participant in surveyRun.AllParticipants)
            {
                Console.WriteLine($"Participant: {participant.Id}");
                if (participant.AnsweredSurvey)
                {
                    for (int i = 0; i < surveyRun.Questions.Count; i++)
                    {
                        var answer = participant.Answer(i);
                        Console.WriteLine($"\t{surveyRun.GetQuestion(i).QuestionText} : {answer}");
                    }
                }
                else
                {
                    Console.WriteLine("\tNo responses");
                }
            }

        }
    }

    public enum QuestionType
    {
        YesNo,
        Number,
        Text
    }

    public class SurveyQuestion
    {
        public string QuestionText { get; }
        public QuestionType TypeOfQuestion { get; }

        public SurveyQuestion(QuestionType typeOfQuestion, string questionText)
        {
            QuestionText = questionText;
            TypeOfQuestion = typeOfQuestion;
        }
    }

    public class SurveyRun
    {
        private readonly List<SurveyQuestion> surveyQuestions = new();
        public void AddQuestion(QuestionType type, string question) =>
            AddQuestion(new SurveyQuestion(type, question));
        public void AddQuestion(SurveyQuestion surveyQuestion) =>
            surveyQuestions.Add(surveyQuestion);

        private List<SurveyResponse>? respondents;

        public IEnumerable<SurveyResponse> AllParticipants => (respondents ??
    Enumerable.Empty<SurveyResponse>());
        public ICollection<SurveyQuestion> Questions => surveyQuestions;
        public SurveyQuestion GetQuestion(int index) => surveyQuestions[index];

        public void PerformSurvey(int numberOfRespondents)
        {
            int respondentsConsenting = 0;
            respondents = new List<SurveyResponse>();
            while (respondentsConsenting < numberOfRespondents)
            {
                var respondent = SurveyResponse.GetRandomId();
                if (respondent.AnswerSurvey(surveyQuestions))
                    respondentsConsenting++;
                respondents.Add(respondent);
            }
        }


    }

    public class SurveyResponse
    {
        private static readonly Random randomGenerator = new();
        public static SurveyResponse GetRandomId() =>
            new(randomGenerator.Next());
        public int Id { get; }
        public SurveyResponse(int id)
        {
            Id = id;
            answerLookups = new Dictionary<QuestionType, Func<string?>>
            {
                [QuestionType.YesNo] = YesNoAnswer,
                [QuestionType.Number] = NumberAnswer,
                [QuestionType.Text] = DefaultTextAnswer
            };
        }


        private Dictionary<int, string>? surveyResponses;
        public bool AnswerSurvey(IEnumerable<SurveyQuestion> questions)
        {
            if (ConsentToSurvey())
            {
                surveyResponses = new Dictionary<int, string>();
                int index = 0;
                foreach (var question in questions)
                {
                    var answer = GenerateAnswer(question);
                    if (answer != null)
                    {
                        surveyResponses.Add(index, answer);

                    }
                    index++;
                }
            }
            return surveyResponses != null;

        }

        public static bool ConsentToSurvey() => randomGenerator.Next(0, 2) == 1;

        // Make a delegate
        private readonly Dictionary<QuestionType, Func<string?>> answerLookups;

        public bool AnsweredSurvey => surveyResponses != null;
        public string Answer(int index) => surveyResponses?.GetValueOrDefault(index)
        ?? "No answer";


        private static string? YesNoAnswer()
        {
            int n = randomGenerator.Next(-1, 2);
            return (n == -1) ? default : (n == 0) ? "No" : "Yes";
        }

        private static string? NumberAnswer()
        {
            int n = randomGenerator.Next(-30, 101);
            return (n < 0) ? default : n.ToString();
        }

        private static string? DefaultTextAnswer()
        {
            return randomGenerator.Next(0, 5) switch
            {
                0 => default,
                1 => "Red",
                2 => "Green",
                3 => "Blue",
                _ => "Red. No, Green. Wait.. Blue... AAARGGGGGHHH!",
            };
        }


        public string? GenerateAnswer(SurveyQuestion question)
        {
            var f = answerLookups[question.TypeOfQuestion];
            return f?.Invoke();
        }
    }

}