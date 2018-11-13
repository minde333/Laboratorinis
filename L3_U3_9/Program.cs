using System;
using System.CodeDom;
using System.IO;
using System.Linq;
using System.Text;

namespace L3_U3_9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; //Konsolėje rašo lietuviškas raides

            Program p = new Program();

            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "L3*.csv");
            VideoEnthusiastsContainer videoEnthusiastsContainer = new VideoEnthusiastsContainer();

            foreach (string path in filePaths)
            {
                videoEnthusiastsContainer.AddVideoEnthusiast(ReadVideoEnthusiastData(path));
            }

            // 1 punktas
            PrintFavouriteActors(videoEnthusiastsContainer);

            // 2punktas
            int k;
            //var allSaw = GetMovies(videoEnthusiastsContainer, out k, null);
            var allSaw = GetMovies(videoEnthusiastsContainer, null);
            WriteToFileAll("MateVisi.csv", allSaw);
            
            // 3 punktas
            for (int i = 0; i < videoEnthusiastsContainer.Count; i++)
            {
                var enthusiast = videoEnthusiastsContainer.Get(i);
                var reccomendations =
                    GetMovies(videoEnthusiastsContainer, enthusiast);
                reccomendations.SortByGenreAndName();
                WriteToFileAll($"Rekomendacija_{enthusiast.VideoEnthusiastName}_{enthusiast.VideoEnthusiastSurname}.csv",
                    reccomendations);
            }

            CreateReportTable(videoEnthusiastsContainer, @"L3ReportTable.txt");

            Console.WriteLine();

            Console.ReadKey();
        }

        static void WriteToFileAll(string filename, VideoContainer container)
        {
            using (var writer = new StreamWriter(filename))
            {
                for (int i = 0; i < container.Count; i++)
                {
                    writer.WriteLine(container.Get(i));
                }
            }
        }

        /// <summary>
        /// sudaro filmu ir serialu sarasa, kuriuos perziurejo visi megejai.
        /// Jeigu nurodytas enthusiastFor parametras, iesko filmu ir serialu, kuriuos mate visi isskyrus nurodyta enthusiastFor
        /// </summary>
        /// <param name="enthusiasts"></param>
        /// <param name="enthusiastFor">kinomanas, kuriam sudarom rekomendaciju sarasa</param>
        /// <returns></returns>
        //static Video[] GetMovies(VideoEnthusiastsContainer enthusiasts, out int k, VideoEnthusiast enthusiastFor)
        static VideoContainer GetMovies(VideoEnthusiastsContainer enthusiasts, VideoEnthusiast enthusiastFor)
        {
            Video[] allMovies = new Video[1000];
            int n = 0;

            // surenka visus filmus 
            for (int i = 0; i < enthusiasts.Count; i++)
            {
                var enthusiast = enthusiasts.Get(i);
                int m;
                var videos = enthusiast.GetUniqueVideos(out m);
                for (int j = 0; j < m; j++)
                {
                    if (!allMovies.Contains(videos[j]))
                    {
                        allMovies[n++] = videos[j];
                    }
                }
            }

            //Video[] allSaw = new Video[1000];
            VideoContainer allSaw = new VideoContainer();
            //k = 0;
            // ciklu eina per visus filmus ir kitam cikle tikrina, ar visi kinomanai ji mate
            for (int i = 0; i < n; i++)
            {
                bool all = true;
                for (int j = 0; j < enthusiasts.Count; j++)
                {
                    int m;
                    var enthusiastMovies = enthusiasts.Get(j).GetUniqueVideos(out m);

                    // jeigu tas pats kinomanas mate filma, reiskia jo netrauksim i rekomenduojamu sarasa
                    if (enthusiastFor != null && enthusiastMovies.Contains(allMovies[i]) && enthusiasts.Get(j) == enthusiastFor)
                        all = false;

                    // jeigu tas pats kinomanas, netikrinam ar jis mates filma
                    if (enthusiastFor != null && enthusiasts.Get(j) == enthusiastFor) continue;
                    
                    if (!enthusiastMovies.Contains(allMovies[i]))
                        all = false;
                }

                if (all)
                {
                    //allSaw[k++] = allMovies[i];
                    allSaw.AddVideo(allMovies[i]);
                }
            }

            return allSaw;
        }

        /// <summary>
        /// Atspausdina ekrane kiekvieno kino megejo megstamiausia aktoriu
        /// </summary>
        /// <param name="enthusiasts"></param>
        static void PrintFavouriteActors(VideoEnthusiastsContainer enthusiasts)
        {
            for (int i = 0; i < enthusiasts.Count; i++)
            {
                var enthusiast = enthusiasts.Get(i);

                int nVideos = 0;
                string[] videoActors = enthusiast.Videos.GetAllActors(out nVideos);

                int n = 0;
                ActorPerforms[] performs = new ActorPerforms[nVideos];

                for (int j = 0; j < nVideos; j++)
                {
                    int index = GetActorIndex(performs, n, videoActors[j]);
                    if (index == -1)
                    {
                        performs[n++] = new ActorPerforms
                        {
                            PerformTimes = 1,
                            Actor = videoActors[j]
                        };
                    }
                    else
                    {
                        performs[index].PerformTimes++;
                    }
                }

                performs = SortActorPerformace(performs, n);
                Console.WriteLine($"Kino megejo {enthusiast.VideoEnthusiastName} megstamiausias aktorius yra {performs[0].Actor}, jis vaidino {performs[0].PerformTimes} kartu/s");
            }

            Console.WriteLine();
        }

        static ActorPerforms[] SortActorPerformace(ActorPerforms[] actors, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (actors[i].PerformTimes > actors[j].PerformTimes)
                    {
                        var temp = actors[i];
                        actors[i] = actors[j];
                        actors[j] = temp;
                    }
                }
            }

            return actors;
        }

        /// <summary>
        /// jeigu masyve yra toks aktorius, grazina jo indeksa. kitu atveju -1
        /// </summary>
        /// <param name="actors"></param>
        /// <param name="count"></param>
        /// <param name="actor"></param>
        /// <returns></returns>
        static int GetActorIndex(ActorPerforms[] actors, int count, string actor)
        {
            for (int i = 0; i < count; i++)
            {
                if (actors[i].Actor == actor)
                {
                    return i;
                }
            }

            return -1;
        }

        private static VideoEnthusiast ReadVideoEnthusiastData(string file)
        {
            VideoEnthusiast videoEnthusiast;

            using (StreamReader reader = new StreamReader(file))
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                string VideoEnthusiastName = values[0];
                string VideoEnthusiastSurname = values[1];
                string YearOfBirth = reader.ReadLine();
                string City = reader.ReadLine();
                videoEnthusiast = new VideoEnthusiast(VideoEnthusiastName, VideoEnthusiastSurname, YearOfBirth, City);

                while (null != (line = reader.ReadLine()))
                {
                    values = line.Split(',');
                    char type = line[0];
                    string Name = values[1];
                    string Genre = values[2];
                    string Studio = values[3];
                    string Actor1 = values[4];
                    string Actor2 = values[5];
                    switch (type)
                    {
                        case 'M':
                            int Release = int.Parse(values[6]);
                            string Director = values[7];
                            double Profit = double.Parse(values[8]);
                            Movie movie = new Movie(Name, Genre, Studio, Actor1, Actor2, Release, Director, Profit);

                            if (!videoEnthusiast.Videos.Contains(movie))
                            {
                                videoEnthusiast.AddVideo(movie);
                            }
                            break;
                        case 'S':
                            string StartDate = values[6];
                            string EndDate = values[7];
                            int Episodes = int.Parse(values[8]);
                            string Airing = values[9];
                            Series series = new Series(Name, Genre, Studio, Actor1, Actor2, StartDate, EndDate, Episodes, Airing);
                            if (!videoEnthusiast.Videos.Contains(series))
                            {
                                videoEnthusiast.AddVideo(series);
                            }
                            break;
                    }
                }
            }
            return videoEnthusiast;
        }
        private static void CreateReportTable(VideoEnthusiastsContainer videoEnthusiastsContainer, string file)
        {
            for (int i = 0; i < videoEnthusiastsContainer.Count; i++)
            {
                using (StreamWriter writer = new StreamWriter(file, true, Encoding.UTF8))
                {
                    writer.WriteLine("Duomenys apie įrašo mėgėją ir jo peržiurėtus įrašus");
                    writer.WriteLine(new string('-', 218));
                    writer.WriteLine("| Vardas: {0, -97} |", videoEnthusiastsContainer.Get(i).VideoEnthusiastName);
                    writer.WriteLine(new string('-', 218));
                    writer.WriteLine("| Pavardė: {0, -97} |", videoEnthusiastsContainer.Get(i).VideoEnthusiastSurname);
                    writer.WriteLine(new string('-', 218));
                    writer.WriteLine("| Gimimo metai: {0, -97} |", videoEnthusiastsContainer.Get(i).YearOfBirth);
                    writer.WriteLine(new string('-', 218));
                    writer.WriteLine("| Miestas: {0, -97} |", videoEnthusiastsContainer.Get(i).City);
                    writer.WriteLine(new string('-', 218));
                    writer.WriteLine("| {0, -40} | {1,-14} | {2,-30} | {3,-35} | {4,-20} | {5,-20} | {6,-20} | {7,-14} | {8,-14} | {9,-14} | {10,-14} | {11,-14} |",
                        "Pavadinimas", "Žanras", "Studija", "Aktorius 1", "Aktorius 2", "Filmo leidimo metai", "Filmo režisierius", "Filmo Pelnas", "Serialo pradžios metai", "Serialo pabaigos metai", "Serialo serijų skaičius", "Ar tęsiasi serialas");
                    writer.WriteLine(new string('-', 218));

                    for (int j = 0; j < ; j++)
                    {
                        writer.WriteLine();
                    }
                    writer.WriteLine(new string('-', 218));
                }
            }
        }       
    }
}
