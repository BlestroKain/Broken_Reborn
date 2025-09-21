namespace Intersect.Server.Entities
{
    public interface IPet
    {
        Player Owner { get; }

        bool IsLeveable { get; }

        void GivePetExperience(long amount);
    }
}

