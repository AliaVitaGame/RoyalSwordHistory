
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // ------------------------------------ \\

        //--Main--
        public string NamePlayer;
        public int CoinCount;

        //--Flags--
        public int SelectedFlagID;
        public bool[] IsBuyFlag = new bool[1000];

        //--Friends--
        public bool[] AvailableFriends;


        //--Settings--
        public float MusicVolume;
        public float AudioEffectsVolume;

        //--Cheats--
        public bool[] ActiveCheat = new bool[1000];

        //--Stats--
        public bool[] CountFinishLevel = new bool[1000];
        public int CountKilledEnemy;

        // ------------------------------------ \\


        public SavesYG()
        {

        }
    }
}
