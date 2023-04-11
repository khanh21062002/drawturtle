
namespace ArcFaceService.Utils
{
    class ErrorMessage
    {
        public static string ERR_SUCCESS = "Success";
        public static string ERR_IMAGE_INPUT = "Image is not defined";
        public static string ERR_IMAGE_INPUT_ARG = "Image is not defined {0}";
        public static string ERR_DETECT_FACE = "Cannot detect face";
        public static string ERR_DETECT_AGE = "Lỗi xác định tuổi";
        public static string ERR_DETECT_GENDER = "Lỗi xác định giới tính";
        public static string ERR_DETECT_ANGLE3D = "Lỗi xác định góc khuôn mặt";
        public static string ERR_DETECT_LANDMARK = "Lỗi xác định vùng trán";
        public static string ERR_DETECT_LIVENESS = "Lỗi phát hiện sự sống";
        public static string ERR_MORE_THAN_ONE_FACE = "More than one face";
        public static string ERR_NO_EXISTS_FACE = "Cannot found face";
        public static string ERR_NO_EXISTS_FACE_ARG = "Cannot found face in image {0}";
        public static string ERR_CANNOT_EXTRACT_FEATURE = "Ảnh không đủ chất lượng để trích xuất thuộc tính";
        public static string ERR_CANNOT_EXTRACT_FEATURE_ARG = "Ảnh không đủ chất lượng để trích xuất thuộc tính {0}";
        public static string ERR_GROUP_NOT_FOUND = "Group is not found or face is not exists";
    }
}
