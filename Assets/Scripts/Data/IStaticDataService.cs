namespace Data
{
    public interface IStaticDataService
    {
        public void Load();
        public LevelStaticData ForLevel(string sceneName);
    }
}