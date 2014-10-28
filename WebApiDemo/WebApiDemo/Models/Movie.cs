using System;

namespace WebApiDemo.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan Length { get; set; }
        public string Genre { get; set; }
    }
}