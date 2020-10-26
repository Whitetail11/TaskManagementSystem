using DataLayer.Classes;

namespace BusinessLayer.DTOs
{
    public class TaskPageDTO
    {
        private int _pageNumber;
        private int _pageSize;

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                if (value < 1)
                {
                    _pageNumber = 1;
                }
                else
                {
                    _pageNumber = value;
                }
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value < 1)
                {
                    _pageSize = ApplicationConstants.DEFAULT_TASK_PAGE_SIZE;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }
    }
}
