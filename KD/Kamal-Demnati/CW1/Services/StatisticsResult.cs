using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW1.Services
{
    public class StatisticsResult
    {
        public int TotalStudents { get; set; }

        public int TotalGrades { get; set; }

        public double MeanAverage { get; set; }

        public int MaxGrade { get; set; }

        public bool HasFailing { get; set; }

        public bool AllHaveEmail { get; set; }
    }
}
