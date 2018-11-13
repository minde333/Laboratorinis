using System;

namespace L3_U3_9
{
    /// Klasė saugoti duomenis apie filmą
    class Movie : Video
    {
        // Filmo išleidimo metai 
        public int Release { get; set; }
        // Filmą sukūrusio režisieriaus vardas
        public string Director { get; set; }
        // Filmo pelnas
        public double Profit { get; set; }

        public Movie(string name, string genre, string studio, string actor1, string actor2, int release, string director, double profit) : base(name, genre, studio, actor1, actor2)
        {
            Release = release;
            Director = director;
            Profit = profit;
        }
      
        public override string ToString()
        {
            return String.Format("| {0, -40} | {1,-14} | {2,-30} | {3,-35} | {4,-20} | {5,-20} | {6,-20} | {7,-14} | {8,-14} | {9,-14} | {10,-14} | {11,-14} |",
                        Name, Genre, Studio, Actor1, Actor2, Release, Director, Profit, "", "", "", "");
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Movie other))
            {
                return false;
            }
            return Name == other.Name &&
                   Release == other.Release &&
                   Genre == other.Genre &&
                   Studio == other.Studio &&
                   Director == other.Director &&
                   Actor1 == other.Actor1 &&
                   Actor2 == other.Actor2 &&
                   Math.Abs(Profit - other.Profit) < 0.1;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Genre.GetHashCode() ^ Studio.GetHashCode() ^ Director.GetHashCode() ^ Actor1.GetHashCode() ^ Actor2.GetHashCode() ^ Profit.GetHashCode();
        }

        public static bool operator ==(Movie lhs, Movie rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    return true;
                }
                return false;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Movie lhs, Movie rhs)
        {
            return !(lhs == rhs);
        }
    }
}
