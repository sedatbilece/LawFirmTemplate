﻿namespace LawFirmTemplate.Data.Entities
{
    public class PracticeArea :BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
    }
}
