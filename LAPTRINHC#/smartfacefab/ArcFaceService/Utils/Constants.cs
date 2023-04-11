
namespace ArcFaceService.Utils
{
    public class HttpType
    {
        public const int DETECT = 1;
        public const int COMPARE = 2;
        public const int ADD_FEATURE = 3;
        public const int UPDATE_FEATURE = 4;
        public const int REMOVE_FEATURE = 5;
        public const int SEARCH_FACE = 6;
        public const int REMOVE_GROUP = 7;
    }

    public class SocketService
    {
        public const string IP = "localhost";
        public const int PORT = 3333;
    }

    public class FaceType
    {
        public const int URL = 1;
        public const int FILE = 2;
        public const int BASE64 = 3;
        public const int FEATURE = 4;
    }
}
