using System;
using System.Collections.Generic;

namespace GPBackend.DTOs
{
    public class TimeSeriesDTO
    {
        public int points { get; set; }
        public DateTime start_date { get; set; }
        public string interval { get; set; }
        public List<TimeSeriesPointDTO> results { get; set; } = new List<TimeSeriesPointDTO>();
    }

    public class TimeSeriesPointDTO
    {
        public DateTime date { get; set; }
        public int total_applications { get; set; }
        public int rejections { get; set; }
        public int acceptances { get; set; }
        public int pending { get; set; }
    }
} 