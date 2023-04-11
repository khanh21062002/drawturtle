namespace ArcFaceService.Utils
{
    public class RequestMessage <I,O>
    {
        public string requestId;
        public int requestType;
        public I input;
        public O output;

        public RequestMessage(string requestId, int requestType, I input, O output)
        {
            this.requestId = requestId;
            this.requestType = requestType;
            this.input = input;
            this.output = output;
        }
    }
}
