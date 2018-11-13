using System;

namespace L3_U3_9
{
    class Series : Video
    {
        // Serialo pradžios metai 
        public string StartDate { get; set; }
        // Serialo epizodų skaičius
        public int Episodes { get; set; }
        // Serialo pabaigos metai
        public string EndDate { get; set; }
        // Požymis: ar serialas tęsiasi
        public string Airing { get; set; }

        public Series(string name, string genre, string studio, string actor1, string actor2, string startDate, string endDate, int episodes, string airing) : base(name, genre, studio, actor1, actor2)
        {
            StartDate = startDate;
            Episodes = episodes;
            EndDate = endDate;
            Airing = airing;
        }

        public override string ToString()
        {
            return String.Format("| {0, -40} | {1,-14} | {2,-30} | {3,-35} | {4,-20} | {5,-20} | {6,-20} | {7,-14} | {8,-14} | {9,-14} | {10,-14} | {11,-14} |",
                        Name, Genre, Studio, Actor1, Actor2, "", "", "", StartDate, EndDate, Episodes, Airing);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Series other))
            {
                return false;
            }
            return Name == other.Name &&
                   Genre == other.Genre &&
                   Studio == other.Studio &&
                   Actor1 == other.Actor1 &&
                   Actor2 == other.Actor2 &&
                   StartDate == other.StartDate &&
                   Episodes == other.Episodes &&
                   EndDate == other.EndDate &&
                   Airing == other.Airing;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Genre.GetHashCode() ^ Studio.GetHashCode() ^ Actor1.GetHashCode() ^ Actor2.GetHashCode() ^ StartDate.GetHashCode() ^ Episodes.GetHashCode() ^ EndDate.GetHashCode() ^ Airing.GetHashCode();
        }

        public static bool operator ==(Series lhs, Series rhs)
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

        public static bool operator !=(Series lhs, Series rhs)
        {
            return !(lhs == rhs);
        }
    }
}
