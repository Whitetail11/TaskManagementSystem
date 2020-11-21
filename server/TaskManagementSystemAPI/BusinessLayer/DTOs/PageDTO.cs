using DataLayer.Classes;

namespace BusinessLayer.DTOs
{
    public class PageDTO
    {
        private int _number;
        private int _size;

        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (value < 1)
                {
                    _number = 1;
                }
                else
                {
                    _number = value;
                }
            }
        }

        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (value < 1)
                {
                    _size = ApplicationConstants.DEFAULT_PAGE_SIZE;
                }
                else
                {
                    _size = value;
                }
            }
        }
    }
}
