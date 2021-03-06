﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Classes
{
    public class ApplicationConstants
    {
        public const int DEFAULT_PAGE_SIZE = 10;
        public const int TASK_SHORT_INFO_CHARACTER_COUNT = 200;
        public const int DONE_STATUS_ID = 4;

        public class Roles
        {
            public const string ADMINISTRATOR = "administrator";
            public const string CUSTOMER = "customer";
            public const string EXECUTOR = "executor";
        }
    }
}
