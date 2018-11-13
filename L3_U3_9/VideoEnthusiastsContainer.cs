namespace L3_U3_9
{
    //Įrašų entuziastų konteineris
    class VideoEnthusiastsContainer
    {
        private const int MaxVideoEnthusiasts = 50;
        public VideoEnthusiast[] VideoEnthusiasts;
        public int Count { get; private set; }

        public VideoEnthusiastsContainer()
        {
            VideoEnthusiasts = new VideoEnthusiast[MaxVideoEnthusiasts];
            Count = 0;
        }

        public void AddVideoEnthusiast(VideoEnthusiast videoEnthusiast)
        {
            VideoEnthusiasts[Count++] = videoEnthusiast;
        }

        public VideoEnthusiast Get(int index)
        {
            return VideoEnthusiasts[index];
        }
    }
}
