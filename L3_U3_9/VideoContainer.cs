using System.Linq;

namespace L3_U3_9
{
    //Įrašų konteineris
    class VideoContainer
    {
        private const int MaxVideos = 50;
        private Video[] Videos { get; set; }
        public int Count { get; private set; }

        public VideoContainer()
        {
            Videos = new Video[MaxVideos];
        }

        public void AddVideo(Video video)
        {
            Videos[Count] = video;
            Count++;
        }

        public Video Get(int index)
        {
            return Videos[index];
        }

        public bool Contains(Video video)
        {
            return Videos.Contains(video);
        }

        /// <summary>
        /// grazina visu aktoriu masyva
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public string[] GetAllActors(out int n)
        {
            n = 0;
            string[] actors = new string[Count * 2];
            for (int i = 0; i < Count; i++)
            {
                actors[n++] = Videos[i].Actor1;
                actors[n++] = Videos[i].Actor2;
            }

            return actors;
        }

        public void SortByGenreAndName()
        {
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    //if (videos[i].Compare(videos[j]) == -1)
                    //{
                    //    var temp = videos[i];
                    //    videos[i] = videos[j];
                    //    videos[j] = temp;
                    //}
                    var cmp = Videos[i].Genre.CompareTo(Videos[j].Genre);
                    if (cmp == -1)
                    {
                        var temp = Videos[i];
                        Videos[i] = Videos[j];
                        Videos[j] = temp;
                    }
                    else if (cmp == 0)
                    {
                        if (Videos[i].Name.CompareTo(Videos[j].Name) == -1)
                        {
                            var temp = Videos[i];
                            Videos[i] = Videos[j];
                            Videos[j] = temp;
                        }
                    }
                }
            }
        }
    }
}
