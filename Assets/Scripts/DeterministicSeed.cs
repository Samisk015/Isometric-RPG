public static class DeterministicSeed
{
    public static int ForEntity(string entityId, long gameTick)
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + (entityId?.GetHashCode() ?? 0);
            hash = hash * 31 + gameTick.GetHashCode();
            return hash;
        }
    }
}
