using DataLayer.Classes;
using System;

namespace BusinessLayer.DTOs
{
    public class ShowTaskShorInfoDTO
    {
        public int Id { get; set; }
        public string Deadline { get; set; }
        public string Title { get; set; }
        
        private string _description; 
        public string Description 
        {
            get 
            {
                return _description;
            }
            set
            { 
                if (value.Length == ApplicationConstants.TASK_SHORT_INFO_CHARACTER_COUNT)
                {
                    _description = value + "...";
                }
                else
                {
                    _description = value;
                }
            } 
        }
        public string ExecutorName { get; set; }
        public int StatusId { get; set; }
        public bool MissedDeadline { get; set; }
    }
}
