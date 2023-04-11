using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FAQuestions
{
    public class FAQuestionsGridDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Typestr { get; set; }
    }
}
