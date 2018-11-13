using System.Linq;

namespace L3_U3_9
{
    class VideoEnthusiast
    {
        //Įrašų megėjo vardas
        public string VideoEnthusiastName { get; set; }
        //Įrašų megėjo pavardė
        public string VideoEnthusiastSurname { get; set; }
        //Įrašų megėjo gimimo metai
        public string YearOfBirth { get; set; }
        //Įrašų megėjo miestas
        public string City { get; set; }
        //Įrašų konteineris
        public VideoContainer Videos { get; set; }

        public VideoEnthusiast(string videoEnthusiastName, string videoEnthusiastSurname, string yearOfBirth, string city)
        {
            VideoEnthusiastName = videoEnthusiastName;
            VideoEnthusiastSurname = videoEnthusiastSurname;
            YearOfBirth = yearOfBirth;
            City = city;
            Videos = new VideoContainer();
        }
        public VideoEnthusiast()
        {
        }

        public void AddVideo(Video video)
        {
            Videos.AddVideo(video);
        }

        /// <summary>
        /// metodas grazina unikaliu filmu/serialu masyva
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Video[] GetUniqueVideos(out int n)
        {
            n = 0;
            Video[] videos = new Video[Videos.Count];
            for (int i = 0; i < Videos.Count; i++)
            {
                var video = Videos.Get(i);
                if (!videos.Contains(video))
                {
                    videos[n++] = video;
                }
            }

            return videos;
        }
    }
}
