namespace L3_U3_9
{
    class ActorPerforms
    {
        /// <summary>
        /// aktoriaus vardas
        /// </summary>
        public string Actor { get; set; }
        /// <summary>
        /// kiek kartu aktorius vaidino
        /// </summary>
        public int PerformTimes { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is ActorPerforms)) return false;

            return ((ActorPerforms) obj).Actor == Actor;
        }

        public override int GetHashCode()
        {
            return Actor.GetHashCode();
        }
    }
}
