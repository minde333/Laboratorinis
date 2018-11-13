using System;

namespace L3_U3_9
{
    abstract class Video
    {
        // Įrašo pavadinimas
        public string Name { get; set; }
        // Įrašo žanras
        public string Genre { get; set; }
        // Įrašą sukūrios studijos pavadinimas
        public string Studio { get; set; }
        // Įraše vaidinęs aktorius
        public string Actor1 { get; set; }
        // Įraše vaidinęs aktorius
        public string Actor2 { get; set; }

        public Video(string name, string genre, string studio, string actor1, string actor2)
        {
            Name = name;
            Genre = genre;
            Studio = studio;
            Actor1 = actor1;
            Actor2 = actor2;
        }
        public override string ToString()
        {
            return String.Format("| {0, -40} | {1,-14} | {2,-30} | {3,-35} | {4,-20} | {5,-20} | {6,-20} | {7,-14} | {8,-14} | {9,-14} | {10,-14} | {11,-14} |",
                        Name, Genre, Studio, Actor1, Actor2, "", "", "", "", "", "", "");
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Video other))
            {
                return false;
            }
            return Name == other.Name &&
                   Genre == other.Genre &&
                   Studio == other.Studio &&
                   Actor1 == other.Actor1 &&
                   Actor2 == other.Actor2;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Genre.GetHashCode() ^ Studio.GetHashCode() ^ Actor1.GetHashCode() ^ Actor2.GetHashCode();
        }

        public static bool operator ==(Video lhs, Video rhs)
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

        public static bool operator !=(Video lhs, Video rhs)
        {
            return !(lhs == rhs);
        }
    }
}
