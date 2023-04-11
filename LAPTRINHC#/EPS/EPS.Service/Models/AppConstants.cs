using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.Service.Models
{
    public static class AppConstants
    {
        public static class CompanyType
        {
            public static int FACTORY = 0;

            public static int SCHOOL = 1;
        }

        public static class DepartmentType
        {
            public static int FACTORY = 0;

            public static int SCHOOL = 1;

            public static int DEFAULT_PARENT_DEPT = 2;
        }

        public static class PersonCompType
        {
            public static int FACTORY_EMP = 0;

            public static int SCHOOL_STU = 1;

            public static int SCHOOL_PARENT = 2;

        }

        public static class DEFAULT_PARENT_CODE
        {
            public static string DEFAULT_PARENT_CODE_TAIL = "_DEPT_PARENT_DEFAULT";
        }

        public static class SCHOOL_COMMON_STRING
        {
            public static string PARENT = "Người ủy quyền";
            public static string STUDENT = "Học sinh";

            public static string DEFAULT_VALUE_STUDENT_PARENT_NOTE = "Người ủy quyền đưa đón";
        }

        public static class PERSON_STATUS
        {
            public static int AVAILABLE = 1;
            public static int UNAVAILABLE = 0;
        }


        public static class PERSON
        {
            public static Guid UNKNOWN = Guid.Parse("00000000-0000-0000-0000-000000000000");


        }


    }
}
