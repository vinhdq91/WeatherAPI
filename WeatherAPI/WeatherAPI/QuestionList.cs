using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public static class QuestionList
    {
        public static Dictionary<int, string> ListQuestions()
        {
            Dictionary<int, string> list = new Dictionary<int, string>()
            {
                [1] = "Should I go outside?",
                [2] = "Should I wear sunscreen?",
                [3] = "Can I fly my kite?"
            };
            return list;
        }
    }
}
