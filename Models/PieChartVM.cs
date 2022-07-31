using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class PieChartVM
    {
        public PieChartVM()
        {
            labels = new List<string>();
            datasets = new List<PieChartChildVM>();
        }

        public List<string> labels { get; set; }

        public List<PieChartChildVM> datasets { get; set; }

        
    }

    public class PieChartChildVM
    {
        public PieChartChildVM()
        {
            backgroundColor = new List<string>();
            data = new List<int>();
        }
        public List<string> backgroundColor { get; set; }
        public List<int> data { get; set; }
    }
}